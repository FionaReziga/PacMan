using Pacman.IA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman.Metier
{
    class Pac
    {
        private Coord _pos;
        private ObjetAnime _objAnime;
        private string _direction;

        public Coord Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }

        public ObjetAnime ObjAnime
        {
            get { return _objAnime; }
            set { _objAnime = value; }
        }

        public string Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public Pac(Coord pos, ObjetAnime objAnime, string direction)
        {
            _pos = pos;
            _objAnime = objAnime;
            _direction = direction;
        }
        public Pac(Coord pos, string direction)
        {
            _pos = pos;
            _direction = direction;
        }

    }
}
