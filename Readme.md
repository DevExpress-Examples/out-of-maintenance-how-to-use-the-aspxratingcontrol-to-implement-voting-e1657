# How to use the ASPxRatingControl to implement voting


<p>This example shows how to implement voting using the ASPxRatingControl. All data is contained in the RatingStorage object, which is saved in Session. Also, there may be multiple votes. After every vote, using ASPxCallback, a callback is sent to the server to save the value and recalculate the rating. During a callback, the ASPxClientRatingControl.SetReadOnly() method control is disabled. When the callback is returned to the client, the value is restored from the server, and the control is enabled.</p>

<br/>


