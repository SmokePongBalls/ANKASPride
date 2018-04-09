using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{
    class Health : Point
    {
        public Health(Vector2 position, Texture2D texture, int worth) : base(position, texture, worth)
        {
        }

        public override Player Intersect(GameTime gameTime, Player player)
        {
            if(player.health<player.maxHealth)
            player.health += worth;

            return player;
        }
    }
}
