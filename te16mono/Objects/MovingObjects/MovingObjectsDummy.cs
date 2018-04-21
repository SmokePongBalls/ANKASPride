using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{
    //Anton
    class MovingObjectsDummy : MovingObjects
    {
        public MovingObjectsDummy(Texture2D texture, Vector2 position, bool walkLeft, float maxSpeed, float maxX, float minX)
        {
            name = "MovingObjectsDummy";
        }

        //Borde aldrig köras 
        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        //Ska vara tom så att den inte syns 
        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
