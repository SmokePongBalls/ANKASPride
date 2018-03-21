using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{
    class FinishLine : Point
    {

        public FinishLine(Vector2 position, Texture2D texture, int worth) : base(position, texture, worth)
        {
        }
        public override void Intersect(GameTime gameTime)
        {
            Main.currentState = Main.State.Finish;
        }
        public override Rectangle Hitbox
        {
            get
            {
                Rectangle hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width * 3, texture.Height * 3);
                return hitbox;
            }
        }
    }
}
