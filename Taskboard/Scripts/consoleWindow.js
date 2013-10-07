(function (taskboard, events, $)
{
	var _output = $('#discussion');
	var _input = $('#message');
	window.debugMode = true;
	
	if (debugMode)
	{
		var log = window.console.log;
		var error = window.console.error;

		function redirectConsoleOutput(sourceName, baseFunction)
		{
			return function()
			{
				var message = "";
				for (var i = 0; i < arguments.length; i++)
				{
					message += arguments[i].toString();
				}

				addToOutput({
					data: {
						sourceName: sourceName,
						output: message
					}
				});

				baseFunction.apply(this, arguments);
			};
		}

		window.console.log = redirectConsoleOutput("LOG", log);
		window.console.error = redirectConsoleOutput("<strong>ERROR</strong>", error);
	}

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
		var userDisplayName = taskboard.displayName;
		var inputText = _input.val();
		if (event.keyCode === 13 && inputText)
		{
			events.publish(events.console.messageEntered, { sourceName: userDisplayName, output: inputText });
			_input.val('').focus();
		}
	});

	events.subscribe(events.chat.messageReceived, addToOutput);

})( window.taskboard = window.taskboard || {},
	window.events = window.events || {},
	jQuery);