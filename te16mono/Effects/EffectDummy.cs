using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{
    class EffectDummy : Point
    {
        public EffectDummy(Vector2 position, Texture2D texture, int worth) : base(position, texture, worth)
        {
           
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
