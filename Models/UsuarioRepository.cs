using System;
using Microsoft.AspNetCore.Http;
using MySqlConnector;

namespace AT2.Models
{
    public class UsuarioRepository
    {
        private const string DadosConexao = "Database=teste;Data Source=localhost;User Id=root;";
        public void Insert(Usuarios novoUsuario)
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);
            conexao.Open();
            string sql = "INSERT INTO usuarios(nome, login, senha, dataNas) VALUES (@nome, @login, @senha, @dataNas)";
            MySqlCommand comando = new MySqlCommand(sql, conexao);
            comando.Parameters.AddWithValue("@nome", novoUsuario.nome);
            comando.Parameters.AddWithValue("@login", novoUsuario.login);
            comando.Parameters.AddWithValue("@senha", novoUsuario.senha);
            comando.Parameters.AddWithValue("@dataNas", novoUsuario.dataNasc.ToString("yyyy-MM-dd"));
            comando.ExecuteNonQuery();
            conexao.Close();
        }
        public Usuarios ValidarLogin(Usuarios u)
        {

            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

            string Query = "SELECT * FROM usuarios WHERE login = @login AND senha = @senha;";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);

            Comando.Parameters.AddWithValue("@login", u.login);
            Comando.Parameters.AddWithValue("@senha", u.senha);

            MySqlDataReader Reader = Comando.ExecuteReader();

            Usuarios user = new Usuarios();
            user.id = 0;
            while (Reader.Read())
            {
                user.id = Reader.GetInt32("id");


                if (!Reader.IsDBNull(Reader.GetOrdinal("nome")))
                {
                    user.nome = Reader.GetString("nome");
                }
                if (!Reader.IsDBNull(Reader.GetOrdinal("login")))
                {
                    user.login = Reader.GetString("login");
                }
                if (!Reader.IsDBNull(Reader.GetOrdinal("senha")))
                {
                    user.senha = Reader.GetString("senha");
                }
            }

            Conexao.Close();

            return user;
        }
    }
}