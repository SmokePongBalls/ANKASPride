using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{


    //Anton

    class Katt : MovingObjects
    {

        public Katt(Texture2D texture, Vector2 position, bool walkLeft, float maxSpeed, float maxX, float minX)
        {
            name = "Katt";
            this.texture = texture;
            this.position = position;
            this.walkLeft = walkLeft;
            this.maxSpeed = maxSpeed;
            velocity = new Vector2(0);
            acceleration = 0;
            damage = 1;
            canStandOn = true;
            //Bestämmer hur långt den får gå
            this.maxX = maxX;
            this.minX = minX;
            health = 1;
        }

        public override void Update(GameTime gameTime)
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

            /*
             * PLATTFORMAR FUNGERAR NU GGWP
             * 
            //Längst ner på skärmen
            //DETTA MÅSTE BORT NÄR VI HAR FUNGERADE PLATTFORMAR
            if (position.Y + texture.Height > 1080)
            {
                position.Y = 1080 - texture.Height;
                velocity.Y = -velocity.Y * (float)0.5 ;
            }
            */
        }

        
            

    }
}

