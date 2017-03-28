using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman.Niveau
{
    public class Dijkstra
    {
        public static int Direction(Coord dep, Coord arr, Level niveau)
        {
            Sommet[,] sommets = new Sommet[Level.LARGEUR, Level.HAUTEUR];
            for (int i = 0; i < Level.LARGEUR; i++)
            {
                for (int j = 0; j < Level.HAUTEUR; j++)
                {
                    if(niveau.Get(i, j) != Level.WALL)
                        sommets[i,j] = new Sommet();
                }
            }

            sommets[arr.X, arr.Y].Potentiel = 0;
            Coord current = arr;

            //Algo de recherche
            while (current != dep)
            {
                Sommet z = sommets[current.X, current.Y];
                z.Marque = true;
                
                //Haut
                if (current.Y > 0)
                {
                    if (sommets[current.X, current.Y - 1] != null)
                    {
                        Sommet s = sommets[current.X, current.Y - 1];
                        if (s.Potentiel > z.Potentiel + 1)
                        {
                            s.Potentiel = z.Potentiel + 1;
                            s.Pred = current;
                        }

                    }
                }

                //Bas
                if (current.Y + 1 < Level.HAUTEUR)
                {
                    if (sommets[current.X, current.Y + 1] != null)
                    {
                        Sommet s = sommets[current.X, current.Y + 1];
                        if (s.Potentiel > z.Potentiel + 1)
                        {
                            s.Potentiel = z.Potentiel + 1;
                            s.Pred = current;
                        }

                    }
                }

                //Gauche
                if (current.X > 0)
                {
                    if (sommets[current.X - 1, current.Y] != null)
                    {
                        Sommet s = sommets[current.X - 1, current.Y];
                        if (s.Potentiel > z.Potentiel + 1)
                        {
                            s.Potentiel = z.Potentiel + 1;
                            s.Pred = current;
                        }

                    }
                }

                //Droite
                if (current.X + 1< Level.LARGEUR )
                {
                    if (sommets[current.X + 1, current.Y] != null)
                    {
                        Sommet s = sommets[current.X + 1, current.Y];
                        if (s.Potentiel > z.Potentiel + 1)
                        {
                            s.Potentiel = z.Potentiel + 1;
                            s.Pred = current;
                        }

                    }
                }

                int min = Sommet.INFINI;

                for (int i = 0; i < Level.LARGEUR; i++)
                {
                    for (int j = 0; j < Level.HAUTEUR; j++)
                    {
                        if(sommets[i, j] != null)
                        {
                            if (!sommets[i, j].Marque && sommets[i, j].Potentiel < min)
                            {
                                min = sommets[i, j].Potentiel;
                                current = new Coord(i, j);
                            }
                        }
                    }
                }
            }

            Coord next = sommets[current.X, current.Y].Pred;
            int res = 0;
            if (next.X != dep.X)
                if (next.X > dep.X)
                    res = Personnages.Fantome.DROITE;
                else
                    res = Personnages.Fantome.GAUCHE;
            else
                if (next.Y > dep.Y)
                    res = Personnages.Fantome.BAS;
                else
                    res = Personnages.Fantome.HAUT;

            return res;
        }

    }
}
