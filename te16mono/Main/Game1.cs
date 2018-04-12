using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

    public enum GameSection { CoreGame, LevelBuilding}

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static GameSection gameSection;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //Fullscreen Hugo F --
            //Gör så att spelet fyller hela skärmen.
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            //Är false för att spelet buggar lite om man tabbar ur det när det är fullscreen. måste fixas. (kanske om man gör en "if-sats" som kollar om "alt" och "tab" trycks samtidigt. Då så går det ur fullscreen?) Hugo F
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            //--
            Main.currentState = Main.State.Meny;
            Main.Initialize(Content);
            MainLevelBuilder.Initialize(Content, GraphicsDevice);
            gameSection = GameSection.CoreGame;

            IsMouseVisible = true;

            base.Initialize();
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
                switch (Main.currentState)
                {
                    case Main.State.Run:
                        Main.RunUpdate(gameTime);// kör själva spelet 
                        break;

                    case Main.State.Quit:
                        this.Exit();
                        break;
                    
                case Main.State.Pause:Main.PauseUpdate();
                    break;

                    case Main.State.Finish:
                        Main.FinishUpdate();
                        if (Main.currentState == Main.State.Run)
                            Main.LoadMap();
                        break;


                    case Main.State.GameOver:
                        Main.GameOverUpdate();
                        if (Main.currentState == Main.State.Run)
                            Main.LoadMap();
                        break;


                    default:
                        Main.currentState = Main.MenyUpdate(gameTime);
                        if (Main.currentState == Main.State.Run)
                            Main.LoadMap();
                        break;


                    }
                }
                else if (gameSection == GameSection.LevelBuilding)
                {
                MainLevelBuilder.Update(GraphicsDevice);
                if (gameSection == GameSection.CoreGame)
                    MainLevelBuilder.Reset();
                }
            base.Update(gameTime);
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
                switch (Main.currentState)
                {

                    default:
                        Main.MenyDraw();
                        break;

                case Main.State.Run: Main.RunDraw(GraphicsDevice, gameTime);
                    break;

                case Main.State.Pause: Main.PauseDraw();
                break;
                    case Main.State.Finish:
                        Main.FinishDraw(GraphicsDevice);
                        break;

                    case Main.State.GameOver:
                        Main.GameOverDraw(GraphicsDevice);
                        break;

                }
            }
            else if (gameSection == GameSection.LevelBuilding)
            {
                MainLevelBuilder.Draw(GraphicsDevice);
            }
            


            base.Draw(gameTime);
        }

    }

}
