using Application.Command.User;
using Application.Handler.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestCourseApp.Controllers.Auth
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly LoggingUserHandler _logging;
        private readonly RegisterUserHandler _registerUserHandler;

        public AuthController(LoggingUserHandler logging, RegisterUserHandler registerUserHandler)
        {
            _logging = logging;
            _registerUserHandler = registerUserHandler;
        }
       
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var result = await _registerUserHandler.Handle(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoggingUserCommand command)
        {
            var result = await _logging.Handle(command);

            if (!result.IsSuccess)
            {
                return Unauthorized(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
