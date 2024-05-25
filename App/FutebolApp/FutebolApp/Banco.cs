using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutebolApp
{
    internal class Banco
    {
        readonly string Conexao = "Data Source=127.0.0.1; Initial Catalog=DBFutebol; User Id=sa; Password=SqlServer2019!; TrustServerCertificate=Yes";

        public Banco()
        {

        }
        public string Caminho()
        {
            return Conexao;
        }
    }
}

