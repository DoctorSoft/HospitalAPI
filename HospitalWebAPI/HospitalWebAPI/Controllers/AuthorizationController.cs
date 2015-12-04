using System.Collections.Generic;
using System.Web.Http;

namespace HospitalWebAPI.Controllers
{
    public class AuthorizationController : ApiController
    {
        // Action to get authorization token or message of mistake authorization
        // POST api/authorization
        public string Post([FromBody]string value)
        {
            return null;
        }
    }
}