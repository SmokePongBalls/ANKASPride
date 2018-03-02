using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace te16mono
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 
    
    

    public class Game1 : Game
    {
        protected float gravity = (float)0.5;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        SpriteFont font;
        Song music;
        double countdown = 0;
        List<Block> testblocks;

        //TestKatten
        List<MovingObjects> testObjects;




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
            testblocks = new List<Block>();
            testObjects = new List<MovingObjects>();

            // TODO: Add your initialization logic here
            player = new Player(1, Content.Load<Texture2D>("square"));
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
            //testblocks.Add(new Block(new Vector2(500, 450), 500, 100, new Vector2(0), Content.Load<Texture2D>("square"), TypeOfBlock.plattform));
            testblocks.Add(new Block(new Vector2(0, 900), 19200, 100, new Vector2(0), Content.Load<Texture2D>("square"), TypeOfBlock.plattform));

            //Testkatten
            testObjects.Add(new Katt(1, Content.Load<Texture2D>("kattModel"), new Vector2(100, 100), false, (float)0.5, 1700, 0));
            testObjects.Add(new Frog(1, Content.Load<Texture2D>("frog"), new Vector2(100, 100), false, (float)0.5, 1700, 0));



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


            //Testkatten
            

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.T))
            {
                graphics.ToggleFullScreen();
            }

            foreach (Block testBlock in testblocks)
            {
                if (player.Hitbox.Intersects(testBlock.Hitbox))
                {

                    player.Intersect(testBlock.Hitbox, testBlock.velocity, testBlock.damage, testBlock.canStandOn);

                    /*
                     *  IRRELEVANT KOD 
                     *
                    if (player.Hitbox.X <= testblock.position.X && player.Hitbox.Y > testblock.position.Y)
                    {
                        player.velocity.X = -player.velocity.X;
                        player.position.X -= 1;
                    }

                    player.velocity.Y = -player.velocity.Y;
                    player.position.Y -= (float)0.5;

                    // if (testblock.type == TypeOfBlock.teleporter)
                    //    player.position = Vector2.Zero;
                    */
                }

                else
                    player.gravity = (float)0.5;
            }
            

            //Om katten rör hitboxen
            foreach (MovingObjects testObject in testObjects)
            {
                if (player.Hitbox.Intersects(testObject.Hitbox))
                {
                    player.Intersect(testObject.Hitbox, testObject.velocity, testObject.damage, testObject.canStandOn);
                    
                }
                foreach (Block testblock in testblocks)
                    if (testObject.Hitbox.Intersects(testblock.Hitbox))
                    {
                        testObject.Intersect(testblock.Hitbox, testblock.velocity, testblock.damage ,testblock.canStandOn);
                    }
                if (testObject.Hitbox.Intersects(player.Hitbox))
                {
                    
                }
                foreach (MovingObjects obj in testObjects)
                {
                    if (testObject.Hitbox.Intersects(obj.Hitbox))
                    {
                        testObject.Intersect(obj.Hitbox, obj.velocity, obj.damage,obj.canStandOn);
                    }
                }


                testObject.Update();
            }

            
            

            countdown -= gameTime.ElapsedGameTime.TotalMilliseconds;
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

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.LinearWrap, DepthStencilState.None, null, null, null);

            //Testkatten
            foreach (MovingObjects testObjekt in testObjects)
                testObjekt.Draw(spriteBatch);

            foreach (Block testblock in testblocks)
                testblock.Draw(spriteBatch);

            player.Draw(spriteBatch);



            spriteBatch.DrawString(font, "Time: " + player.health + "," + gameTime.TotalGameTime.Milliseconds, Vector2.Zero, Color.White);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
