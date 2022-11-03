using System.Runtime.CompilerServices;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MovieLibrary.Models.Responses;
using Newtonsoft.Json;

namespace Movie_Library_Final_Project.Extensions
{
    public static class HealthCheckExtension
    {
        public static IApplicationBuilder RegisterHealthChecks(this IApplicationBuilder app)
        {
            return app.UseHealthChecks("/healthz", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    var response = new HealthCheckResponse()
                    {
                        Status = report.Status.ToString(),
                        HealthCheckList = report.Entries
                        .Select(x => new IndividualHealthCheckResponse()
                        {
                            Component = x.Key,
                            Status = x.Value.Status.ToString(),
                            Description = x.Value.Description
                        }),
                        HealthCheckDuration = report.TotalDuration
                    };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response, Formatting.Indented));
                }
            });
        }
    }
}
