using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace te16mono
{
    static class Main
    {
        
        static SpriteBatch spriteBatch;
        static Player player;
        static SpriteFont font;
        static Song music;
        static double countdown = 0;
        static ContentManager Content;

        static List<Block> testBlocks;
        static List<Projectiles> projectiles;
        static List<Point> points;

        //TestKatten
        static List<MovingObjects> testObjects;

        static public void Initialize(ContentManager content)
        {

            Content = content;
            testBlocks = new List<Block>();
            testObjects = new List<MovingObjects>();
            projectiles = new List<Projectiles>();
            points = new List<Point>();

            // TODO: Add your initialization logic here
            player = new Player(1, Content.Load<Texture2D>("square"));
            player.up = Keys.W;
            player.down = Keys.S;
            player.left = Keys.A;
            player.right = Keys.D;
        }
        static public void LoadContent(GraphicsDevice graphicsDevice)
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(graphicsDevice);
            //Block för nivån. en lösning så att det inte är så mycket kod?(json,xml)
            //testblocks.Add(new Block(new Vector2(500, 450), 500, 100, new Vector2(0), Content.Load<Texture2D>("square"), TypeOfBlock.plattform));
            testBlocks.Add(new Block(new Vector2(0, 900), 1900, 100, new Vector2(0), Content.Load<Texture2D>("square")));
            testBlocks.Add(new Block(new Vector2(2300, 900), 300, 100, new Vector2(0), Content.Load<Texture2D>("square")));
            testBlocks.Add(new Block(new Vector2(2500, 800), 300, 100, new Vector2(0), Content.Load<Texture2D>("square")));
            testBlocks.Add(new Block(new Vector2(2700, 700), 300, 100, new Vector2(0), Content.Load<Texture2D>("square")));
            testBlocks.Add(new Block(new Vector2(3000, 700), 1000, 100, new Vector2(0), Content.Load<Texture2D>("square")));
            testBlocks.Add(new Block(new Vector2(500, -1100), 40, 1700, new Vector2(0), Content.Load<Texture2D>("square")));
            testBlocks.Add(new Block(new Vector2(700, -1100), 40, 1700, new Vector2(0), Content.Load<Texture2D>("square")));

            //Poäng


            //Testkatten
            testObjects.Add(new Bird(1, Content.Load<Texture2D>("bird"), new Vector2(900, 300), false, 0.25f, 1700, 0));
            testObjects.Add(new Katt(1, Content.Load<Texture2D>("kattModel"), new Vector2(100, 100), false, 0.5f, 1700, 0));
            testObjects.Add(new Frog(1, Content.Load<Texture2D>("frog"), new Vector2(100, 100), false, 0.5f, 1700, -1000));
            testObjects.Add(new Hedgehog(1, Content.Load<Texture2D>("hedgehog"), new Vector2(2350, 0), true, 0f, 2600, 2300));


            //Används för att lägga till text i programmet. Används i det här programmet för att skriva tid och HP.
            font = Content.Load<SpriteFont>("Font");

            //Om vi vill ha music så har kvar den här raden. Kanske ska bytas så att det inte är samma. Olika låtar för olika banor? Annan music för boss? boss?
            music = Content.Load<Song>("megaman2");
            MediaPlayer.Play(music);
        }
        public static void Update(GameTime gameTime)
        {
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



            countdown -= gameTime.ElapsedGameTime.TotalMilliseconds;
            player.Update(gameTime);

        }
        public static void Draw(GameTime gameTime, GraphicsDevice graphicsDevise)
        {
            
            //Här i ska alla saker som kan hamna utanför skärmen vara
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, DepthStencilState.None, null, null, Camera.Position(player, graphicsDevise.DisplayMode.Width, graphicsDevise.DisplayMode.Height));

            //Testkatten
            foreach (MovingObjects testObjekt in testObjects)
                testObjekt.Draw(spriteBatch);

            foreach (Block testblock in testBlocks)
                testblock.Draw(spriteBatch);

            foreach (Projectiles projectile in projectiles)
                projectile.Draw(spriteBatch);

            player.Draw(spriteBatch);

            spriteBatch.End();

            //Här ska alla saker som stannar i skärmen vara
            // (UI)
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Health: " + player.health + " Time: " + gameTime.TotalGameTime.Seconds + "," + gameTime.TotalGameTime.Milliseconds, Vector2.Zero, Color.White);
            spriteBatch.End();

            // TODO: Add your drawing code here

            
        }

        public static void Shoot(string type, Vector2 position, Vector2 velocity, int damage, int health)
        {
            if (type == "regular")
                projectiles.Add(new RegularProjectile(health, damage, position, velocity, Content.Load<Texture2D>("RegularProjectile")));
        }


    }

}
