using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono 
{

    //Anton, Hugo F
    public class Block : ObjectsBase
    {
        public int width, height;

        public Block(Vector2 position, int width, int height, Vector2 velocity, Texture2D texture)
        {
            name = "Block";
            this.velocity = velocity;
            this.position = position;
            this.width = width;
            this.height = height;
            this.texture = texture;
            canStandOn = true;
            damage = 0;
            solid = true;
        }
        //Blocken rör inte på sig så update  ska vara tom
        public override void Update(GameTime gameTime){}
        public override bool ProjectileIntersect(int damage, Oriantations oriantation)
        {
            return true;
        }

        public override ObjectsBase Intersect(ObjectsBase collided)
        {
            return collided;
        }
        public override Rectangle Hitbox
        {
            get
            {
                Rectangle hitbox = new Rectangle((int)position.X, (int)position.Y, width, height);
                return hitbox;
            }
        }
        public override int Width
        {
            get
            {
                return width;
            }
        }
        public override int Height
        {
            get
            {
                return height;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Hitbox, Color.Pink);
        }


    }
}
