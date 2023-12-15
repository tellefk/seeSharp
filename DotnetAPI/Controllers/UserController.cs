using Microsoft.AspNetCore.Mvc;
using Dapper;

namespace DotnetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {       
        private readonly IConfiguration _config;

        DataContextDapper _dapper;

        public UserController(IConfiguration config) {
            
            _config = config;
            _dapper = new DataContextDapper(_config);

        }

        [HttpGet("testconnection")]
        public DateTime TestConnection() {

            return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");

        }

        [HttpGet("GetUsers/{UserId}", Name = "GetUsers")]
        public List<string> GetUsers(string UserId)
        {   
            List<string> responseList = new List<string> { "test1", "test2", "test3" };

            if (UserId != null)
            {
                responseList.Add(UserId);
            }

            
            return responseList;
        }
}
}