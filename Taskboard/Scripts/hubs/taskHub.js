(function (hubs, events, $)
{
	var taskHub = $.connection.taskHub;

	function setupTaskHub()
	{
		var tasks = [];
		taskHub.client.addTask = function (model)
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

	setupTaskHub();
})(	window.hubs = window.hubs || {},
	window.events = window.events || {},
	jQuery);