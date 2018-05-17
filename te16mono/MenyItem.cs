using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace te16mono
{

    // Klass för att spara olika meny Val   Filip

    class MenyItem
    {



        protected Texture2D texture; //Bilden för meny valet 
        protected Vector2 position; // Positionen för menyvalet 
        int currentState; // meny valets state 

        public MenyItem(Texture2D texture, Vector2 position, int currentState)
        {
            this.texture = texture;
            this.position = position;
            this.currentState = currentState;



        }

        public Texture2D Texture { get { return texture; } }
        public Vector2 Position { get { return position; } }       // get egenskaper för menyitems 
        public int State { get { return currentState; } }


    }


    class Menyer
    {
        private Texture2D logo;
        protected List<MenyItem> meny; // Lista på meny items 
        protected int selected = 0; //Highligtar första valet 

        float currentheight = 0; // används för att välja höjden på valerna 

        protected double lastChange = 0; // används för att sakta ner menyvalen 

        protected int defaultMenyState;

        public Menyer(int defaultMenystate) // konstruktor som skapar en listan med menyvalen 
        {
            meny = new List<MenyItem>();
            this.defaultMenyState = defaultMenystate;
            this.logo = Main.Content.Load<Texture2D>("Title");

        }

        public void AddItem(int State, Texture2D itemTexture) // lägger till meny valen i listan 
        {

            // sätter höjden på föremplerna (item)
            float X = 960 - itemTexture.Width/2;
            float Y = 200 + currentheight;

            // ändrar valets höjd + 20 pixlar för lite extra mellan rum ;)

            currentheight += itemTexture.Height + 20;

            MenyItem temp = new MenyItem(itemTexture, new Vector2(X, Y), State);

            meny.Add(temp);


        }


        public virtual int Update(GameTime gameTime) // fortsätt här 
        {

            KeyboardState keyboardState = Keyboard.GetState();



            if (keyboardState.IsKeyDown(Keys.Down) && Game1.lastKeyboardstate.IsKeyUp(Keys.Down))
            {

                selected++;

                if (selected > meny.Count - 1)
                    selected = 0;

            }

            if (keyboardState.IsKeyDown(Keys.Up) && Game1.lastKeyboardstate.IsKeyUp(Keys.Up))
            {
                selected--;

                if (selected < 0)
                    selected = meny.Count - 1;



            }

            lastChange = gameTime.TotalGameTime.TotalMilliseconds;

            if (keyboardState.IsKeyDown(Keys.Enter) && Game1.lastKeyboardstate.IsKeyUp(Keys.Enter))
            {
                if (selected == 1)
                    Game1.gameSection = GameSection.LevelBuilding;

                return meny[selected].State;
            }








            return defaultMenyState;

        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            for (int i = 0; i < meny.Count; i++)
            {

                if (i == selected)
                    spriteBatch.Draw(meny[i].Texture, meny[i].Position, Color.RosyBrown);

                else
                    spriteBatch.Draw(meny[i].Texture, meny[i].Position, Color.White);


            };

            spriteBatch.Draw(logo,new Vector2((float)960-logo.Width/2,(float) 20), Color.White);

            spriteBatch.End();
        }




    }


    class PauseMenyItem : MenyItem
    {
        public PauseMenyItem(Texture2D texture, Vector2 position, int currentState) : base(texture, position, currentState)
        {

        }




    }

    class PauseMeny : Menyer
    {
        public PauseMeny(int defaultMenystate) : base(defaultMenystate)
        {

        }
        public override int Update(GameTime gameTime)
        {

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Down) && Game1.lastKeyboardstate.IsKeyUp(Keys.Down))
            {
                selected++;

                if (selected > meny.Count - 1)
                    selected = 0;



            }

            if (keyboardState.IsKeyDown(Keys.Up) && Game1.lastKeyboardstate.IsKeyUp(Keys.Up))
            {
                selected--;

                if (selected < 0)
                    selected = meny.Count - 1;



            }

            lastChange = gameTime.TotalGameTime.TotalMilliseconds;

            if (keyboardState.IsKeyDown(Keys.Enter) && Game1.lastKeyboardstate.IsKeyUp(Keys.Enter))
            {

                return meny[selected].State;

            }

            if (selected == 2)
            {
                Main.LoadMap();

            }

            return defaultMenyState;


        }

    }


    class GameOverMenyItem : MenyItem
    {
        public GameOverMenyItem(Texture2D texture, Vector2 position, int currentState) : base(texture, position, currentState)
        {
        }






    }

    class GameOverMeny : Menyer
    {
        public GameOverMeny(int defaultMenystate) : base(defaultMenystate)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            for (int i = 0; i < meny.Count; i++)
            {

                if (i == selected)
                    spriteBatch.Draw(meny[i].Texture, meny[i].Position, Color.RosyBrown);

                else
                    spriteBatch.Draw(meny[i].Texture, meny[i].Position, Color.White);


            };


            spriteBatch.DrawString(Main.pointFont, Convert.ToString(Main.player.points), new Vector2(1280 , 1080 / 2 - 250), Color.White);

            spriteBatch.End();
        }


        public override int Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Down) && Game1.lastKeyboardstate.IsKeyUp(Keys.Down))
            {
                selected++;

                if (selected > meny.Count - 1)
                    selected = 0;



            }

            if (keyboardState.IsKeyDown(Keys.Up) && Game1.lastKeyboardstate.IsKeyUp(Keys.Up))
            {
                selected--;

                if (selected < 0)
                    selected = meny.Count - 1;



            }


            lastChange = gameTime.TotalGameTime.TotalMilliseconds;

            if (keyboardState.IsKeyDown(Keys.Enter) && Game1.lastKeyboardstate.IsKeyUp(Keys.Enter))
            {

                return meny[selected].State;

            }





            return defaultMenyState;





        }


    }

    class FinishMenyitem : MenyItem
    {
        public FinishMenyitem(Texture2D texture, Vector2 position, int currentState) : base(texture, position, currentState)
        {
        }
    }






    class FinishMeny : Menyer
    {
        public FinishMeny(int defaultMenystate) : base(defaultMenystate)
        {
        }

        public override int Update(GameTime gameTime)
        {


            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Down) && Game1.lastKeyboardstate.IsKeyUp(Keys.Down))
            {
                selected++;

                if (selected > meny.Count - 1)
                    selected = 0;



            }


            if (keyboardState.IsKeyDown(Keys.Up) && Game1.lastKeyboardstate.IsKeyUp(Keys.Up))
            {
                selected--;

                if (selected < 0)
                    selected = meny.Count - 1;



            }


            lastChange = gameTime.TotalGameTime.TotalMilliseconds;

            if (keyboardState.IsKeyDown(Keys.Enter) && Game1.lastKeyboardstate.IsKeyUp(Keys.Enter))
            {

                return meny[selected].State;

            }

            

            return defaultMenyState;



        }

        public override void Draw(SpriteBatch spriteBatch)
        {


            spriteBatch.Begin();

            for (int i = 0; i < meny.Count; i++)
            {

                if (i == selected)
                    spriteBatch.Draw(meny[i].Texture, meny[i].Position, Color.RosyBrown);

                else
                    spriteBatch.Draw(meny[i].Texture, meny[i].Position, Color.White);


            };


            spriteBatch.DrawString(Main.pointFont, Convert.ToString(Main.player.points), new Vector2(1280, 1080 / 2 - 250), Color.White);

            spriteBatch.End();








        }




    }

}










 

             

        
    






