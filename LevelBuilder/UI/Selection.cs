
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace te16mono.LevelBuilder.UI
{
    //Anton
    class Selection
    {
        
        static Vector2 position;

        public static void Update()
        {
            MainLevelBuilder.selectedObject = CheckForInteraction();
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            position = new Vector2(1500, 100);

            spriteBatch.Draw(Menu.Square, Menu.MenuRectangle, Color.Gray);
            DrawOptions(spriteBatch);


        }

        static Rectangle SelectionRectangle
        {
            get
            {
                return new Rectangle((int)position.X - 10, (int)position.Y - 3, 300, 30);
            }
        }
        //Ritar ut de olika alternativen
        static void DrawOptions(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.LightSeaGreen);
            spriteBatch.DrawString(MainLevelBuilder.spriteFont, "None", position, Color.Black);
            position.Y += 40;
            spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.LightSeaGreen);
            spriteBatch.DrawString(MainLevelBuilder.spriteFont, "Block", position, Color.Black);
            position.Y += 40;
            spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.LightSeaGreen);
            spriteBatch.DrawString(MainLevelBuilder.spriteFont, "Cat", position, Color.Black);
            position.Y += 40;
            spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.LightSeaGreen);
            spriteBatch.DrawString(MainLevelBuilder.spriteFont, "Frog", position, Color.Black);
            position.Y += 40;
            spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.LightSeaGreen);
            spriteBatch.DrawString(MainLevelBuilder.spriteFont, "Point", position, Color.Black);
            position.Y += 40;
            spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.LightSeaGreen);
            spriteBatch.DrawString(MainLevelBuilder.spriteFont, "Hedgehog", position, Color.Black);
            position.Y += 40;
            spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.LightSeaGreen);
            spriteBatch.DrawString(MainLevelBuilder.spriteFont, "Finishline", position, Color.Black);
            position.Y += 40;
        }

        //Kollar ifall man trycker på en knapp
        static SelectedObject CheckForInteraction()
        {
            position = new Vector2(1500, 100);
            if (MainLevelBuilder.mouse.LeftButton == ButtonState.Pressed)
            {
                if (SelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                    return SelectedObject.Null;
                position.Y += 40;
                if (SelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                    return SelectedObject.Block;
                position.Y += 40;
                if (SelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                    return SelectedObject.Cat;
                position.Y += 40;
                if (SelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                    return SelectedObject.Frog;
                position.Y += 40;
                if (SelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                    return SelectedObject.Point;
                position.Y += 40;
                if (SelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                    return SelectedObject.Hedgehog;
                position.Y += 40;
                if (SelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                    return SelectedObject.FinishLine;
                position.Y += 40;
            }
            
            return MainLevelBuilder.selectedObject;
        }

    }
}
