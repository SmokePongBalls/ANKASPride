using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{

    //Ánton

    class Frog : MovingObjects
    {
        public Frog(Texture2D texture, Vector2 position, bool walkLeft, float maxSpeed, float maxX, float minX)
        {
            name = "Frog";
            this.texture = texture;
            this.position = position;
            this.walkLeft = walkLeft;
            this.maxSpeed = maxSpeed;
            velocity = new Vector2(0);
            acceleration = 0;
            damage = 1;
            canStandOn = true; //Om denna är true kan man stå på objektet utan att ta skada
            //Bestämmer hur långt den får gå
            this.maxX = maxX;
            this.minX = minX;
            health = 5;
            solid = true;
        }

        public override void Update(GameTime gameTime)
        {

            TryJump();
            
            Move();


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
        void TryJump()
        {
            //Om den ska hoppa
            if (canJump == true)
            {
                velocity.Y -= 20;
                canJump = false;
            }
        }
    }

}
