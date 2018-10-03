using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace EmployeeService
{
    public class RequireHttpsAttribute : AuthorizationFilterAttribute 
    {
        // Følgende funktion har en parameter actionContext som provider os med adgang til både the request og response objekt. 
        // Find ud af hvad HttpActionContext klassen gør 
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Hvis browser request ikke er issued med https protokol så redirect the request, så det benytter https

            if(actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                // Response er en instance af HttpResponseMessage klassen: Defination af HttpResponseMessage:  https://www.oreilly.com/library/view/tcpip-guide/9781593270476/ch81s03.html 
                // https://www.techopedia.com/definition/27178/http-header 
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Found);
                actionContext.Response.Content = new StringContent("<p> Benyt HTTPS istedetfor HTTP </p>", Encoding.UTF8, "text/html"); // text/html gør at Content-typen ændres til html 
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