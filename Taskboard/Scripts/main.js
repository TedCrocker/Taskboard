(function (hubs, events, $)
{
	$(document).ready(function ()
	{
		window.displayName = prompt("Enter your name:", "");//Fix this later

		$.connection.hub.start().done(function ()
		{
			events.publish(events.connection.started);
		});
	});
	
})(	window.hubs = window.hubs || {},
	window.events = window.events || {},
	jQuery);