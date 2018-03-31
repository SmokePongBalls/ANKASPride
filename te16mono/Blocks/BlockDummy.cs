using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{
    class BlockDummy : Block
    {
        public BlockDummy(Vector2 position, int width, int height, Vector2 velocity, Texture2D texture) : base(position, width, height, velocity, texture)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
