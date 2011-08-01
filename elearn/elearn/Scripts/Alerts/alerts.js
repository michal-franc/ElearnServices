function ShowGreenAlert(msg,size) {

    var width = 300;
    if (size)
        width = size;
    $("#messagesContainer").append(
    ' <div class="validation-summary-errors alert alert_green alert_box" style="margin:10px 10px 10px '+(760-width)+'px;width:' + width + 'px;"><span><img height="24" src="/Content/Adminica/images/icons/small/white/speech_bubble_2.png" width="24"></span>' +
    '<span style="margin: 20px;">' + msg + '</span></div>');
}


function ShowRedAlert(msg,size) {

    var width = 300;
    if (size)
        width = size;

    if (!msg)
        msg = 'Error , pls try again later.'

    $("#messagesContainer").append(
    ' <div class="validation-summary-errors alert alert_red alert_box" style="margin:10px 10px 10px ' + (760 - width )+ 'px;width:' + width + 'px;"><span><img height="24" src="/Content/Adminica/images/icons/small/white/alert_2.png" width="24"></span>' +
    '<span style="margin: 20px;">' + msg + '</span></div>');
}


function ShowAlert(msg, type)
{
    if (type == 'Red')
        ShowRedAlert(msg);
    else if (type == 'Green')
        ShowGreenAlert(msg);   
}