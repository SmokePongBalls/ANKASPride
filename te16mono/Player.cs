using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace te16mono
{
    //Anton, Hugo F

    class Player : MovingObjects
    {
        //Ha kvar "points" ifall vi använder det senare.
        public int points;
        public int maxHealth = 10;
        private Oriantation lastTouchedSurface;
        private bool holdingJump = true;
        private int shootCooldown;


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
            holdingJump = false;
            shootCooldown = 0;
            rng = new Random(seed);
            //Initiera värden

        }

        public override void Update(GameTime gameTime)
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
                if (canJump == true && holdingJump == false)
                {
                    velocity.Y -= 30;
                    canJump = false;
                }
                holdingJump = true;
            }
            else
            {
                holdingJump = false;
            }



            //<summary>De som kollar ifall man trycker på skjutknapparna</summary>
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && shootCooldown <= 0)
            {
                Main.Shoot("regular", new Vector2(-10 + velocity.X, velocity.Y / 4 + 0), new Vector2(position.X - 21, position.Y), 1, 100000);
                shootCooldown = 500;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up) && shootCooldown <= 0)
            {
                Main.Shoot("regular", new Vector2(0 + velocity.X / 4,velocity.Y -10), new Vector2(position.X , position.Y - 21), 1, 100000);
                shootCooldown = 500;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && shootCooldown <= 0)
            {
                Main.Shoot("regular", new Vector2(+10 + velocity.X, velocity.Y/4 + 0), new Vector2(position.X + texture.Width + 1, position.Y), 1, 100000);
                shootCooldown = 500;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) && shootCooldown <= 0)
            {
                Main.Shoot("regular", new Vector2(velocity.X/4, velocity.Y + 10), new Vector2(position.X + texture.Width/2, position.Y + texture.Height), 1, 100000);
                shootCooldown = 500;
            }
            else
                shootCooldown -= gameTime.ElapsedGameTime.Milliseconds;


            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                position = new Vector2(0);
            }

                position += velocity;

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
                        lastTouchedSurface = Oriantation.Up;
                    }
                    else if (oriantation == Oriantation.Up && collidedCanStandOn == false)
                    {
                        //Slänger den upp i luften
                        velocity.Y = -25;

                        if (rng.Next(0, 2) == 1)
                            velocity.X = -10;
                        else
                            velocity.X = 10;

                        health -= damage;
                        //Ser till så att objekten inte längre är innuti varandra
                        position += velocity;
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
                        lastTouchedSurface = Oriantation.Up;
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

                        //Om man inte rörde en högervägg senast
                        if (lastTouchedSurface != Oriantation.Right)
                        canJump = true;

                        lastTouchedSurface = Oriantation.Right;

                    }
                    else if (oriantation == Oriantation.Left)
                    {
                        //Ser till så att objekten inte längre är innuti varandra
                        position.X -= velocity.X;

                        //position.X = collided.X - velocity.X - texture.Width;
                        //Återställer velocity så den inte fortsätter in i objektet
                        velocity.X = 0;


                        //Om inte rörde en vänstervägg senast
                        if (lastTouchedSurface != Oriantation.Left)
                            canJump = true;

                        lastTouchedSurface = Oriantation.Left;
                    }
                }
                

                
            }
        }


    }
    

}
