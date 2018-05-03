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
        static ContentManager Content;
        static SpriteFont pointFont;
        static Vector2 heartPosition, pointPosition,leftUIBackgroundPosition,rightUIBackgroundPosition;
        
        static public void Initialize(ContentManager content)
        {            
            Content = content;
           
            pointPosition = new Vector2((float)1700,(float) 5);
            heartPosition = new Vector2((float)20, (float)10);
            leftUIBackgroundPosition = new Vector2((float)0, (float)-10);
            rightUIBackgroundPosition = new Vector2((float)10, (float)-10);
            pointFont = Content.Load<SpriteFont>("pointFont");
        }
        static public void DrawBackground(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Content.Load<Texture2D>("background"), Vector2.Zero, Color.White);
        }
        static public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(Content.Load<Texture2D>("leftuibackground"), leftUIBackgroundPosition, Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("uibackground"), rightUIBackgroundPosition, null, Color.White, 0f, Vector2.Zero,1f, SpriteEffects.FlipHorizontally,0f);
                for (int i = 0; i < Main.player.health; i++)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("heart"), heartPosition, Color.White);
                heartPosition.X += 60;
            }
            
            spriteBatch.DrawString(pointFont, Main.player.points.ToString(), pointPosition, Color.White);
            ResetHeartPosition();
            
        }
        static void ResetHeartPosition()
        {
            heartPosition.X = 20;
            heartPosition.Y = 10;
        }

    }
}
