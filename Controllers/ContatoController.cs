﻿using MeuSiteEmMVC.Data;
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
        public async Task<IActionResult> Criar(ContatoModel contato)
        {
            if (!ModelState.IsValid)
            {
                TempData["MensagemErro"] = "Erro ao criar contato!";
                return View(contato);
            }
            else
            {
                var contatoCriacaoDto = new ContatoCriacaoDto
                {
                    Nome = contato.Nome,
                    Telefone = contato.Telefone,
                    Email = contato.Email
                };

                await _contato.CriarContato(contatoCriacaoDto);
                TempData["MensagemSucesso"] = "Contato criado com sucesso!";
                return RedirectToAction("Index");
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
            // Limpa qualquer mensagem anterior para evitar sobreposição
            TempData.Remove("MensagemSucesso");
            TempData.Remove("MensagemErro");

            if (!ModelState.IsValid)
            {
                TempData["MensagemErro"] = "Erro ao editar contato!";
                return View(contato);
            }

            // Caso a validação passe
            var contatoEditar = await _contato.EditarContato(contato);
            TempData["MensagemSucesso"] = "Contato editado com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ExcluirConfirmacao(int? id)
        {
            var contatoDto = await _contato.BuscarContatoPorId(id);

            if (contatoDto == null)
            {
                TempData["MensagemErro"] = "Contato não encontrado.";
                return RedirectToAction("Index");
            }

            // Convertendo ContatoEdicaoDto para ContatoModel
            var contatoModel = new ContatoModel
            {
                Id = contatoDto.id,
                Nome = contatoDto.Nome,
                Telefone = contatoDto.Telefone,
                Email = contatoDto.Email
            };

            return View(contatoModel); // Passando o ContatoModel para a view
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
