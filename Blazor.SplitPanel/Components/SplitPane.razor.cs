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
        public int MinSize { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public double Size { get; set; } = 50;
        public ElementReference PaneElement { get; set; }

        public string SizeStyle => $"flex-basis: {Size.ToString(new NumberFormatInfo() { NumberDecimalSeparator = "." })}%;";
        public void SetSize(double size)
        {
            Size = size;
            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Parent.AddPane(this);
        }
    }
}
