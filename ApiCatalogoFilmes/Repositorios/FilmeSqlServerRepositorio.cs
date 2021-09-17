using ApiCatalogoFilmes.Entidades;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApiCatalogoFilmes.Repositorios
{
    public class FilmeSqlServerRepository : IFilmeRepositorio
    {
        private readonly SqlConnection sqlConnection;
        public FilmeSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }
        public async Task<List<Filme>> Obter(int pagina, int quantidade)
        {
            var filmes = new List<Filme>();

            var comando = $"select * from Filmes order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                filmes.Add(new Filme
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Diretor = (string)sqlDataReader["Diretor"],
                    Ano = (int)sqlDataReader["Ano"],
                    Genero = (string)sqlDataReader["Genero"],
                    NmrAvaliacoes = (int)sqlDataReader["NmrAvaliacoes"],
                    Avaliacao = (int)sqlDataReader["Avaliacao"]
                });
            }

            await sqlConnection.CloseAsync();

            return filmes;
        }
        public async Task<Filme> Obter(Guid id)
        {
            Filme filme = null;

            var comando = $"select * from Filmes where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                filme = new Filme
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Diretor = (string)sqlDataReader["Diretor"],
                    Ano = (int)sqlDataReader["Ano"],
                    Genero = (string)sqlDataReader["Genero"],
                    NmrAvaliacoes = (int)sqlDataReader["NmrAvaliacoes"],
                    Avaliacao = (int)sqlDataReader["Avaliacao"]
                };
            }

            await sqlConnection.CloseAsync();

            return filme;
        }
        public async Task<List<Filme>> Obter(string nome, string diretor)
        {
            var filmes = new List<Filme>();

            var comando = $"select * from Filmes where Nome = '{nome}' and Diretor = '{diretor}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                filmes.Add(new Filme
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Diretor = (string)sqlDataReader["Diretor"],
                    Ano = (int)sqlDataReader["Ano"],
                    Genero = (string)sqlDataReader["Genero"],
                    NmrAvaliacoes = (int)sqlDataReader["NmrAvaliacoes"],
                    Avaliacao = (int)sqlDataReader["Avaliacao"]
                });
            }

            await sqlConnection.CloseAsync();

            return filmes;
        }
        public async Task Inserir(Filme filme)
        {
            var comando = $"insert Filmes (Id, Nome, Diretor, Ano, Genero, NmrAvaliacoes, Avaliacao, Resenha) values ('{filme.Id}', '{filme.Nome}', '{filme.Diretor}', {filme.Ano}, {filme.Genero}, {filme.NmrAvaliacoes}, {filme.Avaliacao}, {filme.Resenha})";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }
        public async Task Atualizar(Filme filme)
        {
            var comando = $"update Filmes set Nome = '{filme.Nome}', Diretor = '{filme.Diretor}', Ano = '{filme.Ano}', Genero = '{filme.Genero}', NmrAvaliacoes = '{filme.NmrAvaliacoes}', Avaliacao = '{filme.Avaliacao}', Resenha = '{filme.Resenha}' where Id = '{filme.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }
        public async Task Avaliar(Guid id, double avaliacao)
        {
            var comando = $"update Filmes set Avaliacao = '{avaliacao}', NmrAvaliacoes = NmrAvaliacoes++";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }
        public async Task Remover(Guid id)
        {
            var comando = $"delete from Filmes where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }
        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}