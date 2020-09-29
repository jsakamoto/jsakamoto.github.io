using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace CUIFlavoredPortfolioSite.PreRenderer
{
    class Program
    {
        private enum RewritingHtmlState
        {
            BeforeMarker,
            InsideMarkers,
            AfterMarker
        }

        static async Task Main(string[] args)
        {
            var content = await RenderComponentAsync(
                componentType: typeof(App),
                parameters: new Dictionary<string, object> { { nameof(App.RuntimeMode), App.RuntimeModes.PreRendering } },
                configureServices: services =>
                {
                    CUIFlavoredPortfolioSite.Program.ConfigureServices(services, baseAddress: "http://localhost:5000/");
                });

            //var stringWriter = new StringWriter();
            //content.WriteTo(stringWriter, HtmlEncoder.Default);
            //Console.WriteLine(stringWriter.ToString());

            var targetHtmlFilePath = args[0];
            RewriteHtmlFile(targetHtmlFilePath, content);
        }

        public static async Task<IHtmlContent> RenderComponentAsync(
            Type componentType,
            Action<IServiceCollection> configureServices,
            IDictionary<string, object> parameters = null,
            RenderMode renderMode = RenderMode.Static)
        {
            var diContainer = new ServiceCollection();
            diContainer.AddLogging();
            diContainer.AddRazorPages();
            configureServices(diContainer);
            diContainer.TryAddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5000/") });

            using var serviceProvider = diContainer.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();

            //using var host = Host.CreateDefaultBuilder(args)
            //.ConfigureServices(services =>
            //{
            //    services.AddRazorPages();
            //    configureServices(services);
            //}).Build();
            //using var scope = host.Services.CreateScope();

            var featureCollection = new FeatureCollection();
            featureCollection.Set<IHttpRequestFeature>(new HttpRequestFeature
            {
                Protocol = "HTTP/2",
                Scheme = "http",
                Method = "GET",
                PathBase = "",
                Path = "/",
                QueryString = "",
                RawTarget = "/",
                Headers = { { "Host", "localhost:5000" } }
            });
            featureCollection.Set<IHttpResponseFeature>(new HttpResponseFeature());

            var httpContextFactory = new DefaultHttpContextFactory(scope.ServiceProvider);
            var httpContext = httpContextFactory.Create(featureCollection);

            var attributes = new TagHelperAttributeList();

            var tagHelperContext = new TagHelperContext(attributes, new Dictionary<object, object>(), Guid.NewGuid().ToString("N"));

            var tagHelperOutput = new TagHelperOutput(
               tagName: string.Empty,
               attributes,
               getChildContentAsync: (_, _) => Task.FromResult(new DefaultTagHelperContent() as TagHelperContent));

            var componentTagHelper = new ComponentTagHelper
            {
                ComponentType = componentType,
                RenderMode = renderMode,
                Parameters = parameters,
                ViewContext = new ViewContext { HttpContext = httpContext }
            };

            await componentTagHelper.ProcessAsync(tagHelperContext, tagHelperOutput);

            httpContextFactory.Dispose(httpContext);

            return tagHelperOutput.Content;
        }

        private static void RewriteHtmlFile(string targetHtmlFilePath, IHtmlContent content)
        {
            var sourceHtmlLines = File.ReadAllLines(targetHtmlFilePath);

            var state = RewritingHtmlState.BeforeMarker;
            using var targetHtmlFileWriter = File.CreateText(targetHtmlFilePath);
            foreach (var sourceHtmlLine in sourceHtmlLines)
            {
                state = sourceHtmlLine.EndsWith("<!-- END PRERENDERING -->") ? RewritingHtmlState.AfterMarker : state;

                if (state != RewritingHtmlState.InsideMarkers)
                    targetHtmlFileWriter.WriteLine(sourceHtmlLine);

                if (sourceHtmlLine.EndsWith("<!-- BEGIN PRERENDERING -->"))
                {
                    state = RewritingHtmlState.InsideMarkers;

                    content.WriteTo(targetHtmlFileWriter, HtmlEncoder.Default);
                    targetHtmlFileWriter.WriteLine();
                }
            }
        }
    }
}
