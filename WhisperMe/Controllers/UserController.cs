using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhisperMe.Helpers;
using WhisperMe.Services.Interfaces;
using WhisperMe.ViewModels.Dtos;
using WhisperMe.ViewModels.Responses;

namespace WhisperMe.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IJwtUtils _jwtUtils;
        private readonly IUserService _userService;



        public UserController(IUserService userService, IConfiguration configuration, IHttpContextAccessor htppContextAccessor)
        {
            _userService = userService;
            _configuration = configuration;
            _httpContextAccessor = htppContextAccessor;
            _jwtUtils = new JwtUtils(configuration);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDTO request)
        {
            try
            {
                await _userService.Register(request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    ErrorKey = ex.Message,
                }
                );
            }

        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserDTO request)
        {
            try
            {
                await _userService.Login(request);
                _httpContextAccessor.HttpContext = CreateToken(_httpContextAccessor.HttpContext, request);
                var jwt = _jwtUtils.GenerateJwtToken(request.UserName);

                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    ErrorKey = ex.Message,
                }
);
            }
        }




        private HttpContext CreateToken(HttpContext context, UserDTO user)
        {
            var jwt = _jwtUtils.GenerateJwtToken(user.UserName);

            var httpOnlyOptions = new CookieOptions
            {
                HttpOnly = true
            };

            context.Response.Cookies.Append(
                "token",
                jwt,
                httpOnlyOptions
            );

            return context;
        }

    }
}
