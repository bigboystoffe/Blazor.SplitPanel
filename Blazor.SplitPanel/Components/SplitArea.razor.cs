using Blazor.SplitPanel.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SplitPanel.Components
{
    public partial class SplitArea
    {
        private string cursor;
        private string gutterClass;
        private string splitAreaClass;

        public List<SplitPane> Panes { get; private set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public int GutterSize { get; set; }

        [Parameter]
        public SplitDirection Direction { get; set; }

        [Parameter]
        public string Cursor
        {
            get => cursor ?? (Direction == SplitDirection.Horizontal ? "col-resize" : "row-resize");
            set => cursor = value;
        }

        [Parameter]
        public string GutterClass
        {
            get => gutterClass ?? (Direction == SplitDirection.Horizontal ? "gutter gutter__horizontal" : "gutter gutter__vertical");
            set => gutterClass = value;
        }

        public string SplitAreaClass
        {
            get => splitAreaClass ?? (Direction == SplitDirection.Horizontal ? "split-area split-area__horizontal" : "split-area split-area__vertical");
            set => splitAreaClass = value;
        }

        public void AddPane(SplitPane pane)
        {
            Panes.Add(pane);
            StateHasChanged();
        }

        public (SplitPane, SplitPane) GetPanePair(SplitPane pane)
        {
            return new(Panes[Panes.IndexOf(pane) - 1], pane);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Panes = new List<SplitPane>();
        }
    }
}
