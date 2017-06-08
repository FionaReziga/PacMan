using Pacman.IA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman.Métier
{
    class Fantome
    {
        private Coord _pos;
        private int _numero;
        private ObjetAnime _objAnime;
        private string _direction;

        public Coord Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }

        public int Numero
        {
            get { return _numero; }
            set { _numero = value; }
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

        public Fantome(Coord pos, int numero, ObjetAnime objAnime, string direction)
        {
            this._pos = pos;
            this._numero = numero;
            this._objAnime = objAnime;
            this._direction = direction;
        }

        public Fantome(Coord pos, int numero, string direction)
        {
            this._pos = pos;
            this._numero = numero;
            this._direction = direction;
        }

    }
}
