using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using WebApi29.Services.IServices;

namespace WebApi29.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthController : Controller
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest request)
		{
			var token = await _authService.Login(request);
			return Ok(new { token });
		}
	}
}
