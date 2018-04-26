using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace te16mono
{

    // Klass för att spara olika meny Val   Filip

    class MenyItem
    {



        Texture2D texture; //Bilden för meny valet 
        Vector2 position; // Positionen för menyvalet 
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

        protected List<MenyItem> meny; // Lista på meny items 
        protected int selected = 0; //Highligtar första valet 

        float currentheight = 0; // används för att välja höjden på valerna 

        protected double lastChange = 0; // används för att sakta ner menyvalen 

      protected  int defaultMenyState;

        public Menyer(int defaultMenystate) // konstruktor som skapar en listan med menyvalen 
        {
            meny = new List<MenyItem>();
            this.defaultMenyState = defaultMenystate;


        }

        public void AddItem(int State, Texture2D itemTexture) // lägger till meny valen i listan 
        {

            // sätter höjden på föremplerna (item)
            float X = 0;
            float Y = 0 + currentheight;

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
                if (selected == 2)
                    Game1.gameSection = GameSection.LevelBuilding;

                return meny[selected].State;
            }








            return defaultMenyState;

        }


        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();

            for (int i = 0; i < meny.Count; i++)
            {

                if (i == selected)
                    spritebatch.Draw(meny[i].Texture, meny[i].Position, Color.RosyBrown);

                else
                    spritebatch.Draw(meny[i].Texture, meny[i].Position, Color.White);


            };

            spritebatch.End();
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

            if(selected==2)
            {
                Main.LoadMap();

            }

            return defaultMenyState;


        }

    }

}



 

             

        
    






