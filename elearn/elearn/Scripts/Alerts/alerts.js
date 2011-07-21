function ShowGreenAlert(msg) {

    $("#messagesContainer").append(
    ' <div class="validation-summary-errors alert alert_green alert_box" style="margin:10px 10px 10px 380px;width:300px;"><span><img height="24" src="/elearn/Content/Adminica/images/icons/small/white/speech_bubble_2.png" width="24"></span>' +
    '<span style="margin: 20px;">' + msg + '</span></div>');
}


function ShowRedAlert(msg) {

    if (!msg)
        msg = 'Error , pls try again later.'

    $("#messagesContainer").append(
    ' <div class="validation-summary-errors alert alert_red alert_box" style="margin:10px 10px 10px 380px;width:300px;"><span><img height="24" src="/elearn/Content/Adminica/images/icons/small/white/alert_2.png.png" width="24"></span>' +
    '<span style="margin: 20px;">' + msg + '</span></div>');
}