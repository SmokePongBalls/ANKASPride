using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te16mono
{
    enum Oriantation { Up, Down, Left, Right }
    abstract class Objects
    {
        protected Texture2D texture;
        public Vector2 velocity, position;
        public abstract Rectangle Hitbox { get; }

        protected Oriantation CheckCollision(Rectangle collided)
        {
            //Om den är till vänster
            if (Hitbox.Intersects(new Rectangle(collided.X - collided.Width, collided.Y + (int)velocity.Y + 1, collided.Width, collided.Height)))
                return Oriantation.Left;
            //Om den är till höger
            else if (Hitbox.Intersects(new Rectangle(collided.X + collided.Width, collided.Y + (int)velocity.Y + 1, collided.Width, collided.Height)))
                return Oriantation.Right;
            //Om den är över
            else if (Hitbox.Intersects(new Rectangle(collided.X, collided.Y - collided.Height, collided.Width, collided.Height)))
                return Oriantation.Up;
            //Om den är under
            else
                return Oriantation.Down;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
        
    }
}
