using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using MySqlConnector;

namespace AT2.Models
{
    public class PackageRepository
    {
        private const string DadosConexao = "Database=teste;Data Source=localhost;User Id=root;";
        public void InsertPackage(Packege novoPacote)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

            string Query = "INSERT INTO pacotesturisticos(nome, origem, destino, atrativos, saida, retorno, usuario) VALUES (@nome, @origem, @destino, @atrativos, @saida, @retorno, @usuario);";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);

            Comando.Parameters.AddWithValue("@nome", novoPacote.nome);
            Comando.Parameters.AddWithValue("@origem", novoPacote.origem);
            Comando.Parameters.AddWithValue("@destino", novoPacote.destino);
            Comando.Parameters.AddWithValue("@atrativos", novoPacote.atrativos);
            Comando.Parameters.AddWithValue("@saida", novoPacote.saida);
            Comando.Parameters.AddWithValue("@retorno", novoPacote.retorno);
            Comando.Parameters.AddWithValue("@usuario", novoPacote.usuario);


            Comando.ExecuteNonQuery();

            Conexao.Close();
        }
        public List<Packege> Listar()
        {
            List<Packege> lista = new List<Packege>();

            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

            string Query = "SELECT * FROM pacotesturisticos;";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);

            MySqlDataReader Reader = Comando.ExecuteReader();

            while (Reader.Read())   
            {
                Packege pkg = new Packege();

                pkg.id = Reader.GetInt32("id");

                if (!Reader.IsDBNull(Reader.GetOrdinal("nome")))
                {
                    pkg.nome = Reader.GetString("nome");
                }
                if (!Reader.IsDBNull(Reader.GetOrdinal("origem")))
                {
                    pkg.origem = Reader.GetString("origem");
                }
                if (!Reader.IsDBNull(Reader.GetOrdinal("destino")))
                {
                    pkg.destino = Reader.GetString("destino");
                }
                if (!Reader.IsDBNull(Reader.GetOrdinal("atrativos")))
                {
                    pkg.atrativos = Reader.GetString("atrativos");
                }
                if (!Reader.IsDBNull(Reader.GetOrdinal("saida")))
                {
                    pkg.saida = Reader.GetDateTime("saida");
                }
                if (!Reader.IsDBNull(Reader.GetOrdinal("retorno")))
                {
                    pkg.retorno = Reader.GetDateTime("retorno");
                }
                lista.Add(pkg);
            }

            Conexao.Close();

            return lista;
        }

        public void Editar(Packege pacTur)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

            string Query = "UPDATE pacotesturisticos SET nome=@nome, origem=@origem, destino=@destino, atrativos=@atrativos, saida=@saida, retorno=@retorno, usuario=@usuario WHERE id=@id";

            MySqlCommand Comando = new MySqlCommand(Query, Conexao);

            Comando.Parameters.AddWithValue("@nome", pacTur.nome);
            Comando.Parameters.AddWithValue("@origem", pacTur.origem);
            Comando.Parameters.AddWithValue("@destino", pacTur.destino);
            Comando.Parameters.AddWithValue("@atrativos", pacTur.atrativos);
            Comando.Parameters.AddWithValue("@saida", pacTur.saida.ToString("yyyy-MM-dd"));
            Comando.Parameters.AddWithValue("@retorno", pacTur.retorno.ToString("yyyy-MM-dd"));
            Comando.Parameters.AddWithValue("@usuario", pacTur.usuario);
            Comando.Parameters.AddWithValue("@id", pacTur.id);

            Comando.ExecuteNonQuery();
            Conexao.Close();
        }

        public void Deletar(int id)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

            string Query = "DELETE FROM pacotesturisticos WHERE id=@id";

            MySqlCommand Comando = new MySqlCommand(Query, Conexao);

            Comando.Parameters.AddWithValue("@id", id);

            Comando.ExecuteNonQuery();
            Conexao.Close();
        }

        public Packege BuscarPorId(int id)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

            string Query = "SELECT * FROM pacotesturisticos WHERE id=@id;";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);

            Comando.Parameters.AddWithValue("@id", id);

            MySqlDataReader Reader = Comando.ExecuteReader();

            Packege pacTur = new Packege();

            while (Reader.Read())
            {
                pacTur.id = Reader.GetInt32("id");

                if (!Reader.IsDBNull(Reader.GetOrdinal("nome")))
                {
                    pacTur.nome = Reader.GetString("nome");
                }
                if (!Reader.IsDBNull(Reader.GetOrdinal("origem")))
                {
                    pacTur.origem = Reader.GetString("origem");
                }
                if (!Reader.IsDBNull(Reader.GetOrdinal("destino")))
                {
                    pacTur.destino = Reader.GetString("destino");
                }
                if (!Reader.IsDBNull(Reader.GetOrdinal("atrativos")))
                {
                    pacTur.atrativos = Reader.GetString("atrativos");
                }
                if (!Reader.IsDBNull(Reader.GetOrdinal("saida")))
                {
                    pacTur.saida = Reader.GetDateTime("saida");
                }
                if (!Reader.IsDBNull(Reader.GetOrdinal("retorno")))
                {
                    pacTur.retorno = Reader.GetDateTime("retorno");
                }
                if (!Reader.IsDBNull(Reader.GetOrdinal("usuario")))
                {
                    pacTur.usuario = Reader.GetInt32("usuario");
                }
            }

            Conexao.Close();
            return pacTur;
        }


    }
}