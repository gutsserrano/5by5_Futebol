using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutebolApp
{
    internal class Jogo
    {
        public Campeonato Campeonato { get; set;}
        public Equipe Mandante { get; set;}
        public Equipe Visitante { get; set;}
        public int GolsMandante { get; set;}
        public int GolsVisitante { get; set;}

        public Jogo(Campeonato campeonato, Equipe mandante, Equipe visitante, int golsMandante, int golsVisitante)
        {
            Campeonato = campeonato;
            Mandante = mandante;
            Visitante = visitante;
            GolsMandante = golsMandante;
            GolsVisitante = golsVisitante;
        }
    }
}
