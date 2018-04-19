using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using te16mono.Input;

namespace te16mono.LevelBuilder.UI
{
    //Anton har gjort allt i den här klassen
    class MovingObjectChanging : ValueChanging
    {
        string currentMaxX, currentMinX, currentX, currentY, currentMaxSpeed;
        //Konstruktorn
        public MovingObjectChanging(MovingObjects input)
        {
            //Alla de olika sakerna man kan redigera
            options = new List<string>();
            options.Add("X");
            options.Add("Y");
            options.Add("MinX");
            options.Add("MaxX");
            options.Add("Max speed");
            position = new Vector2(0);
            currentMaxSpeed = Convert.ToString(input.maxSpeed);
            currentMaxX = Convert.ToString(input.maxX);
            currentMinX = Convert.ToString(input.minX);
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
            if (editing != Editing.MinX)
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.White);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentMinX, position, Color.Black);
                position.Y += 80;
            }
            else
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.AntiqueWhite);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, TextInput.DrawWithMarker(editPosition, editString), position, Color.Black);
                position.Y += 80;
            }
            if (editing != Editing.MaxX)
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.White);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentMaxX, position, Color.Black);
                position.Y += 80;
            }
            else
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.AntiqueWhite);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, TextInput.DrawWithMarker(editPosition, editString), position, Color.Black);
                position.Y += 80;
            }
            if (editing != Editing.MaxSpeed)
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.White);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentMaxSpeed, position, Color.Black);
                position.Y += 80;
            }
            else
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.AntiqueWhite);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, TextInput.DrawWithMarker(editPosition, editString), position, Color.Black);
                position.Y += 80;
            }

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
                    editing = Editing.MinX;
                    editString = currentMinX;
                    return true;
                }
                position.Y += 80;
                if (BigSelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                {
                    editing = Editing.MaxX;
                    editString = currentMaxX;
                    return true;
                }
                position.Y += 80;
                if (BigSelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                {
                    editing = Editing.MaxSpeed;
                    editString = currentMaxSpeed;
                    return true;
                }
            }
            //Returnerar false om ingen var tryckt
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
            //Returnerar false om ingen var tryckt
            return false;
        }
        //Sparar värdena
        protected override void SetValues()
        {
            //Gör ett temp movingobject för att ändra värdena på
            MovingObjects movingObject = MainLevelBuilder.selectedMovingObject;
            movingObject.maxX = (float)Convert.ToDouble(currentMaxX);
            movingObject.minX = (float)Convert.ToDouble(currentMinX);
            movingObject.position.X = (float)Convert.ToDouble(currentX);
            movingObject.position.Y = (float)Convert.ToDouble(currentY);
            movingObject.maxSpeed = (float)Convert.ToDouble(currentMaxSpeed);
            MainLevelBuilder.selectedMovingObject = movingObject;
        }
        //Kollar ifall man trycker på exit
        protected override void CheckForExit()
        {
            if (MainLevelBuilder.LeftClick() && MainLevelBuilder.MouseHitbox.Intersects(ExitRectangle))
                Menu.DoneWithMovingObject();
        }
        //Ändrar värdet på den sak som editas
        protected override void SetEdit()
        {
            if (editing == Editing.MaxSpeed)
                currentMaxSpeed = editString;
            else if (editing == Editing.MaxX)
                currentMaxX = editString;
            else if (editing == Editing.MinX)
                currentMinX = editString;
            else if (editing == Editing.X)
                currentX = editString;
            else if (editing == Editing.Y)
                currentY = editString;
        }
    }
}
