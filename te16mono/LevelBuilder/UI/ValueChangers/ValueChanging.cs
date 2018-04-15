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
        //Metoder som alla subklasser måste ha
        protected abstract void SetEdit();
        protected abstract bool CheckHitbox();
        protected abstract void CheckForExit();
        protected abstract void SetValues();
        protected abstract bool CheckForChange();
        protected abstract void DrawValues(SpriteBatch spriteBatch);
        public void Update()
        {
            //Om en utav textboxarna är valda
            if (isEditing)
            {
                Edit();
                SetEdit();

                if (isEditing == false)
                {
                    editing = Editing.Null;
                    SetValues();
                }
                else if (CheckForChange())
                {
                    if (editString.Length == 0)
                    {
                        editString = "0";
                    }
                    SetEdit();
                    SetValues();
                    CheckHitbox();
                }
                
            }
            else
            {
                SetValues();
                isEditing = CheckHitbox();
                CheckForDelete();
            }
            //Kollar ifall exitknappen trycks
            CheckForExit();
        }
        //Main drawdelen
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            BeginDrawing(spriteBatch);
            DrawOptions(spriteBatch);
            DrawDelete(spriteBatch);
            DrawValues(spriteBatch);
            DrawBack(spriteBatch);
        }
        //Ritar ut de olika alternativen man kan välja
        void DrawOptions(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < options.Count; i++)
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.LightSeaGreen);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, options[i], position, Color.Black);
                position.Y += 80;
            }
        }
        //Sätter positionen och ritar ut menyrektangeln
        protected void BeginDrawing(SpriteBatch spriteBatch)
        {
            position = new Vector2(1500, 100);
            spriteBatch.Draw(Menu.Square, Menu.MenuRectangle, Color.Gray);
        }
        //Målar ut backknappen och texten som står i den
        protected virtual void DrawBack(SpriteBatch spriteBatch)
        {
            position = new Vector2(1500, 1000);
            spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.Red);
            spriteBatch.DrawString(MainLevelBuilder.spriteFont, "<-----BACK", position, Color.Black);
        }
        //Målar ut delete knappen och texten i den
        void DrawDelete(SpriteBatch spriteBatch)
        {
            position = new Vector2(1500, 950);
            spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.Red);
            spriteBatch.DrawString(MainLevelBuilder.spriteFont, "!!DELETE!!", position, Color.Black);
        }
        //Metoden som körs ifall en textbox är vald
        protected virtual void Edit()
        {
            KeyboardState keyboardState = MainLevelBuilder.keyboardState;
            KeyboardState lastKeyboardState = MainLevelBuilder.lastKeyboardState;
            //Kollar ifall backspace är nertryckt
            editString = TextInput.CheckForBackSpace(editString, keyboardState, lastKeyboardState);
            //Ifall enterknappen trycks ner slutar man redigera
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                isEditing = false;
            }
            //Kollar ifall någon siffra trycktes in
            else
            {
                editString += TextInput.CheckNumbers(keyboardState, lastKeyboardState);
            }
            
        }
        //Kollar ifall deletknappen har blivit nertryckt och tar bort objektet isåfall
        void CheckForDelete()
        {
            if (MainLevelBuilder.MouseHitbox.Intersects(DeleteRectangle) && MainLevelBuilder.LeftClick())
            Menu.DeleteValueChanging();
        }
        //Alla rektanglar som används
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
    }
}
