using MeuSiteEmMVC.Data;
using MeuSiteEmMVC.Dto.Contato;
using MeuSiteEmMVC.Enums;
using MeuSiteEmMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeuSiteEmMVC.Controllers
{
    public class UsuarioController : Controller
    {
        // Injeção de dependência para o contexto do banco de dados
        private readonly AppDbContext _context;
        private readonly IUsuarioInterface _usuario;
        public UsuarioController(AppDbContext context, IUsuarioInterface usuario)
        {
            _context = context;
            _usuario = usuario;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return View(usuarios);
        }
        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Criar(UsuarioModel usuario)
        {
            if (!ModelState.IsValid)
            {
                TempData["MensagemErro"] = "Erro ao criar contato!";
                return View(usuario);
            }
            else
            {
                usuario.Nome = usuario.Nome;
                usuario.Login = usuario.Login;
                usuario.Senha = usuario.Senha;
                usuario.Status = usuario.Status;
                usuario.Email = usuario.Email;
                usuario.Perfil = usuario.Perfil;
            }

                usuario.DataCadastro = DateTime.Now;
                usuario.Status = StatusEnum.Ativo;
            {
                await _usuario.CriarUsuario(usuario);
                TempData["MensagemSucesso"] = "Contato criado com sucesso!";
                return RedirectToAction("Index");
            }
        }
    }
}
