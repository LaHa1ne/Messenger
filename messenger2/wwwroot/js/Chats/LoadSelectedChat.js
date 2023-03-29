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
            console.log(data);
            if (data.statusCode == 200) {
                console.log("moremessages=" + data.data.hasMoreMessages);
                $("div.chat_caption").text(data.data.name);
                if (data.data.hasMoreMessages) $("#load_more_messages").css("display", "block");
                else $("#load_more_messages").css("display", "none");
                AddMessagesFromJson(data.data, add_position="end");
                AddChatMembersFromJson(data.data)
            }
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

function AddMessagesFromJson(data,add_position)
{
    let message_list = $("#selected_chat_messages");
    if (add_position == "end")
    {
        $(message_list).empty();
        $(message_list).attr('name', data.chatId);
    }
    let system_message = $("li[name='empty_system_message'");
    let my_message = $("li[name='empty_my_message'");
    let user_message = $("li[name='empty_user_message'");
    for (var message of data.messages) {
        if (message.isSystem) {
            let new_system_message = $(system_message).clone(true);
            $(new_system_message).attr("name", message.messageId);
            $(new_system_message).children('span.message_text').text(message.text);
            if (add_position == "end") $(message_list).append(new_system_message);
            else $(message_list).prepend(new_system_message);
            continue;
        }
        if (message.isMine) {
            let new_my_message = $(my_message).clone(true);
            $(new_my_message).attr("name", message.messageId);
            $(new_my_message).children('span.nickname').text(message.nickname);
            $(new_my_message).children('span.message_date').text(ConvertDateFromJson(message.date));
            $(new_my_message).children('span.message_text').text(message.text);
            if (add_position == "end") $(message_list).append(new_my_message);
            else $(message_list).prepend(new_my_message);
            continue;
        }
        let new_user_message = $(user_message).clone(true);
        $(new_user_message).attr("name", message.messageId);
        $(new_user_message).children('span.nickname').text(message.nickname);
        $(new_user_message).children('span.message_date').text(ConvertDateFromJson(message.date));
        $(new_user_message).children('span.message_text').text(message.text);
        if (add_position == "end") $(message_list).append(new_user_message);
        else $(message_list).prepend(new_user_message);
    }
}

function AddChatMembersFromJson(data)
{
    let members_list = $("#chat_members_list");
    $(members_list).empty();
    $(members_list).attr('name', data.chatId);
    let empty_member = $("li[name='empty_member'");
    for (var member of data.members) {
        let new_member = $(empty_member).clone(true);
        $(new_member).attr("name", member.userId);
        $(new_member).children('span.nickname').text(member.nickname);
        $(members_list).append(new_member);
    }
}

function LoadMoreMessages()
{
    var token = $('#RequestVerificationToken').val();
    let ChatId = Number($("#selected_chat_messages").attr('name'));
    let FirstMessageId = Number($("#selected_chat_messages").find('li:first-child').attr('name'));
    $.ajax({
        url: '/Chats/LoadMoreMessages',
        data: JSON.stringify({ ChatId: ChatId, FirstMessageId: FirstMessageId }),
        headers:
        {
            "RequestVerificationToken": token
        },
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            if (data.statusCode == 200) {
                console.log("moremessages=" + data.data.hasMoreMessages);
                if (data.data.hasMoreMessages) $("#load_more_messages").css("display", "block");
                else $("#load_more_messages").css("display", "none");
                AddMessagesFromJson(data.data, add_position="begin");
            }
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
}

function ConvertDateFromJson(date_json) {
    let date = new Date(date_json);
    let date_string = date.getDay() + "." + date.getMonth() + "." + date.getFullYear() + " ";
    date_string += date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
    return date_string;
}
