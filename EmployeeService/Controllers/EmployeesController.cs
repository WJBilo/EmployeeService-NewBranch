using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using EmployeeDataAcces;

namespace EmployeeService.Controllers
{
        public class EmployeesController : ApiController
    {
        private object entities;

        // Følgende Get udskriver en liste med alle Eleverne 
        public IEnumerable<ElevTable> Get()
        {
            /* 
             * 
             Årsagen til brugen af using er at det sikre, at objectet bliver bortskaffet,
             så snart den er out of scope (Færdig med dens opgave). 
             https://stackoverflow.com/questions/75401/what-are-the-uses-of-using-in-c-sharp 
             */
            using (EleverEntities entities = new EleverEntities())
            {

             
                    // Følgende er en collection property (ElevTables) der kommer til at retunere en liste over eleverne
                    return entities.ElevTables.ToList();
                
            }

        }

        // Følgende Get udskriver en bestemt elev 
        public ElevTable Get(int id)
        {
            using (EleverEntities entities = new EleverEntities())
            {
                // Retunere eleven med id'et som blev specificeret i Get Parameteren. 
                return entities.ElevTables.FirstOrDefault(elev => elev.userID == id);

            }

        }

    }
}
  

