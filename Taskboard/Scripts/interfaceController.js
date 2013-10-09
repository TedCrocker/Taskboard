(function (taskboard, events, $)
{
	var selected = $([]);
	var _offset = { top: 0, left: 0 };

	function draggableStart(event, ui)
	{
		if ($(this).hasClass('ui-selected'))
		{
			selected = $(".ui-selected").each(function ()
			{
				var element = $(this);
				element.data("offset", element.offset());
			});
		}
		else
		{
			selected = $([]);
			$('.story,.task').removeClass('ui-selected');
		}
		_offset = $(this).offset();
	}
	
	function draggableDrag(event, ui)
	{
		var dt = ui.position.top - _offset.top;
		var dl = ui.position.left - _offset.left;
		
		selected.not(this).each(function ()
		{
			var element = $(this);
			var offset = element.data('offset');
			element.css({ top: offset.top + dt, left: offset.left + dl });
		});
	}

	$('.task,.story').on('click', function (e)
	{
		var $this = $(this);
		if (!e.metaKey && !e.shiftKey)
		{
			$('.task,.story').removeClass('ui-selected');
			$this.addClass('ui-selected');
		}
		else
		{
			if ($this.hasClass('ui-selected'))
			{
				$this.removeClass('ui-selected');
			}
			else
			{
				$this.addClass('ui-selected');
			}
		}

	});

	taskboard.makeDraggable = function(element)
	{
		element.draggable({
			start: draggableStart,
			drag: draggableDrag
		});
		$('body').selectable({
			filter: '.task,.story'
		});
	};
	
})( window.taskboard = window.taskboard || {},
	window.events = window.events || {},
	jQuery);