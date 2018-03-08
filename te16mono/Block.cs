using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te16mono
{

    class Block
    {
        public Vector2 position, velocity;
        private Texture2D texture;
        private int width, height;
        public int damage = 0;
        public bool canStandOn;


        
        public Block(Vector2 position, int width, int height, Vector2 velocity, Texture2D texture)
        {
            //Behövs fö Intersect metoden
            this.velocity = velocity;
            this.position = position;
            this.width = width;
            this.height = height;
            this.texture = texture;
            canStandOn = true;
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
            spriteBatch.Draw(texture, Hitbox, Color.White);
        }


    }
}
