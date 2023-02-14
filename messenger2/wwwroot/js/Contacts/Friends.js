window.onload = function () {
    $('ul.friend_list').on("click", 'li i.delete_friend', function () { 
        var li = $(this).parent().parent();
        alert($('#RequestVerificationToken').val());
        var data = { UserId: "some data" };
        var token = $('#RequestVerificationToken').val();
        $.ajax({
            url: '/Contacts/MyJson',
            data: JSON.stringify(data),
            type: "POST",
            headers:
            {
                "X-XSRF-TOKEN": $('#RequestVerificationToken').val()
            },
            dataType: "json",
            contentType: "application/json",
            success: function (data) {
                alert('success!');
            },
            error: function (data) {
                alert('error!');
            }

        });

        /*let response = await fetch("/antiforgery/token", {
            method: "GET",
            headers: { "Authorization": authorizationToken }
        });*/



        /*if (response.ok) {
            const xsrfToken = document.cookie
                .split("; ")
                .find(row => row.startsWith("XSRF-TOKEN="))
                .split("=")[1];

            alert(xsrfToken);
        }
        else alert("Токен не получен");*/

        $.ajax({
            url: '/Contacts/DeleteFriend',
            //url: '/contacts/del',
        data: JSON.stringify(data),
            headers:
        {
            "RequestVerificationToken": $('#RequestVerificationToken').val()
        },
        type: "POST",
            dataType: "json",
                contentType: "application/json"
    });
    alert('У2222');
});
    







}
