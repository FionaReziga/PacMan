using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Pacman.Métier;
using Pacman.IA;

namespace Pacman
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ObjetAnime mur;
        ObjetAnime bean;
        Pac pacman;
        Fantome fantome1;
        Fantome fantome2;
        int score;
        String direction, fantome1Direction, fantome2Direction;
        byte[,] map;
        const int VX = 31, VY = 28;
        Sommet[,] mesSommets;
        bool tabSommetRemplis;

        protected Random random;

        String path = "Ressources/Images/";

        public Game1()
        {
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 20.0f);
            random = new Random(GetHashCode());

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            direction = "Droite";
            tabSommetRemplis = false;
            mesSommets = new Sommet[VX, VY];
            pacman = new Pac(new Coord(5,6),"Droite");
            fantome1 = new Fantome(new Coord(14, 13),1,"Haut");
            fantome2 = new Fantome(new Coord(14, 14),2,"Haut");

            map = new byte[VX, VY]{
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 2, 2, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 2, 2, 2, 2, 2, 2, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 2, 2, 4, 5, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 2, 2, 2, 2, 2, 2, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0},
            {0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0},
            {0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},

        };

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //  changing the back buffer size changes the window size (when in windowed mode)
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 660;
            graphics.ApplyChanges();
            // on charge un objet mur 
            mur = new ObjetAnime(Content.Load<Texture2D>(path + "mur2"), new Vector2(0f, 0f), new Vector2(20f, 20f));
            bean = new ObjetAnime(Content.Load<Texture2D>(path + "gros_bean"), new Vector2(0f, 0f), new Vector2(20f, 20f));
            pacman.ObjAnime = new ObjetAnime(Content.Load<Texture2D>(path + "pacmanHaut"), new Vector2(0f, 0f), new Vector2(20f, 20f));
            fantome1.ObjAnime = new ObjetAnime(Content.Load<Texture2D>(path + "fantome_cyan"), new Vector2(0f, 0f), new Vector2(20f, 20f));
            fantome2.ObjAnime = new ObjetAnime(Content.Load<Texture2D>(path + "fantome_orange"), new Vector2(0f, 0f), new Vector2(20f, 20f));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Right))
            {
                direction = "Droite";
            }
            else if (keyboard.IsKeyDown(Keys.Left))
            {
                direction = "Gauche";
            }
            else if (keyboard.IsKeyDown(Keys.Up))
            {
                direction = "Haut";
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                direction = "Bas";
            }

            //ghostcoll(gameTime);
            ghostai2(gameTime);
            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            for (int x = 0; x < VX; x++)
            {
                for (int y = 0; y < VY; y++)
                {
                    switch (map[x, y])
                    {
                        case 0:
                            DessinerTextureMap(x, y, mur.Texture);
                            break;
                        case 1:
                            DessinerTextureMap(x, y, bean.Texture);
                            break;
                        case 3:
                            pacman.Pos.X = x;
                            pacman.Pos.Y = y;
                            DessinerTextureMap(x, y, pacman.ObjAnime.Texture);
                            break;
                        case 4:
                            fantome1.Pos.X = x;
                            fantome1.Pos.Y = y;
                            DessinerTextureMap(x, y, fantome1.ObjAnime.Texture);
                            break;
                        case 5:
                            fantome2.Pos.X = x;
                            fantome2.Pos.Y = y;
                            DessinerTextureMap(x, y, fantome2.ObjAnime.Texture);
                            break;

                    }
                }
            }
            pacman.ObjAnime.Texture = Content.Load<Texture2D>(path + "pacman" + direction);

            VerifierPositionPacman();

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void DessinerTextureMap(int x, int y, Texture2D texture)
        {
            int xpos, ypos;
            xpos = x * 20;
            ypos = y * 20;
            Vector2 pos = new Vector2(ypos, xpos);
            spriteBatch.Draw(texture, pos, Color.White);
            if (texture != mur.Texture && !tabSommetRemplis)
            {
                mesSommets[x, y] = new Sommet();
                tabSommetRemplis = true;
            }

        }

        public void VerifierPositionPacman()
        {
            switch (direction)
            {
                case "Haut":
                    Avancer(pacman.Pos.X - 1, pacman.Pos.Y);
                    break;
                case "Bas":
                    Avancer(pacman.Pos.X + 1, pacman.Pos.Y);
                    break;
                case "Gauche":
                    Avancer(pacman.Pos.X, pacman.Pos.Y - 1);
                    break;
                case "Droite":
                    Avancer(pacman.Pos.X, pacman.Pos.Y + 1);
                    break;
            }
        }

        public void Avancer(int posX, int posY)
        {
            if (map[posX, posY] != 0)
            {
                if (map[posX, posY] == 1)
                    score += 10;
                map[posX, posY] = 3;
                map[pacman.Pos.X, pacman.Pos.Y] = 2;
            }

        }

        // Phase d’initialisation de tous les sommets de notre matrice 

        public Boolean LevelComplete()
        {
            for (int y = 0; y < VY; y++)
            {
                for (int x = 0; x < VX; x++)
                {
                    if (map[y, x] == 1)
                        return false;
                }

            }
            return true;
        }



        public void ghostai(GameTime gameTime)
        {

            int r1 = random.Next(4);
            int r2 = random.Next(4);
            if (r1 == 1)
            {
                fantome1Direction = "Droite";
            }
            else if (r1 == 2)
            {
                fantome1Direction = "Gauche";
            }
            else if (r1 == 3)
            {
                fantome1Direction = "Bas";
            }
            else if (r1 == 4)
            {
                fantome1Direction = "Haut";
            }

            if (r2 == 1)
            {
                fantome2Direction = "Droite";
            }
            else if (r2 == 2)
            {
                fantome2Direction = "Gauche";
            }
            else if (r2 == 3)
            {
                fantome2Direction = "Bas";
            }
            else if (r2 == 4)
            {
                fantome2Direction = "Haut";
            }

            VerifierPositionFantome(gameTime);

        }

        public void ghostai2(GameTime gameTime)
        {
            double v1, v2, v3, v4; v1 = 0.0; v2 = 0.0; v3 = 0.0; v4 = 0.0;
            if (map[fantome1.Pos.X - 1, fantome1.Pos.Y] != 0)
            {
                v1 = Math.Sqrt(Math.Pow((pacman.Pos.X - fantome1.Pos.X - 1) + (pacman.Pos.Y - fantome1.Pos.Y), 2));
            }
            if (map[fantome1.Pos.X, fantome1.Pos.Y - 1] != 0)
            {
                v2 = Math.Sqrt(Math.Pow((pacman.Pos.X - fantome1.Pos.X) + (pacman.Pos.Y - fantome1.Pos.Y - 1), 2));
            }
            if (map[fantome1.Pos.X, fantome1.Pos.Y + 1] != 0)
            {
                v3 = Math.Sqrt(Math.Pow((pacman.Pos.X - fantome1.Pos.X) + (pacman.Pos.Y - fantome1.Pos.Y + 1), 2));
            }
            if (map[fantome1.Pos.X + 1, fantome1.Pos.Y] != 0)
            {
                v4 = Math.Sqrt(Math.Pow((pacman.Pos.X - fantome1.Pos.X + 1) + (pacman.Pos.Y - fantome1.Pos.Y), 2));
            }

            if (v1 >= v2 && v1 >= v3 && v1 >= v4)
            {
                fantome1Direction = "Haut";
            }
            else if (v2 >= v1 && v2 >= v3 && v2 >= v4)
            {
                fantome1Direction = "Gauche";
            }
            else if (v3 >= v1 && v3 >= v2 && v3 >= v4)
            {
                fantome1Direction = "Droite";
            }
            else if (v4 >= v1 && v4 >= v2 && v4 >= v3)
            {
                fantome1Direction = "Bas";
            }

            VerifierPositionFantome(gameTime);
        }

        public void VerifierPositionFantome(GameTime gameTime)
        {
            switch (fantome1Direction)
            {
                case "Haut":
                    AvancerFantome1(fantome1.Pos.X - 1, fantome1.Pos.Y);
                    break;
                case "Bas":
                    AvancerFantome1(fantome1.Pos.X + 1, fantome1.Pos.Y);
                    break;
                case "Gauche":
                    AvancerFantome1(fantome1.Pos.X, fantome1.Pos.Y - 1);
                    break;
                case "Droite":
                    AvancerFantome1(fantome1.Pos.X, fantome1.Pos.Y + 1);
                    break;
            }
            switch (fantome2Direction)
            {
                case "Haut":
                    AvancerFantome2(fantome1.Pos.X - 1, fantome1.Pos.Y);
                    break;
                case "Bas":
                    AvancerFantome2(fantome1.Pos.X + 1, fantome1.Pos.Y);
                    break;
                case "Gauche":
                    AvancerFantome2(fantome1.Pos.X, fantome1.Pos.Y - 1);
                    break;
                case "Droite":
                    AvancerFantome2(fantome1.Pos.X, fantome1.Pos.Y + 1);
                    break;
            }
        }

        public void AvancerFantome1(int posX, int posY)
        {
            if (map[posX, posY] != 0)
            {
                if (map[posX, posY] == 1)
                    map[fantome1.Pos.X, fantome1.Pos.Y] = 1;
                else
                    map[fantome1.Pos.X, fantome1.Pos.Y] = 2;

                map[posX, posY] = 4;

            }

        }

        public void AvancerFantome2(int posX, int posY)
        {
            if (map[posX, posY] != 0)
            {
                if (map[posX, posY] == 1)
                    map[fantome2.Pos.X, fantome2.Pos.Y] = 1;
                else
                    map[fantome2.Pos.X, fantome2.Pos.Y] = 2;

                map[posX, posY] = 5;

            }

        }
    }
}
