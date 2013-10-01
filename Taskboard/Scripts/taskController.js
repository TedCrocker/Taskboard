(function (events, $)
{
	var _addTaskButton = $('#addTask');
	var _body = $('body');

	function taskReceived(event)
	{
		var taskDiv = $("<div/>").addClass("task");
		taskDiv.attr("id", "task-" + event.data.Id.toString());
		taskDiv.css("left", event.data.left);
		taskDiv.css("top", event.data.top);
		taskDiv.draggable({
			containment: "body",
			drag: dragUpdateTask
		});
		var textArea = $("<textarea/>").text(event.data.content);
		taskDiv.append(textArea);
		_body.append(taskDiv);
	}

	var timeStamp = new Date();
	function dragUpdateTask()
	{
		if (new Date() - timeStamp > 100)
		{
			updateTask.apply($(this).find("textarea"));
		}
	}
	
	function updateTask() {
		var data = {
			Id: parseInt($(this).parent().attr('id').substring(5), 10),
			left: parseInt($(this).parent().css('left'), 10),
			top: parseInt($(this).parent().css('top'), 10),
			content: $(this).val()
		};

		events.publish(events.task.update, data);
	}

	function taskUpdated(event)
	{
		var taskDiv = $('#task-' + event.data.Id);
		taskDiv.css("left", event.data.left);
		taskDiv.css("top", event.data.top);
		taskDiv.find("textarea").val(event.data.content);
	}

	_addTaskButton.on("click", function ()
	{
		events.publish(events.task.taskAdded);
	});

	_body.on("change", ".task textArea", updateTask);

	events.subscribe(events.task.taskReceived, taskReceived);
	events.subscribe(events.task.taskUpdated, taskUpdated);

})(	window.events = window.events || {},
	jQuery);