﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{
    //Hugo F
    //ändrade så att alla effekter ärver av Effekt nu istället. Hugo F
    class Immortality : Effect
    {
        public Immortality(Vector2 position, Texture2D texture, int worth) : base(position, texture, worth)
        {
            //Dess namn som används används i Xml loader för att veta vilket object det är
            name = "Immortality";
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override Player PlayerIntersect(Player player)
        {
            //ser till så att de försvinner när player vidrör dem. negativt "health" = död
            health = -4;
            //ser till så att programmet vet att player är under en effekt
            player.underEffect = true;
            //ser till så att programmet vet vilken sorts effect det är
            player.effects.Add("Immortality");
            //ser till så att players "underEffect" och "effect" blir faktist ändrad och kan då användas. Den skickar tillbaka player och dess värden och dess värden som är ändrade
            return player;
        }

    }
}
