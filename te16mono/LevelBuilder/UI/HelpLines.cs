

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace te16mono.LevelBuilder.UI
{
    //Anton
    static class HelpLines
    {
        public static void Draw(Vector2 position, SpriteBatch spriteBatch)
        {
            position.X -= 960;
            position.Y -= 540;
            DrawLines(spriteBatch, position);
            DrawText(spriteBatch, position);
        }

        static void DrawLines(SpriteBatch spriteBatch, Vector2 position)
        {
            for (int i = 0; i < 1920; i++)
            {
                //Om pixel är modul utav 50
                if (Convert.ToInt32(position.X + i) % 50 == 0)
                {
                    spriteBatch.Draw(Menu.Square, VerticalRectangle(i, 2), Color.Black);
                }
                //Om pixel är modul utav 25
                else if (Convert.ToInt32(position.X + i) % 25 == 0)
                {
                    spriteBatch.Draw(Menu.Square, VerticalRectangle(i, 1), Color.Black);
                }
                //Om pixel är modul utav 100
                if (Convert.ToInt32(position.X + i) % 100 == 0)
                {
                    spriteBatch.Draw(Menu.Square, VerticalRectangle(i, 3), Color.Black);
                }

            }
            //Går igenom varje pixel från top till botten
            for (int i = 0; i < 1080; i++)
            {

                //Om pixel är modul utav 50
                if (Convert.ToInt32(position.Y + i) % 50 == 0)
                {
                    spriteBatch.Draw(Menu.Square, HorizontalRectangle(i, 2), Color.Black);
                }
                //Om pixel är modul utav 25
                else if (Convert.ToInt32(position.Y + i) % 25 == 0)
                {
                    spriteBatch.Draw(Menu.Square, HorizontalRectangle(i, 1), Color.Black);
                }
                //Om pixel är modul utav 100
                if (Convert.ToInt32(position.Y + i) % 100 == 0)
                {
                    spriteBatch.Draw(Menu.Square, HorizontalRectangle(i, 3), Color.Black);

                }

            }
        }
        static void DrawText(SpriteBatch spriteBatch, Vector2 position)
        {
            for (int i = 0; i < 1920; i++)
            {
                if (Convert.ToInt32(position.X + i) % 100 == 0)
                {
                    spriteBatch.DrawString(MainLevelBuilder.spriteFont, Convert.ToString(Convert.ToInt32(position.X + i)), new Vector2(i + 15, 0), Color.White);
                }
            }
            for (int i = 0; i < 1080; i++)
            {
                if (Convert.ToInt32(position.Y + i) % 100 == 0)
                {
                    spriteBatch.DrawString(MainLevelBuilder.spriteFont, Convert.ToString(Convert.ToInt32(position.Y + i)), new Vector2(0, i + 15), Color.White);
                }
            }
        }
        private static Rectangle VerticalRectangle(int x, int width)
        {
            return new Rectangle(x - 1, 0, width, 1080);
        }
        private static Rectangle HorizontalRectangle(int y, int width)
        {
            return new Rectangle(0, y -1, 1920, width);
        }

    }
}