(function (hubs, events, $)
{
	var chatHub = $.connection.chatHub;

	function htmlEncode(value)
	{
		return $('<div/>').text(value).html();
	}

	function setupChatHub()
	{
		$('#displayname').val(prompt("Enter your name:", ""));
		$('#message').focus();


		chatHub.client.addNewMessageToPage = function (name, message)
		{
			$('#discussion').append("<li><strong>" + htmlEncode(name) + "</strong>: " + htmlEncode(message) + "</li>");
		};

		events.subscribe(events.connection.started, function (e)
		{
			$('#sendmessage').click(function ()
			{
				var displayName = $('#displayname').val();
				var message = $('#message').val();

				chatHub.server.send(displayName, message);
				$('#message').val('').focus();
			});
		});
	}
	
	setupChatHub();

})(window.hubs = window.hubs || {}, window.events = window.events || {}, jQuery);