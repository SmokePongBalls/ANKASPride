using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace te16mono.LevelBuilder.UI
{
    //Anton
    static class Options
    {
        static List<string> options = new List<string>();
        public static bool lastUpdate;
        static Vector2 position;
        public static void Initialize()
        {
            options.Add("Resume");
            options.Add("Reset");
            options.Add("Save");
            options.Add("Save and quit");
            options.Add("Quit");
            lastUpdate = false;
        }
        public static void Update()
        {
            if (lastUpdate)
            {
                Game1.gameSection = GameSection.CoreGame;
            }
            else
            {
                if (MainLevelBuilder.mouse.LeftButton == ButtonState.Pressed && MainLevelBuilder.lastMouse.LeftButton != ButtonState.Pressed)
                {
                    int selectedOption = -1;
                    position = new Vector2(810, 300);

                    for (int i = 0; i < options.Count; i++)
                    {
                        if (MainLevelBuilder.MouseHitbox.Intersects(SelectionRectangle))
                        {
                            selectedOption = i;
                        }
                        position.Y += 50;
                    }
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
        private static void Resume()
        {
            Menu.DoneWithOptions();
        }
        private static void Reset()
        {
            MainLevelBuilder.Reset();
            Menu.DoneWithOptions();
        }
        private static void Save()
        {
            MainLevelBuilder.state = LevelBuilderState.Saving;
        }
        private static void SaveAndQuit()
        {
            MainLevelBuilder.state = LevelBuilderState.Saving;
            lastUpdate = true;
        }
        private static void Quit()
        {
            lastUpdate = true;
        }
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
