(function (events, $)
{
	var _output = $('#discussion');
	var _input = $('#message');

	$('#consoleOutput').draggable({ containment: "body"});

	function htmlEncode(value)
	{
		return $('<div/>').text(value).html();
	}

	function addToOutput(event)
	{
		var ul = $('<li/>').text(event.data.sourceName + ": " + htmlEncode(event.data.output));
		_output.append(ul);

		_output.animate({ scrollTop: _output[0].scrollHeight }, 100);
	}

	_input.on("keyup", function (e) 
	{
		var userDisplayName = $('#displayName').val();
		var inputText = _input.val();
		if (event.keyCode === 13 && inputText)
		{
			events.publish(events.console.messageEntered, { sourceName: userDisplayName, output: inputText });
			_input.val('').focus();
		}
	});

	events.subscribe(events.chat.messageReceived, addToOutput);

})(window.events = window.events || {},
	jQuery);