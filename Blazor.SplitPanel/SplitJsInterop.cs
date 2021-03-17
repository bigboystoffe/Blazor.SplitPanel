using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Microsoft.JSInterop.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SplitPanel
{
    public class SplitJsInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> _module;

        public SplitJsInterop(IJSRuntime runtime)
        {
            _module = new(() => runtime.InvokeAsync<IJSObjectReference>(
                "import",
                "./_content/Blazor.SplitPanel/Blazor.SplitPanel.js").AsTask());
        }


        public async Task SetElementStyleAsync(ElementReference elementReference, string property, string value)
        {
            var module = await _module.Value;
            await module.InvokeVoidAsync("SetElementStyle", elementReference, property, value);
        }


        public async Task Alert (ElementReference elementReference, string property, string value)
        {
            var module = await _module.Value;
            await module.InvokeVoidAsync("Alert", elementReference, property, value);
        }

        public async ValueTask DisposeAsync()
        {
            if (_module.IsValueCreated)
            {
                await (await _module.Value).DisposeAsync();
            }
        }
    }
}
