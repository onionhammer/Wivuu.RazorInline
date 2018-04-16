using System.IO;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.FileProviders;
using Wivuu.RazorInline;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddRazorInline(this IServiceCollection services, 
                                                        string customApplicationBasePath = null)
        {
            IFileProvider fileProvider;
            if (!string.IsNullOrEmpty(customApplicationBasePath))
                fileProvider = new PhysicalFileProvider(customApplicationBasePath);
            else
                fileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProviders.Clear();
                options.FileProviders.Add(fileProvider);
            });

            return services.AddTransient<ViewRenderService>();
        }
    }
}