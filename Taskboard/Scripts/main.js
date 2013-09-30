(function (hubs, events, $)
{
	$.connection.hub.start().done(function ()
	{
		events.publish(events.connection.started);
	});
})(	window.hubs = window.hubs || {},
	window.events = window.events || {},
	jQuery);