using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{
    class MovingObjectsDummy : MovingObjects
    {
        public MovingObjectsDummy(Texture2D texture, Vector2 position, bool walkLeft, float maxSpeed, float maxX, float minX)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
