using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te16mono
{
    abstract class Projectiles
    {
        //Hur många milisekunder till som det ska leva
        protected int health;
        public int damage;
        public Vector2 velocity, position;
        protected Texture2D texture;
        public bool isDead;

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

        public virtual Rectangle BlastRadious
        {
            get
            {
                Rectangle hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                return hitbox;
            }
        }


        public virtual void Intersect()
        {
            isDead = true;
        }



        public abstract void Update(GameTime gameTime);
    }
}
