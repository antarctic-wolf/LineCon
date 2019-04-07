using LineCon.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineCon
{
    //public static class MiddleWareExtensions
    //{
    //    public static IApplicationBuilder UseConIdentifierMiddleware(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<ConIdentifierMiddleware>();
    //    }
    //}

    ////this is to get the conIdentifier out of the request url and associate the Convention with the request
    //public class ConIdentifierMiddleware
    //{
    //    private readonly RequestDelegate _next;
    //    private readonly AppSettings _settings;

    //    public ConIdentifierMiddleware(RequestDelegate next, IOptions<AppSettings> options)
    //    {
    //        _next = next;
    //        _settings = options.Value;
    //    }

    //    public async Task Invoke(HttpContext httpContext)
    //    {
    //        //TODO: figure this out maybe?
    //        var conIdentifier = httpContext.GetRouteValue("conIdentifier") as string;
    //        var controller = httpContext.GetRouteValue("controller") as string;

    //        if (conIdentifier == null && controller == null)
    //        {
    //            controller = "App";
    //        }

    //        if (_settings.RequireConIdentifierControllers.Contains(controller))
    //        {
    //            using (var context = httpContext.RequestServices.GetService<LineConContext>())
    //            {
    //                var convention = context.Conventions.SingleOrDefault(c => c.UrlIdentifier == conIdentifier);
    //                if (convention == null)
    //                {
    //                    //break the pipeline
    //                }
    //                httpContext.Items.Add("conventionId", conIdentifier);
    //            }
    //        }
    //        await _next(httpContext);
    //    }
    //}

    //this is for the MapRoute middleware
    public class ConIdentifierConstraint : IRouteConstraint
    {
        //checks that the conIdentifier matches a convention in the database
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            //var conIdentifier = values["conIdentifier"] as string;
            //using (var context = httpContext.RequestServices.GetService<LineConContext>())
            //{
            //    //TODO: turn this on
            //    //return context.Conventions.Any(c => c.UrlIdentifier == conIdentifier);
            //    return true;
            //}
            return true; //TODO
        }
    }
}
