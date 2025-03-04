using System.ComponentModel.DataAnnotations;

namespace MeuSiteEmMVC.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo telefone é obrigatório")]
        [Phone(ErrorMessage = "Digite um telefone válido")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "O campo email é obrigatório")]
        [EmailAddress(ErrorMessage = "Digite um email válido")]
        public string Email { get; set; }
    }
}
