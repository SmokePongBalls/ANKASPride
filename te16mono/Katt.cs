using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te16mono
{




    class Katt : MovingObjects
    {

        bool walkLeft;
        float maxSpeed;
        float acceleration = 0;


        public Katt(int seed, Texture2D texture, Vector2 position, bool walkLeft, float maxSpeed)
        {
            this.texture = texture;
            this.position = position;
            rng = new Random(seed);
            this.walkLeft = walkLeft;
            this.maxSpeed = maxSpeed;
        }

        public void Update()
        {

            if (walkLeft)
            {
                if (acceleration == maxSpeed)
                {

                }
            }

        }

    }
}

