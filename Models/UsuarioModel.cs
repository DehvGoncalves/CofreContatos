﻿using MeuSiteEmMVC.Enums;

namespace MeuSiteEmMVC.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public PerfilEnum Perfil { get; set; }
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; } //Obrigatório
        public DateTime? DataAlteracao { get; set; } //não obrigatório
        public StatusEnum Status { get; set; }
    }
}
