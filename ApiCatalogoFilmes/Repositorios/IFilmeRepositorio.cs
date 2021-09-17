using ApiCatalogoFilmes.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCatalogoFilmes.Repositorios
{
    public interface IFilmeRepositorio : IDisposable
    {
        Task<List<Filme>> Obter(int pagina, int quantidade);
        Task<Filme> Obter(Guid id);
        Task<List<Filme>> Obter(string nome, string diretor);
        Task Inserir(Filme filme);
        Task Atualizar(Filme filme);
        Task Avaliar(Guid id, double avaliacao);
        Task Remover(Guid id);
    }
}