using System;
using System.Threading;
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
        private SpriteFont czcionkaGry;
        private Model ModelSfera;
        private Model ModelPlane;
        private Model ModelRyby;
        private Model ModelZarcia;
        private Model ModelRekina;

        private Otoczenie Swiat = new Otoczenie(); //obiekt otoczenia (sfera z piaskiem)
        private Kamera Camera = new Kamera(); //Obiekt kamery

        private List<Jedzenie> ListaJedzenia = new List<Jedzenie>(); //Lista z jedzeniem
        private List<Rybcia> ListaRybek = new List<Rybcia>(); //Lista z rybkami

        private Rybcia Nemo;
        private Jedzenie Glizda;

        private KeyboardState StanKlawiatury;

        GraphicsDeviceManager graphics;
        GraphicsDevice device;
        SpriteBatch spriteBatch;
        private float myszkaRoll;

        Random losowanie = new Random(); //Randomajzer

        public void dodajJedzenie()
        // Dodawanie jedzenia w losowo wybranym miejscu "mapy" na koordynatach X:<-60;60> Y:<0;30> Z:<-60;60>
        {
            Vector3 polozenieJedzenia;
            polozenieJedzenia.X = (losowanie.Next(12001) / 100f) - 60f;
            polozenieJedzenia.Y = losowanie.Next(3001) / 100f;
            polozenieJedzenia.Z = (losowanie.Next(12001) / 100f) - 60f;
            Jedzenie nowe = new Jedzenie(polozenieJedzenia, losowanie.Next(301) + 300);
            ListaJedzenia.Add(nowe);
        }

        public void dodajRybcie()
        // Dodawanie rybki w losowo wybranym miejscu "mapy" na koordynatach X:<-60;60> Y:<0;30> Z:<-60;60>
        {
            Vector3 polozenieRybki;
            polozenieRybki.X = (losowanie.Next(12001) / 100f) - 60f;
            polozenieRybki.Y = losowanie.Next(3001) / 100f;
            polozenieRybki.Z = (losowanie.Next(12001) / 100f) - 60f;
            Rybcia nowa = new Rybcia(polozenieRybki, 0.09f);
            ListaRybek.Add(nowa);
        }

        Vector3 getJedzenie()
        {
            Vector3 nowyCel;
            Jedzenie test;
            double dlugosc = 100000;
            nowyCel = Vector3.Zero;

            for (int i = 0; i < ListaJedzenia.Count; i++)
            {
                test = ListaJedzenia.ElementAt(i);
                if (dlugosc > test.getPolozenie().Length() && test.getPolozenie().Length() < 10)
                {
                    dlugosc = test.getPolozenie().Length();
                    nowyCel = test.getPolozenie();
                }
            }

            if (nowyCel == Vector3.Zero)
            {
                nowyCel.X = (losowanie.Next(12001) / 100f) - 60f;
                nowyCel.Y = losowanie.Next(3001) / 100f;
                nowyCel.Z = (losowanie.Next(12001) / 100f) - 60f;
            }

            return nowyCel;
        }


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
            ModelRyby = Content.Load<Model>(@"Models\rybka");
            ModelPlane = Content.Load<Model>(@"Models\EnvPla");
            ModelSfera = Content.Load<Model>(@"Models\EnvSph");
            ModelZarcia = Content.Load<Model>(@"Models\Zarcie");
            ModelRekina = Content.Load<Model>(@"Models\rekinek");

            czcionkaGry = Content.Load<SpriteFont>("konsolowa");
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
        /// 
        protected override void Update(GameTime gameTime)
        {
            MouseState staryStanMyszy = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            KeyboardState stanKlawiatury = Keyboard.GetState();
            if (stanKlawiatury.IsKeyDown(Keys.Escape)) this.Exit();
            if (stanKlawiatury.IsKeyDown(Keys.C))
            {
                dodajJedzenie();
                dodajRybcie();
                Thread.Sleep(200);
                Nemo = ListaRybek.ElementAt(0);
                Glizda = ListaJedzenia.ElementAt(0);
                Nemo.UstawCel(Glizda.getPolozenie(), 4);
            }
            if (ListaRybek != null)
            {
                for (int i = 0; i < ListaRybek.Count(); i++)
                {
                    Nemo = ListaRybek.ElementAt(i);
                    if (i == 0) Camera.ustawCel(new Vector3(Glizda.getPolozenie().X,Glizda.getPolozenie().Y+5,Glizda.getPolozenie().Z-5),Nemo.getPolozenie());
                    if (Nemo.getStan() == 4 || Nemo.getStan() == 1) Nemo.Plyn();
                }
            }
            base.Update(gameTime);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            //DrawText(czcionkaGry, RekinekWld.Translation.ToString(), new Vector2(10, 10), Color.Gold);
            DrawModel(ModelSfera, Swiat.getWorld(), Camera.getView(), Camera.getProjection());
            DrawModel(ModelPlane, Swiat.getWorld(), Camera.getView(), Camera.getProjection());

            if (ListaJedzenia != null)
            {
                foreach (Jedzenie jedzenie in ListaJedzenia)
                {
                    DrawModel(ModelZarcia, jedzenie.getMatrix(), Camera.getView(), Camera.getProjection());
                }
            }
            if (ListaRybek != null)
            {
                foreach (Rybcia rybcia in ListaRybek)
                {
                    DrawModel(ModelRyby, rybcia.getMatrix(), Camera.getView(), Camera.getProjection());
                }
            }

            base.Draw(gameTime);
        }

        private void DrawText(SpriteFont czcionka, String tekst, Vector2 pozycja, Color kolor)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.DrawString(czcionka, tekst, pozycja, kolor);
            spriteBatch.End();
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
