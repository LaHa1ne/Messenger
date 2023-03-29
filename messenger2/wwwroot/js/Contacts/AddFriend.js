function SendFriendRequest(obj) {
    var Nickname = $("#friend_nickname").val();
    if (Nickname != null && Nickname != "") {
        var token = $('#RequestVerificationToken').val();
        var data = { Nickname: Nickname };
        $(".alert").alert('close')
        $.ajax({
            url: '/Contacts/SendFriendRequest',
            data: JSON.stringify(data),
            headers:
            {
                "RequestVerificationToken": token
            },
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            success: function (data) {
                if (data.statusCode == 200) {
                    if (data.data) AddSuccessAlert(data.description);
                    else AddWarningAlert(data.description);
                }
                else AddServerErrorAlert(data.statusCode, data.description); 
            },
            error: function (jqXHR, exception) {
                var msg = '';
                if (jqXHR.status === 0) {
                    msg = 'Not connect.\n Verify Network.';
                } else if (jqXHR.status == 404) {
                    msg = 'Requested page not found. [404]';
                } else if (jqXHR.status == 500) {
                    msg = 'Internal Server Error [500].';
                } else if (exception === 'parsererror') {
                    msg = 'Requested JSON parse failed.';
                } else if (exception === 'timeout') {
                    msg = 'Time out error.';
                } else if (exception === 'abort') {
                    msg = 'Ajax request aborted.';
                } else {
                    msg = 'Uncaught Error.\n' + jqXHR.responseText;
                }
                AddServerErrorAlert(jqXHR.status, msg);
            }
        });
        AutocloseAlert();
    }
};