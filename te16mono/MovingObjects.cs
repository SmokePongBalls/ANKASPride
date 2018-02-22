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
        protected int health;
        public Vector2 velocity, position;
        protected Texture2D texture;
        public float gravity = (float)0.5;
        protected float acceleration = (float)0.5;
        protected bool walkLeft;


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


        public virtual void Intersect(Rectangle collided, Vector2 collidedVelocity)
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
                    velocity.Y = collidedVelocity.Y;
                    //Ser till så att objekten inte längre är innuti varandra
                    position.Y = collided.Y - texture.Height;
                }
                else if (oriantation == Oriantation.Down)
                {
                    //Får samma y velocity som objektet det krockar med
                    //Vi kanske kan göra fungerande hissar med det här
                    velocity.Y = collidedVelocity.Y;
                    //Ser till så att objekten inte längre är innuti varandra
                    position.Y = collided.Y + texture.Height;
                }
                else if (oriantation == Oriantation.Right)
                {
                    //Ser till så att objekten inte längre är innuti varandra
                    position.X = collided.X + collided.Width - velocity.X;
                    //Säger ut fienden att gå åt andra hållet
                    walkLeft = false;
                    //Återställer acceleration och velocity så den inte fortsätter in i objektet
                    acceleration = 0;
                    velocity.X = acceleration;
                }
                else if (oriantation == Oriantation.Left)
                {
                    //Ser till så att objekten inte längre är innuti varandra
                    position.X = collided.X - velocity.X - texture.Width;
                    //Säger ut fienden att gå åt andra hållet
                    walkLeft = true;
                    //Återställer acceleration och velocity så den inte fortsätter in i objektet
                    acceleration = 0;
                    velocity.X = acceleration;
                }
            }
        }

        //Tar reda på vilken sida utav objektet som hitboxen befinner sig
        //Fungerar hyfsat bra men kollisionen underifrån kan göras bättre
        protected Oriantation CheckCollision(Rectangle collided)
        {
            //Om den är till vänster
            if (Hitbox.Intersects(new Rectangle(collided.X - collided.Width, collided.Y + (int)velocity.Y + 1, collided.Width, collided.Height + ((int)velocity.Y - 1) * 2)))
                return Oriantation.Left;
            //Om den är till höger
            else if (Hitbox.Intersects(new Rectangle(collided.X + collided.Width, collided.Y + (int)velocity.Y + 1, collided.Width, collided.Height + ((int)velocity.Y - 1)*2)))
                return Oriantation.Right;
            //Om den är över
            else if (Hitbox.Intersects(new Rectangle(collided.X + (int)velocity.X, collided.Y - collided.Height, collided.Width + (int)velocity.X, collided.Height)))
                return Oriantation.Up;
            //Om den är under
            else
                return Oriantation.Down;
        }
    }





}
