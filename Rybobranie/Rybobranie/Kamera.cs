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
    class Kamera
    {
        private Matrix view;
        private Matrix projection;

        private int obiekt;
        private bool typ; // 1-ryba, 2-pozywienie

        public Kamera()
        {
            view = Matrix.CreateLookAt(new Vector3(20, 20, 20), new Vector3(0, 0, 0), Vector3.UnitY);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.1f, 800f);
        }

        public Matrix getView()
        {
            return view;
        }

        public Matrix getProjection()
        {
            return projection;
        }

        public void ustawCel(Vector3 pozycja, Vector3 cel)
        {
            view = Matrix.CreateLookAt(pozycja, cel, Vector3.UnitY);
        }
    }
}
