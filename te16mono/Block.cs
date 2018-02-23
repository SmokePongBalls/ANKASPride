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
        private Texture2D texture;
        public bool isAlive;
        public TypeOfBlock type;
        private int width, height;


        
        public Block(Vector2 position, int width, int height, Vector2 velocity, Texture2D texture, TypeOfBlock type)
        {
            //Behövs fö Intersect metoden
            this.velocity = velocity;
            this.position = position;
            this.width = width;
            this.height = height;
            this.texture = texture;
            this.type = type;
        }

        public Rectangle Hitbox
        {
            get
            {
                Rectangle hitbox = new Rectangle((int)position.X, (int)position.Y, width, height);
                return hitbox;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Använder sig utav hitboxen för att bestämma hur stor den ska vara
            spriteBatch.Draw(texture, Hitbox, Color.Blue);
        }


    }
}
