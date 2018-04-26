using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace te16mono.LevelBuilder.UI
{
    //Anton har gjort allt i den här klassen
    static class Options
    {
        static List<string> options = new List<string>();
        public static bool lastUpdate; //Om den är true avslutas leveleditorn i nästa Options.Update()
        static Vector2 position;
        //Sätter alla optionsvärdena
        public static void Initialize()
        {
            options.Add("Resume");
            options.Add("Reset");
            options.Add("Save");
            options.Add("Save and quit");
            options.Add("Quit");
            lastUpdate = false;
        }
        //Update
        public static void Update()
        {
            //Om leveleditorn ska avslutas
            if (lastUpdate)
            {
                Game1.gameSection = GameSection.CoreGame;
                lastUpdate = false;
            }
            else
            {
                //Kollar om vänstermusknapp är nedtryckt
                if (MainLevelBuilder.LeftClick())
                { 
                    int selectedOption = CheckForSelection();
                    //Om den intersectade med någon utav dem
                    if (selectedOption != -1)
                    {
                        if (selectedOption == 0)
                        {
                            Resume();
                        }
                        else if (selectedOption == 1)
                        {
                            Reset();
                        }
                        else if (selectedOption == 2)
                        {
                            Save();
                        }
                        else if (selectedOption == 3)
                        {
                            SaveAndQuit();
                        }
                        else if (selectedOption == 4)
                        {
                            Quit();
                        }
                    }
                }

            }
        }
        //Kollar ifall några utav alternativen blir valda
        private static int CheckForSelection()
        {
            int selectedOption = -1;
            position = new Vector2(810, 300);
            for (int i = 0; i < options.Count; i++)
            {
                //Kollar om den intersectar med någon utav rectanglarna om den gör ändras selectedOptions till det
                if (MainLevelBuilder.MouseHitbox.Intersects(SelectionRectangle))
                {
                    selectedOption = i;
                }
                position.Y += 50;
            }

            return selectedOption;
        }
        //Draw
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Menu.Square, BackgroundRectangle, Color.Black);
            position = new Vector2(810, 300);
            for (int i = 0; i < options.Count; i++)
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.White);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, options[i], position, Color.Black);
                position.Y += 50;
            }
        }
        //Fortsätta med leveleditorn
        private static void Resume()
        {
            Menu.DoneWithOptions();
        }
        //Återställa till en tom leveleditor
        private static void Reset()
        {
            MainLevelBuilder.Reset();
            Menu.DoneWithOptions();
        }
        //Spara som det är och sen fortsätta
        private static void Save()
        {
            MainLevelBuilder.state = LevelBuilderState.Saving;
        }
        //Spara och gå till coregame
        private static void SaveAndQuit()
        {
            MainLevelBuilder.state = LevelBuilderState.Saving;
            lastUpdate = true;
        }
        //Gå tillbaka till coregame
        private static void Quit()
        {
            lastUpdate = true;
        }
        //Rektanglarna som används
        static Rectangle SelectionRectangle
        {
            get
            {
                return new Rectangle((int)position.X - 10, (int)position.Y - 3, 300, 30);
            }
        }
        static Rectangle BackgroundRectangle
        {
            get
            {
                return new Rectangle(790, 280, 320, 270);
            }
        }
    }
}
