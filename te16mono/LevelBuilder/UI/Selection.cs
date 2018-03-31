
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace te16mono.LevelBuilder.UI
{
    //Anton
    class Selection
    {
        
        static Vector2 position;
        static string[] options = new string[] { "None", "Block", "Cat", "Frog", "Point", "Hedgehog", "Finishline", "Bird" };

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

            for (int i = 0; i < options.Length; i++)
            {
                spriteBatch.Draw(Menu.Square, SelectionRectangle, Color.LightSeaGreen);
                spriteBatch.DrawString(MainLevelBuilder.spriteFont, options[i], position, Color.Black);
                position.Y += 40;
            }
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
                if (SelectionRectangle.Intersects(MainLevelBuilder.MouseHitbox))
                    return SelectedObject.Bird;
            }
            
            return MainLevelBuilder.selectedObject;
        }

    }
}
