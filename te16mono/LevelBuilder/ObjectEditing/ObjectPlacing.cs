using Microsoft.Xna.Framework;
using te16mono.LevelBuilder.UI;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono.LevelBuilder.ObjectEditing
{
    //Anton har gjort allt i den här klassen
    static class ObjectPlacing
    {
        //Gör ett nytt objekt
        public static void Create()
        {   
            if (CheckPosition() == false && MainLevelBuilder.LeftClick())
            {
                Place();
            }
        }
        //Kollar ifall rektangeln som ska placeras ut kolliderar med något. Returnerar true ifall det gör det 
        public static bool CheckPosition()
        {
            Rectangle checkRectangle = CheckRectangle;
            foreach (MovingObjects movingObject in MainLevelBuilder.movingObjects)
            {
                if (movingObject.Hitbox.Intersects(checkRectangle))
                    return true;
            }
            foreach (Effect effect in MainLevelBuilder.effects)
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
        //Retunerar en rektangel 
        static Rectangle CheckRectangle
        {
            get
            {
                Vector2 mousePosition = MainLevelBuilder.MousePosition;
                //Kollar vilket objekt det är och retunerar en rektangel i den storleken
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
                if (MainLevelBuilder.selectedObject == SelectedObject.HighGravity)
                    return new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 54, 54);
                if (MainLevelBuilder.selectedObject == SelectedObject.Whammy)
                    return new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 54, 54);
                if (MainLevelBuilder.selectedObject == SelectedObject.Health)
                    return new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 54, 54);



                return new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 0, 0);
            }
        }
        //Placerar ut objektet
        static void Place()
        {
            //Får texturen som senare ska användas
            Texture2D texture = MainLevelBuilder.GetTexture(MainLevelBuilder.selectedObject);
            //Kollar vilket objekt som ska placeras ut
            if (MainLevelBuilder.selectedObject == SelectedObject.Block)
            {
                MainLevelBuilder.blocks.Add(new Block(MainLevelBuilder.MousePosition, 50, 50, new Vector2(0), texture));
            }
            else if (MainLevelBuilder.selectedObject == SelectedObject.Cat)
            {
                MainLevelBuilder.movingObjects.Add(new Katt(texture, MainLevelBuilder.MousePosition, true, 0.5f, 5000000, -500000));
            }
            else if (MainLevelBuilder.selectedObject == SelectedObject.Frog)
            {
                MainLevelBuilder.movingObjects.Add(new Frog(texture, MainLevelBuilder.MousePosition, true, 0.5f, 5000000, -500000));
            }
            else if (MainLevelBuilder.selectedObject == SelectedObject.Hedgehog)
            {
                MainLevelBuilder.movingObjects.Add(new Hedgehog(texture, MainLevelBuilder.MousePosition, true, 0.5f, 5000000, -500000));
            }
            else if (MainLevelBuilder.selectedObject == SelectedObject.Bird)
            {
                MainLevelBuilder.movingObjects.Add(new Bird(texture, MainLevelBuilder.MousePosition, true, 0.5f, 5000000, -500000));
            }
            else if (MainLevelBuilder.selectedObject == SelectedObject.FinishLine)
            {
                MainLevelBuilder.effects.Add(new FinishLine(MainLevelBuilder.MousePosition, texture, 0));    
            }
            else if (MainLevelBuilder.selectedObject == SelectedObject.Point)
            {
                MainLevelBuilder.effects.Add(new Point(MainLevelBuilder.MousePosition, texture, 100));
            }
            else if (MainLevelBuilder.selectedObject == SelectedObject.HighGravity)
            {
                MainLevelBuilder.effects.Add(new HighGravity(MainLevelBuilder.MousePosition, texture, 100));
            }
            else if (MainLevelBuilder.selectedObject == SelectedObject.Whammy)
            {
                MainLevelBuilder.effects.Add(new Whammy(MainLevelBuilder.MousePosition, texture, 100));
            }
            else if (MainLevelBuilder.selectedObject == SelectedObject.Health)
            {
                MainLevelBuilder.effects.Add(new Health(MainLevelBuilder.MousePosition, texture, 100));
            }
            else if (MainLevelBuilder.selectedObject == SelectedObject.Immortality)
            {
                MainLevelBuilder.effects.Add(new Immortality(MainLevelBuilder.MousePosition, texture, 100));
            }
        }
    }
}
