using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace te16mono
{
    //Anton, Hugo F, Filip
    static class Main
    {


        public enum State { Meny, Quit, Run, Finish ,Pause, GameOver, RetryMap,LoadMap};

        
        public static State currentState;

        public static int map;
        static SpriteBatch spriteBatch;
        public static Player player;
        public static SpriteFont font, pointFont;
        static Song music;
        static double countdown = 0;
        public static ContentManager Content;
        static List<Projectiles> addQueue;
        public static List<ObjectsBase> objects;
        



        static Menyer meny;
        static PauseMeny pauseMeny;
        static GameOverMeny gameoverMeny;
        static FinishMeny finishMeny;


        static public void Initialize(ContentManager content)
        {
            
            Content = content;         
            objects = new List<ObjectsBase>();
            addQueue = new List<Projectiles>();
            CreatePlayer();
            UI.Initialize(content);
            Background.Initialize(content);
            map = 1;
        }

        private static void CreatePlayer()
        {
            player = new Player(1, Content.Load<Texture2D>("bird"), Content.Load<Texture2D>("gunbird"), Content.Load<Texture2D>("transparentshield"), Content.Load<Texture2D>("weight"));
            player.up = Keys.W;
            player.down = Keys.S;
            player.left = Keys.A;
            player.right = Keys.D;
        }

        public static void LoadContent(GraphicsDevice graphicsDevice , GameWindow window)
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(graphicsDevice);

            //refractorade denna här. Ändra namn om du vet något bättre. Hugo F
            MenuItems();

            //refractorade denna här. Ändra namn om du vet något bättre. Hugo F
            PauseMenyItems();

            //refractorade denna här. Ändra namn om du vet något bättre. Hugo F
            GameOverItems();

            //refractorade denna här. Ändra namn om du vet något bättre. Hugo F
            FinishMenyItems();

            //Hugo F
            Fonts();


            //music = Content.Load<Song>("megaman2");
            //MediaPlayer.Play(music);
        }

        private static void Fonts()
        {
            font = Content.Load<SpriteFont>("Font");
            pointFont = Content.Load<SpriteFont>("pointFont");
        }

        private static void FinishMenyItems()
        {
            finishMeny = new FinishMeny((int)State.Finish);
            finishMeny.AddItem((int)State.LoadMap, Content.Load<Texture2D>("Next"));
            finishMeny.AddItem((int)State.RetryMap, Content.Load<Texture2D>("Retry"));
            finishMeny.AddItem((int)State.Quit, Content.Load<Texture2D>("Quit"));
        }

        private static void GameOverItems()
        {
            gameoverMeny = new GameOverMeny((int)State.GameOver);
            gameoverMeny.AddItem((int)GameSection.CoreGame, Content.Load<Texture2D>("Meny"));
            gameoverMeny.AddItem((int)State.RetryMap, Content.Load<Texture2D>("Retry"));
            gameoverMeny.AddItem((int)State.Quit, Content.Load<Texture2D>("Quit"));
        }

        private static void PauseMenyItems()
        {
            pauseMeny = new PauseMeny((int)State.Pause);
            pauseMeny.AddItem((int)GameSection.CoreGame, Content.Load<Texture2D>("Meny"));
            pauseMeny.AddItem((int)State.Run, Content.Load<Texture2D>("Resume"));
            pauseMeny.AddItem((int)State.Run, Content.Load<Texture2D>("Retry"));
            pauseMeny.AddItem((int)State.Quit, Content.Load<Texture2D>("Quit"));
        }

        private static void MenuItems()
        {
            meny = new Menyer((int)State.Meny);
            meny.AddItem((int)State.Run, Content.Load<Texture2D>("Start"));
            meny.AddItem((int)GameSection.LevelBuilding, Content.Load<Texture2D>("Level"));
            meny.AddItem((int)State.Quit, Content.Load<Texture2D>("Quit"));
            
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

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                currentState = State.Pause;

            Background.Update(player);
            player.Update(gameTime);
            ObjectsUpdate(gameTime);
            countdown -= gameTime.ElapsedGameTime.TotalMilliseconds;
            MergeWithQueue();
            if (player.health <= 0)
                currentState = State.GameOver;

            return currentState; // Stannar kvar i run 

        }
        //Updaterar alla saker i objects listan  Anton
        private static void ObjectsUpdate(GameTime gameTime)
        {
            var screenRectangle = Camera.Rectangle(player.Hitbox);

            //Går igenom alla objekt och uppdaterar ifall de är nära nog till player
            //refractorade denna här. Ändra namn om du vet något bättre. Hugo F
            CheckPlayerVicinity(gameTime, screenRectangle);

            //En array som används för att man ska kunna ändra värden på objekten i listan utan att förstöra något
            ObjectsBase[] outOfLoopStorage = objects.ToArray();

            //Loopar igenom alla objekt i objects listan. 
            //refractorade denna här. Ändra namn om du vet något bättre namn. Hugo F
            LoopObjectList(outOfLoopStorage);


            //Tar bort alla objekt som har "dött" under loopen
            //refractorade denna här. Ändra namn om du vet något bättre. Hugo F
            RemoveDeadObjects();
        }
        //Kollar ifall objekten är inom ett visst avstånd ifrån player. Ifall det är nära nog så körs objektets update.
        //Detta är så att fiender utanför skärmen inte ska börja gå av kanten innan spelaren är där för att se det
        //Anton
        private static void CheckPlayerVicinity(GameTime gameTime, Rectangle screenRectangle)
        {
            foreach (ObjectsBase obj in objects)
            {
                if (screenRectangle.Intersects(obj.Hitbox))
                    obj.Update(gameTime);
            }
        }
        //Tar bort alla objekten som har "dött"
        private static void RemoveDeadObjects()
        {
            foreach (ObjectsBase obj in objects.ToArray())
            {
                if (obj.health < 0)
                    objects.Remove(obj);
            }
        }

        private static void LoopObjectList(ObjectsBase[] outOfLoopStorage)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                //En annan loop som går igenom samma lista 
                for (int u = 0; u < objects.Count; u++)
                {
                    //Om de två olika objectet intersectar med varandra och inte är samma objekt ska bådas intersect metoder köras.
                    //Värdet som kommer ut förvaras i outOfLoopStorage för att inte förstöra forlooperna
                    if (objects[u] != objects[i] && objects[i].Hitbox.Intersects(objects[u].Hitbox))
                    {
                        outOfLoopStorage[u] = outOfLoopStorage[i].Intersect(objects[u]);
                        outOfLoopStorage[i] = outOfLoopStorage[u].Intersect(objects[i]);
                    }
                }
                //Kollar om den krockar med player
                if (objects[i].Hitbox.Intersects(player.Hitbox))
                {
                    player = objects[i].PlayerIntersect(player);
                }
                objects = new List<ObjectsBase>(outOfLoopStorage);

            }
        }
        //Updating menu Filip
        public static State GameoverUpdate (GameTime gameTime)
        {
            return (State)gameoverMeny.Update(gameTime);
            
        }
        //Updating menu Filip
        public static void GameOverDraw(GraphicsDevice graphicsDevice)
        {
            gameoverMeny.Draw(spriteBatch);


        }
        //Updating menues Filip
        public static State FinishUpdate(GameTime gameTime)
        {
            return (State)finishMeny.Update(gameTime);
        }
        public static void FinishDraw(GraphicsDevice graphicsDevice)
        {
            finishMeny.Draw(spriteBatch);
        }
        //Updating menues Filip
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
            DrawWithoutCameraPosition(graphicsDevice);

         
            //Det som ritas ut i den här draw metoden har som default att ritas med kamerans position som bas om inte annat sägs. Hugo F
            DrawWithCameraPosition();



        }

        private static void DrawWithoutCameraPosition(GraphicsDevice graphicsDevice)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, DepthStencilState.None, null, null, Camera.Position(player, graphicsDevice.DisplayMode.Width, graphicsDevice.DisplayMode.Height));

            //Backgrunden ritas här för att den inte ska följa med cameran som allt annat i UI ska. Hugo F
            Background.Draw(spriteBatch, player);
            
            foreach (ObjectsBase obj in objects)
            {
                obj.Draw(spriteBatch);
            }

            player.Draw(spriteBatch);

            spriteBatch.End();
        }

        private static void DrawWithCameraPosition()
        {
            spriteBatch.Begin();

            //Användar informationen ligger här för att det ska följa kameran. 
            UI.Draw(spriteBatch);
     
            spriteBatch.End();
        }

        //Gör en ny projectile och lägger till den i projectiles Anton
        public static void Shoot(string type, Vector2 velocity, Vector2 position, int damage, int health, bool playerShooter)
        {
            if (type == "regular")
                addQueue.Add(new RegularProjectile(health, damage, velocity, position, Content.Load<Texture2D>("RegularProjectile"),playerShooter));
            
        }
        //Flyttar över alla objekt i addQueue till objects listan Anton
        static void MergeWithQueue()
        {
            foreach (Projectiles projectile in addQueue)
                objects.Add(projectile);
            addQueue = new List<Projectiles>();
        }
        //Laddar in en bana. Anton
        public static State LoadMap()
        {

            

            //Återställer alla variabler tills nästa bana
            CreatePlayer();
            objects = new List<ObjectsBase>();


            try
            {
                XmlLoader.LoadMap(Content, "WorldLoading/" + map + ".xml");
                return State.Run;
            }
            catch
            {
                map = 1;
                XmlLoader.LoadMap(Content, "WorldLoading/" + map + ".xml");
                return State.Meny;
            }
          
        }
        //Laddar in en bana. Anton
        public static State RetryMap()
        {


            int tempStorage = player.points;
            //Återställer alla variabler tills nästa bana
            CreatePlayer();
            Background.Initialize(Content);
            UI.Initialize(Content);
            player.points = tempStorage;
            objects = new List<ObjectsBase>();


            try
            {
                XmlLoader.LoadMap(Content, "WorldLoading/" + map + ".xml");
                return State.Run;
            }
            catch
            {
                map = 1;
                XmlLoader.LoadMap(Content, "WorldLoading/" + map + ".xml");
                return State.Meny;
            }
          

        }
    }
       
     

}
