using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SplitPanel
{
    public static class InteropExtensions
    {
        public static IServiceCollection AddSplitPanelJS(this IServiceCollection services)
        {
            return services.AddScoped<SplitJsInterop>();
        }
    }
}
