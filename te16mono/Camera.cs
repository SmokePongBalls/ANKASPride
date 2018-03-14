using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te16mono
{
    //Anton
    static class Camera
    {

        public static Matrix Position(Player player, int screenWidth, int screenHeight)
        {
            Matrix position = Matrix.CreateTranslation(-player.position.X  - (player.Hitbox.Width/ 2), -player.position.Y - (player.Hitbox.Height / 2), 0);

            Matrix screenSize = Matrix.CreateTranslation((float)screenWidth / 3, (float)screenHeight / (float)1.5, 0);

            return position * screenSize;
        }
    }
}
