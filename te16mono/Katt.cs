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
            velocity = new Vector2(0);
        }

        public void Update()
        {

            // Om den inte har uppnåt maxfart
            if (acceleration < maxSpeed)
            {
                //Om den ska gå åt vänster
                if (walkLeft)
                {
                    if (acceleration == maxSpeed)
                    {
                        acceleration -= (float)0.01;
                    }
                }
            //Om den ska åka höger
                else
                {
                    acceleration += (float)0.01;
                }

                velocity.X += acceleration;
            }
            velocity.Y += gravity;


            position += velocity;

        }

        public void Intersect(Rectangle collided)
        {
            velocity.Y = (float)0;
            position.Y -= (float)0.5;
        }

    }
}
