using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te16mono
{
    public enum TypeOfBlock {plattform};

    class Block
    {
        public Vector2 position, velocity;
        public Texture2D texture;
        public bool isAlive;
        public TypeOfBlock type;


        
        public Block()
        {
            //Behövs för Intersect metoden
            velocity = new Vector2(0);
        }

        public Rectangle Hitbox
        {
            get
            {
                Rectangle hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                return hitbox;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.Chocolate);
        }


    }
}
