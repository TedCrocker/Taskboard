(function (taskboard, events, $)
{
	var selectors = '.story,.task,.issue';
	var selected = $([]);
	var _offset = { top: 0, left: 0 };
	var _dragCallbacks = {};

	function draggableStart(event, ui)
	{
		ui.position.top -= $(window).scrollTop();
		ui.position.left -= $(window).scrollLeft();
		
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
			$(selectors).removeClass('ui-selected');
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
			_dragCallbacks[element.attr('id')].apply(this);
		});
	}

	$(selectors).on('click', function (e)
	{
		var $this = $(this);
		if (!e.metaKey && !e.shiftKey)
		{
			$(selectors).removeClass('ui-selected');
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

	taskboard.fadeBorder = function(element)
	{
		element.css('border-width', 3);
		element.css('border-style', 'solid');
		element.css('border-color', 'rgba(255,153,0,100)');
		setTimeout(function()
		{
			$({ alpha: 1 }).animate({ alpha: 0 }, {
				duration: 1000,
				step: function () {
					element.css('border-color', 'rgba(255,153,0,' + this.alpha + ')');
				}
			});
		}, 5000);
	};

	taskboard.makeDraggable = function(element, dragCallback)
	{
		if (dragCallback)
		{
			_dragCallbacks[element.attr('id')] = dragCallback;
		}

		element.draggable({
			start: draggableStart,
			drag: function(event, ui)
			{
				draggableDrag.apply(this, arguments);
				if (dragCallback)
				{
					dragCallback.apply(this, arguments);
				}
			}
		});
		$('body').selectable({
			filter: selectors
		});
	};
	
})( window.taskboard = window.taskboard || {},
	window.events = window.events || {},
	jQuery);