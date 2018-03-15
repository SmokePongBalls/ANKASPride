using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{
    class Hedgehog : Katt
    {
        public Hedgehog(Texture2D texture, Vector2 position, bool walkLeft, float maxSpeed, float maxX, float minX) : base(texture, position, walkLeft, maxSpeed, maxX, minX)
        {
            this.texture = texture;
            this.position = position;
            this.walkLeft = walkLeft;
            this.maxSpeed = maxSpeed;
            velocity = new Vector2(0);
            acceleration = 0;
            damage = 1;
            canStandOn = true;
            //Bestämmer hur långt den får gå
            this.maxX = maxX;
            this.minX = minX;
            health = 1;
            canStandOn = false;
        }
    }
}
