using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


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

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;



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
            //Fullscreen --
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            //--
            Main.currentState = Main.State.Meny;
            Main.Initialize(Content);
            
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

            

            // TODO: use this.Content to load your game content here
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
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.T))
            {
                graphics.ToggleFullScreen();
            }

            
            // TODO: Add your update logic here

            switch(Main.currentState)
            {
                case Main.State.Run: Main.RunUpdate(gameTime);// kör själva spelet 
                break;

                case Main.State.Quit: this.Exit();
                break;


                case Main.State.Finish: Main.FinishUpdate();
                    if (Main.currentState == Main.State.Run)
                        Main.LoadMap();
                    break;

                case Main.State.GameOver: Main.GameOverUpdate();
                    if (Main.currentState == Main.State.Run)
                        Main.LoadMap();
                    break;


                default:
                    {
                        Main.MenyUpdate();
                        if (Main.currentState == Main.State.Run)
                            Main.LoadMap();
                        break;
                    } 
                

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

            spriteBatch.Begin(); // "Ritar menyn"

            switch (Main.currentState)
            {

                default: Main.MenyDraw();
                    break;

                case Main.State.Finish:
                    Main.FinishDraw(GraphicsDevice);
                    break;

                case Main.State.GameOver: Main.GameOverDraw(GraphicsDevice);
                    break;

                case Main.State.Run: Main.RunDraw(GraphicsDevice, gameTime);
                    break; 

                


            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }

}
