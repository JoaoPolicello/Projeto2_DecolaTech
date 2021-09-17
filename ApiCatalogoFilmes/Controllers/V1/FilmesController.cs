using ApiCatalogoFilmes.Exceptions;
using ApiCatalogoFilmes.InputModel;
using ApiCatalogoFilmes.Servicos;
using ApiCatalogoFilmes.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoFilmes.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private readonly IFilmeServico _filmeServico;
        public FilmesController(IFilmeServico filmeServico)
        {
            _filmeServico = filmeServico;
        }

        /// <summary>
        /// Buscar todos os filmes de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os filmes sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de registros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de filmes</response>
        /// <response code="204">Caso não haja filmes</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmeViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var filmes = await _filmeServico.Obter(pagina, quantidade);

            if (filmes.Count() == 0)
                return NoContent();

            return Ok(filmes);
        }

        /// <summary>
        /// Buscar um filme pelo seu Id
        /// </summary>
        /// <param name="idFilme">Id do filme buscado</param>
        /// <response code="200">Retorna o filme filtrado</response>
        /// <response code="204">Caso não haja filme com este id</response>
        [HttpGet("{idFilme:guid}")]
        public async Task<ActionResult<FilmeViewModel>> Obter([FromRoute] Guid idFilme)
        {
            try
            {
                var filme = await _filmeServico.Obter(idFilme);

                return Ok(filme);
            }
            catch (FilmeNaoCadastradoException)
            {
                return NotFound("Não existe um filme com este id");
            }
        }

        /// <summary>
        /// Inserir um filme no catálogo
        /// </summary>
        /// <param name="filmeInputModel">Dados do filme a ser inserido</param>
        /// <response code="200">Caso o filme seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um filme com mesmo nome e diretor</response>
        [HttpPost]
        public async Task<ActionResult<FilmeViewModel>> InserirFilme([FromBody] FilmeInputModel filmeInputModel)
        {
            try
            {
                var filme = await _filmeServico.Inserir(filmeInputModel);
                return Ok(filme);
            }
            catch (FilmeJaCadastradoException)
            {
                return UnprocessableEntity("Já existe um filme com este nome e diretor");
            }
        }

        /// <summary>
        /// Atualizar um filme no catálogo
        /// </summary>
        /// /// <param name="idFilme">Id do filme a ser atualizado</param>
        /// <param name="filmeInputModel">Novos dados para atualizar o filme indicado</param>
        /// <response code="200">Caso o filme seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um filme com este Id</response>
        [HttpPut("{idFilme:guid}")]
        public async Task<ActionResult> AtualizarFilme([FromRoute] Guid idFilme, [FromBody] FilmeInputModel filmeInputModel)
        {
            try
            {
                await _filmeServico.Atualizar(idFilme, filmeInputModel);
                return Ok();
            }
            catch (FilmeNaoCadastradoException)
            {
                return NotFound("Não existe um filme com este id");
            }
        }

        /// <summary>
        /// Avaliar um filme
        /// </summary>
        /// /// <param name="idFilme">Id do filme a ser avaliado</param>
        /// <param name="avaliacao">Nova avaliação do filme</param>
        /// <response code="200">Caso a avaliação seja atualizada com sucesso</response>
        /// <response code="404">Caso não exista um filme com este Id</response>
        [HttpPatch("{idFilme:guid}/avaliacao/{avaliacao:int}")]
        public async Task<ActionResult> AvaliarFilme([FromRoute] Guid idFilme, [FromRoute] int avaliacao)
        {
            try
            {
                await _filmeServico.Avaliar(idFilme, avaliacao);

                return Ok();
            }
            catch (FilmeNaoCadastradoException)
            {
                return NotFound("Não existe um filme com este id");
            }
        }

        /// <summary>
        /// Excluir um filme
        /// </summary>
        /// /// <param name="idFilme">Id do filme a ser excluído</param>
        /// <response code="200">Caso o filme seja excluido com sucesso</response>
        /// <response code="404">Caso não exista um filme com este Id</response>
        [HttpDelete("{idFilme:guid}")]
        public async Task<ActionResult> ApagarFilme([FromRoute] Guid idFilme)
        {
            try
            {
                await _filmeServico.Remover(idFilme);

                return Ok();
            }
            catch (FilmeNaoCadastradoException)
            {
                return NotFound("Não existe um filme com este id");
            }
        }
    }
}