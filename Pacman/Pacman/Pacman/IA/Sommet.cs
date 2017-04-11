using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman.IA
{
    class Sommet
    {
        public const int INFINI = 1000000;
        public int Potentiel;
        public Coord Pred;
        public bool Marque;
        public Coord Suivant;

        public Sommet()
        {
            Potentiel = INFINI;
            Marque = false;
            Pred = null;
        }
    }
}
