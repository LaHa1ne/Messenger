function DeleteFriend(obj) {
    var li = $(obj).parent().parent();
    var data = { UserId: $(li).attr("name") };
    var token = $('#RequestVerificationToken').val();

    $.ajax({
        url: '/Contacts/DeleteFriend',
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
                li.css("display", "none");
                if ($(".friend_list li:visible").length == 0) { $("#EmptyFriendList").css("display", "block"); }
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
};
