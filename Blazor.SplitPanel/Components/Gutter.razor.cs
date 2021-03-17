﻿using Majorsoft.Blazor.Components.Common.JsInterop.ElementInfo;
using Majorsoft.Blazor.Components.Common.JsInterop.GlobalMouseEvents;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SplitPanel.Components
{
    public partial class Gutter
    {
        [Inject]
        private IGlobalMouseEventHandler MouseEventHandler { get; set; }

        [Inject]
        private SplitJsInterop JsInterop { get; set; }

        private double _size;
        private double _start, _end;
        private double _dragOffset;
        private bool _isDragging;

        private string _mouseMove, _mouseUp;


        [CascadingParameter(Name = "SplitArea")]
        public SplitArea Parent { get; set; }

        [Parameter]
        public (SplitPane, SplitPane) Pair { get; set; }

        public string GutterStyle { get; private set; }

        public async Task DragStart(MouseEventArgs e)
        {
            var a = await Pair.Item1.PaneElement.GetClientRectAsync();
            var b = await Pair.Item2.PaneElement.GetClientRectAsync();

            var aSize = Parent.Direction == Models.SplitDirection.Horizontal ? a.Width : a.Height;
            var bSize = Parent.Direction == Models.SplitDirection.Horizontal ? b.Width : b.Height;

            _size = aSize + bSize + Parent.GutterSize;
            _start = Parent.Direction == Models.SplitDirection.Horizontal ? a.Left : a.Top;
            _end = Parent.Direction == Models.SplitDirection.Horizontal ? a.Right : a.Bottom;

            _dragOffset = GetMousePosition(e) - _end;
            _isDragging = true;

            _mouseMove = await MouseEventHandler.RegisterPageMouseMoveAsync(DragAsync);
            _mouseUp = await MouseEventHandler.RegisterPageMouseUpAsync(DragEndAsync);

            await Parent.DragStartAsync(Pair);
        }

        public async Task DragAsync(MouseEventArgs e)
        {
            if (!_isDragging)
                return;

            var percentage = (Pair.Item1.Size ?? 0) + (Pair.Item2.Size ?? 0);
            var offset = GetMousePosition(e) - _start + (Parent.GutterSize - _dragOffset);

            if (offset <= Pair.Item1.MinSize + Parent.GutterSize)
            {
                offset = Pair.Item1.MinSize + Parent.GutterSize;
            }
            else if (offset >= _size - (Pair.Item2.MinSize + Parent.GutterSize))
            {
                offset = _size - (Pair.Item2.MinSize + Parent.GutterSize);
            }

            var aSize = (offset / _size) * percentage;
            var bSize = percentage - ((offset / _size) * percentage);

            await Pair.Item1.SetSizeAsync(aSize);
            await Pair.Item2.SetSizeAsync(bSize);
        }

        public async Task DragEndAsync(MouseEventArgs e)
        {
            _isDragging = false;

            await Task.WhenAll(
                MouseEventHandler.RemovePageMouseMoveAsync(_mouseMove),
                MouseEventHandler.RemovePageMouseUpAsync(_mouseUp),
                Parent.DragEndAsync(Pair)
            );
        }

        private double GetMousePosition(MouseEventArgs e) => Parent.Direction == Models.SplitDirection.Horizontal ? e.ClientX : e.ClientY;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            GutterStyle = $"{(Parent.Direction == Models.SplitDirection.Horizontal ? "width: " : "height:")}{Parent.GutterSize}px ";
        }

        public async ValueTask DisposeAsync()
        {
            if (MouseEventHandler is not null)
            {
                await MouseEventHandler.DisposeAsync();
            }
        }
    }
}
