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

        protected override void DrawValues(SpriteBatch spriteBatch)
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
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentWidth + "|", position, Color.Black);
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
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentHeight + "|", position, Color.Black);
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

        protected override void CheckForExit()
        {
            if (MainLevelBuilder.mouse.LeftButton == ButtonState.Pressed && MainLevelBuilder.MouseHitbox.Intersects(ExitRectangle))
                Menu.DoneWithBlock();
        }

        protected override bool CheckHitbox()
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
            return false;
        }

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

        protected override void SetValues()
        {
            Block block = MainLevelBuilder.selectedBlock;

            block.velocity.X = (float)Convert.ToDouble(currentVelocityX);
            block.velocity.Y = (float)Convert.ToDouble(currentVelocityY);
            block.position.X = (float)Convert.ToDouble(currentX);
            block.position.Y = (float)Convert.ToDouble(currentY);
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
