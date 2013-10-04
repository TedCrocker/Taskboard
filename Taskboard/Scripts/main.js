(function (taskboard, events, $)
{
	$(document).ready(function ()
	{
		window.displayName = prompt("Enter your name:", "");//Fix this later

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