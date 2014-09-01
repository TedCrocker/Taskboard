(function (taskboard, events, $) {
	taskboard.LogHub = function ()
	{
		var _baseHub = new taskboard.Hub("log");

		_baseHub.client.clearAll = function ()
		{
			events.publish("events.log.cleared", null);
		}

		events.subscribe("events.log.clear", function()
		{
			_baseHub.server.clearAll(); 
		});
	};
})(window.taskboard = window.taskboard || {},
	window.events = window.events || {},
	jQuery);