using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using te16mono.Input;
using te16mono.LevelBuilder.UI;

namespace te16mono.LevelBuilder
{
    //Anton
    class Saving
    {
        string toSave;

        public Saving()
        {
            toSave = "";
        }
        public void Update(KeyboardState keyboardState, KeyboardState lastKeyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Enter) && lastKeyboardState.IsKeyUp(Keys.Enter) || MainLevelBuilder.MouseHitbox.Intersects(Save) && MainLevelBuilder.lastMouse.LeftButton == ButtonState.Released && MainLevelBuilder.mouse.LeftButton == ButtonState.Pressed)
            {
                XmlSaver.Save(toSave);
                MainLevelBuilder.state = LevelBuilderState.Main;
            }
            else if (MainLevelBuilder.MouseHitbox.Intersects(Back) && MainLevelBuilder.lastMouse.LeftButton == ButtonState.Released && MainLevelBuilder.mouse.LeftButton == ButtonState.Pressed)
            {
                MainLevelBuilder.state = LevelBuilderState.Main;
                Options.lastUpdate = false;
            }
            else
            {
                toSave = TextInput.CheckForBackSpace(toSave, keyboardState, lastKeyboardState);
                toSave += TextInput.CheckForInput(keyboardState, lastKeyboardState);
            }
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(MainLevelBuilder.square, BackgroundRectangle, Color.Black);
            spriteBatch.Draw(MainLevelBuilder.square, ExitRectangle, Color.White);
            spriteBatch.DrawString(MainLevelBuilder.spriteFont, toSave + "|", new Vector2(810, 300), Color.Black);
            spriteBatch.Draw(MainLevelBuilder.square, Back, Color.White);
            spriteBatch.Draw(MainLevelBuilder.square, Save, Color.White);
            spriteBatch.DrawString(MainLevelBuilder.spriteFont, "Back", new Vector2(810, 350), Color.Black);
            spriteBatch.DrawString(MainLevelBuilder.spriteFont, "Save", new Vector2(1010, 350), Color.Black);
        }

        static Rectangle ExitRectangle
        {
            get
            {
                return new Rectangle(810, 300, 300, 30);
            }
        }
        static Rectangle Back
        {
            get
            {
                return new Rectangle(810, 350, 100, 30);
            }
        }
        static Rectangle Save
        {
            get
            {
                return new Rectangle(1010, 350, 100, 30);
            }
        }

        static Rectangle BackgroundRectangle
        {
            get
            {
                return new Rectangle(790, 285, 340, 110);
            }
        }
    }
}
