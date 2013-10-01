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

		var deleteButton = $("<button/>").text("X").addClass("deleteTask");
		taskDiv.append(deleteButton);
		
		_body.append(taskDiv);
	}

	var timeStamp = new Date();
	function dragUpdateTask()
	{
		if (new Date() - timeStamp > 100)
		{
			updateTask.apply($(this).find("textarea"));
			timeStamp = new Date();
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
	
	function deleteTask()
	{
		var data = { Id: parseInt($(this).parent().attr('id').substring(5), 10) };
		events.publish(events.task.remove, data);
	}

	function taskUpdated(event)
	{
		var taskDiv = $('#task-' + event.data.Id);
		taskDiv.css("left", event.data.left);
		taskDiv.css("top", event.data.top);
		taskDiv.find("textarea").val(event.data.content);
	}
	
	function taskDeleted(event)
	{
		var taskDiv = $('#task-' + event.data.Id);
		taskDiv.remove();
	}

	_addTaskButton.on("click", function ()
	{
		events.publish(events.task.taskAdded);
	});

	_body.on("change", ".task textArea", updateTask);
	_body.on("click", ".task .deleteTask", deleteTask);
		
	events.subscribe(events.task.taskReceived, taskReceived);
	events.subscribe(events.task.taskUpdated, taskUpdated);
	events.subscribe(events.task.taskDeleted, taskDeleted);

})(	window.events = window.events || {},
	jQuery);