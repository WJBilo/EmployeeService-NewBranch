using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace EmployeeService
{

    public class CustomJsonFormatter : JsonMediaTypeFormatter
    {

        public CustomJsonFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html")); 
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json"); 
        }
    }


    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            // Følgende sørger for at den rå json data som brugeren får når de benytter Get metoden er ordentligt indented. 
            // Og har camelcase isdedet for pascalcase 
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();


            config.Formatters.Add(new CustomJsonFormatter()); 
        }
    }
}
