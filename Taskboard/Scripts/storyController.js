(function (events, $)
{
	var _addStoryButton = $('#addStory');
	var _body = $('body');

	var _sizeDropdown = $('<select/>').addClass('size');
	_sizeDropdown.append($('<option/>').val('').text(''));
	_sizeDropdown.append($('<option/>').val(1).text('XS'));
	_sizeDropdown.append($('<option/>').val(2).text('S'));
	_sizeDropdown.append($('<option/>').val(4).text('M'));
	_sizeDropdown.append($('<option/>').val(8).text('L'));
	_sizeDropdown.append($('<option/>').val(16).text('XL'));

	function storyReceived(event)
	{
		var storyDiv = $('<div/>').addClass('story');
		storyDiv.attr('id', 'story-' + event.data.Id);
		storyDiv.css('left', event.data.left);
		storyDiv.css('top', event.data.top);
		

		var textArea = $('<textarea/>').text(event.data.content).addClass('content');
		storyDiv.append(textArea);

		var deleteButton = $('<button/>').text('X').addClass('deleteStory');
		storyDiv.append(deleteButton);

		var openButton = $('<button/>').text('Open').addClass('openStory');
		storyDiv.append(openButton);

		var closeButton = $('<button/>').text('Close').addClass('closeStory');
		storyDiv.append(closeButton);

		var openDate = $('<div/>').text(event.data.opened == null ? '' : event.data.opened).addClass('openDate');
		storyDiv.append(openDate);
		
		var closeDate = $('<div/>').text(event.data.closed == null ? '' : event.data.closed).addClass('closeDate');
		storyDiv.append(closeDate);

		storyDiv.append(_sizeDropdown.clone());


		if (event.data.workFlowState == 1)
		{
			storyDiv.addClass('open');
		}
		else if (event.data.workFlowState == 2)
		{
			storyDiv.addClass('closed');
		}

		_body.append(storyDiv);
		storyDiv.draggable({
			containment: 'body',
			drag: dragUpdateStory
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

		var data = {
			Id: storyDiv.attr('id').substring(6),
			left: parseInt(storyDiv.css('left'), 10),
			top: parseInt(storyDiv.css('top'), 10),
			content: storyDiv.find('.content').val(),
			workFlowState: state,
			
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
		var date = new Date();
		parent.find('.openDate').text(date.toDateString());
		
		updateStory.apply(this);
	}
	
	function closeStory()
	{
		var parent = $(this).parent();
		parent.addClass('closed');
		parent.removeClass('open');
		
		var date = new Date();
		parent.find('.closeDate').text(date.toDateString());
		updateStory.apply(this);
	}

	_addStoryButton.click(function ()
	{
		events.publish("events.story.add");
	});
	
	_body.on("keyup", ".story textArea",	 updateStory);
	_body.on("click",  ".story .deleteStory", deleteStory);
	_body.on("click",  ".story .openStory",	 openStory);
	_body.on("click",  ".story .closeStory",	 closeStory);
	
	events.subscribe("events.story.added",	 storyReceived);
	events.subscribe("events.story.updated", storyUpdated);
	events.subscribe("events.story.removed", storyDeleted);
	events.subscribe("events.story.fetched", storiesFetched);

})(window.events = window.events || {},
	jQuery);