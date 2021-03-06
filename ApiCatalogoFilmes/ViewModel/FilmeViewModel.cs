using System;

namespace ApiCatalogoFilmes.ViewModel
{
    public class FilmeViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Diretor { get; set; }
        public int Ano { get; set; }
        public string Genero { get; set; }
        public int NmrAvaliacoes { get; set; }
        public double Avaliacao { get; set; }
        public string Resenha { get; set; }
    }
}