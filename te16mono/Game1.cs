using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace te16mono
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        SpriteFont font;
        Song music;
        double countdown = 0;
        Block testblock;


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
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            //--

            testblock = new Block();
            testblock.position.X = 200;
            testblock.position.Y = 900;
            testblock.isAlive = true;
            testblock.type = TypeOfBlock.plattform;

            // TODO: Add your initialization logic here
            player = new Player();
            player.up = Keys.W;
            player.down = Keys.S;
            player.left = Keys.A;
            player.right = Keys.D;
            
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

            testblock.texture = Content.Load<Texture2D>("square");

            player.texture = Content.Load<Texture2D>("square");

            font = Content.Load<SpriteFont>("Font");

            music = Content.Load<Song>("megaman2");
            MediaPlayer.Play(music);

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
            if (player.Hitbox.Intersects(testblock.Hitbox))
            {
                if (player.Hitbox.X <= testblock.position.X && player.Hitbox.Y > testblock.position.Y)
                {
                    player.velocity.X = -player.velocity.X;
                    player.position.X -= 1;
                }

                player.velocity.Y = -player.velocity.Y;
                player.position.Y -= (float)0.5;

                // if (testblock.type == TypeOfBlock.teleporter)
                //    player.position = Vector2.Zero;
            }

            else
                player.gravity = (float)0.5;

            countdown -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Space))
            {

                if (countdown <= 0)
                {
                    player.velocity.Y -= 30;
                    countdown = 2000;
                }

            }
            player.Update();
          
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            testblock.Draw(spriteBatch);

            player.Draw(spriteBatch);



            spriteBatch.DrawString(font, "Time: " + gameTime.TotalGameTime.Seconds + "," + gameTime.TotalGameTime.Milliseconds, Vector2.Zero, Color.White);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
