using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SplitPanel.Components
{
    public partial class SplitPane
    {

        [CascadingParameter(Name = "SplitArea")]
        public SplitArea Parent { get; set; }

        [Parameter]
        public string CssClass { get; set; }

        [Parameter]
        public int MinSize { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public double? Size { get; set; }

        public ElementReference PaneElement { get; set; }

        private string SizeStyle => $"{Size.GetValueOrDefault(Parent.GetPaneAutoSize()).ToString(new NumberFormatInfo() { NumberDecimalSeparator = "." })}%;";

        public Task SetSizeAsync(double size)
        {
            Size = size;
            StateHasChanged();

            return Task.CompletedTask;
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Parent.AddPane(this);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            await JsInterop.SetElementStyleAsync(PaneElement, "flexBasis", SizeStyle);
        }
    }
}
