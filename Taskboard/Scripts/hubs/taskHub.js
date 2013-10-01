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
		
		_taskHub.client.updateTask = function (model)
		{
			events.publish(events.task.taskUpdated, model);
		};

		_taskHub.client.deleteTask = function(model)
		{
			events.publish(events.task.taskDeleted, model);
		};

		events.subscribe(events.connection.started, function (e)
		{
			events.subscribe(events.task.taskAdded, addTask);
		});

		events.subscribe(events.task.update, function (e)
		{
			_taskHub.server.updateTask(e.data);
		});
		
		events.subscribe(events.task.remove, function (e)
		{
			_taskHub.server.deleteTask(e.data);
		});
	}

	setupTaskHub();
})(	window.hubs = window.hubs || {},
	window.events = window.events || {},
	jQuery);