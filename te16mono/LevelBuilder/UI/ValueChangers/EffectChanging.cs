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
    class EffectChanging : ValueChanging
    {
        string currentWorth, currentX, currentY;
        public EffectChanging(Point input)
        {
            options = new List<string>();
            options.Add("X");
            options.Add("Y");
            options.Add("Worth");
            position = new Vector2(0);
            currentX = Convert.ToString(input.position.X);
            currentY = Convert.ToString(input.position.Y);
            currentWorth = Convert.ToString(input.worth);
        }

        protected override void CheckForExit()
        {
            if (MainLevelBuilder.mouse.LeftButton == ButtonState.Pressed && MainLevelBuilder.MouseHitbox.Intersects(ExitRectangle))
                Menu.DoneWithEffect();
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
                    editing = Editing.Worth;
                    editString = currentWorth;
                    return true;
                }
            }
            return false;
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

            if (editing != Editing.Worth)
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.White);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentWorth, position, Color.Black);
                position.Y += 80;
            }
            else
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.AntiqueWhite);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, currentWorth + "|", position, Color.Black);
                position.Y += 80;
            }
        }

        protected override void SetEdit()
        {
            if (editing == Editing.X)
                currentX = editString;
            else if (editing == Editing.Y)
                currentY = editString;
            else if (editing == Editing.Worth)
                currentWorth = editString;
        }

        protected override void SetValues()
        {
            Point effect = MainLevelBuilder.selectedEffect;
            effect.worth = Convert.ToInt32(currentWorth);
            effect.position.X = (float)Convert.ToDouble(currentX);
            effect.position.Y = (float)Convert.ToDouble(currentY);
        }
    }
}
