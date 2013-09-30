(function(hubs, events, $)
{
	var chatHub = $.connection.chatHub;
	var taskHub = $.connection.taskHub;

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

	function setupTaskHub()
	{
		var tasks = [];
		taskHub.client.addTask = function(model)
		{
			tasks.push(model);
			$('<div/>').text(model.content)
					.css({ left: model.left, top: model.top, "background-color": "red" })
					.addClass('task').appendTo($('body'));
		};

		events.subscribe(events.connection.started, function (e)
		{

		});
	}

	function htmlEncode(value)
	{
		return $('<div/>').text(value).html();
	}

	$.connection.hub.start().done(function ()
	{
		events.publish(events.connection.started);
	});

	setupChatHub();
	setupTaskHub();
})(window.hubs = window.hubs || {}, window.events = window.events || {}, jQuery);