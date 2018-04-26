﻿using Microsoft.Xna.Framework;
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
        static List<Projectiles> addQueue;
        public static List<ObjectsBase> objects;
        static Vector2 heartposition;



        static Menyer meny;
        static PauseMeny pauseMeny;


        static public void Initialize(ContentManager content)
        {

            Content = content;
            heartposition = new Vector2((float)20, (float)10);
            objects = new List<ObjectsBase>();
            addQueue = new List<Projectiles>();
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
            foreach (ObjectsBase obj in objects)
            {
                if (screenRectangle.Intersects(obj.Hitbox))
                    obj.Update(gameTime);
            }
            //En array som används för att man ska kunna ändra värden på objekten i listan utan att förstöra något
            ObjectsBase[] outOfLoopStorage = objects.ToArray();
            //Loopar igenom alla objekt i bojects listan
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
            }
            objects = new List<ObjectsBase>(outOfLoopStorage);
            //Tar bort alla objekt som har "dött" under loopen
            foreach (ObjectsBase obj in objects.ToArray())
            {
                if (obj.health < 0)
                    objects.Remove(obj);
            }
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
        //Målas när state är finish Anton
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

            foreach (ObjectsBase obj in objects)
            {
                obj.Draw(spriteBatch);
            }

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
        //Gör en ny projectile och lägger till den i projectiles Anton
        public static void Shoot(string type, Vector2 velocity, Vector2 position, int damage, int health)
        {
            if (type == "regular")
                addQueue.Add(new RegularProjectile(health, damage, velocity, position, Content.Load<Texture2D>("RegularProjectile")));
        }
        //Flyttar över alla objekt i addQueue till objects listan Anton
        static void MergeWithQueue()
        {
            foreach (Projectiles projectile in addQueue)
                objects.Add(projectile);
            addQueue = new List<Projectiles>();
        }
        //Laddar in en bana. Anton
        public static void LoadMap()
        {

            //Återställer alla variabler tills nästa bana
            player.position = new Vector2(0);
            player.velocity = new Vector2(0);
            player.health = 10;
            objects = new List<ObjectsBase>();


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
