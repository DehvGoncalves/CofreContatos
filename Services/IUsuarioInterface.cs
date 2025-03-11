using MeuSiteEmMVC.Dto.Contato;
using MeuSiteEmMVC.Models;

public interface IUsuarioInterface
{
    Task<UsuarioModel> CriarUsuario(UsuarioModel usuario);
    Task<UsuarioModel> BuscarUsuarioPorId(int? id);
    Task<UsuarioModel> CancelarUsuario(int? id);
    Task<UsuarioModel> EditarUsuario(UsuarioModel usuario);
}
