using System.Data.SqlClient;
using Dapper;
namespace SistemaLogin.Commands
{
    public class Conexao
    {
        public static void Conectar()
        {
            const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=2NNje*mG*!FoS5;TrustServerCertificate=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

            }
        }
    }
}