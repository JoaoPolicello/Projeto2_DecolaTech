using ApiCatalogoFilmes.Entidades;
using ApiCatalogoFilmes.Exceptions;
using ApiCatalogoFilmes.InputModel;
using ApiCatalogoFilmes.Repositorios;
using ApiCatalogoFilmes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoFilmes.Servicos
{
    public class FilmeServico : IFilmeServico
    {
        private readonly IFilmeRepositorio _filmeRepositorio;

        public FilmeServico(IFilmeRepositorio filmeRepositorio)
        {
            _filmeRepositorio = filmeRepositorio;
        }
        public async Task<List<FilmeViewModel>> Obter(int pagina, int quantidade)
        {
            var filmes = await _filmeRepositorio.Obter(pagina, quantidade);

            return filmes.Select(filme => new FilmeViewModel
            {
                Id = filme.Id,
                Nome = filme.Nome,
                Diretor = filme.Diretor,
                Ano = filme.Ano,
                Genero = filme.Genero,
                NmrAvaliacoes = filme.NmrAvaliacoes,
                Avaliacao = filme.Avaliacao,
                Resenha = filme.Resenha
            }).ToList();
        }
        public async Task<FilmeViewModel> Obter(Guid id)
        {
            var filme = await _filmeRepositorio.Obter(id);

            if (filme == null)
                return null;

            return new FilmeViewModel
            {
                Id = filme.Id,
                Nome = filme.Nome,
                Diretor = filme.Diretor,
                Ano = filme.Ano,
                Genero = filme.Genero,
                NmrAvaliacoes = filme.NmrAvaliacoes,
                Avaliacao = filme.Avaliacao,
                Resenha = filme.Resenha
            };
        }
        public async Task<FilmeViewModel> Inserir(FilmeInputModel filme)
        {
            var entidadeFilme = await _filmeRepositorio.Obter(filme.Nome, filme.Diretor);

            if (entidadeFilme.Count > 0)
                throw new FilmeJaCadastradoException();

            var filmeInsert = new Filme
            {
                Id = Guid.NewGuid(),
                Nome = filme.Nome,
                Diretor = filme.Diretor,
                Ano = filme.Ano,
                Genero = filme.Genero,
                NmrAvaliacoes = filme.NmrAvaliacoes,
                Avaliacao = filme.Avaliacao,
                Resenha = filme.Resenha
            };

            await _filmeRepositorio.Inserir(filmeInsert);

            return new FilmeViewModel
            {
                Id = filmeInsert.Id,
                Nome = filme.Nome,
                Diretor = filme.Diretor,
                Ano = filme.Ano,
                Genero = filme.Genero,
                NmrAvaliacoes = filme.NmrAvaliacoes,
                Avaliacao = filme.Avaliacao,
                Resenha = filme.Resenha
            };
        }
        public async Task Atualizar(Guid id, FilmeInputModel filme)
        {
            var entidadeFilme = await _filmeRepositorio.Obter(id);

            if (entidadeFilme == null)
                throw new FilmeNaoCadastradoException();

            entidadeFilme.Nome = filme.Nome;
            entidadeFilme.Nome = filme.Nome;
            entidadeFilme.Diretor = filme.Diretor;
            entidadeFilme.Ano = filme.Ano;
            entidadeFilme.Genero = filme.Genero;
            entidadeFilme.NmrAvaliacoes = filme.NmrAvaliacoes;
            entidadeFilme.Avaliacao = filme.Avaliacao;
            entidadeFilme.Resenha = filme.Resenha;

            await _filmeRepositorio.Atualizar(entidadeFilme);
        }
        public async Task Avaliar(Guid id, int avaliacao)
        {
            var entidadeFilme = await _filmeRepositorio.Obter(id);

            if (entidadeFilme == null)
                throw new FilmeNaoCadastradoException();

            entidadeFilme.Avaliacao = Math.Round((entidadeFilme.Avaliacao * entidadeFilme.NmrAvaliacoes + avaliacao) / (entidadeFilme.NmrAvaliacoes + 1), 1);

            await _filmeRepositorio.Avaliar(id, entidadeFilme.Avaliacao);
        }
        public async Task Remover(Guid id)
        {
            var filme = await _filmeRepositorio.Obter(id);

            if (filme == null)
                throw new FilmeNaoCadastradoException();

            await _filmeRepositorio.Remover(id);
        }
        public void Dispose()
        {
            _filmeRepositorio?.Dispose();
        }
    }
}