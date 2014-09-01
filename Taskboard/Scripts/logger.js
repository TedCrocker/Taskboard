(function (taskboard, events, $)
{
	var logWindow = $('#logOutput');
	logWindow.draggable({ containment: "body" });
	
	taskboard.logger = {
		add: function(text)
		{
			events.publish('events.log.add', text);
		}
	};

	function addItemToLog(item)
	{
		var time = moment(item.TimeStamp).format("DD/MM/YY A");
		var div = $('<div/>').addClass('logItem').addClass('ui-helper-clearfix');
		var timeDiv = $('<div/>').addClass('logItemTime').text(time).appendTo(div);
		var text = $('<div/>').addClass('logItemText').text(item.Text).appendTo(div);
		logWindow.prepend(div);
	}

	function logReceived(event)
	{
		addItemToLog(event.data);
	}

	function logFetched(event)
	{
		for (var i = 0; i < event.data.length; i++)
		{
			addItemToLog(event.data[i]);
		}
	}

	function logCleared()
	{
		logWindow.find('.logItem').remove();
	}

	function toggle()
	{
		logWindow.toggle();
	}

	function clearLog()
	{
		events.publish("events.log.clear");
	}

	events.subscribe("events.log.added",   logReceived);
	//events.subscribe("events.log.updated", logUpdated);
	events.subscribe("events.log.cleared", logCleared);
	events.subscribe("events.log.fetched", logFetched);

	$('body').on('click', '#showLog', toggle);
	$('body').on('click', '.clearLog', clearLog);
})(window.taskboard = window.taskboard || {},
	window.events = window.events || {},
	jQuery);