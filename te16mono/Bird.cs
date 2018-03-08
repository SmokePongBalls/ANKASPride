using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te16mono
{
    class Bird : MovingObjects
    {

        public Bird(int seed, Texture2D texture, Vector2 position, bool walkLeft, float maxSpeed, float maxX, float minX)
        {
            this.texture = texture;
            this.position = position;
            rng = new Random(seed);
            this.walkLeft = walkLeft;
            this.maxSpeed = maxSpeed;
            velocity = new Vector2(0);
            acceleration = 0;
            damage = 0;
            canStandOn = true;
            //Bestämmer hur långt den får gå
            this.maxX = maxX;
            this.minX = minX;
        }

        public override void Update()
        {

            // Om den inte har uppnåt maxfart
            if (acceleration < maxSpeed && acceleration > -maxSpeed)
            {
                //Om den ska gå åt vänster
                if (!walkLeft)
                {
                    acceleration += (float)0.01;
                }
                //Om den ska åka höger
                else
                {
                    acceleration -= (float)0.01;
                }

                velocity.X += acceleration;
            }
            position.X += velocity.X;


            //Om den har nått sin maxposition på X

            if (position.X + texture.Width >= maxX && walkLeft == false)
            {
                position.X = maxX - texture.Width;
                walkLeft = true;
                acceleration = 0;
                velocity.X = acceleration;
            }

            //Om den har nått minposition utav X

            if (position.X < minX && walkLeft == true)
            {
                position.X = minX;
                walkLeft = false;
                acceleration = 0;
                velocity.X = acceleration;
            }

        }

        public override void Intersect(Rectangle collided, Vector2 collidedVelocity, int damage, bool collidedCanStandOn)
        {
            //Ser till så att den inte krockat med sig själv
            //Är mest ett failsafe ifall alla movingObjects ligger i samma lista
            if (Hitbox != collided)
            {
                Oriantation oriantation = CheckCollision(collided);

                if (oriantation == Oriantation.Right)
                {
                    //Ser till så att objekten inte längre är innuti varandra

                    position.X -= velocity.X;

                    //position.X = collided.X + collided.Width - velocity.X;


                    //Säger ut fienden att gå åt andra hållet
                    walkLeft = false;
                    //Återställer acceleration och velocity så den inte fortsätter in i objektet
                    acceleration = 0;
                    velocity.X = acceleration;
                }
                else if (oriantation == Oriantation.Left)
                {
                    //Ser till så att objekten inte längre är innuti varandra
                    position.X -= velocity.X;

                    //position.X = collided.X + collided.Width - velocity.X;

                    //Säger ut fienden att gå åt andra hållet
                    walkLeft = true;
                    //Återställer acceleration och velocity så den inte fortsätter in i objektet
                    acceleration = 0;
                    velocity.X = acceleration;
                }
            }
        }

    }

}
