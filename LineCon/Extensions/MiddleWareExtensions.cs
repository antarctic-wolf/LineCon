using LineCon.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LineCon
{
    public static class MiddleWareExtensions
    {
        public static IApplicationBuilder UseConIdentifierMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ConIdentifierMiddleware>();
        }
    }

    //this is to get the conIdentifier out of the request url and associate the Convention with the request
    public class ConIdentifierMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _settings;

        public ConIdentifierMiddleware(RequestDelegate next, IOptions<AppSettings> options)
        {
            _next = next;
            _settings = options.Value;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var conIdentifier =  httpContext.GetRouteValue("conIdentifier") as string;
            var controller = httpContext.GetRouteValue("controller") as string;

            if (conIdentifier == null && controller == null)
            {
                controller = "App";
            }

            if (_settings.RequireConIdentifierControllers.Contains(controller) && conIdentifier != null)
            {
                using (var scope = httpContext.RequestServices.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<LineConContext>();

                    var convention = context.Conventions
                        .Include(c => c.ConConfig)
                            .ThenInclude(cc => cc.RegistrationHours)
                        .SingleOrDefault(c => c.UrlIdentifier == conIdentifier);

                    if (convention == null)
                    {
                        //TODO: break the pipeline
                        //httpContext.Response.Redirect("some error page");
                    }
                    else
                    {
                        httpContext.Items.Add("convention", convention);
                        await _next(httpContext);
                    }
                }
            }
            else
            {
                await _next(httpContext);
            }
        }
    }
}
