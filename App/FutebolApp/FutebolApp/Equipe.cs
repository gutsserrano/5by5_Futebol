using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutebolApp
{
    internal class Equipe
    {
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public DateOnly DataCriacao { get; set; }

        public Equipe(string nome, string apelido, DateOnly dataCriacao)
        {
            Nome = nome;
            Apelido = apelido;
            DataCriacao = dataCriacao;
        }
    }
}
