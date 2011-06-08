


function ShowError(msg) {
    $("<div id='errorModal'/>").dialog({
        modal: true,
        open: function () {
            $("#errorModal").html(msg);
        },
        close: function () {
            $("#errorModal").remove();
        },
        height: 400,
        width: 500,
        title: 'Error',
        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        },
        dialogClass: 'alert'
    });
}

function ShowError() {
    ShowError("Application Error");
}


function GetId(data) {
    var index = data.toString().indexOf('_');
    var id = data.toString().substring(index + 1, data.length);
    return id;
}