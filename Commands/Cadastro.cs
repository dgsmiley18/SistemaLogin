using System.Data.SqlClient;
using Dapper;
using Newtonsoft.Json;
using SistemaLogin.Models;

namespace SistemaLogin.Commands
{
    public class Cadastro
    {
        public static void Cadastrando()
        {
            StreamReader config = new StreamReader("config.json");
            var banco = JsonConvert.DeserializeObject<Banco>(config.ReadToEnd());
            string connectionString = $"Server={banco.Server},{banco.Port};Database={banco.Database};Uid={banco.Uid};Pwd={banco.Pwd};TrustServerCertificate=true";

            Console.WriteLine("Digite seu nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite seu email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Digite sua senha: ");
            string senha = Console.ReadLine();

            // Instanciar a conexão
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var clientes = new Clientes();
                clientes.Id = Guid.NewGuid();
                clientes.Nome = nome;
                clientes.Email = email;
                clientes.Senha = senha;

                // Fazer a busca do email para evitar contas duplicadas
                var busca =
                    @"
                SELECT
                    [Email]
                FROM
                    [clientes]
                WHERE
                    [Email] = @email";
                var buscador = connection.Query<Clientes>(busca, new { email = email });

                if (buscador.Count() == 1)
                {
                    Console.WriteLine("Email já cadastrado, por favor fazer login.");
                }
                else if (buscador.Count() == 0)
                {
                    var sql =
                        @"
                        INSERT INTO 
                            [clientes]
                        VALUES (@id, @nome, @email, @senha)";
                    connection.Execute(
                        sql,
                        new
                        {
                            id = clientes.Id,
                            nome = clientes.Nome,
                            email = clientes.Email,
                            senha = clientes.Senha
                        }
                    );
                    Console.WriteLine("Usuario cadastrado com sucesso!");
                }
            }
        }
    }
}
