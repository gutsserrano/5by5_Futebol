using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutebolApp
{
    internal class Campeonato
    {
        public string Nome_camp { get; set; }
        public string Temporada { get; set; }
        public List<Equipe> Equipes { get; set; }
        public List<Estatistica> Tabela { get; set; }
        public readonly string Status_camp;

        public Campeonato(string nome_camp, string temporada)
        {
            this.Nome_camp = nome_camp;
            this.Temporada = temporada;
            Equipes = new();
            Tabela = new();
            Status_camp = "NAO INICIADO";
        }
    }
}
