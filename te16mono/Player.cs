using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te16mono
{
    class Player : MovingObjects
    {
        //Ha kvar "points" ifall vi använder det senare.
        public int points;
        private bool canJump;


        //kontroller
        public Keys up, down, left, right;
        KeyboardState pressedKeys;


        // "Seed" är tillför att se till så att alla object som -->
        // --> vill ha ett random värde får olika värde. Olika seeds olika random värden.
        public Player(int seed, Texture2D texture)
        {
            position = new Vector2();
            velocity = new Vector2();
            this.texture = texture;
            canJump = true;
            health = 10;
            //Initiera värden

        }

        public override void Update()
        {

            velocity = velocity * (float)0.95;
            velocity.Y += Program.Gravity;
            //Spellogik
            pressedKeys = Keyboard.GetState();

            
            if (pressedKeys.IsKeyDown(left))
                velocity.X -= acceleration;
            if (pressedKeys.IsKeyDown(down))
                velocity.Y += acceleration;
            if (pressedKeys.IsKeyDown(right))
                velocity.X += acceleration;
            if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (canJump == true)
                {
                    velocity.Y -= 30;
                    canJump = false;
                }
                
            }


            //Själva: Ordna styrning för a, s, d också

            position += velocity;

            
            /*
             * PLATTFORMAR FUNGERAR NU
             * 
            //Ser till så att karaktären inte kan åka ur skärmen.
            //Får tas bort senare för att det är lättare att testa om vi har det så här.
            if(position.X<0)
            {
                position.X = 0;
                velocity.X = -velocity.X;
            }

            if (position.Y < 0)
            {
                position.Y = 0;
                velocity.Y = -velocity.Y;
            }

            if (position.X+texture.Width >1920)
            {
                position.X = 1920-texture.Width;
                velocity.X = -velocity.X;
            }

            if (position.Y + texture.Height> 1080)
            {
                position.Y = 1080- texture.Height;
                velocity.Y = -velocity.Y;
            }
            */
        }

        public override void Intersect(Rectangle collided,  Vector2 collidedVelocity, int damage, bool collidedCanStandOn)
        {
            //Ser till så att den inte krockat med sig själv
            //Är mest ett failsafe ifall alla movingObjects ligger i samma lista
            if (Hitbox != collided)
            {
                Oriantation oriantation = CheckCollision(collided);

                //Om objektet har en damage
                if (damage > 0)
                {
                    if (oriantation == Oriantation.Up && collidedCanStandOn)
                    {
                        //Får samma y velocity som objektet det krockar med
                        //Vi kanske kan göra fungerande hissar med det här
                        velocity.Y = collidedVelocity.Y;
                        //Ser till så att objekten inte längre är innuti varandra
                        position.Y = collided.Y - Hitbox.Height;
                        //Står på solid mark så man får hoppa igen
                        canJump = true;
                    }
                    else if (oriantation == Oriantation.Up && collidedCanStandOn == false)
                    {
                        //Slänger den upp i luften
                        velocity.Y = -10;
                        //Ser till så att objekten inte längre är innuti varandra
                        position.Y = collided.Y - Hitbox.Height;
                    }
                    else if (oriantation == Oriantation.Down)
                    {
                        
                        //Ifall player åker upp i objektet
                        if (velocity.Y < 0)
                            position.Y -= velocity.Y;

                        //Återstället velocity
                        velocity.Y = 0;
                        
                        //Ser till så att objekten inte längre är innuti varandra

                    }
                    else if (oriantation == Oriantation.Right)
                    {
                        //Ser till så att objekten inte längre är innuti varandra
                        position.X = collided.X + collided.Width - velocity.X;
                        //Ger den en slänger den åt sidan skadad
                        velocity.X = 25;
                        velocity.Y = 10;
                        health -= damage;

                    }
                    else if (oriantation == Oriantation.Left)
                    {
                        //Ser till så att objekten inte längre är innuti varandra
                        position.X = collided.X - velocity.X - texture.Width;
                        //Återställer velocity så den inte fortsätter in i objektet
                        velocity.X = - 25;
                        velocity.Y = 10;
                        health -= damage;
                    }
                }

                //Om objektet inte har någon damage (plattformar)
                else {
                    if (oriantation == Oriantation.Up)
                    {
                        //Får samma y velocity som objektet det krockar med
                        //Vi kanske kan göra fungerande hissar med det här
                        velocity.Y = collidedVelocity.Y;
                        //Ser till så att objekten inte längre är innuti varandra
                        position.Y = collided.Y - Hitbox.Height;
                        //Står på solid mark så man får hoppa igen
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
                        //Återställer velocity så den inte fortsätter in i objektet
                        velocity.X = 0;

                    }
                    else if (oriantation == Oriantation.Left)
                    {
                        //Ser till så att objekten inte längre är innuti varandra
                        position.X -= velocity.X;

                        //position.X = collided.X - velocity.X - texture.Width;
                        //Återställer velocity så den inte fortsätter in i objektet
                        velocity.X = 0;
                    }
                }
                

                
            }
        }


    }
    

}
