using MeuSiteEmMVC.Data;
using MeuSiteEmMVC.Dto.Contato;
using MeuSiteEmMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace MeuSiteEmMVC.Services
{
    public class ContatoService : IContatoInterface
    {
        //injeção de dependência do DbContext para manipular o banco de dados
        private readonly AppDbContext _context;

        public ContatoService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ContatoModel> CriarContato(ContatoCriacaoDto contatoCriacaoDto)
        {
            try
            {
                var contato = new ContatoModel
                {
                    Nome = contatoCriacaoDto.Nome,
                    Telefone = contatoCriacaoDto.Telefone,
                    Email = contatoCriacaoDto.Email
                };

                _context.Add(contato);
                await _context.SaveChangesAsync();
                return contato;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar contato", ex);
            }
        }



        public async Task<ContatoEdicaoDto> BuscarContatoPorId(int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var contato = await _context.Contatos.FirstOrDefaultAsync(x => x.Id == id);

                    if (contato == null)
                    {
                        return null;
                    }

                    // Mapeia ContatoModel para ContatoEdicaoDto
                    var contatoEdicaoDto = new ContatoEdicaoDto
                    {
                        id = contato.Id,
                        Nome = contato.Nome,
                        Email = contato.Email,
                        Telefone = contato.Telefone
                    };

                    return contatoEdicaoDto;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar contato", ex);
            }
        }

        public async Task<ContatoModel> ExcluirContato(int? id)
        {
            try
            {
                var contatoExcluir = await _context.Contatos.FirstOrDefaultAsync(x => x.Id == id);
                if (id != null)
                {
                    //Remore o contato 
                    _context.Contatos.Remove(contatoExcluir);
                    await _context.SaveChangesAsync();
                }
                return contatoExcluir;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir contato", ex);
            }
        }

        public async Task<ContatoModel> EditarContato(ContatoEdicaoDto contato)
        {
            if (contato == null)
            {
                throw new ArgumentNullException(nameof(contato), "O contato não pode ser nulo.");
            }

            try
            {
                // Busca o contato no banco de dados
                var contatoModel = await _context.Contatos.FirstOrDefaultAsync(x => x.Id == contato.id);

                if (contatoModel == null)
                {
                    throw new Exception("Contato não encontrado.");
                }

                // Atualiza as propriedades do contato rastreado
                contatoModel.Nome = contato.Nome;
                contatoModel.Telefone = contato.Telefone;
                contatoModel.Email = contato.Email;

                // Salva as alterações no banco de dados
                _context.Contatos.Update(contatoModel);
                await _context.SaveChangesAsync();

                return contatoModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao editar contato", ex);
            }
        }
    }
}
