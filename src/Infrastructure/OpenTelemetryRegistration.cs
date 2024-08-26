using System.Diagnostics;
using Application.OpenTelemetry;
using Infrastructure.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Exporter;
using OpenTelemetry.Instrumentation.Http;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Infrastructure;

public static class OpenTelemetryRegistration
{
    internal static void AddOpenTelemetryRegistration(this WebApplicationBuilder builder)
    {
        ActivitySourceProvider.Source = new ActivitySource(ApplicationConstants.ActivitySourceName);
        var resource = ResourceBuilder.CreateDefault().AddService(ApplicationConstants.ServiceName,
            ApplicationConstants.ServiceNamespace, ApplicationConstants.ServiceVersion);
        
        builder.Logging.ClearProviders();
        builder.Logging.AddOpenTelemetry(options =>
        {
            options.SetResourceBuilder(resource);
            options.AddOtlpExporter(ConfigureOtlpExporter);
            // options.AddConsoleExporter();
        
            options.IncludeScopes = true;
            options.IncludeFormattedMessage = true;
            options.ParseStateValues = true;
        });
        
        builder.Services.AddOpenTelemetry()
            .WithMetrics(metrics => metrics
                .SetResourceBuilder(resource)
                .AddMeter(OpenTelemetryMetric.UserCreatedEventCounterMeterName)
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation()
                .AddProcessInstrumentation()
                .AddOtlpExporter(ConfigureOtlpExporter)
                // .AddPrometheusExporter()
                // .AddConsoleExporter()
            )
            .WithTracing(traces => traces
                .AddSource(ApplicationConstants.ActivitySourceName)
                .SetResourceBuilder(resource)
                .AddAspNetCoreInstrumentation(opts =>
                {
                    opts.Filter = (context) => context.Request.Path.Value != null && 
                                               context.Request.Path.Value.Contains("api", StringComparison.InvariantCulture);
                    opts.RecordException = true;
                })
                .AddEntityFrameworkCoreInstrumentation(opts =>
                {
                    opts.SetDbStatementForText = true;
                    opts.SetDbStatementForStoredProcedure = true;
                    opts.EnrichWithIDbCommand = (activity, command) =>
                    {
                        // enrich
                    };
                })
                .AddHttpClientInstrumentation(HttpClientRequestResponseSetTag)
                .AddOtlpExporter(ConfigureOtlpExporter)
                // .AddConsoleExporter()
            );
    }
    
    private static void ConfigureOtlpExporter(OtlpExporterOptions opts)
    {
        opts.Endpoint = new Uri("http://localhost:4317");
        opts.Protocol = OtlpExportProtocol.Grpc;
    }

    private static void HttpClientRequestResponseSetTag(HttpClientTraceInstrumentationOptions opts)
    {
        opts.EnrichWithHttpRequestMessage = async (activity, request) =>
        {
            var requestContent = "empty";
            if (request.Content != null)
            {
                requestContent = await request.Content.ReadAsStringAsync();
            }

            activity.SetTag("http.request.body", requestContent);
                        
        };
        opts.EnrichWithHttpResponseMessage = async (activity, response) =>
        {
            activity.SetTag("http.response.body", await response.Content.ReadAsStringAsync());
        };
    }
}