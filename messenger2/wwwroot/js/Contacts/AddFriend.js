function SendFriendRequest(obj) {
    var Nickname = $("#friend_nickname").val();
    if (Nickname != null && Nickname != "") {
        var token = $('#RequestVerificationToken').val();
        var data = { Nickname: Nickname };

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
                if (data.data == 1) {
                    $("#user_found").css("display", "block");
                }
                else if (data.data == 0) {
                    $("#user_not_found").css("display", "block");
                }
                else {
                    $("#ErrorMessageToUser").html(data.description);
                    $("#ErrorMessageToUser").css("display", "block");
                }

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
                $('#ErrorMessageToUser').html(msg);
                $("#ErrorMessageToUser").css("display", "block");
            }
        });
    }
};