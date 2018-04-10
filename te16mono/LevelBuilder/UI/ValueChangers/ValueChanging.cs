using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using te16mono.Input;

namespace te16mono.LevelBuilder.UI
{
    //Anton
    abstract class ValueChanging
    {
        protected List<string> options;
        protected Editing editing = Editing.Null;
        protected Vector2 position;
        protected string editString = "";
        protected bool isEditing = false;

        protected abstract void SetEdit();
        protected abstract bool CheckHitbox();
        protected abstract void CheckForExit();
        protected abstract void SetValues();
        protected abstract void DrawValues(SpriteBatch spriteBatch);

        public void Update()
        {
            if (isEditing)
            {
                Edit();
                SetEdit();
                if (isEditing == false)
                {
                    SetValues();
                    editing = Editing.Null;
                }

            }
            else
            {
                SetValues();
                isEditing = CheckHitbox();
                CheckForDelete();
            }
            CheckForExit();
        }

        //Main drawdelen
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            BeginDrawing(spriteBatch);
            for (int i = 0; i < options.Count; i++)
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.LightSeaGreen);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, options[i], position, Color.Black);
                position.Y += 80;
            }
            DrawDelete(spriteBatch);
            DrawValues(spriteBatch);
            DrawBack(spriteBatch);
        }

        protected void BeginDrawing(SpriteBatch spriteBatch)
        {
            position = new Vector2(1500, 100);
            spriteBatch.Draw(Menu.Square, Menu.MenuRectangle, Color.Gray);
        }

        

        
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
            editString += TextInput.CheckNumbers(keyboardState, lastKeyboardState);
        }

        public static Rectangle ExitRectangle
        {
            get
            {
                return new Rectangle(1500, 1000 - 20, 300, 50);
            }
        }
        public static Rectangle DeleteRectangle
        {
            get
            {
                return new Rectangle(1500, 950 - 20, 300, 50);
            }
        }

        //Delete delen
        void DrawDelete(SpriteBatch spriteBatch)
        {
            position = new Vector2(1500, 950);
            spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.Red);
            spriteBatch.DrawString(MainLevelBuilder.spriteFont, "!!DELETE!!", position, Color.Black);
        }
        void CheckForDelete()
        {
            if (MainLevelBuilder.MouseHitbox.Intersects(DeleteRectangle) && MainLevelBuilder.mouse.LeftButton == ButtonState.Pressed)
            Menu.DeleteValueChanging();
        }

    }
}
