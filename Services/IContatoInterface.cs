using MeuSiteEmMVC.Dto.Contato;
using MeuSiteEmMVC.Models;

public interface IContatoInterface
{
    Task<ContatoModel> CriarContato(ContatoCriacaoDto contatoCriacaoDto);
    Task<ContatoEdicaoDto> BuscarContatoPorId(int? id);
    Task<ContatoModel> ExcluirContato(int? id);
    Task<ContatoModel> EditarContato(ContatoEdicaoDto contato);
}
