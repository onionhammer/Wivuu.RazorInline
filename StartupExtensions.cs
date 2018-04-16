
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Wivuu.RazorInline;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddRazorInline(this IServiceCollection services) =>
            services.AddScoped<IViewRenderService, ViewRenderService>();
    }
}