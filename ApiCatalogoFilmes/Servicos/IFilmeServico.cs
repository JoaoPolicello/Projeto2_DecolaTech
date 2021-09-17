using ApiCatalogoFilmes.InputModel;
using ApiCatalogoFilmes.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCatalogoFilmes.Servicos
{
    public interface IFilmeServico : IDisposable
    {
        Task<List<FilmeViewModel>> Obter(int pagina, int quantidade);
        Task<FilmeViewModel> Obter(Guid id);
        Task<FilmeViewModel> Inserir(FilmeInputModel filme);
        Task Atualizar(Guid id, FilmeInputModel filme);
        Task Avaliar(Guid id, int avaliacao);
        Task Remover(Guid id);
    }
}