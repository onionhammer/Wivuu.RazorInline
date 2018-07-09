using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.ObjectPool;
using Wivuu.RazorInline;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddRazorInline(this IServiceCollection services, 
                                                        string applicationName = null,
                                                        string customWorkingDirectory = null)
        {
            applicationName = applicationName 
                ?? Assembly.GetEntryAssembly()?.GetName()?.Name 
                ?? "Wivuu.RazorInline";

            var fileProvider = new PhysicalFileProvider(
                customWorkingDirectory 
                ?? Directory.GetCurrentDirectory());

            services.AddSingleton<IHostingEnvironment>(new HostingEnvironment
            {
                ApplicationName     = applicationName,
                WebRootFileProvider = fileProvider
            });

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProviders.Clear();
                options.FileProviders.Add(fileProvider);
            });

            var diagnosticSource = new DiagnosticListener("Microsoft.AspNetCore");
            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddSingleton<DiagnosticSource>(diagnosticSource);
            services.AddLogging();
            services.AddMvc();

            return services.AddScoped<IViewRenderService, ViewRenderService>();
        }
    }
}