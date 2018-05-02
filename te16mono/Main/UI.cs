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
            rightUIBackgroundPosition = new Vector2((float)1600, (float)-10);
            pointFont = Content.Load<SpriteFont>("pointFont");
        }
        static public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Content.Load<Texture2D>("leftuibackground"), leftUIBackgroundPosition, Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("uibackground"), rightUIBackgroundPosition, null, Color.White, 0f, Vector2.Zero,new Vector2(1,1), SpriteEffects.FlipHorizontally,1);
                for (int i = 0; i < Main.player.health; i++)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("heart"), heartPosition, Color.White);
                heartPosition.X += 60;
            }
            
            spriteBatch.DrawString(pointFont, Main.player.points.ToString(), pointPosition, Color.White);
            ResetHeartPosition();
            
            //spriteBatch.DrawString(font, "Health: " + player.health + " Time: " + gameTime.TotalGameTime.Minutes + ":" +  gameTime.TotalGameTime.Seconds + ":" + gameTime.TotalGameTime.Milliseconds, Vector2.Zero, Color.White);
        }
        static void ResetHeartPosition()
        {
            heartPosition.X = 20;
            heartPosition.Y = 10;
        }

    }
}
