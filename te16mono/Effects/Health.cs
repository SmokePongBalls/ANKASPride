﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{
    //Hugo F
    class Health : Point
    {
        public Health(Vector2 position, Texture2D texture, int worth) : base(position, texture, worth)
        {
            // Dess namn som används används i Xml loader för att veta vilket object det är
             name = "Health"; 
        }

        public override Player Intersect(GameTime gameTime, Player player)
        {
            //ser till så att players health inte kan gå över "maxHealth"
            if (player.health < player.maxHealth)
                player.health += worth;
            //ser till så att players "health" blir faktist ändrad.
            return player;
        }
    }
}