function LoadSelectedChat(ChatId) {   
    var token = $('#RequestVerificationToken').val();
    $.ajax({
        url: '/Chats/LoadSelectedChat',
        data: JSON.stringify({ ChatId: ChatId }),
        headers:
        {
            "RequestVerificationToken": token
        },
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            if (data.statusCode == 200) {
                $("div.chat_caption").text(data.data.name);
                AddMessagesFromJson(data.data);
            }
            $("div.chat_caption").text(data.data.name);
            $("#load_more_messages").css("display", "block");
        },
        error: function (jqXHR, exception) {
            alert("Ошибка");
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
        }
    });
};

function AddMessagesFromJson(data)
{
    let message_list = $("#selected_chat_messages");
    $(message_list).empty();
    $(message_list).attr('name', data.chatId);
    let system_message = $("li[name='empty_system_message'");
    let my_message = $("ul[name='empty_my_message'");
    let user_message = $("ul[name='empty_user_message'");
    for (var message in data.messages) {
        console.log("aaa");
        if (message.isSystem) {
            console.log("aaa");
            alert("дОбавлен");
            let new_message = $(system_message).clone(true);
            $(new_message).attr("name", message.messageId);
            /*$(new_message).children('span.message_text')[0].text(message.text);
            $(message_list).append(new_message);
            alert("дОбавлен");*/
            continue;
        }
        if (message.isMine) {
            console.log("mine");
            continue;
        }
        console.log("user");
    }
}