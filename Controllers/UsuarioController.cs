using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi29.Services.IServices;

namespace WebApi29.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class UsuarioController : ControllerBase
	{
		private readonly IUsuarioServices _usuarioServices;

		// Constructor con inyección del servicio de usuarios
		public UsuarioController(IUsuarioServices usuarioServices)
		{
			_usuarioServices = usuarioServices;
		}

		// Obtener todos los usuarios
		[HttpGet]
		public async Task<IActionResult> GetUsers()
		{
			var response = await _usuarioServices.ObtenerUsuarios();
			return Ok(response);
		}

		// Obtener usuario por ID
		[HttpGet("id")]
		public async Task<IActionResult> GetUser(int id)
		{
			return Ok(await _usuarioServices.ById(id));
		}

		// Crear nuevo usuario
		[HttpPost]
		public async Task<IActionResult> PostUser(UsuarioRequest request)
		{
			var response = await _usuarioServices.Crear(request);
			return Ok(response);
		}

		// Actualizar usuario
		//Otra forma de seleccionar el id es [("{id}")]
		[HttpPut("{id:int}")]
		public async Task<IActionResult> PutUser(int id, UsuarioRequest request)
		{
			var response = await _usuarioServices.Actualizar(id, request);
			return Ok(response);
		}

		// Eliminar usuario
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			var response = await _usuarioServices.Eliminar(id);
			return Ok(response);
		}


	}
}
