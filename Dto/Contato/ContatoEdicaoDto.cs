using System.ComponentModel.DataAnnotations;

namespace MeuSiteEmMVC.Dto.Contato
{
    public class ContatoEdicaoDto
    {
        public int id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo telefone é obrigatório")]
        [RegularExpression(@"^\(?\d{2}\)?[\s-]?\d{4,5}-?\d{4}$", ErrorMessage = "Digite um telefone válido no formato (XX) XXXXX-XXXX ou (XX) XXXX-XXXX")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo email é obrigatório")]
        [EmailAddress(ErrorMessage = "Digite um email válido")]
        public string Email { get; set; }
    }
}