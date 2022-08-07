using System.Data.SqlClient;
using Dapper;
namespace SistemaLogin.Commands
{
    public class Conexao
    {
        public static void Conectar()
        {

            StreamReader config = new StreamReader("config.json");
            var banco = JsonConvert.DeserializeObject<Banco>(config.ReadToEnd());
            string connectionString = $"Server={banco.Server},{banco.Port};Database={banco.Database};Uid={banco.Uid};Pwd={banco.Pwd};TrustServerCertificate=true";
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

            }
        }
    }
}