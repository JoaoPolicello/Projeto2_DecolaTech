using ApiCatalogoFilmes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoFilmes.Repositorios
{
    public class FilmeRepositorio : IFilmeRepositorio
    {
        private static Dictionary<Guid, Filme> filmes = new Dictionary<Guid, Filme>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Filme{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Nome = "Black Panther", Diretor = "Ryan Coogler", Ano = 2018, Genero = "Ação", NmrAvaliacoes = 525, Avaliacao = 9.6, Resenha = "https://universohq.com/filmes/resenha-pantera-negra-celebra-10-anos-da-marvel-studios-com-ousadia/"} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Filme{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Nome = "Citizen Kane", Diretor = "Orson Welles", Ano = 1941, Genero = "Drama", NmrAvaliacoes = 117, Avaliacao = 9.9, Resenha = "https://www.omelete.com.br/filmes/icidadao-kanei"} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Filme{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Nome = "Parasite", Diretor = "Bong Joon-ho", Ano = 2019, Genero = "Suspense", NmrAvaliacoes = 463, Avaliacao = 9.8, Resenha = "https://estacaonerd.com/critica-parasita-parasite/"} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Filme{ Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Nome = "Us", Diretor = "Jordan Peele", Ano = 2019, Genero = "Horror", NmrAvaliacoes = 552, Avaliacao = 9.3, Resenha = "https://www.culturagenial.com/nos-explicacao-analise-filme/"} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Filme{ Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Nome = "Get Out", Diretor = "Jordan Peele", Ano = 2017, Genero = "Mistério", NmrAvaliacoes = 397, Avaliacao = 9.8, Resenha = "https://medium.com/lobanocovil/resenha-corra-get-out-61c85cbac682"} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Filme{ Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Nome = "O Poderoso Chefão", Diretor = "Francis Ford Coppola", Ano = 1972, Genero = "Crime", NmrAvaliacoes = 133, Avaliacao = 9.7, Resenha = "https://www.papodecinema.com.br/filmes/o-poderoso-chefao/"} }
        };
        public Task<List<Filme>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(filmes.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }
        public Task<Filme> Obter(Guid id)
        {
            if (!filmes.ContainsKey(id))
                return Task.FromResult<Filme>(null);

            return Task.FromResult(filmes[id]);
        }
        public Task<List<Filme>> Obter(string nome, string diretor)
        {
            return Task.FromResult(filmes.Values.Where(filme => filme.Nome.Equals(nome) && filme.Diretor.Equals(diretor)).ToList());
        }
        public Task Inserir(Filme filme)
        {
            filmes.Add(filme.Id, filme);
            return Task.CompletedTask;
        }
        public Task Atualizar(Filme filme)
        {
            filmes[filme.Id] = filme;
            return Task.CompletedTask;
        }
        public Task Avaliar(Guid id, double avaliacao)
        {
            filmes[id].Avaliacao = avaliacao;
            filmes[id].NmrAvaliacoes++;
            return Task.CompletedTask;
        }
        public Task Remover(Guid id)
        {
            filmes.Remove(id);
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            //Fechar conexão com o banco
        }
    }
}