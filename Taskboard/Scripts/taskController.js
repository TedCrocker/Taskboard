﻿(function (taskboard, events, $)
{
	var _addTaskButton = $('#addTask');
	var _body = $('body');

	function taskReceived(event)
	{
		var data = event.data;
		taskboard.model[data.ID] = data;

		var taskDiv = $("<div/>").addClass("task");
		taskDiv.attr("id", "task-" + data.Id);
		taskDiv.css("left", data.left);
		taskDiv.css("top", data.top);
		
		var textArea = $("<textarea/>").text(data.content).addClass('content');
		taskDiv.append(textArea);

		var deleteButton = $("<button/>").text("X").addClass("deleteTask");
		taskDiv.append(deleteButton);

		var openButton = $("<button/>").text("Open").addClass("openTask");
		taskDiv.append(openButton);
		
		var closeButton = $("<button/>").text("Close").addClass("closeTask");
		taskDiv.append(closeButton);

		if (data.workFlowState == 1)
		{
			taskDiv.addClass("open");
		}
		else if (data.workFlowState == 2)
		{
			taskDiv.addClass("closed");
		}

		var assignedToHidden = $("<div/>").addClass("assignedTo").text(data.assignedTo == null ? "" : data.assignedTo);
		taskDiv.append(assignedToHidden);

		_body.append(taskDiv);

		taskboard.makeDraggable(taskDiv, dragUpdateTask);
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
	
	function updateTask()
	{
		var taskDiv = $(this).parent();
		
		var state = 0;
		if (taskDiv.hasClass('open'))
		{
			state = 1;
		}
		else if (taskDiv.hasClass('closed'))
		{
			state = 2;
		}

		var data = {
			Id: taskDiv.attr('id').substring(5),
			left: parseInt(taskDiv.css('left'), 10),
			top: parseInt(taskDiv.css('top'), 10),
			content: taskDiv.find('.content').val(),
			workFlowState: state,
			assignedTo: taskDiv.find('.assignedTo').text()
		};

		events.publish("events.task.update", data);
	}
	
	function deleteTask()
	{
		var data = { Id: $(this).parent().attr('id').substring(5) };
		events.publish("events.task.remove", data);
	}
	
	function openTask()
	{
		var parent = $(this).parent();
		parent.addClass('open');
		parent.find('.assignedTo').text(taskboard.displayName.match(/([A-Z])/g).join(''));
		updateTask.apply(this);
	}
	
	function closeTask()
	{
		var parent = $(this).parent();
		parent.addClass('closed');
		parent.removeClass('open');
		updateTask.apply(this);
	}

	function taskUpdated(event)
	{
		var data = event.data;
		taskboard.model[data.Id] = data;
		var taskDiv = $('#task-' + data.Id);
		taskDiv.css("left", data.left);
		taskDiv.css("top", data.top);
		taskDiv.find("textarea").val(data.content);

		taskDiv.removeClass("closed");
		taskDiv.removeClass("open");
		if (data.workFlowState == 1)
		{
			taskDiv.addClass("open");
		}
		else if (data.workFlowState == 2)
		{
			taskDiv.addClass("closed");
		}
		if (data.assignedTo)
		{
			taskDiv.find(".assignedTo").text(data.assignedTo); 
		}
	}
	
	function taskDeleted(event)
	{
		delete taskboard.model[event.data.Id];
		var taskDiv = $('#task-' + event.data.Id);
		taskDiv.remove();
	}
	
	function tasksFetched(event)
	{
		for (var i = 0; i < event.data.length; i++)
		{
			taskReceived({ data: event.data[i] });
		}
	}

	_addTaskButton.on("click", function ()
	{
		events.publish("events.task.add");
	});

	_body.on("change", ".task textArea", updateTask);
	_body.on("click", ".task .deleteTask", deleteTask);
	_body.on("click", ".task .openTask", openTask);
	_body.on("click", ".task .closeTask", closeTask);
		
	events.subscribe("events.task.added", taskReceived);
	events.subscribe("events.task.updated", taskUpdated);
	events.subscribe("events.task.removed", taskDeleted);
	events.subscribe("events.task.fetched", tasksFetched);

})( window.taskboard = window.taskboard || {},
	window.events = window.events || {},
	jQuery);