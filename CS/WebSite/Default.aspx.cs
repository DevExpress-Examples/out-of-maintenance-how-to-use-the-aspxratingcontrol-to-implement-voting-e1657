#region Using
using System;
using DevExpress.Web;
using System.Threading;
#endregion
public partial class _Default : System.Web.UI.Page {
    const string VoteSessionKey = "CC41B70C-9586-4883-86F5-88E23D66B63B";
    const string StorageSessionKey = "2414B076-304B-4e1b-BF40-C4328D978262";

    protected bool IsVote {
        get {
            if(Session[VoteSessionKey] == null)
                Session[VoteSessionKey] = false;
            return (bool)Session[VoteSessionKey];
        }
        set { Session[VoteSessionKey] = value; }
    }
    protected RatingStorage Storage {
        get {
            if(Session[StorageSessionKey] == null) {
                RatingStorage storage = new RatingStorage();
                // Emulate voting
                foreach(int value in new int[] { 5, 4, 4, 2, 4, 5, 3 })
                    storage.AddMark(value);
                Session[StorageSessionKey] = storage;
            }
            return (RatingStorage)Session[StorageSessionKey];
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        buttonUnlock.ClientEnabled = IsVote && !multipleVoting.Checked;
        rating.Value = Storage.GetRating();
        ratingLabel.Text = rating.Value.ToString("0.##");
        rating.ReadOnly = IsVote && !multipleVoting.Checked;
    }

    protected void Callback_Callback(object source, CallbackEventArgs e) {
        Thread.Sleep(1000); // Intentionally pauses server-side processing, to demonstrate the Rating Control functionality.
        if(!IsVote || multipleVoting.Checked)
            Storage.AddMark(int.Parse(e.Parameter) + 1);
        e.Result = string.Format("{0} {0:0.##}", Storage.GetRating());
        IsVote = true;
    }

    protected void buttonUnlock_Click(object sender, EventArgs e) {
        IsVote = false;
        rating.ReadOnly = IsVote;
        buttonUnlock.ClientEnabled = IsVote;
    }
}
#region Rating Storage
public class RatingStorage {
    int m_ratingCount;
    decimal m_ratingSum;

    public RatingStorage() {
        this.m_ratingCount = 0;
        this.m_ratingSum = 0;
    }

    public void AddMark(int mark) {
        m_ratingSum += mark;
        m_ratingCount++;
    }

    public decimal GetRating() { return m_ratingSum / m_ratingCount; }
}
#endregion