using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te16mono
{

    // Klass för att spara olika meny Val 

    class MenyItem
    {



        Texture2D texture; //Bilden för meny valet 
        Vector2 position; // Positionen för menyvalet 
        int currentState; // meny valets state 

        public MenyItem(Texture2D texture, Vector2 position , int currentState)
        {
            this.texture = texture;
            this.position = position;
            this.currentState = currentState;



        }

        public Texture2D Texture { get { return texture; } }
        public Vector2 Position { get { return position; } }       // get egenskaper för menyitems 
        public int State { get { return currentState; } }             


    }


     class Sak
     {

        List<MenyItem> meny; // Lista på meny items 
        int selected = 0; //Highligtar första valet 

        float currentheight = 0; // används för att välja höjden på valerna 

        double lastChange = 0; // används för att sakta ner menyvalen 

        int defaultMenyState;

        public void Menu (int defaultMenystate) // konstruktor som skapar en listan med menyvalen 
        {



        }

        public void AddItem(Texture2D itemTexture, int State) // lägger till meny valen i listan 
        {



        }


        public int Update(GameTime gameTime) // fortsätt här 
        {

            return 1;

        }


     }



}
