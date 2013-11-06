(function (taskboard, events, $)
{
	taskboard.Hub = function (hubName)
	{
		var _hubName = hubName;
		var _self = this;
		var _hub = $.connection[_hubName + "Hub"];
		var _client = _hub.client;
		var _server = _hub.server;
		
		_client.update = function (model)
		{
			events.publish("events." + _hubName + ".updated", model);
		};
		
		_client.add = function (model) {
			events.publish("events." + _hubName + ".added", model);
		};
		
		_client.remove = function (model) {
			events.publish("events." + _hubName + ".removed", model);
		};
		
		_client.getAll = function (model) {
			events.publish("events." + _hubName + ".fetched", model);
		};

		events.subscribe("events." + _hubName + ".update", function (e)
		{
			_server.update(e.data);
		});

		events.subscribe("events." + _hubName + ".remove", function (e)
		{
			_server.remove(e.data);
		});
		
		events.subscribe("events." + _hubName + ".add", function (e)
		{
			if (e.data)
			{
				_server.add(e.data);
			}
			else
			{
				_server.add();
			}
		});

		events.subscribe(events.connection.started, function (e)
		{
			_server.getAll();
		});
	};
})( window.taskboard = window.taskboard || {},
	window.events = window.events || {},
	jQuery);