using System.Data.SqlClient;
using Dapper;
using Newtonsoft.Json;
using SistemaLogin.Models;

namespace SistemaLogin.Commands
{
    public class Login
    {
        public static void Logando()
        {
            StreamReader config = new StreamReader("config.json");
            var banco = JsonConvert.DeserializeObject<Banco>(config.ReadToEnd());
            string connectionString = $"Server={banco.Server},{banco.Port};Database={banco.Database};Uid={banco.Uid};Pwd={banco.Pwd};TrustServerCertificate=true";

            Console.WriteLine("Digite seu email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Digite sua senha: ");
            string senha = Console.ReadLine();

            // Instanciar a conex�o
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var clientes = new Clientes();
                clientes.Email = email;
                clientes.Senha = senha;

                // Fazer a busca do email para evitar contas duplicadas
                var busca =
                    @"
                SELECT
                    [Email],
                    [Senha]
                FROM
                    [clientes]
                WHERE
                    [Email] = @email
                AND
                    [Senha] = @senha";
                var buscador = connection.Query<Clientes>(busca, new { email = email, senha = senha });

                if (buscador.Count() == 1)
                {
                    Console.WriteLine("Logado com sucesso!");
                    Console.ReadKey();
                }
                else if (buscador.Count() == 0)
                {
                    Console.WriteLine("Email ou senha incorretos!");
                    Login.Logando();
                }
                else
                {
                    Console.WriteLine("Erro ao logar!");
                }
            }
        }
    }
}
