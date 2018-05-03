using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace te16mono
{
    //Anton, Hugo F
    public class Player : MovingObjects
    {
        
        public int points;
        public int maxHealth = 10;
        public int Time = 5000;
        private int shootCooldown;
        private int whammy = 10;
        private double gravity = Program.Gravity;
        public Oriantations lastTouchedSurface;
        private bool holdingJump = true;
        public bool underEffect;
        public bool canBeDamaged = true;
        public bool isWhammy = false;
        public List<string> effects = new List<string>();
        public string effect, shootingDirection, walkDirection;
        bool resetNextUpdate;
        Texture2D shootingTexture, choosentexture;
        
        
        //kontroller
        public Keys up, down, left, right;
        KeyboardState pressedKeys;

        // "Seed" är tillför att se till så att alla object som -->
        // --> vill ha ett random värde får olika värde. Olika seeds olika random värden.
        public Player(int seed, Texture2D texture, Texture2D shootingTexture)
        {
            name = "Player";
            this.shootingTexture = shootingTexture;
            position = new Vector2();
            velocity = new Vector2();
            extraVelocity = new Vector2(0);
            this.texture = texture;
            canJump = true;
            health = 10;
            points = 0;
            holdingJump = false;
            shootCooldown = 0;
            rng = new Random(seed);
            
            //Initiera värden
        }
        public override void Draw(SpriteBatch spriteBatch)
        {         
            if (shootCooldown < -500)
              choosentexture = texture;

           else 
             choosentexture = shootingTexture;

            if (shootingDirection == "left" || walkDirection == "left")
                spriteBatch.Draw(choosentexture,position, null, Color.White,0f,Vector2.Zero,1f,SpriteEffects.FlipHorizontally,1f);

            else if(shootingDirection == "right" || walkDirection == "rigth")
                spriteBatch.Draw(choosentexture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

            else
                spriteBatch.Draw(choosentexture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }

        public override void Update(GameTime gameTime)
        {
            //Kollar om position är utanför tillåtna spelområdet
            CheckOutOfBounds();
            velocity = velocity * (float)0.95;
            velocity.Y += (float)gravity;
            //Spellogik
            pressedKeys = Keyboard.GetState();
            {
                //Alla händelser med knapptryck är i denna metod.
                KeyActions(gameTime);
            }

            //startar om din position till 0
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                ResetPosition();
            }

            position += velocity + extraVelocity;

            //kollar om player är under någon effect. Det är vad boolen är till för Hugo F
            if (underEffect == true)
            {
                //Ser till så att om player tar upp yttligare en effekt så tas den som player redan har bort. Hugo F
                //Gjorde detta eftersom effekterna över tid störde lite på whammy effekten så att den aldrig försvann. Hugo F
                if (effects.Count > 1)               
                    effects.RemoveAt(0);
                
                Effects(gameTime);              
            }

            if (extraVelocity != new Vector2(0))
            {
                resetNextUpdate = true;
            }
            else if (resetNextUpdate)
            {
                extraVelocity = new Vector2(0);
                resetNextUpdate = false;
            }
        }

        private void ResetPosition()
        {
            position = new Vector2(0);
        }

        private void KeyActions(GameTime gameTime)
        {
            if (pressedKeys.GetPressedKeys() != null)

                if (pressedKeys.IsKeyDown(left))
                {
                    velocity.X -= acceleration;
                    walkDirection = "left";
                }

            if (pressedKeys.IsKeyDown(down))
            {
                velocity.Y += acceleration;
                walkDirection = "down";
            }

            if (pressedKeys.IsKeyDown(right))
            {
                velocity.X += acceleration;
                walkDirection = "right";
            }

            //Om man har fått whammy efekten på sig så blir canJump false och då går det icke att hoppa. Hugo F = just den if-satsen 
            if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                walkDirection = "up";
                if (isWhammy)
                {
                    Whammy();
                }
                else
                {
                    Jump();
                }
            }
            else
            {
                holdingJump = false;
            }


            //<summary>De som kollar ifall man trycker på skjutknapparna</summary>
            // if (Keyboard.GetState().IsKeyDown(Keys.Left) && shootCooldown <= 0)
            if (Keyboard.GetState().IsKeyDown(Keys.RightControl) && shootCooldown <= 0)
            {
                if (walkDirection == "left")
                {
                    ShotLeft();
                    shootingDirection = "left";
                }
                //else if (Keyboard.GetState().IsKeyDown(Keys.Up) && shootCooldown <= 0)
                else if (walkDirection == "up")
                {
                    ShotUp();
                    shootingDirection = "up";
                }
                //else if (Keyboard.GetState().IsKeyDown(Keys.Right) && shootCooldown <= 0)
                else if (walkDirection == "right")
                {
                    ShotRight();
                    shootingDirection = "right";
                }
                //else if (Keyboard.GetState().IsKeyDown(Keys.Down) && shootCooldown <= 0)
                else if (walkDirection == "down")
                {
                    ShotDown();
                    shootingDirection = "down";
                }
            }
            else
                shootCooldown -= gameTime.ElapsedGameTime.Milliseconds;

        }

        private void ShotDown()
        {
            Main.Shoot("regular", new Vector2(velocity.X / 2, velocity.Y / 4 + 10), new Vector2(position.X + texture.Width / 2, position.Y + texture.Height + velocity.Y ), 1, 100000, true );
            shootCooldown = 500;
        }

        private void ShotRight()
        {
            Main.Shoot("regular", new Vector2(+10 + velocity.X / 2, 0), new Vector2(position.X + texture.Width + velocity.X, position.Y), 1, 100000, true);
            shootCooldown = 500;
        }

        private void ShotUp()
        {
            Main.Shoot("regular", new Vector2(0 + velocity.X / 2, velocity.Y / 4 - 10), new Vector2(position.X, position.Y - 21 + velocity.Y ), 1, 100000, true);
            shootCooldown = 500;
        }

        private void ShotLeft()
        {
            Main.Shoot("regular", new Vector2(-10 + velocity.X / 2, 0), new Vector2(position.X - 21 + velocity.X, position.Y), 1, 100000, true);
            shootCooldown = 500;
        }

        private void Jump()
        {
            if (canJump == true && holdingJump == false)
                if (holdingJump == false)
                {
                    velocity.Y -= 30;
                    canJump = false;
                    
                }
            holdingJump = true;
        }

        private void Whammy()
        {
            if (holdingJump == false)
            {
                whammy--;
            }
            holdingJump = true;
        }

        private void Effects(GameTime gameTime)
        {
            //kollar om player är under specifikt "Immortality" effekten
            if (effects[0] == "Immortality")
            {
                //player kan inte bli skadade om detta är false
                canBeDamaged = false;

                //ser till så att man är odödlig under en specifik tid och inte längre
                Time -= gameTime.ElapsedGameTime.Milliseconds;
                if (Time <= 0)
                {
                    //sätter tillbaka så att player inte är under effect och kan bli skadad
                    underEffect = false;
                    canBeDamaged = true;
                    Time = 5000;
                }

            }

            if (effects[0] == "Whammy")
            {
                if (whammy <= 0)
                {
                    isWhammy = false;
                    underEffect = false;
                    whammy = 10;
                }
                else
                    isWhammy = true;

            }
            else
                isWhammy = false;

            if (effects[0] == "HighGravity")
            {
                //höjer gravity så att player inte kan hoppa lika högt och åker ner snabbare
                gravity = 1;
                //ser till så att man är påverkad av ökad gravitation under en specifik tid och inte längre
                Time -= gameTime.ElapsedGameTime.Milliseconds;
                if (Time <= 0)
                {
                    //sätter tillbaka så att player inte är under effect och kan hoppa som vanligt. Dessutom så sätts timern tillbaka till 5000 milisekunder
                    underEffect = false;
                    gravity = 0.5;
                    Time = 5000;
                }

            }
        }
        //
        private void CheckOutOfBounds()
        {
            if (position.Y > 2080)
            {
                ResetPosition();
            }
        }

        //Kollar ifall man ska få hoppa
        public void SetCanJump(Oriantations oriantation)
        {
            if (oriantation != Oriantations.Up)
            {
                if (lastTouchedSurface != oriantation)
                    canJump = true;
            }
            else
                canJump = true;
        }
    }
}
