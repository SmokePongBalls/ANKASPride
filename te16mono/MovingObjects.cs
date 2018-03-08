using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te16mono
{


    abstract class MovingObjects : Objects
    {

        protected Random rng;
        protected float acceleration = (float)0.5;
        protected bool walkLeft;
        public int damage;
        public bool canStandOn;
        protected bool canJump;
        protected float maxSpeed;
        protected float maxX, minX;
        public int health;


        //Måla ut allting

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, position, Color.White);
        }

        /* <summary>Gammal intersect </summary>

        public virtual void Intersect(Rectangle collided, Vector2 collidedVelocity, int damage, bool collidedCanStandOn)
        {
            //Ser till så att den inte krockat med sig själv
            //Är mest ett failsafe ifall alla movingObjects ligger i samma lista
            if (Hitbox != collided)
            {
                Oriantation oriantation = CheckCollision(collided);

                if (oriantation == Oriantation.Up)
                {
                    //Får samma y velocity som objektet det krockar med
                    //Vi kanske kan göra fungerande hissar med det här
                    position.Y -= velocity.Y;
                    velocity.Y = collidedVelocity.Y;
                    //Ser till så att objekten inte längre är innuti varandra
                    
                }
                else if (oriantation == Oriantation.Down)
                {
                    health = 0;
                }
                else if (oriantation == Oriantation.Right)
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
        */

        //<Summary> Hitboxen</summary>
        public override Rectangle Hitbox
        {
            get
            {
                Rectangle hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                return hitbox;
            }
        }


        public virtual void Intersect(Rectangle collided, Vector2 collidedVelocity, int damage, bool collidedCanStandOn)
        {
            //Ser till så att den inte krockat med sig själv
            //Är mest ett failsafe ifall alla movingObjects ligger i samma lista
            if (Hitbox != collided)
            {
                Oriantation oriantation = CheckCollision(collided);

                if (oriantation == Oriantation.Up)
                {
                    //Får samma y velocity som objektet det krockar med
                    //Vi kanske kan göra fungerande hissar med det här
                    position.Y -= velocity.Y;
                    velocity.Y = collidedVelocity.Y;
                    //Ser till så att objekten inte längre är innuti varandra
                    canJump = true;

                }
                else if (oriantation == Oriantation.Down)
                {
                    //Ifall player åker upp i objektet
                    if (velocity.Y < 0)
                        position.Y -= velocity.Y;
                    
                    //Återstället velocity
                    velocity.Y = 0;
                }
                else if (oriantation == Oriantation.Right)
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
        public abstract void Update();


        //Tar reda på vilken sida utav objektet som hitboxen befinner sig
        //Fungerar hyfsat bra men kollisionen underifrån kan göras bättre
        
    }





}
