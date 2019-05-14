$(function () {
    var chat = $.connection.chatHub;
    var $username = $('#txtUsername');
    var $message = $('#txtMessage');
    var $messages = $('#messages');
    $message.focus();
    chat.client.sendMessage = function (name, message) {
        $messages.append('<li><strong>' + name + '</strong>: ' + message + '</li>');
    };
    $.connection.hub.start().done(function () {
        $('#btnSendMessage').click(function () {

            chat.server.send($username.val(), $message.val());

            $message.val('').focus();
        });
    });
});

