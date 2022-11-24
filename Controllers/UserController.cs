using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConnectPgSql
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto userRequest) 
        {
            if(_userService.CheckIfUsernameExists(userRequest)) return BadRequest("Username already exists");
            if(!_userService.CheckIfStrongPassword(userRequest)) return BadRequest("Weak password");
            return Ok(await _userService.Register(userRequest));
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto userRequest) 
        {
            if(!_userService.CheckIfUsernameExists(userRequest)) return BadRequest("Username do not exists");
            return Ok(_userService.Login(userRequest));
        }

        [HttpGet("all"), Authorize]
        public ActionResult<List<User>> GetUsers() 
        {
            return Ok(_userService.GetUsers());
        }
    }
}