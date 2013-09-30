(function (hubs, events, $)
{
	var chatHub = $.connection.chatHub;

	function setupChatHub()
	{
		$('#displayName').val(prompt("Enter your name:", ""));
		$('#message').focus();

		chatHub.client.addNewMessageToPage = function (name, message)
		{
			events.publish(events.chat.messageReceived, { sourceName: name, output: message });
		};

		function messageSent(event)
		{
			chatHub.server.send(event.data.sourceName, event.data.output);
		}

		events.subscribe(events.connection.started, function (e)
		{
			events.subscribe(events.console.messageEntered, messageSent);
		});
	}
	
	setupChatHub();

})(window.hubs = window.hubs || {}, window.events = window.events || {}, jQuery);