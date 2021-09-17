using System;

namespace ApiCatalogoFilmes.Exceptions
{
    public class FilmeJaCadastradoException : Exception
    {
        public FilmeJaCadastradoException()
            : base("Este já filme está cadastrado")
        { }
    }
}