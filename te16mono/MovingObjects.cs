using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te16mono
{
    class MovingObjects
    {

        protected Random rng;
        protected int health;
        public Vector2 velocity, position;
        protected Texture2D texture;
        public float gravity = (float)0.5;

        


        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, position, Color.White);
        }


        public Rectangle Hitbox
        {
            get
            {
                Rectangle hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                return hitbox;
            }
        }
    }





}
