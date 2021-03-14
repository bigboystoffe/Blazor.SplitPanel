using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SplitPanel.Components
{
    public class SplitComponentBase : ComponentBase
    {
        public string Id { get; private set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Id = Guid.NewGuid().ToString();
        }
    }
}
