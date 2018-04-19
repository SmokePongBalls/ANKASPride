using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace te16mono.LevelBuilder.UI
{
    //Anton
    class BlockChanging : ValueChanging
    {
        string currentX, currentY, currentWidth, currentHeight, currentVelocityY, currentVelocityX;

        public BlockChanging(Block input)
        {
            //Alla de olika alternativen som finns
            options = new List<string>();
            options.Add("X");
            options.Add("Y");
            options.Add("Width");
            options.Add("Height");
            options.Add("Velocity Y");
            options.Add("Velocity X");
            position = new Vector2(0);
            currentVelocityY = Convert.ToString(input.velocity.Y);
            currentVelocityX = Convert.ToString(input.velocity.X);
            currentWidth = Convert.ToString(input.width);
            currentHeight = Convert.ToString(input.height);
            currentX = Convert.ToString(input.position.X);
            currentY = Convert.ToString(input.position.Y);
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
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, TextInput.DrawWithMarker(editPosition, editString), position, Color.Black);
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

            if (editing != Editing.Width)
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.White);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentWidth, position, Color.Black);
                position.Y += 80;
            }
            else
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.AntiqueWhite);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, TextInput.DrawWithMarker(editPosition, editString), position, Color.Black);
                position.Y += 80;
            }
            if (editing != Editing.Height)
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.White);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentHeight, position, Color.Black);
                position.Y += 80;
            }
            else
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.AntiqueWhite);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, TextInput.DrawWithMarker(editPosition, editString), position, Color.Black);
                position.Y += 80;
            }
            if (editing != Editing.VelocityY)
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.White);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentVelocityY, position, Color.Black);
                position.Y += 80;
            }
            else
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.AntiqueWhite);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentVelocityY + "|", position, Color.Black);
                position.Y += 80;
            }
            if (editing != Editing.VelocityX)
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.White);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentVelocityX, position, Color.Black);
                position.Y += 80;
            }
            else
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.AntiqueWhite);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentVelocityX + "|", position, Color.Black);
                position.Y += 80;
            }
        }
        //Kollar ifall exitknappen trycks ner
        protected override void CheckForExit()
        {
            if (MainLevelBuilder.LeftClick() && MainLevelBuilder.MouseHitbox.Intersects(ExitRectangle))
                Menu.DoneWithBlock();
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
                    editing = Editing.Width;
                    editString = currentWidth;
                    return true;
                }
                position.Y += 80;
                if (BigSelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                {
                    editing = Editing.Height;
                    editString = currentHeight;
                    return true;
                }
                position.Y += 80;
                if (BigSelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                {
                    editing = Editing.VelocityY;
                    editString = currentVelocityY;
                    return true;
                }
                position.Y += 80;
                if (BigSelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                {
                    editing = Editing.VelocityX;
                    editString = currentVelocityX;
                    return true;
                }
            }
            //Retunerar false ifall inget blev tryckt
            return false;
        }
        //Kollar ifall användaren klickar på någon utav hitboxarna.
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
                position.Y += 80;
                if (BigSelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                {
                    return true;
                }
            }
            //Retunerar false ifall inget blev tryckt
            return false;
        }
        //Ändrar värdet på den sak som editas
        protected override void SetEdit()
        {
            if (editing == Editing.Width)
                currentWidth = editString;
            else if (editing == Editing.Height)
                currentHeight = editString;
            else if (editing == Editing.VelocityX)
                currentVelocityX = editString;
            else if (editing == Editing.VelocityY)
                currentVelocityY = editString;
            else if (editing == Editing.X)
                currentX = editString;
            else if (editing == Editing.Y)
                currentY = editString;
        }
        //Sparar värdena
        protected override void SetValues()
        {
            //Gör ett temp movingobject för att ändra värdena på
            Block block = MainLevelBuilder.selectedBlock;
            block.velocity.X = (float)Convert.ToDouble(currentVelocityX);
            block.velocity.Y = (float)Convert.ToDouble(currentVelocityY);
            block.position.X = (float)Convert.ToDouble(currentX);
            block.position.Y = (float)Convert.ToDouble(currentY);
            //Kollar så att längden och bredden båda är heltal
            try
            {
                block.width = Convert.ToInt32(currentWidth);
            }
            catch
            {
                currentWidth = Convert.ToString(Math.Round(Convert.ToDouble(currentWidth)));
                block.width = Convert.ToInt32(currentWidth);
            }
            try
            {
                block.height = Convert.ToInt32(currentHeight);
            }
            catch
            {
                currentHeight = Convert.ToString(Math.Round(Convert.ToDouble(currentHeight)));
                block.width = Convert.ToInt32(currentHeight);
            }
            

            MainLevelBuilder.selectedBlock = block;
        }
    }
}
