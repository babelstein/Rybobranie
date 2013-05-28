using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Rybobranie
{
    public class Rybcia
    {
        private Matrix world;
        private Vector3 polozenie;
        private Vector3 przemiesz;
        private Vector3 cel;
        private Vector3 docelu;

        private float x;
        private float y;
        private float z;

        private int energia;
        private float szybkosc;
        private float ryzyko;

        private int stan; 
        /*
        * 0 - nie robie nic
        * 1 - plyne do celu
        * 2 - uciekam
        * > 2 - szukam jedzenia
        */

        public Rybcia()
        {
            world = Matrix.CreateTranslation(new Vector3(0,0,2));           
        }

        public Rybcia(Vector3 polozenie, float szybkosc)
        {
            world = Matrix.CreateTranslation(polozenie);
            this.polozenie = polozenie;
            this.szybkosc = szybkosc;
            energia = 1000;
        }

        public int getStan()
        {
            return this.stan;
        }

        public void setStan(int stan)
        {
            this.stan = stan;
        }

        public int getEnergia()
        {
            return this.energia;
        }

        public Matrix getMatrix()
        {
            return world;
        }

        public Vector3 getPolozenie()
        {
            return polozenie;
        }

        public Vector3 getCel()
        {
            return cel;
        }

        public int UstawCel(Vector3 cel,int stan) // ustawia cel podany w argumencie funkcji a następnie wylicza współczynnik przemieszczenia
        {
            if (cel == Vector3.Zero)
                return 1;
            this.cel = cel;
            docelu = cel - polozenie;
            przemiesz = docelu ;
            przemiesz.Normalize();
            przemiesz *= szybkosc;
            this.stan = stan;
            return 0;
        }


        public void CzyUciekac(Vector3 rekin)
        //Decydujemy się czy uciekać czy nie.
        {

        }

        public void Plyn() // w zakleżności od stanu nasza rybka będzie płynąć...
        {
            if (docelu.Length() > 2.5)
            {
                polozenie += przemiesz;
                world = Matrix.CreateTranslation(przemiesz);
                world = Matrix.CreateWorld(polozenie, przemiesz, Vector3.UnitY);
                energia -= 1;
                docelu = cel - polozenie;
            }
            else
            {
                /* Tutaj trzeba otrzymać wartość energi jedzenia
                 * zmienić stan rybki
                 * wyzerować wektor celu
                 * wyzerować wektor docelu
                 */
            }
        }
    }
}
