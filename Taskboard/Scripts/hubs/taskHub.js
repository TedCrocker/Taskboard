(function (hubs, events, $)
{
	var _taskHub = $.connection.taskHub;
	
	function addTask()
	{
		_taskHub.server.addTask();
	}

	function setupTaskHub()
	{
		_taskHub.client.addTask = function (model)
		{
			events.publish(events.task.taskReceived, model);
		};

		events.subscribe(events.connection.started, function (e)
		{
			events.subscribe(events.task.taskAdded, addTask);
		});
	}

	setupTaskHub();
})(	window.hubs = window.hubs || {},
	window.events = window.events || {},
	jQuery);