
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace te16mono.LevelBuilder.UI
{
    //Anton
    class Selection
    {
        
        static Vector2 position; //Position som används för att lättare sätta ut rektanglarna
        static string[] options = new string[] { "None", "Block", "Cat", "Frog", "Point", "Hedgehog", "Finishline", "Bird" }; //Alternativen av objekt man kan välja mellan

        public static void Update()
        {
            MainLevelBuilder.selectedObject = CheckForInteraction();
            CheckMenuButtons();

        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            //Sätter rätt position för att rita ut options
            position = new Vector2(1500, 100);

            spriteBatch.Draw(Menu.Square, Menu.MenuRectangle, Color.Gray);
            DrawOptions(spriteBatch);
            DrawOption(spriteBatch);
            DrawGridButton(spriteBatch);

        }
        //Ritar ut de olika alternativen
        static void DrawOptions(SpriteBatch spriteBatch)
        {

            for (int i = 0; i < options.Length; i++)
            {
                //Om objektet är valt ska det ritas ut med en annan färg
                if (i == Convert.ToInt32(MainLevelBuilder.selectedObject))
                {
                    spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.DarkTurquoise);
                    spriteBatch.DrawString(MainLevelBuilder.spriteFont, options[i], position, Color.Black);
                }
                else
                {
                    spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.LightSeaGreen);
                    spriteBatch.DrawString(MainLevelBuilder.spriteFont, options[i], position, Color.Black);
                }
                position.Y += 40;
            }
        }

        //Kollar ifall man trycker på en knapp
        static SelectedObject CheckForInteraction()
        {
            //sätter positionen så att den börjar på rätt ställe
            position = new Vector2(1500, 100);
            //Kollar om vänstermusknapp är nertryckt
            if (MainLevelBuilder.LeftClick())
            {
                //Kollar ifall man trycker på någon utav rectanglarna
                if (SelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                    return SelectedObject.Null;
                position.Y += 40;
                if (SelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                    return SelectedObject.Block;
                position.Y += 40;
                if (SelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                    return SelectedObject.Cat;
                position.Y += 40;
                if (SelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                    return SelectedObject.Frog;
                position.Y += 40;
                if (SelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                    return SelectedObject.Point;
                position.Y += 40;
                if (SelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                    return SelectedObject.Hedgehog;
                position.Y += 40;
                if (SelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                    return SelectedObject.FinishLine;
                position.Y += 40;
                if (SelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                    return SelectedObject.Bird;
            }
            
            return MainLevelBuilder.selectedObject;
        }

        
        //Ritar ut optionknappen
        private static void DrawOption(SpriteBatch spriteBatch)
        {
            
            position = new Vector2(1500, 1000);
            spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.Red);
            spriteBatch.DrawString(MainLevelBuilder.spriteFont, "OPTIONS", position, Color.Black);
        }
        //Ritar ut gridknappen
        private static void DrawGridButton(SpriteBatch spriteBatch)
        {
            position = new Vector2(1500, 950);
            spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.Red);
            //Olika text beroende på ifall grid är aktiverat eller inte
            if (Menu.gridEnabled)
            {
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, "HIDE GRID", position, Color.Black);
            }
            else
            {
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, "SHOW GRID", position, Color.Black);
            }
                
        }

        private static void CheckMenuButtons()
        {
            MouseState mouse = MainLevelBuilder.mouse;
            MouseState lastMouse = MainLevelBuilder.lastMouse;

            //Kollar ifall vänstermusknapp är nertryckt
            if (MainLevelBuilder.LeftClick())
            {
                //Om man trycker på options
                if (MainLevelBuilder.MouseHitbox.Intersects(OptionsRectangle))
                {
                    Menu.StartOptions();
                }
                //Om man trycker på gridknappen
                else if (MainLevelBuilder.MouseHitbox.Intersects(GridRectangle))
                {
                    Menu.GridToggle();
                }
            }
        }
        //Alla rektangel
        static Rectangle SelectionRectangle
        {
            get
            {
                return new Rectangle((int)position.X - 10, (int)position.Y - 3, 300, 30);
            }
        }
        public static Rectangle OptionsRectangle
        {
            get
            {
                return new Rectangle(1500, 1000 - 20, 300, 50);
            }
        }
        public static Rectangle GridRectangle
        {
            get
            {
                return new Rectangle(1500, 950 - 20, 300, 50);
            }
        }
    }
}
