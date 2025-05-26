using Domain.DTO;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi29.Services.IServices
{
	public interface IUsuarioServices
	{
		public Task<Response<List<Usuario>>> ObtenerUsuarios();
		public Task<Response<Usuario>> ById(int id);
		Task<Response<Usuario>> Crear(UsuarioRequest usuario);
	}
}
