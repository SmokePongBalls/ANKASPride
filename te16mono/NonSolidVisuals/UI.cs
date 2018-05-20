using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace te16mono
{
    static class UI
    {       
        //Hugo F
        //initierar allt som behövs i den här klassen
        static ContentManager Content;
        static SpriteFont pointFont;
        //alla positioner
        static Vector2 heartPosition, pointPosition,leftUIBackgroundPosition,rightUIBackgroundPosition;
        

        //Ger allt som behöver ett start värde ett värde. Namnet säger sig självt.
        static public void Initialize(ContentManager content)
        {
        
            Content = content;

            //Alla start kordinater.--
            pointPosition = new Vector2(1700, 5);
            heartPosition = new Vector2(20, 10);
            leftUIBackgroundPosition = new Vector2(0, -10);
            rightUIBackgroundPosition = new Vector2(10, -10);
            //--

            pointFont = Content.Load<SpriteFont>("pointFont");
        }

        static public void Draw(SpriteBatch spriteBatch)
        {
            //Ritar ut spelar information som t ex liv och antal poäng i det här fallet.
            spriteBatch.Draw(Content.Load<Texture2D>("leftuibackground"), Vector2.Zero, Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("uibackground"), rightUIBackgroundPosition, null, Color.White, 0f, Vector2.Zero,1f, SpriteEffects.FlipHorizontally,0f);

            //Kollar hur mycket liv player har och rita ut mängden hjärtan efter det.--
                for (int i = 0; i < Main.player.health; i++)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("heart"), heartPosition, Color.White);
                heartPosition.X += 60;
            }
            //--

            //ritar ut poäng.--
            spriteBatch.DrawString(pointFont, Main.player.points.ToString(), pointPosition, Color.White);
            //--

            ResetHeartPosition();
            
        }
        static void ResetHeartPosition()
        {
            //ser till så att det första hjärtat ritas ut på rätt plats eftersom alla hjärtan efter det bygger på första hjärtats position. 
            heartPosition = new Vector2((float)20, (float)10);
         
        }
        //Reset:ar mountainbackgroundposition och den används när player ramlar för långt ner och innan den här metoden fanns så var backgrunden där 
        //ursprunglien player föll och inte där han spawna.
        

    }
}
