using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Enums.Enums;
using HospitalWebAPI.Filters;
using Repositories.DataBaseRepositories.UserRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;

namespace HospitalWebAPI.Controllers
{
    public class TestController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public TestController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET api/test/token/{token}
        [TokenAuthorizationFilter(FunctionIdentityName.AdministratorPrimaryAccess)]
        public IEnumerable<string> Get([FromUri]Guid token)
        {
            //Gets clinic users
            var list = _userRepository.GetModels().Where(model => model.UserTypeId == 1).Select(model => model.Name).ToList();
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
