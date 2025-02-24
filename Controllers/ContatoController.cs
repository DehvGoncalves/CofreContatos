using MeuSiteEmMVC.Data;
using MeuSiteEmMVC.Dto.Contato;
using MeuSiteEmMVC.Models;
using MeuSiteEmMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MeuSiteEmMVC.Controllers
{
    public class ContatoController : Controller
    {
        // Injeção de dependência para o contexto do banco de dados
        private readonly AppDbContext _context;
        private readonly IContatoInterface _contato;
        public ContatoController(AppDbContext context, IContatoInterface contato)
        {
            _context = context;
            _contato = contato;
        }
        public async Task<IActionResult> Index()
        {
            var contatos = await _context.Contatos.ToListAsync();
            return View(contatos);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string nome, string email, string telefone)
        {
            if (ModelState.IsValid)
            {
                var contato = new ContatoModel
                {
                    Nome = nome,
                    Email = email,
                    Telefone = telefone
                };
                _context.Contatos.Add(contato);
                _context.SaveChanges();
                TempData["MensagemSucesso"] = "Contato criado com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["MensagemErro"] = "Erro ao criar contato!";
                return View();
            }

        }
        
        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            var contadoEdit = await _contato.BuscarContatoPorId(id);
            return View(contadoEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ContatoEdicaoDto contato)
        {
            if (ModelState.IsValid)
            {
                var contatoEditar = await _contato.EditarContato(contato);
                TempData["MensagemSucesso"] = "Contato editado com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["MensagemErro"] = "Erro ao editar contato!";
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> ExcluirConfirmacao(int? id)
        {
            var contato = await _contato.BuscarContatoPorId(id);
            return View(contato); // Retorna a view com o contato
        }
        public async Task<IActionResult> Excluir(int? id)
        {
            if (id == null)
            {
                TempData["MensagemErro"] = "ID do contato não fornecido.";
                return RedirectToAction("Index");
            }

            var contatoExcluido = await _contato.ExcluirContato(id);
            TempData["MensagemSucesso"] = $"Contato '{contatoExcluido.Nome}' (ID: {id}) excluído com sucesso!";
            return RedirectToAction("Index");
        }
    }

}
