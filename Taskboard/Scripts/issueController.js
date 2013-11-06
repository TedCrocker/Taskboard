﻿(function (taskboard, events, $)
{
	var _addIssueButton = $('#addIssue');
	var _body = $('body');

	function issueReceived(event)
	{
		var data = event.data;

		if (data.workFlowState == 1)
		{
			event.data.stateClass = "open";
		}
		else if (data.workFlowState == 2)
		{
			event.data.stateClass = "closed";
		}

		dust.render("issue", event.data, function (err, output)
		{
			var $output = $(output);
			_body.append($output);
			$output.css('left', event.data.left);
			$output.css('top', event.data.top);
			taskboard.makeDraggable($output, dragUpdateTask);
		});
	}

	var timeStamp = new Date();
	function dragUpdateTask()
	{
		if (new Date() - timeStamp > 100)
		{
			updateIssue.apply($(this).find("textarea"));
			timeStamp = new Date();
		}
	}

	function updateIssue()
	{
		var issueDiv = $(this).parent();

		var state = 0;
		if (issueDiv.hasClass('open'))
		{
			state = 1;
		}
		else if (issueDiv.hasClass('closed'))
		{
			state = 2;
		}

		var data = {
			Id: issueDiv.attr('id').substring(6),
			left: parseInt(issueDiv.css('left'), 10),
			top: parseInt(issueDiv.css('top'), 10),
			content: issueDiv.find('.content').val(),
			workFlowState: state,
			assignedTo: issueDiv.find('.assignedTo').text()
		};

		events.publish("events.issue.update", data);
	}

	function deleteIssue()
	{
		var data = { Id: $(this).parent().attr('id').substring(5) };
		events.publish("events.issue.remove", data);
	}

	function openIssue()
	{
		var parent = $(this).parent();
		parent.addClass('open');
		parent.find('.assignedTo').text(taskboard.displayName.match(/([A-Z])/g).join(''));
		updateTask.apply(this);
	}

	function closeIssue()
	{
		var parent = $(this).parent();
		parent.addClass('closed');
		parent.removeClass('open');
		updateIssue.apply(this);
	}

	function issueUpdated(event)
	{
		var data = event.data;
		taskboard.model[data.Id] = data;
		var issueDiv = $('#issue-' + data.Id);
		issueDiv.css("left", data.left);
		issueDiv.css("top", data.top);
		issueDiv.find("textarea").val(data.content);

		issueDiv.removeClass("closed");
		issueDiv.removeClass("open");
		if (data.workFlowState == 1)
		{
			issueDiv.addClass("open");
		}
		else if (data.workFlowState == 2)
		{
			issueDiv.addClass("closed");
		}
		if (data.assignedTo)
		{
			issueDiv.find(".assignedTo").text(data.assignedTo);
		}
	}

	function issueDeleted(event)
	{
		delete taskboard.model[event.data.Id];
		var issueDiv = $('#issue-' + event.data.Id);
		issueDiv.remove();
	}

	function issuesFetched(event)
	{
		for (var i = 0; i < event.data.length; i++)
		{
			issueReceived({ data: event.data[i] });
		}
	}

	_addIssueButton.on("click", function ()
	{
		events.publish("events.issue.add");
	});

	_body.on("change", ".issue textArea", updateIssue);
	_body.on("click", ".issue .deleteTask", deleteIssue);
	_body.on("click", ".issue .openTask", openIssue);
	_body.on("click", ".issue .closeTask", closeIssue);

	events.subscribe("events.issue.added", issueReceived);
	events.subscribe("events.issue.updated", issueUpdated);
	events.subscribe("events.issue.removed", issueDeleted);
	events.subscribe("events.issue.fetched", issuesFetched);

})(window.taskboard = window.taskboard || {},
	window.events = window.events || {},
	jQuery);