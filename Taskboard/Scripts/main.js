(function (taskboard, events, $)
{
	taskboard.model = {};
	$(document).ready(function ()
	{
		$.connection.hub.start().done(function ()
		{
			events.publish(events.connection.started);
		});

		var taskHub = new taskboard.Hub("task");
		var storyHub = new taskboard.Hub("story");
	});
	
})(	window.taskboard = window.taskboard || {},
	window.events = window.events || {},
	jQuery);