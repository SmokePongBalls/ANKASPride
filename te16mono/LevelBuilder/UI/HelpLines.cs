
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace te16mono.LevelBuilder.UI
{
    //Anton har gjort allt i den här klassen
    static class HelpLines
    {
        public static void Draw(Vector2 position, SpriteBatch spriteBatch)
        {
            //Ändrar positionerna för skärmen
            position.X -= 960;
            position.Y -= 540;
            DrawLines(spriteBatch, position);
            DrawText(spriteBatch, position);
        }
        //Ritar ut linjerna
        static void DrawLines(SpriteBatch spriteBatch, Vector2 position)
        {
            DrawVertical(spriteBatch, position);
            DrawHorizontal(spriteBatch, position);
        }
        //Går igenom alla pixlar horizontalt och ritar ut sträck
        static void DrawHorizontal(SpriteBatch spriteBatch, Vector2 position)
        {
            //Går igenom varje position på skärmen
            for (int i = 0; i < 1080; i++)
            {
                //Ritar ut pixlar med olika tjockhet beroende på vad det är en modul utav
                //Var hundade pixel
                if (Convert.ToInt32(position.Y + i) % 100 == 0)
                {
                    spriteBatch.Draw(Menu.Square, HorizontalRectangle(i, 3), Color.Black);

                }
                //Var 50:ionde pixel
                else if (Convert.ToInt32(position.Y + i) % 50 == 0)
                {
                    spriteBatch.Draw(Menu.Square, HorizontalRectangle(i, 2), Color.Black);
                }
                //Var 25:e pixel
                else if (Convert.ToInt32(position.Y + i) % 25 == 0)
                {
                    spriteBatch.Draw(Menu.Square, HorizontalRectangle(i, 1), Color.Black);
                }
            }
        }
        //Går igenom alla pixlar Verticalt och ritar ut sträck
        static void DrawVertical(SpriteBatch spriteBatch, Vector2 position)
        {
            for (int i = 0; i < 1920; i++)
            {
                //Ritar ut pixlar med olika tjockhet beroende på vad det är en modul utav
                //Var hundade pixel
                if (Convert.ToInt32(position.X + i) % 100 == 0)
                {
                    spriteBatch.Draw(Menu.Square, VerticalRectangle(i, 3), Color.Black);
                }
                //Var 50:ionde pixel
                else if (Convert.ToInt32(position.X + i) % 50 == 0)
                {
                    spriteBatch.Draw(Menu.Square, VerticalRectangle(i, 2), Color.Black);
                }
                //Var 25:e pixel
                else if (Convert.ToInt32(position.X + i) % 25 == 0)
                {
                    spriteBatch.Draw(Menu.Square, VerticalRectangle(i, 1), Color.Black);
                }
            }
        }
        //Skriver ut texten
        static void DrawText(SpriteBatch spriteBatch, Vector2 position)
        {
            DrawVerticalNumbers(spriteBatch, position);
            DrawHorizontalNumbers(spriteBatch, position);
        }
        //Går igenom alla pixlar horizontalt och ritar ut nummer
        static void DrawHorizontalNumbers(SpriteBatch spriteBatch, Vector2 position)
        {
            for (int i = 0; i < 1080; i++)
            {
                //Skriver ut var hundrade position som ett nummer
                if (Convert.ToInt32(position.Y + i) % 100 == 0)
                {
                    spriteBatch.DrawString(MainLevelBuilder.spriteFont, Convert.ToString(Convert.ToInt32(position.Y + i)), new Vector2(0, i + 15), Color.White);
                }
            }
        }
        //Går igenom alla pixlar Verticalt och ritar ut nummer
        static void DrawVerticalNumbers(SpriteBatch spriteBatch, Vector2 position)
        {
            for (int i = 0; i < 1920; i++)
            {
                //Skriver ut var hundrade position som ett nummer
                if (Convert.ToInt32(position.X + i) % 100 == 0)
                {
                    spriteBatch.DrawString(MainLevelBuilder.spriteFont, Convert.ToString(Convert.ToInt32(position.X + i)), new Vector2(i + 15, 0), Color.White);
                }
            }
        }
        //Rektanglarna som används
        static Rectangle VerticalRectangle(int x, int width)
        {
            return new Rectangle(x - 1, 0, width, 1080);
        }
        static Rectangle HorizontalRectangle(int y, int width)
        {
            return new Rectangle(0, y - 1, 1920, width);
        }

    }
}