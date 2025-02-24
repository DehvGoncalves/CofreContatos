using System.ComponentModel.DataAnnotations;

namespace MeuSiteEmMVC.Dto.Contato
{
    public class ContatoEdicaoDto
    {
        public int id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        public string Email { get; set; }
    }
}