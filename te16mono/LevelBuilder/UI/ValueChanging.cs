using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace te16mono.LevelBuilder.UI
{
    abstract class ValueChanging
    {
        protected List<string> options;
        protected Editing editing = Editing.Null;
        protected Vector2 position;
        protected string editString = "";
        protected bool isEditing = false;
        public abstract void Update();
        public abstract void Draw(SpriteBatch spriteBatch);


        public virtual Rectangle SelectionRectangle
        {
            get
            {
                return new Rectangle((int)position.X - 10, (int)position.Y - 3, 300, 30);
            }
        }
        public virtual Rectangle BigSelectionRectangle
        {
            get
            {
                return new Rectangle((int)position.X - 10, (int)position.Y - 3, 300, 80);
            }
        }
        protected void BeginDrawing(SpriteBatch spriteBatch)
        {
            position = new Vector2(1500, 100);
            spriteBatch.Draw(Menu.Square, Menu.MenuRectangle, Color.Gray);
        }

        protected virtual void DrawBack(SpriteBatch spriteBatch)
        {
            position = new Vector2(1500, 1000);
            spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.Red);
            spriteBatch.DrawString(MainLevelBuilder.spriteFont, "<-----BACK", position, Color.Black);
        }
        protected virtual void Edit()
        {
            KeyboardState keyboardState = MainLevelBuilder.keyboardState;
            KeyboardState lastKeyboardState = MainLevelBuilder.lastKeyboardState;

            if (editString.Length > 0 && keyboardState.IsKeyDown(Keys.Back) && lastKeyboardState.IsKeyDown(Keys.Back) == false)
            {
                editString = editString.Remove(editString.Length - 1);
            }
            else if (keyboardState.IsKeyDown(Keys.Enter))
            {
                isEditing = false;
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad0) && lastKeyboardState.IsKeyDown(Keys.NumPad0) == false|| keyboardState.IsKeyDown(Keys.D0) && lastKeyboardState.IsKeyDown(Keys.D0) == false)
            {
                editString += "0";
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad1) && lastKeyboardState.IsKeyDown(Keys.NumPad1) == false || keyboardState.IsKeyDown(Keys.D1) && lastKeyboardState.IsKeyDown(Keys.D1) == false)
            {
                editString += "1";
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad2) && lastKeyboardState.IsKeyDown(Keys.NumPad2) == false || keyboardState.IsKeyDown(Keys.D2) && lastKeyboardState.IsKeyDown(Keys.D2) == false)
            {
                editString += "2";
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad3) && lastKeyboardState.IsKeyDown(Keys.NumPad3) == false || keyboardState.IsKeyDown(Keys.D3) && lastKeyboardState.IsKeyDown(Keys.D3) == false)
            {
                editString += "3";
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad4) && lastKeyboardState.IsKeyDown(Keys.NumPad4) == false || keyboardState.IsKeyDown(Keys.D4) && lastKeyboardState.IsKeyDown(Keys.D4) == false)
            {
                editString += "4";
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad5) && lastKeyboardState.IsKeyDown(Keys.NumPad5) == false || keyboardState.IsKeyDown(Keys.D5) && lastKeyboardState.IsKeyDown(Keys.D5) == false)
            {
                editString += "5";
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad6) && lastKeyboardState.IsKeyDown(Keys.NumPad6) == false || keyboardState.IsKeyDown(Keys.D6) && lastKeyboardState.IsKeyDown(Keys.D6) == false)
            {
                editString += "6";
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad7) && lastKeyboardState.IsKeyDown(Keys.NumPad7) == false || keyboardState.IsKeyDown(Keys.D7) && lastKeyboardState.IsKeyDown(Keys.D7) == false)
            {
                editString += "7";
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad8) && lastKeyboardState.IsKeyDown(Keys.NumPad8) == false || keyboardState.IsKeyDown(Keys.D8) && lastKeyboardState.IsKeyDown(Keys.D8) == false)
            {
                editString += "8";
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad9) && lastKeyboardState.IsKeyDown(Keys.NumPad9) == false || keyboardState.IsKeyDown(Keys.D9) && lastKeyboardState.IsKeyDown(Keys.D9) == false)
            {
                editString += "9";
            }
        }

        public static Rectangle ExitRectangle
        {
            get
            {
                return new Rectangle(1500, 1000 - 20, 300, 50);
            }
        }
    }
}
