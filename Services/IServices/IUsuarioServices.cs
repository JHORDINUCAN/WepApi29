using Domain.DTO;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi29.Services.IServices
{
	public interface IUsuarioServices
	{
		//Read todos los usuarios
		public Task<Response<List<Usuario>>> ObtenerUsuarios();

		//Read for Id de usuario
		public Task<Response<Usuario>> ById(int id);

		//Create users
		public Task<Response<Usuario>> Crear(UsuarioRequest usuario);

		//Update users
		public Task<Response<Usuario>> Actualizar(int id, UsuarioRequest request);

		//Delete users
		public Task<Response<string>> Eliminar(int id);
	}
}
