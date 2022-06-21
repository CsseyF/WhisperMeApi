using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhisperMe.Services.Interfaces;
using WhisperMe.ViewModels.Dtos;
using WhisperMe.ViewModels.Requests;
using WhisperMe.ViewModels.Responses;

namespace WhisperMe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WhisperController : Controller
    {
        private readonly IWhisperService _whisperService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WhisperController(IWhisperService whisperService, IHttpContextAccessor httpContextAccessor)
        {
            _whisperService = whisperService;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        [HttpPost("SendWhisper")]
        public async Task<IActionResult> SendWhisper([FromBody] WhisperDTO request)
        {
            try
            {
                await _whisperService.SendWhisper(request);
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

        [HttpDelete("RemoveWhisper")]
        public async Task<IActionResult> RemoveWhisper([FromBody] RemoveWhisperRequest request)
        {
            try
            {
                await _whisperService.RemoveWhisper(request.Guid);
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

        [HttpGet("ListWhispers")]
        public async Task<IActionResult> ListWhispers()
        {
            try
            {
                var list = await _whisperService.ListWhispers(_httpContextAccessor.HttpContext.Request.Cookies.FirstOrDefault((jwt) => jwt.Key == "token").Value);
                return Ok(list);
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

    }


}
