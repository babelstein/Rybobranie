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
    class Otoczenie
    {
        private Matrix world;

        public Otoczenie()
        {
            world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        }

        public Matrix getWorld()
        {
            return world;
        }
     }      
}
