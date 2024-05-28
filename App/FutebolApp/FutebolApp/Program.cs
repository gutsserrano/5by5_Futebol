using Microsoft.Data.SqlClient;
using System.Data;

namespace FutebolApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());

            Controller controller = new(conexaosql);

            controller.Executar();
        }

        
    }
}
