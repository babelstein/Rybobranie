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
        private double docelulenght;

        private float x;
        private float y;
        private float z;

        private int energia;
        private float szybkosc;
        private float ryzyko;

        private int stan; 
        /*
        * 0 - nie robie nic
        * 1 - plyne bez celu
        * 2 - plyne do celu
        * 3 - uciekam 
        * 4 - mam na celu zarcie
        * 5 - nie mam na celu zarcia
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

        public Matrix getMatrix()
        {
            return world;
        }

        public Vector3 getPolozenie()
        {
            return polozenie;
        }

        public void UstawCel(Vector3 cel,int stan)
        {
            this.cel = cel;
            docelu = cel - polozenie;
            przemiesz = docelu ;
            przemiesz.Normalize();
            przemiesz *= szybkosc;
            this.stan = stan;
        }

        public void Plyn()
        {
            if (this.stan == 4) stan = 1;
            if (this.stan == 5) stan = 2;
            docelulenght = docelu.Length();
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
                cel = Vector3.Zero;
                docelu = cel;
                przemiesz = docelu;
                energia += 500;
                stan = 0;
            }
        }
    }
}
