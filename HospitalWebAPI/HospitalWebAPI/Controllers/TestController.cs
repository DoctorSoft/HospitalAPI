using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DataBaseTools.Interfaces;
using Enums.Enums;
using HospitalWebAPI.Filters;
using StorageModels.Models.UserModels;

namespace HospitalWebAPI.Controllers
{
    public class TestController : ApiController
    {
        private readonly IDataBaseContext _context;

        public TestController(IDataBaseContext context)
        {
            _context = context;
        }

        // GET api/test/token/{token}
        [TokenAuthorizationFilter(FunctionIdentityName.AdministratorPrimaryAccess)]
        public IEnumerable<string> Get([FromUri]Guid token)
        {
            //Gets clinic users
            var list = _context.Set<UserStorageModel>().Where(model => model.UserTypeId == 1).Select(model => model.Name).ToList();
            return list;
        }

        // GET api/test/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/test
        public void Post([FromBody]string value)
        {
        }

        // PUT api/test/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/test/5
        public void Delete(int id)
        {
        }
    }
}
