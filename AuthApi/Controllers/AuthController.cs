using AuthApi.Models.DTO;
using AuthApi.Service.Iservice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        readonly private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {
            var result = await _authService.Register(registrationRequestDTO);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "User already exists");
            }
            return Ok(result);
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var loginResponse = await _authService.Login(loginRequestDTO);
            if (loginResponse.User == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Invalid username or password");
            }
            return Ok(loginResponse);
        }


        [HttpPost("AssignRole")]

        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {
            var assignRoleSuuceesfull = await _authService.AssignRole(registrationRequestDTO.Email,registrationRequestDTO.Role);
            if (!assignRoleSuuceesfull)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Role not assigned");
            }
            return Ok(assignRoleSuuceesfull);
        }
    }
}
