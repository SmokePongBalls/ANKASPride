using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace te16mono
{
    //Anton, Hugo F, Filip
    static class Main
    {


        public enum State { Meny, Quit, Run, Finish ,Pause, GameOver };


        public static State currentState;

        public static int map;

        static SpriteBatch spriteBatch;
        public static Player player;
        static SpriteFont font, pointFont;
        static Song music;
        static double countdown = 0;
        static ContentManager Content;
        
        public static List<Block> testBlocks;
        public static List<Projectiles> projectiles;
        public static List<Effect> effects;
        //TestKatten
        public static List<MovingObjects> testObjects;
        static new Vector2 heartposition = new Vector2((float)20, (float)10);



        static Menyer meny;
        static PauseMeny pauseMeny;


        static public void Initialize(ContentManager content)
        {

            Content = content;
            testBlocks = new List<Block>();
            testObjects = new List<MovingObjects>();
            projectiles = new List<Projectiles>();
            effects = new List<Effect>();
            // TODO: Add your initialization logic here
            player = new Player(1, Content.Load<Texture2D>("square"));
            player.up = Keys.W;
            player.down = Keys.S;
            player.left = Keys.A;
            player.right = Keys.D;
            map = 1;
        }

        public static void LoadContent(GraphicsDevice graphicsDevice , GameWindow window)
        {       
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(graphicsDevice);

            meny = new Menyer((int)State.Meny);
            meny.AddItem((int)State.Run, Content.Load<Texture2D>("Start"));
            meny.AddItem((int)State.Quit, Content.Load<Texture2D>("Quit"));
            meny.AddItem((int)GameSection.LevelBuilding, Content.Load<Texture2D>("Level"));


            pauseMeny = new PauseMeny((int)State.Pause);
            pauseMeny.AddItem((int)GameSection.CoreGame, Content.Load<Texture2D>("Meny"));
            pauseMeny.AddItem((int)State.Quit, Content.Load<Texture2D>("Quit"));


            //Hugo F
            font = Content.Load<SpriteFont>("Font");
            pointFont = Content.Load<SpriteFont>("pointFont");


            //music = Content.Load<Song>("megaman2");
            //MediaPlayer.Play(music);
        }

     

        public static State MenyUpdate(GameTime gameTime)
        {


            return (State)meny.Update(gameTime);

        }

        public static void MenyDraw()
        {

            meny.Draw(spriteBatch);


        }

        
        


        



        public static State RunUpdate(GameTime gameTime)
        {

            player.Update(gameTime);

            //Testkatten
            foreach (Block testBlock in testBlocks.ToArray())
            {
                if (player.Hitbox.Intersects(testBlock.Hitbox))
                {
                    player.Intersect(testBlock.Hitbox, testBlock.velocity, testBlock.damage, testBlock.canStandOn);
                }
            }


            //Om katten rör hitboxen
            foreach (MovingObjects testObject in testObjects.ToArray())
            {
                if (player.Hitbox.Intersects(testObject.Hitbox))
                {
                    player.Intersect(testObject.Hitbox, testObject.velocity, testObject.damage, testObject.canStandOn);
                    testObject.Intersect(player.Hitbox, player.velocity, player.damage, player.canStandOn);
                }
                foreach (Block testblock in testBlocks)
                    if (testObject.Hitbox.Intersects(testblock.Hitbox))
                    {
                        testObject.Intersect(testblock.Hitbox, testblock.velocity, testblock.damage, testblock.canStandOn);
                    }
                if (testObject.Hitbox.Intersects(player.Hitbox))
                {

                }
                foreach (MovingObjects obj in testObjects)
                {
                    if (testObject.Hitbox.Intersects(obj.Hitbox))
                    {
                        testObject.Intersect(obj.Hitbox, obj.velocity, obj.damage, obj.canStandOn);
                    }
                }

                Rectangle screenRectangle = Camera.Rectangle(player.Hitbox);
                if (screenRectangle.Intersects(testObject.Hitbox))
                testObject.Update(gameTime);



                if (testObject.health <= 0)
                    testObjects.Remove(testObject);
            }
            foreach (Projectiles projectile in projectiles.ToArray())
            {
                projectile.Update(gameTime);
                bool hasCollided = false;
                //Kollar först ifall den krockar med någonting
                if (projectile.Hitbox.Intersects(player.Hitbox))
                {
                    hasCollided = true;
                }
                foreach (MovingObjects testObject in testObjects)
                {
                    if (projectile.Hitbox.Intersects(testObject.Hitbox))
                        hasCollided = true;
                }
                foreach (Block testBlock in testBlocks)
                    {
                        if (projectile.Hitbox.Intersects(testBlock.Hitbox))
                            hasCollided = true;
                    }
                if (hasCollided)
                {
                    
                    if (projectile.BlastRadious.Intersects(player.Hitbox))
                    {
                        player.ProjectileIntersect(projectile.BlastRadious, projectile.damage);
                    }
                        foreach (MovingObjects testObject in testObjects)
                        {
                            if (projectile.BlastRadious.Intersects(testObject.Hitbox))
                                testObject.ProjectileIntersect(projectile.Hitbox, projectile.damage);
                        }
                    projectile.isDead = true;

                }
                
                if (projectile.isDead)
                    projectiles.Remove(projectile);
            }

            foreach (Effect effect in effects.ToArray())
            {
                if (effect.Hitbox.Intersects(player.Hitbox))
                {

                    player = effect.Intersect(gameTime, player);
                    effects.Remove(effect);
                }
            }

            countdown -= gameTime.ElapsedGameTime.TotalMilliseconds;
            


            if (player.health <= 0)
                currentState = State.GameOver;

            return currentState; // Stannar kvar i run 

        }

        public static void GameOverUpdate()
        {
            GameOver.Update();
        }

        public static void GameOverDraw(GraphicsDevice graphicsDevice)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Content.Load<Texture2D>("GameOver"), Finish.Rectangle(graphicsDevice), Color.White);
            spriteBatch.DrawString(pointFont, Convert.ToString(player.points), new Vector2(graphicsDevice.DisplayMode.Width / 2, graphicsDevice.DisplayMode.Height / 2 - 145), Color.White);
            spriteBatch.End();
        }

        public static void FinishUpdate()
        {
            Finish.Update();


        }

        public static void FinishDraw(GraphicsDevice graphicsDevice)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Content.Load<Texture2D>("finish"), Finish.Rectangle(graphicsDevice), Color.White);
            spriteBatch.DrawString(pointFont, Convert.ToString(player.points), new Vector2(graphicsDevice.DisplayMode.Width / 2 - 30, graphicsDevice.DisplayMode.Height / 2 - 250), Color.White);
            spriteBatch.End();
        }

        public static State PauseUpdate(GameTime gameTime)
        {
           return (State)pauseMeny.Update(gameTime);

        }

        public static void PauseDraw()
        {

            pauseMeny.Draw(spriteBatch);


        }




        public static void RunDraw( GraphicsDevice  graphicsDevice , GameTime gameTime)
        {
            
            //Här i ska alla saker som kan hamna utanför skärmen vara
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, DepthStencilState.None, null, null, Camera.Position(player, graphicsDevice.DisplayMode.Width, graphicsDevice.DisplayMode.Height));

            //Testkatten
            foreach (MovingObjects testObjekt in testObjects)
                testObjekt.Draw(spriteBatch);

            foreach (Block testblock in testBlocks)
                testblock.Draw(spriteBatch);

            foreach (Projectiles projectile in projectiles)
                projectile.Draw(spriteBatch);

            foreach (Effect point in effects)
                point.Draw(spriteBatch);

            player.Draw(spriteBatch);

            spriteBatch.End();

            //Här ska alla saker som stannar i skärmen vara
            // (UI)
            //Hugo F
           
            
            spriteBatch.Begin();
            for (int i = 0; i < player.health; i++)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("heart"), heartposition, Color.White);
                heartposition.X += 60;
            }
            heartposition.X = 20;
            //spriteBatch.DrawString(font, "Health: " + player.health + " Time: " + gameTime.TotalGameTime.Minutes + ":" +  gameTime.TotalGameTime.Seconds + ":" + gameTime.TotalGameTime.Milliseconds, Vector2.Zero, Color.White);
            spriteBatch.End();

            // TODO: Add your drawing code here

            
        }

        public static void Shoot(string type, Vector2 position, Vector2 velocity, int damage, int health)
        {
            if (type == "regular")
                projectiles.Add(new RegularProjectile(health, damage, position, velocity, Content.Load<Texture2D>("RegularProjectile")));
        }

        public static void LoadMap()
        {

            //Återställer alla variabler tills nästa bana
            player.position = new Vector2(0);
            player.velocity = new Vector2(0);
            player.health = 10;
            testObjects = new List<MovingObjects>();
            testBlocks = new List<Block>();
            effects = new List<Effect>();


            try
            {
                XmlLoader.LoadMap(Content, "WorldLoading/" + map + ".xml");
            }
            catch
            {
                map = 1;
                XmlLoader.LoadMap(Content, "WorldLoading/" + map + ".xml");
            }
            
        }
    }

}
