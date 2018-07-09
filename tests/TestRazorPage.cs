using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wivuu.RazorInline;

namespace tests
{
    [TestClass]
    public class TestRazorPage
    {
        [TestMethod]
        public async Task TestRendering()
        {
            var service = new ServiceCollection()
                .AddRazorInline("tests")
                .BuildServiceProvider()
                .GetRequiredService<IServiceScopeFactory>();

            using (var scope = service.CreateScope())
            {
                var renderer = scope.ServiceProvider.GetRequiredService<IViewRenderService>();

                var result = await renderer.RenderToStringAsync("ViewTest", null);

                Console.WriteLine(result);
            }
        }
    }
}
