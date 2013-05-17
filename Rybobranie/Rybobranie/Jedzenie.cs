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
    class Jedzenie
    {
        private Matrix world;
        private Vector3 polozenie;

        private int energia;

        public Jedzenie(Vector3 polozenie, int energia)
        {
            world = Matrix.CreateTranslation(polozenie);
            this.polozenie = polozenie;
            this.energia = energia;
        }

        public Matrix getMatrix()
        {
            return world;
        }

        public Vector3 getPolozenie()
        {
            return polozenie;
        }

    }
}
