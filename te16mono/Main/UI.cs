using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;


namespace te16mono
{
    static class UI
    {       
        //Hugo F
        //initierar allt som behövs i den här klassen
        static ContentManager Content;
        static SpriteFont pointFont;
        static Texture2D mountainTexture;
        static Vector2 heartPosition, pointPosition,leftUIBackgroundPosition,rightUIBackgroundPosition,mountainBackgroundPosition, secondMountainBackgroundPosition;
        static double mountainScale;
        static int mountainTextureScale;

        //Ger allt som behöver ett start värde ett värde. Namnet säger sig självt.
        static public void Initialize(ContentManager content)
        {
            //Försöker kompensera för "scale" --
            mountainScale = 1.5;
            //mountainTextureScale = Convert.ToInt32(mountainTexture.Width*mountainScale);
            //--

            Content = content;
            mountainTexture = Content.Load<Texture2D>("mountainBackground");

            //Alla start kordinater.--
            mountainBackgroundPosition = new Vector2(1, -10);
            secondMountainBackgroundPosition = new Vector2(mountainBackgroundPosition.X + mountainTexture.Width, mountainBackgroundPosition.Y);
            pointPosition = new Vector2(1700, 5);
            heartPosition = new Vector2(20, 10);
            leftUIBackgroundPosition = new Vector2(0, -10);
            rightUIBackgroundPosition = new Vector2(10, -10);
            //--

            pointFont = Content.Load<SpriteFont>("pointFont");
        }

        static public void DrawBackground(SpriteBatch spriteBatch, Player player)
        {
            //Bakgrunden för spelet.
            
            //Denna uträckning är här för att simulera en paralax. Om det inte redan är förstått så rör den sig med hälften av player hastighet för att det ser coolt ut.--
            mountainBackgroundPosition.X += player.velocity.X/2;
            secondMountainBackgroundPosition.X = mountainBackgroundPosition.X + mountainTexture.Width;
            //--

            spriteBatch.Draw(mountainTexture, mountainBackgroundPosition, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 1f);
            spriteBatch.Draw(mountainTexture, secondMountainBackgroundPosition, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 1f);
        }



        static public void DrawUI(SpriteBatch spriteBatch)
        {
            //Ritar ut spelar information som t ex liv och antal poäng.
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

        static public void Update()
        {



        }

    }
}
