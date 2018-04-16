using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using te16mono.Input;

namespace te16mono.LevelBuilder
{
    class Saving
    {
        string toSave;

        public Saving()
        {
            toSave = "";
        }
        public void Update(KeyboardState keyboardState, KeyboardState lastKeyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Enter) && lastKeyboardState.IsKeyUp(Keys.Enter))
            {
                XmlSaver.Save(toSave);
                MainLevelBuilder.state = LevelBuilderState.Main;
            }
            else if (keyboardState.IsKeyDown(Keys.Back) && lastKeyboardState.IsKeyUp(Keys.Back) && toSave.Length > 0)
            {
                toSave.Remove(toSave.Length - 1);
            }
            else
            {
                toSave += TextInput.CheckForInput(keyboardState, lastKeyboardState);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(MainLevelBuilder.square, BackgroundRectangle, Color.Black);
            spriteBatch.Draw(MainLevelBuilder.square, ExitRectangle, Color.White);
            spriteBatch.DrawString(MainLevelBuilder.spriteFont, toSave + "|", new Vector2(810, 300), Color.Black);
        }

        public static Rectangle ExitRectangle
        {
            get
            {
                return new Rectangle(810, 300, 300, 30);
            }
        }
        static Rectangle BackgroundRectangle
        {
            get
            {
                return new Rectangle(790, 285, 340, 60);
            }
        }
    }
}
