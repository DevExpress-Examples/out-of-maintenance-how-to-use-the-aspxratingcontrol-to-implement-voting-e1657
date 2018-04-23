<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="DevExpress.Web.v10.2" Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v10.2" Namespace="DevExpress.Web.ASPxRatingControl" TagPrefix="dxrc" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        #container {
            text-align: center;
        }
        
        #content {
            margin: 0 auto;
        }
        
        #updatingMessage {
            visibility: hidden;
        }
        
        #ratingLbl {
            margin-top: 10px;
        }
    </style>
    <script type="text/javascript">
        function Vote(s, e){
            Callback.PerformCallback(e.index);
            rating.SetReadOnly(true);
            document.getElementById("updatingMessage").style.visibility = "visible";
        }
        function Callback_CallbackComplete(s, e) {
            document.getElementById("updatingMessage").style.visibility = "hidden";
            var ratingValues = e.result.split(' ');
            rating.SetValue(ratingValues[0]);
            ratingLabel.SetText(ratingValues[1]);
            var isMultipleVoting = multipleVoting.GetChecked();
            rating.SetReadOnly(!isMultipleVoting);
            buttonUnlock.SetEnabled(!isMultipleVoting);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
            <table id="content" cellpadding="0" cellspacing="8"><tr valign="top"><td>
                <dxrc:ASPxRatingControl ID="rating" runat="server" ImageMapUrl="~/Images/rating.png" FillPrecision="Full"
                    ItemWidth="24" ItemHeight="24" ClientInstanceName="rating">
                    <ClientSideEvents ItemClick="Vote" />
                </dxrc:ASPxRatingControl>
                <div id="ratingLbl">
                    <dxe:ASPxLabel ID="ratingLabel" runat="server" ClientInstanceName="ratingLabel" Font-Size="18pt" />
                </div>
                <div id="updatingMessage">
                    (Updating...)
                </div>
            </td><td>
                <table><tr><td>
                    <dxe:ASPxCheckBox ID="multipleVoting" runat="server" Checked="True" AutoPostBack="true"
                         ClientInstanceName="multipleVoting" />
                    Allow multiple voting
                </td></tr><tr><td>
                    <div>
                        <dxe:ASPxButton ID="buttonUnlock" runat="server" ClientInstanceName="buttonUnlock" 
                            Text="Click to vote again" OnClick="buttonUnlock_Click" />
                    </div>
                </td></tr></table>
            </td></tr></table>    
        </div>
        
        <dxcb:ASPxCallback ID="Callback" runat="server" ClientInstanceName="Callback" OnCallback="Callback_Callback">
            <ClientSideEvents CallbackComplete="Callback_CallbackComplete" />
        </dxcb:ASPxCallback>
        
    </form>
</body>
</html>
