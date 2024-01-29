using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using DotnetAPI.Models;
using DotnetAPI.Abstractions;


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
        public ActionResult<List<string>> PostUser(string UserName, string UserId)
        {
            List<string> responseList = new List<string> { "test1", "test2", "test3" };

            if (UserId != null)
            {
                responseList.Add(UserId);
            }

            return responseList;
        }

        [HttpPost("CreateUser", Name = "Create User")]
        public async Task<IActionResult> CreateUser(User user)
        {

            string sql = @"INSERT INTO TutorialAppSchema.Users (FirstName, LastName, Email, Gender, Active) 
                               VALUES (@FirstName, @LastName, @Email, @Gender, @Active)";


            await _dapper.ExecuteCommandAsync(sql, new
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Gender = user.Gender,
                Active = user.Active
            });

            return Ok("User created successfully");

        }



        [HttpPut("updateuser/{UserId}")]
        public async Task<IActionResult> EditUser(string UserId, User user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string sql = @"UPDATE TutorialAppSchema.Users 
                           SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Gender = @Gender, Active = @Active 
                           WHERE UserId = @UserId";
            await _dapper.ExecuteCommandAsync(sql, new
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Gender = user.Gender,
                Active = user.Active,
                UserId = UserId
            });
            // Your code for updating the user goes here

            return Ok("User updated successfully");
        }


        [HttpPatch("UpdateEmail/{UserId}")]
        public async Task<IActionResult> UpdateEmail(string UserId, [FromBody] string Email)
        {
            string sql = @"UPDATE TutorialAppSchema.Users 
                           SET Email = @Email 
                           WHERE UserId = @UserId";
            await _dapper.ExecuteCommandAsync(sql, new
            {
                Email = Email,
                UserId = UserId
            });

            return Ok("Email updated successfully");
        }


        [HttpPatch("UpdateEmailErrorType/{UserId}")]
        public async Task<Result> UpdateEmail2(string UserId, [FromBody] string Email)
        {
            User? user = await _dapper.LoadDataSingle<User>($"SELECT * FROM TutorialAppSchema.Users WHERE UserId = '{UserId}'");

            if (user == null)
            {
                return Result.Failure(new Error("UserNotFound", $"User with id {UserId} not found"));
            }

            string sql = @"UPDATE TutorialAppSchema.Users 
                           SET Email = @Email 
                           WHERE UserId = @UserId";
            await _dapper.ExecuteCommandAsync(sql, new
            {
                Email = Email,
                UserId = UserId
            });
            
            return Result.Success();
        }







    }


}
