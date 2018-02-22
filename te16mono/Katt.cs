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
        private float maxX, minX;


        public Katt(int seed, Texture2D texture, Vector2 position, bool walkLeft, float maxSpeed, float maxX, float minX)
        {
            this.texture = texture;
            this.position = position;
            rng = new Random(seed);
            this.walkLeft = walkLeft;
            this.maxSpeed = maxSpeed;
            velocity = new Vector2(0);

            //Bestämmer hur långt den får gå
            this.maxX = maxX;
            this.minX = minX;
        }

        public void Update()
        {

            // Om den inte har uppnåt maxfart
            if (acceleration < maxSpeed)
            {
                //Om den ska gå åt vänster
                if (!walkLeft)
                {
                        acceleration -= (float)0.01;
                }
            //Om den ska åka höger
                else
                {
                    acceleration += (float)0.01;
                }

                velocity.X += acceleration;
            }
            velocity.Y += gravity;
            velocity.X += acceleration;

            position.Y += velocity.Y;
            position.X += velocity.X;


            if (position.X + texture.Width > maxX)
            {
                position.X = 1920 - texture.Width;
                walkLeft = false;
            }

            if (position.X < minX)
            {
                position.X = 0;
                walkLeft = true;
            }

            if (position.Y + texture.Height > 1080)
            {
                position.Y = 1080 - texture.Height;
                velocity.Y = -velocity.Y * (float)0.5 ;
            }

        }

        public void Intersect(Rectangle collided)
        {
            velocity.Y = (float)0;
            position.Y -= (float)0.5;
        }

    }
}

