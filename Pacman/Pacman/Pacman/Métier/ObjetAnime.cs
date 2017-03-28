using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;   //   for Texture2D
using Microsoft.Xna.Framework;  //  for Vector2

namespace Pacman.Métier
{
    class ObjetAnime
    {
        private Texture2D _texture;    //  sprite texture 

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }
        private Vector2 _position;     //  sprite position on screen

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        private Vector2 _size;         //  sprite size in pixels

        public Vector2 Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public ObjetAnime(Texture2D texture, Vector2 position, Vector2 size)
        {
            this._texture = texture;
            this._position = position;
            this._size = size;
        }
    }
}
