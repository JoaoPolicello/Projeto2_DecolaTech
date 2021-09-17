using System.ComponentModel.DataAnnotations;

namespace ApiCatalogoFilmes.InputModel
{
    public class FilmeInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do filme deve conter entre 3 e 100 caracteres.")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do diretor deve conter entre 3 e 100 caracteres.")]
        public string Diretor { get; set; }
        [Required]
        [Range(1878, 2021, ErrorMessage = "O ano deve ser entre 1878 e 2021.")]
        public int Ano { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do gênero deve conter entre 3 e 100 caracteres.")]
        public string Genero { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "O número de avaliações deve ser entre 1 e 1000.")]
        public int NmrAvaliacoes { get; set; }
        [Required]
        [Range(0, 10, ErrorMessage = "A avaliação deve ser entre 0 e 10.")]
        public double Avaliacao { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "A resenha deve ter entre 3 e 1000 caracteres.")]
        public string Resenha { get; set; }
    }
}