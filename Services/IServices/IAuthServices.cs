using Domain.DTO;

namespace WebApi29.Services.IServices
{
	public interface IAuthServices
	{
		Task<string> Login(LoginRequest request);
	}
}
