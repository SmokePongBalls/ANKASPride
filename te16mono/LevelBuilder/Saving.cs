﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using te16mono.Input;
using te16mono.LevelBuilder.UI;

namespace te16mono.LevelBuilder
{
    //Anton
    class Saving
    {
        string toSave; //Stringen som bestämmer vilket namn filen kommer ha
        int editPosition;

        public Saving()
        {
            toSave = "";
            editPosition = 0;
        }   
        public void Update(KeyboardState keyboardState, KeyboardState lastKeyboardState)
        {
            //Om man trycker på knappen för att spara eller enter
            if (keyboardState.IsKeyDown(Keys.Enter) && lastKeyboardState.IsKeyUp(Keys.Enter) || MainLevelBuilder.MouseHitbox.Intersects(Save) && MainLevelBuilder.LeftClick())
            {
                XmlSaver.Save(toSave);
                MainLevelBuilder.state = LevelBuilderState.Main;
            }
            //Om man trycker på back knappen
            else if (MainLevelBuilder.MouseHitbox.Intersects(Back) && MainLevelBuilder.LeftClick())
            {
                MainLevelBuilder.state = LevelBuilderState.Main;
                Options.lastUpdate = false;
            }
            //Annars ska det kolla ifall användaren försöker skriva något
            else
            {
                CheckForInput(keyboardState, lastKeyboardState);
            }

        }
        
        private void CheckForInput(KeyboardState keyboardState, KeyboardState lastKeyboardState)
        {
            editPosition = TextInput.CheckEditPosition(editPosition, toSave.Length);
            string tempToSave = TextInput.CheckForBackSpace(keyboardState, lastKeyboardState, toSave, editPosition);
            StringCompare(tempToSave);
            tempToSave = TextInput.CheckForInput(keyboardState, lastKeyboardState, toSave, editPosition);

            StringCompare(tempToSave);
        }

        private void StringCompare(string tempToSave)
        {
            editPosition += TextInput.AdjustPosition(tempToSave, toSave);
            toSave = tempToSave;
        }

        //Ritar ut allt som behöver synas
        public void Draw(SpriteBatch spriteBatch)
        {
            //Alla rektanglarna
            spriteBatch.Draw(MainLevelBuilder.GetTexture(), BackgroundRectangle, Color.Black);
            spriteBatch.Draw(MainLevelBuilder.GetTexture(), Save, Color.White);
            spriteBatch.Draw(MainLevelBuilder.GetTexture(), ExitRectangle, Color.White);
            spriteBatch.DrawString(MainLevelBuilder.spriteFont, TextInput.DrawWithMarker(editPosition, toSave), new Vector2(810, 300), Color.Black);
            spriteBatch.Draw(MainLevelBuilder.GetTexture(), Back, Color.White);
            spriteBatch.DrawString(MainLevelBuilder.spriteFont, "Back", new Vector2(810, 350), Color.Black);
            spriteBatch.DrawString(MainLevelBuilder.spriteFont, "Save", new Vector2(1010, 350), Color.Black);
        }

        //Rektanglarna som används
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
