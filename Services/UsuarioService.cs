using MeuSiteEmMVC.Data;
using MeuSiteEmMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace MeuSiteEmMVC.Services
{
    public class UsuarioService : IUsuarioInterface
    {
        // Injeção de dependência do DbContext para manipular o banco de dados
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioModel> BuscarUsuarioPorId(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id), "ID não pode ser nulo.");

            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
                return usuario ?? throw new Exception("Usuário não encontrado.");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar usuário", ex);
            }
        }

        public async Task<UsuarioModel> CriarUsuario(UsuarioModel usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario), "Usuário não pode ser nulo.");

            try
            {
                usuario.DataCadastro = DateTime.Now;
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar usuário", ex);
            }
        }

        public async Task<UsuarioModel> EditarUsuario(UsuarioModel usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario), "Usuário não pode ser nulo.");

            try
            {
                usuario.DataAlteracao = DateTime.Now;
                _context.Update(usuario);
                await _context.SaveChangesAsync();
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao editar usuário", ex);
            }
        }

        public async Task<UsuarioModel> CancelarUsuario(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id), "ID não pode ser nulo.");

            var usuario = await BuscarUsuarioPorId(id);

            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            try
            {
                usuario.DataAlteracao = DateTime.Now;
                usuario.Status = 0;
                _context.Update(usuario);
                await _context.SaveChangesAsync();
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cancelar usuário", ex);
            }
        }

    }
}
