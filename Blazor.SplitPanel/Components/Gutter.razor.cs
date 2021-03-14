using Majorsoft.Blazor.Components.Common.JsInterop.ElementInfo;
using Majorsoft.Blazor.Components.Common.JsInterop.GlobalMouseEvents;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
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

        private double _size;
        private double _start, _end;
        private double _dragOffset;
        private bool _isDragging;

        private string _mouseMove, _mouseUp;

        [CascadingParameter(Name = "SplitArea")]
        public SplitArea Parent { get; set; }

        [Parameter]
        public (SplitPane,SplitPane) Pair { get; set; }

        public string GutterStyle { get; private set; }

        public async Task OnDragStart(MouseEventArgs e)
        {
            var a = await Pair.Item1.PaneElement.GetClientRectAsync();
            var b = await Pair.Item2.PaneElement.GetClientRectAsync();

            var aSize = Parent.Direction == Models.SplitDirection.Horizontal ? a.Width : a.Height;
            var bSize = Parent.Direction == Models.SplitDirection.Horizontal ? b.Width : b.Height;

            _size = aSize + bSize + Parent.GutterSize;
            _start = Parent.Direction == Models.SplitDirection.Horizontal ? a.Left : a.Top;
            _end = Parent.Direction == Models.SplitDirection.Horizontal ? a.Right : a.Bottom;

            _dragOffset =  GetMousePosition(e) - _end;
            _isDragging = true;

            _mouseMove = await MouseEventHandler.RegisterPageMouseMoveAsync(OnDragAsync);
            _mouseUp = await MouseEventHandler.RegisterPageMouseUpAsync(OnDragEndAsync);
        }

        public Task OnDragAsync(MouseEventArgs e)
        {
            if (!_isDragging)
                return Task.CompletedTask;

            var mousePosition = GetMousePosition(e);

            var offset = mousePosition - _start + (Parent.GutterSize - _dragOffset);


            var aSize = Math.Clamp((offset / _size) * 100, 0, 100);
            var bSize = Math.Clamp(100 - ((offset / _size) * 100), 0, 100);

            Pair.Item1.SetSize(aSize);
            Pair.Item2.SetSize(bSize);

            Console.WriteLine($"aSize: {aSize} bSize: {bSize} offset: {offset} _size: {_size}");
            return Task.CompletedTask;
        }

        public async Task OnDragEndAsync(MouseEventArgs e)
        {
            _isDragging = false;

            await MouseEventHandler.RemovePageMouseMoveAsync(_mouseMove);
            await MouseEventHandler.RemovePageMouseUpAsync(_mouseUp);
        }


        private double GetMousePosition(MouseEventArgs e)
        {
            return Parent.Direction == Models.SplitDirection.Horizontal ? e.ClientX : e.ClientY;

        }

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
