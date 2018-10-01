﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace EmployeeService
{
    public class RequireHttpsAttribute : AuthorizationFilterAttribute 
    {
        // Følgende funktion har en parameter actionContext som provider os med adgang til både the request og response objekt. 
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Hvis browser request ikke er issued med https protokol så redirect the request, så det benytter https

            if(actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                // 
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Found);
                actionContext.Response.Content = new StringContent("<p> Benyt HTTPS istedetfor HTTP </p>");

                // Her redirecter vi automatisk til https ved hjælp af UriBuilder klassen. 
                // Og vi bygger URI'en fra request objektet 
                UriBuilder uriBuilder = new UriBuilder(actionContext.Request.RequestUri);
                // URI'en skal have https som scheme 
                uriBuilder.Scheme = Uri.UriSchemeHttps;
                uriBuilder.Port = 44352;

                actionContext.Response.Headers.Location = uriBuilder.Uri; 


            }
            else
            {   // Hvis det dog ikke er tilfældet vil vi bare udføre base klassens OnAuthorization metode
                base.OnAuthorization(actionContext);
            }
            
        }

    }
}