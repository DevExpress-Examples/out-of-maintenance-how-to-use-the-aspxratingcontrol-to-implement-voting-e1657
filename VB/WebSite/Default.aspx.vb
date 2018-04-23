Imports Microsoft.VisualBasic
#Region "Using"
Imports System
Imports DevExpress.Web.ASPxCallback
Imports System.Threading
#End Region
Partial Public Class _Default
	Inherits System.Web.UI.Page
	Private Const VoteSessionKey As String = "CC41B70C-9586-4883-86F5-88E23D66B63B"
	Private Const StorageSessionKey As String = "2414B076-304B-4e1b-BF40-C4328D978262"

	Protected Property IsVote() As Boolean
		Get
			If Session(VoteSessionKey) Is Nothing Then
				Session(VoteSessionKey) = False
			End If
			Return CBool(Session(VoteSessionKey))
		End Get
		Set(ByVal value As Boolean)
			Session(VoteSessionKey) = value
		End Set
	End Property
	Protected ReadOnly Property Storage() As RatingStorage
		Get
			If Session(StorageSessionKey) Is Nothing Then
'INSTANT VB NOTE: The local variable storage was renamed since Visual Basic will not allow local variables with the same name as their enclosing function or property:
				Dim storage_Renamed As New RatingStorage()
				' Emulate voting
				For Each value As Integer In New Integer() { 5, 4, 4, 2, 4, 5, 3 }
					storage_Renamed.AddMark(value)
				Next value
				Session(StorageSessionKey) = storage_Renamed
			End If
			Return CType(Session(StorageSessionKey), RatingStorage)
		End Get
	End Property

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		buttonUnlock.ClientEnabled = IsVote AndAlso Not multipleVoting.Checked
		rating.Value = Storage.GetRating()
		ratingLabel.Text = rating.Value.ToString("0.##")
		rating.ReadOnly = IsVote AndAlso Not multipleVoting.Checked
	End Sub

	Protected Sub Callback_Callback(ByVal source As Object, ByVal e As CallbackEventArgs)
		Thread.Sleep(1000) ' Intentionally pauses server-side processing, to demonstrate the Rating Control functionality.
		If (Not IsVote) OrElse multipleVoting.Checked Then
			Storage.AddMark(Integer.Parse(e.Parameter) + 1)
		End If
		e.Result = String.Format("{0} {0:0.##}", Storage.GetRating())
		IsVote = True
	End Sub

	Protected Sub buttonUnlock_Click(ByVal sender As Object, ByVal e As EventArgs)
		IsVote = False
		rating.ReadOnly = IsVote
		buttonUnlock.ClientEnabled = IsVote
	End Sub
End Class
#Region "Rating Storage"
Public Class RatingStorage
	Private m_ratingCount As Integer
	Private m_ratingSum As Decimal

	Public Sub New()
		Me.m_ratingCount = 0
		Me.m_ratingSum = 0
	End Sub

	Public Sub AddMark(ByVal mark As Integer)
		m_ratingSum += mark
		m_ratingCount += 1
	End Sub

	Public Function GetRating() As Decimal
		Return m_ratingSum / m_ratingCount
	End Function
End Class
#End Region