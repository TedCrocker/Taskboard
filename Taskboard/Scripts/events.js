(function (events)
{
	events.connection = {};
	events.connection.started = "events.connection.started";

	var _eventMappings = {};

	function initEvent(eventName)
	{
		var event = document.createEvent("Event");
		event.initEvent(eventName, true, true);
		_eventMappings[eventName] = event;
	}

	events.publish = function (eventName)
	{
		if (_eventMappings[eventName])
		{
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