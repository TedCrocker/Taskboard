(function (events)
{
	events.console = {};
	events.console.messageEntered = "events.console.messageEntered";

	events.connection = {};
	events.connection.started = "events.connection.started";

	events.chat = {};
	events.chat.messageReceived = "events.chat.messageReceived";
	
	var _eventMappings = {};

	function initEvent(eventName)
	{
		var event = document.createEvent("Event");
		event.initEvent(eventName, true, true);
		_eventMappings[eventName] = event;
	}

	events.publish = function (eventName, eventData)
	{
		if (_eventMappings[eventName])
		{
			var eventObject = _eventMappings[eventName];
			eventObject.data = eventData;
			document.dispatchEvent(_eventMappings[eventName]);
		}
		else
		{
			console.error("The event " + eventName + " does not currently exist");
		}
	};

	events.subscribe = function (eventName, handler)
	{
		if (!_eventMappings[eventName])
		{
			initEvent(eventName);
		}

		document.addEventListener(eventName, handler, false);
	};

})(window.events = window.events || {});