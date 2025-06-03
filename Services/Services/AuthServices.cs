using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using WebApi29.Context;
using WebApi29.Services.IServices;

namespace WebApi29.Services.Services
{
	public class AuthServices : IAuthServices
	{
		private readonly ApplicationDbContext _context;
		private readonly IConfiguration _config;

		public AuthServices(ApplicationDbContext context, IConfiguration config)
		{
			_context = context;
			_config = config;
		}

		public async Task<string> Login(LoginRequest request)
		{
			var user = await _context.Usuarios
				.Include(u => u.Roles)
				.FirstOrDefaultAsync(u => u.UserName == request.UserName && u.Password == request.Password);
			if (user == null)
			{
				throw new UnauthorizedAccessException("Credenciales incorrectas");
			}
			// Generate JWT token logic here
			var token = GenerateJwtToken(user);
			return token;
		}

		private string GenerateJwtToken(Domain.Entities.Usuario user)
		{
			var claims = new[]
			{
				new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.UserName),
				new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, user.Roles?.Nombre ?? "User")
			};

			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],
				audience: _config["Jwt:Audience"],
				claims: claims,
				expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:DurationInMinutes"])),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

	}
}
