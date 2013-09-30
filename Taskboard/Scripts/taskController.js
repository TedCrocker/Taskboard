(function (events, $)
{
	var _addTaskButton = $('#addTask');
	var _body = $('body');

	function taskReceived(event)
	{
		var taskDiv = $("<div/>").addClass("task");
		taskDiv.css("left", event.data.left);
		taskDiv.css("top", event.data.top);
		taskDiv.text(event.data.content);
		taskDiv.draggable({ containment: "body" });
		_body.append(taskDiv);
	}

	_addTaskButton.on("click", function ()
	{
		events.publish(events.task.taskAdded);
	});

	events.subscribe(events.task.taskReceived, taskReceived);

})(	window.events = window.events || {},
	jQuery);