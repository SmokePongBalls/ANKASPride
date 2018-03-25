using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using te16mono.LevelBuilder.UI;
using Microsoft.Xna.Framework.Input;

namespace te16mono.LevelBuilder.ObjectEditing
{
    static class ObjectPlacing
    {
        public static void Create()
        {

            bool canPlace = CheckPosition();
            if (canPlace == false && MainLevelBuilder.mouse.LeftButton == ButtonState.Pressed && MainLevelBuilder.lastmouse.LeftButton != ButtonState.Pressed)
            {
                Place();
            }
            
        }

        public static bool CheckPosition()
        {
            Rectangle checkRectangle = CheckRectangle;
            foreach (MovingObjects movingObject in MainLevelBuilder.movingObjects)
            {
                if (movingObject.Hitbox.Intersects(checkRectangle))
                    return true;
            }
            foreach (Point effect in MainLevelBuilder.effects)
            {
                if (effect.Hitbox.Intersects(checkRectangle))
                    return true;
            }
            foreach (Block block in MainLevelBuilder.blocks)
            {
                if (block.Hitbox.Intersects(checkRectangle))
                    return true;
            }
            return false;
        }

        static Rectangle CheckRectangle
        {
            get
            {
                Vector2 mousePosition = MainLevelBuilder.MousePosition;

                if (MainLevelBuilder.selectedObject == SelectedObject.Block)
                    return new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 50, 50);
                if (MainLevelBuilder.selectedObject == SelectedObject.Cat)
                    return new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 251, 202);
                if (MainLevelBuilder.selectedObject == SelectedObject.FinishLine)
                    return new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 38, 56);
                if (MainLevelBuilder.selectedObject == SelectedObject.Frog)
                    return new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 112, 112);
                if (MainLevelBuilder.selectedObject == SelectedObject.Hedgehog)
                    return new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 112, 84);
                if (MainLevelBuilder.selectedObject == SelectedObject.Point)
                    return new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 56, 64);
                if (MainLevelBuilder.selectedObject == SelectedObject.Bird)
                    return new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 80, 60);

                return new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 0, 0);
            }
        }

        static void Place()
        {
            if (MainLevelBuilder.selectedObject == SelectedObject.Block)
            {
                MainLevelBuilder.blocks.Add(new Block(MainLevelBuilder.MousePosition, 50, 50, new Vector2(0), MainLevelBuilder.square));
            }
            else if (MainLevelBuilder.selectedObject == SelectedObject.Cat)
            {
                MainLevelBuilder.movingObjects.Add(new Katt(MainLevelBuilder.cat, MainLevelBuilder.MousePosition, true, 0.5f, 5000000, -500000));
            }
            else if (MainLevelBuilder.selectedObject == SelectedObject.Frog)
            {
                MainLevelBuilder.movingObjects.Add(new Frog(MainLevelBuilder.frog, MainLevelBuilder.MousePosition, true, 0.5f, 5000000, -500000));
            }
            else if (MainLevelBuilder.selectedObject == SelectedObject.Hedgehog)
            {
                MainLevelBuilder.movingObjects.Add(new Hedgehog(MainLevelBuilder.hedgehog, MainLevelBuilder.MousePosition, true, 0.5f, 5000000, -500000));
            }
            else if (MainLevelBuilder.selectedObject == SelectedObject.Bird)
            {
                MainLevelBuilder.movingObjects.Add(new Bird(MainLevelBuilder.bird, MainLevelBuilder.MousePosition, true, 0.5f, 5000000, -500000));
            }
            else if (MainLevelBuilder.selectedObject == SelectedObject.FinishLine)
            {
                MainLevelBuilder.effects.Add(new FinishLine(MainLevelBuilder.MousePosition, MainLevelBuilder.finishFlag, 0));    
            }
            else if (MainLevelBuilder.selectedObject == SelectedObject.Point)
            {
                MainLevelBuilder.effects.Add(new Point(MainLevelBuilder.MousePosition, MainLevelBuilder.pear, 100));
            }
        }
    }
}
