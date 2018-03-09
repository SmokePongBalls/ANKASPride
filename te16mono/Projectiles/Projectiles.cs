using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te16mono
{
    class Projectiles
    {
        protected int health;
        public int damage;
        public Vector2 velocity, position;
        private Texture2D texture;
        private Oriantation direction;

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, Hitbox, Color.White);
        }

        public virtual Rectangle Hitbox
        {
            get
            {
                Rectangle hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                return hitbox;
            }
        }
    }
}
