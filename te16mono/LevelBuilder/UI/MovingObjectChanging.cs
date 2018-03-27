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
    class MovingObjectChanging : ValueChanging
    {
        string currentMaxX, currentMinX, currentX, currentY, currentMaxSpeed;

        public MovingObjectChanging(MovingObjects input)
        {
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


        public override void Update()
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
            }
            CheckForExit();
        }

        //Målar
        public override void Draw(SpriteBatch spriteBatch)
        {
            BeginDrawing(spriteBatch);
            for (int i = 0; i < options.Count; i++)
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.LightSeaGreen);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, options[i], position, Color.Black);
                position.Y += 80;
            }
            DrawValues(spriteBatch);
            DrawBack(spriteBatch);
        }

        //Målar ut alla de olika värdena
        private void DrawValues(SpriteBatch spriteBatch)
        {
            position = new Vector2(1500, 140);

            if (editing != Editing.X)
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.White);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentX, position, Color.Black);
                position.Y += 80;
            }
            else
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.AntiqueWhite);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentX + "|", position, Color.Black);
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
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentY + "|", position, Color.Black);
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
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentMinX + "|", position, Color.Black);
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
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentMaxX + "|", position, Color.Black);
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
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentMaxSpeed + "|", position, Color.Black);
                position.Y += 80;
            }

        }

        //Kollar ifall den klickar på någon utav hitboxarna
        private bool CheckHitbox()
        {
            MouseState mouse = MainLevelBuilder.mouse;

            if (mouse.LeftButton == ButtonState.Pressed)
            {
                position = new Vector2(1500, 100);

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
            return false;
        }


        //Sparar värdena
        private void SetValues()
        {
            MovingObjects movingObject = MainLevelBuilder.movingObject;

            movingObject.maxX = (float)Convert.ToDouble(currentMaxX);
            movingObject.minX = (float)Convert.ToDouble(currentMinX);
            movingObject.position.X = (float)Convert.ToDouble(currentX);
            movingObject.position.Y = (float)Convert.ToDouble(currentY);
            movingObject.maxSpeed = (float)Convert.ToDouble(currentMaxSpeed);

            MainLevelBuilder.movingObject = movingObject;
        }

        //Kollar ifall man trycker på exit
        private void CheckForExit()
        {
            if (MainLevelBuilder.mouse.LeftButton == ButtonState.Pressed && MainLevelBuilder.MouseHitbox.Intersects(ExitRectangle))
                Menu.DoneWithMovingObject();
        }

        private void SetEdit()
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
