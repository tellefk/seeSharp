using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DotnetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly DataContextDapper _dapper;

        public UserController(IConfiguration config)
        {
            _config = config;
            _dapper = new DataContextDapper(_config);
        }

        [HttpGet("testconnection")]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                var result = await _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("TestConnectionPostgres")]
        public async Task<IActionResult> TestConnectionPostgres()
        {
            try
            {
                var result = await _dapper.LoadDataPostgresSingle<DateTime>("SELECT NOW()");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("TestConnectionPostgres/multiple")]
        public async Task<IActionResult> TestConnectionPostgresMultiple()
        {
            var result = await _dapper.LoadDataPostgres<IEnumerable<User>>("SELECT * from users ");
            try
            {
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetUsers/{UserId}", Name = "GetUsers")]
        public ActionResult<List<string>> GetUsers(string UserId)
        {
            List<string> responseList = new List<string> { "test1", "test2", "test3" };

            if (UserId != null)
            {
                responseList.Add(UserId);
            }

            return responseList;
        }


        [HttpPost("PostUser", Name = "Add User")]
        public ActionResult<List<string>> PostUser(string UserName string)
        {
            List<string> responseList = new List<string> { "test1", "test2", "test3" };

            if (UserId != null)
            {
                responseList.Add(UserId);
            }

            return responseList;
        }




    }       
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        // Other properties matching the users table columns
    }

}
