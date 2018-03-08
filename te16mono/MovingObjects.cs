using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te16mono
{

    enum Oriantation {Up, Down, Left, Right}
    class MovingObjects
    {

        protected Random rng;
        public int health;
        public Vector2 velocity, position;
        protected Texture2D texture;
        protected float acceleration = (float)0.5;
        protected bool walkLeft;
        public int damage;
        public bool canStandOn;
        protected bool canJump;
        protected float maxSpeed;
        protected float maxX, minX;


        //Måla ut allting

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, position, Color.White);
        }



        //Få objektets hitbox
        public Rectangle Hitbox
        {
            get
            {
                Rectangle hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                return hitbox;
            }
        }

        //Gammal intersect
        /*

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
        public virtual void Update()
        { }

        //Tar reda på vilken sida utav objektet som hitboxen befinner sig
        //Fungerar hyfsat bra men kollisionen underifrån kan göras bättre
        protected Oriantation CheckCollision(Rectangle collided)
        {
            //Om den är till vänster
            if (Hitbox.Intersects(new Rectangle(collided.X - collided.Width, collided.Y + (int)velocity.Y + 1, collided.Width, collided.Height)))
                return Oriantation.Left;
            //Om den är till höger
            else if (Hitbox.Intersects(new Rectangle(collided.X + collided.Width, collided.Y + (int)velocity.Y + 1, collided.Width, collided.Height)))
                return Oriantation.Right;
            //Om den är över
            else if (Hitbox.Intersects(new Rectangle(collided.X, collided.Y - collided.Height, collided.Width, collided.Height)))
                return Oriantation.Up;
            //Om den är under
            else
                return Oriantation.Down;
        }
    }





}
