using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutebolApp
{
    internal class Estatistica
    {
        public Campeonato Campeonato { get; set; }
        public Equipe Equipe { get; set; }
        public int GolsMarcados { get; set; }
        public int GolsSofridos { get; set; }
        public int Pontuacao {  get; set; }

        public Estatistica(Campeonato campeonato, Equipe equipe, int golsMarcados, int golsSofridos, int pontuacao)
        {
            Campeonato = campeonato;
            Equipe = equipe;
            GolsMarcados = golsMarcados;
            GolsSofridos = golsSofridos;
            Pontuacao = pontuacao;
        }
    }
}
