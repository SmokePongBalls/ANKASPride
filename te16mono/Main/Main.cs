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

        public static List<Block> testBlocks;
        public static List<Projectiles> projectiles;
        public static List<Point> points;

        //TestKatten
        public static List<MovingObjects> testObjects;

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
            
            //Laddar in hela banan 
            XmlLoader.LoadMap(Content, "WorldLoading/1.xml");
            
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

            foreach (Point point in points.ToArray())
            {
                if (player.Hitbox.Intersects(point.Hitbox))
                {
                    player.points += point.worth;
                    points.Remove(point);
                }
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

            foreach (Point point in points)
                point.Draw(spriteBatch);

            player.Draw(spriteBatch);

            spriteBatch.End();

            //Här ska alla saker som stannar i skärmen vara
            // (UI)
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Health: " + player.health + " Time: " + gameTime.TotalGameTime.Seconds + "," + gameTime.TotalGameTime.Milliseconds + "                " + player.points, Vector2.Zero, Color.White);
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
