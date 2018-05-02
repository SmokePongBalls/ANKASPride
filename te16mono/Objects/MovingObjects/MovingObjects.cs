using Microsoft.Xna.Framework;
using System;

namespace te16mono 
{
    //Anton 

    public abstract class MovingObjects : ObjectsBase
    {
        protected Random rng;
        public float maxSpeed;
        public float maxX, minX;

        //Metoden som ser till så att objektet byter position Anton
        protected virtual void Move()
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
            velocity.Y += Program.Gravity;

            position.Y += velocity.Y;
            position.X += velocity.X;
        }
        //Ifall objektet krockar med en projektil Anton
        

        //All de olika update är så olika varande att de alla måste skriva över
    }





}