using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using te16mono.Input;

namespace te16mono.LevelBuilder.UI
{
    //Anton
    class EffectChanging : ValueChanging
    {
        string currentWorth, currentX, currentY;
        public EffectChanging(Effect input)
        {
            //De olika sakerna som kan ändras
            options = new List<string>();
            options.Add("X");
            options.Add("Y");
            options.Add("Worth");
            position = new Vector2(0);
            currentX = Convert.ToString(input.position.X);
            currentY = Convert.ToString(input.position.Y);
            currentWorth = Convert.ToString(input.worth);
        }
        //Kollar ifall exitknappen trycks ner
        protected override void CheckForExit()
        {
            if (MainLevelBuilder.LeftClick() && MainLevelBuilder.MouseHitbox.Intersects(ExitRectangle))
                Menu.DoneWithEffect();
        }
        //Kollar ifall användaren klickar på någon utav hitboxarna och byter värde på editString.
        protected override bool CheckHitbox()
        {
            MouseState mouse = MainLevelBuilder.mouse;
            //Körs endast om man trycker ner vänstermusknapp
            if (MainLevelBuilder.LeftClick())
            {
                //Sätter rätt position
                position = new Vector2(1500, 100);
                //Returnerar true ifall och byter state ifall användaren trycker på någon utav rektanglarna
                if (BigSelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                {
                    editing = Editing.X;
                    editString = currentX;
                    return true;
                }
                position.Y += 80;
                if (BigSelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                {
                    editing = Editing.Y;
                    editString = currentY;
                    return true;
                }
                position.Y += 80;
                if (BigSelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                {
                    editing = Editing.Worth;
                    editString = currentWorth;
                    return true;
                }
            }
            //Retunerar false ifall inget blev tryckt
            return false;
        }
        //Kollar ifall användaren klickar på någon utav hitboxarna
        protected override bool CheckForChange()
        {
            MouseState mouse = MainLevelBuilder.mouse;
            //Körs endast om man trycker ner vänstermusknapp
            if (MainLevelBuilder.LeftClick())
            {
                //Sätter rätt position
                position = new Vector2(1500, 100);
                //Returnerar true ifall och byter state ifall användaren trycker på någon utav rektanglarna
                if (BigSelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                {
                    return true;
                }
                position.Y += 80;
                if (BigSelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                {
                    return true;
                }
                position.Y += 80;
                if (BigSelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                {
                    return true;
                }
            }
            //Retunerar false ifall inget blev tryckt
            return false;
        }
        //Målar ut alla de olika värdena
        protected override void DrawValues(SpriteBatch spriteBatch)
        {
            //Sätter positionen
            position = new Vector2(1500, 140);
            //Om man redigerar textboxen så byts färg och "|" är tilllagt i slutet
            if (editing != Editing.X)
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.White);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentX, position, Color.Black);
                position.Y += 80;
            }
            else
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.AntiqueWhite);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, TextInput.DrawWithMarker(editPosition, editString), Color.Black);
                position.Y += 80;
            }
            if (editing != Editing.Y)
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.White);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentY, position, Color.Black);
                position.Y += 80;
            }
            else
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.AntiqueWhite);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, TextInput.DrawWithMarker(editPosition, editString), position, Color.Black);
                position.Y += 80;
            }
            //Done

            if (editing != Editing.Worth)
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.White);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentWorth, position, Color.Black);
                position.Y += 80;
            }
            else
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.AntiqueWhite);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, TextInput.DrawWithMarker(editPosition, editString), position, Color.Black);
                position.Y += 80;
            }
        }
        //Ändrar värdet på den sak som editas
        protected override void SetEdit()
        {
            if (editing == Editing.X)
                currentX = editString;
            else if (editing == Editing.Y)
                currentY = editString;
            else if (editing == Editing.Worth)
                currentWorth = editString;
        }
        //Sparar värdena
        protected override void SetValues()
        {
            //Gör ett temp movingobject för att ändra värdena på
            Effect effect = MainLevelBuilder.selectedEffect;
            effect.worth = Convert.ToInt32(currentWorth);
            effect.position.X = (float)Convert.ToDouble(currentX);
            effect.position.Y = (float)Convert.ToDouble(currentY);
            MainLevelBuilder.selectedEffect = effect;
        }
    }
}
