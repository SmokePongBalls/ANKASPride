using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using te16mono.LevelBuilder;


namespace te16mono
{
    /// <summary>
    /// This is the main type for your game.
    /// 
    /// Har flyttat det mesta utav detta till klassen Main
    /// Detta är så att våran mainklass ska vara statisk.
    /// Några saker ärvs dock ifrån Game.cs och var svåra att flytta över
    /// T.ex Exit(); och GraphicsDeviceManager
    /// 
    /// </summary>
    /// 

    //Anton, Hugo F, Filip

    public enum GameSection { CoreGame, LevelBuilding, BlinkBlink}

    public class Game1 : Game
    {
        public static KeyboardState keyboardState;
        public static KeyboardState lastKeyboardstate;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static GameSection gameSection;
        static GameTime gameTime;
        public static Random rng = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Fullscreen();
            Main.currentState = Main.State.Meny;
            Main.Initialize(Content);
            MainLevelBuilder.Initialize(Content, GraphicsDevice);
            gameSection = GameSection.CoreGame;

            IsMouseVisible = true;
            lastKeyboardstate = new KeyboardState();
            keyboardState = new KeyboardState();

            base.Initialize();
        }

        private void Fullscreen()
        {
            //Fullscreen Hugo F --
            //Gör så att spelet fyller hela skärmen.
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            //Är false för att spelet buggar lite om man tabbar ur det när det är fullscreen. måste fixas. (kanske om man gör en "if-sats" som kollar om "alt" och "tab" trycks samtidigt. Då så går det ur fullscreen?) Hugo F
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            //--
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            Main.LoadContent(GraphicsDevice, Window);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            keyboardState = Keyboard.GetState();

            Game1.gameTime = gameTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Main.currentState = Main.State.Pause;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Q))
                Main.currentState = Main.State.Quit;

            if (Keyboard.GetState().IsKeyDown(Keys.T))
            {
                graphics.ToggleFullScreen();
            }


            // TODO: Add your update logic here


            if (gameSection == GameSection.CoreGame)
            {
                Menus(gameTime);
            }
            else if (gameSection == GameSection.LevelBuilding)
            {
                MainLevelBuilder.Update(GraphicsDevice);
                if (gameSection == GameSection.CoreGame)
                {
                    MainLevelBuilder.Reset();
                    Main.currentState = Main.State.Meny;
                }

            }
            else if (gameSection == GameSection.BlinkBlink)
            {
                BlinkBlink.Blink.Update(gameTime);
            }
            lastKeyboardstate = keyboardState;

            base.Update(gameTime);
        }

        private void Menus(GameTime gameTime)
        {
            switch (Main.currentState)
            {
                case Main.State.Run:
                    Main.RunUpdate(gameTime);// kör själva spelet 
                    break;

                case Main.State.Quit:
                    this.Exit();
                    break;

                case Main.State.Pause:
                    Main.currentState = Main.PauseUpdate(gameTime);

                    break;

                case Main.State.Finish:
                    Main.FinishUpdate();
                    if (Main.currentState == Main.State.Run)
                        Main.LoadMap();
                    break;


                case Main.State.GameOver:
                    Main.currentState = Main.GameoverUpdate(gameTime);
                   
                    break;


                default:
                    Main.currentState = Main.MenyUpdate(gameTime);
                    if (Main.currentState == Main.State.Run)
                        Main.LoadMap();
                    break;


            }
            Console.WriteLine(Main.player.velocity);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime )
        {
            

            GraphicsDevice.Clear(Color.CornflowerBlue); // Rensar skärmen 
            if (gameSection == GameSection.CoreGame)
            {
                DrawMenus(gameTime);
            }
            else if (gameSection == GameSection.LevelBuilding)
            {
                MainLevelBuilder.Draw(GraphicsDevice);
            }
            else if (gameSection == GameSection.BlinkBlink)
                BlinkBlink.Blink.Draw(spriteBatch, Content);


            base.Draw(gameTime);
        }

        private void DrawMenus(GameTime gameTime)
        {
            switch (Main.currentState)
            {

                default:
                    Main.MenyDraw();
                    break;

                case Main.State.Run:
                    Main.RunDraw(GraphicsDevice, gameTime);
                    break;

                case Main.State.Pause:
                    Main.PauseDraw();
                    break;
                case Main.State.Finish:
                    Main.FinishDraw(GraphicsDevice);
                    break;

                case Main.State.GameOver:
                    Main.GameOverDraw(GraphicsDevice);
                    break;

            }
        }
        public static GameTime getGameTime
        {
            get
            {
                return gameTime;
            }
        }
    }

}
