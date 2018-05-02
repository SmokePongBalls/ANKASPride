using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te16mono.BlinkBlink
{
    static class Blink
    {
        static int number = 0;
        public static void Update(GameTime gameTime)
        {
        }

        public static void Draw(SpriteBatch spriteBatch, ContentManager content)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(content.Load<Texture2D>("square"), new Rectangle(0, 0, 2000, 2000), GetColor());

            spriteBatch.End();

        }

        private static Color GetColor()
        {
            if (number != 4)
                number++;
            else
                number = 0;

            if (number == 0)
            {
                return Color.Black;
            }
            else if (number == 1)
            {
                return Color.DarkGray;
            }
            else if (number == 2)
                return Color.DarkGray;
            else if (number == 3)
                return Color.DarkCyan;
            else if (number == 4)
                return Color.GhostWhite;

            return Color.LightGray;
        }

    }
}
