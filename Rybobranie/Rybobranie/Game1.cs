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

namespace Rybobranie
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private Model Rybcia;
        private Model Plane;
        private Model EnvSph;
        private KeyboardState StanKlawiatury;

        private Matrix world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        private Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 10), new Vector3(0, 0, 0), Vector3.UnitY);
        private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.1f, 2000f);

        private Vector3 translacja;

        private float myszkaX;
        private float myszkaY;

        GraphicsDeviceManager graphics;
        GraphicsDevice device;
        SpriteBatch spriteBatch;
        private float myszkaRoll;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            Rybcia = Content.Load<Model>("rybka");
            Plane = Content.Load<Model>("plane");
            // TODO: use this.Content to load your game content here
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
            MouseState staryStanMyszy = Mouse.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            KeyboardState stanKlawiatury = Keyboard.GetState();
            if (stanKlawiatury.IsKeyDown(Keys.W)) translacja.Z = 0.3f;
            if (stanKlawiatury.IsKeyDown(Keys.S)) translacja.Z = -0.3f;
            if (stanKlawiatury.IsKeyDown(Keys.A)) translacja.X = 0.3f;
            if (stanKlawiatury.IsKeyDown(Keys.D)) translacja.X = -0.3f;
            if (stanKlawiatury.IsKeyDown(Keys.NumPad2)) myszkaY = 0.03f;
            if (stanKlawiatury.IsKeyDown(Keys.NumPad8)) myszkaY = -0.03f;
            if (stanKlawiatury.IsKeyDown(Keys.NumPad6)) myszkaX = 0.03f;
            if (stanKlawiatury.IsKeyDown(Keys.NumPad4)) myszkaX = -0.03f;
            if (stanKlawiatury.IsKeyDown(Keys.NumPad7)) myszkaRoll = -0.01f;
            if (stanKlawiatury.IsKeyDown(Keys.NumPad9)) myszkaRoll = 0.01f;
            /*MouseState nowyStanMyszy = Mouse.GetState();
            if(nowyStanMyszy.LeftButton == ButtonState.Pressed)
            {
                myszkaX = (staryStanMyszy.X - nowyStanMyszy.X) / 500.0f;
                myszkaY = (staryStanMyszy.Y - nowyStanMyszy.Y) / 500.0f;
            }
            */
            view *= Matrix.CreateFromYawPitchRoll(myszkaX, myszkaY, myszkaRoll);
            view *= Matrix.CreateTranslation(translacja);

            translacja.X = 0; translacja.Z = 0;
            myszkaX = 0; myszkaY = 0; myszkaRoll = 0;

            base.Update(gameTime);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            DrawModel(Rybcia, world, view, projection);
            DrawModel(Plane, world, view, projection);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                }

                mesh.Draw();
            }
        }
    }
}
