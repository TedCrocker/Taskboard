(function (events, $)
{
	var _addStoryButton = $('#addStory');
	var _body = $('body');
	var _dateDisplayFormat = "DD/MM/YY A";

	function storyReceived(event)
	{
		if (event.data.opened != null)
		{
			event.data.opened = moment(event.data.opened).format(_dateDisplayFormat);
		}
		else
		{
			event.data.opened = '';
		}
		
		if (event.data.closed != null)
		{
			event.data.closed = moment(event.data.closed).format(_dateDisplayFormat);
		}
		else
		{
			event.data.closed = '';
		}

		if (event.data.workFlowState == 1)
		{
			event.data.stateClass = 'open';
		}
		else if (event.data.workFlowState == 2)
		{
			event.data.stateClass = 'closed';
		}

		dust.render("story", event.data, function(err, output)
		{
			var $output = $(output);
			_body.append($output);
			taskboard.makeDraggable($output, dragUpdateStory);
		});
	}

	var _timeStamp = new Date();
	function dragUpdateStory()
	{
		if (new Date() - _timeStamp > 100)
		{
			updateStory.apply($(this).find('textarea'));
			_timeStamp = new Date();
		}
	}

	function storyUpdated(event)
	{
		var storyDiv = $('#story-' + event.data.Id);
		storyDiv.css('left', event.data.left);
		storyDiv.css('top', event.data.top);
		storyDiv.find('.content').val(event.data.content);

		storyDiv.removeClass('closed');
		storyDiv.removeClass('open');
		if (event.data.workFlowState == 1)
		{
			storyDiv.addClass('open');
		}
		else if (event.data.workFlowState == 2)
		{
			storyDiv.addClass('closed');
		}
	}
	
	function storyDeleted(event)
	{
		var storyDiv = $('#story-' + event.data.Id);
		storyDiv.remove();
	}
	
	function storiesFetched(event)
	{
		for (var i = 0; i < event.data.length; i++)
		{
			storyReceived({ data: event.data[i] });
		}
	}
	
	function getDate(dateVal)
	{
		var date = null;
		if (dateVal)
		{
			date = moment(dateVal.substring(0, 8), "DD/MM/YY");
			if (dateVal.indexOf("PM") > -1)
			{
				date.set('hour', 13);
			}
			date = date.toJSON();
		}

		return date;
	}

	function updateStory()
	{
		var storyDiv = $(this).parent();

		var state = 0;
		if (storyDiv.hasClass('open'))
		{
			state = 1;
		}
		else if (storyDiv.hasClass('closed'))
		{
			state = 2;
		}

		var openDate = getDate(storyDiv.find('.openDate').text());
		var closeDate = getDate(storyDiv.find('.closeDate').text());

		var data = {
			Id: storyDiv.attr('id').substring(6),
			left: parseInt(storyDiv.css('left'), 10),
			top: parseInt(storyDiv.css('top'), 10),
			content: storyDiv.find('.content').val(),
			workFlowState: state,
			opened: openDate,
			closed: closeDate
		};

		events.publish("events.story.update", data);
	}
	
	function deleteStory()
	{
		var data = { Id: $(this).parent().attr('id').substring(6) };
		events.publish('events.story.remove', data);
	}
	
	function openStory()
	{
		var parent = $(this).parent();
		parent.addClass('open');
		parent.find('.openDate').text(moment().format(_dateDisplayFormat));
		
		updateStory.apply(this);
	}
	
	function closeStory()
	{
		var parent = $(this).parent();
		parent.addClass('closed');
		parent.removeClass('open');
		
		parent.find('.closeDate').text(moment().format(_dateDisplayFormat));
		updateStory.apply(this);
	}

	_addStoryButton.click(function ()
	{
		events.publish("events.story.add");
	});
	
	_body.on("keyup",  ".story textArea",	 updateStory);
	_body.on("click",  ".story .deleteStory", deleteStory);
	_body.on("click",  ".story .openStory",	 openStory);
	_body.on("click",  ".story .closeStory",	 closeStory);
	
	events.subscribe("events.story.added",	 storyReceived);
	events.subscribe("events.story.updated", storyUpdated);
	events.subscribe("events.story.removed", storyDeleted);
	events.subscribe("events.story.fetched", storiesFetched);

})(window.events = window.events || {},
	jQuery);