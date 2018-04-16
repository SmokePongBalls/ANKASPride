using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace te16mono
{
    //Anton

    class Bird : MovingObjects
    {
        int timeToShoot;
        public Bird(Texture2D texture, Vector2 position, bool walkLeft, float maxSpeed, float maxX, float minX)
        {
            name = "Bird";
            this.texture = texture;
            this.position = position;
            this.walkLeft = walkLeft;
            this.maxSpeed = maxSpeed;
            velocity = new Vector2(0);
            acceleration = 0;
            damage = 0;
            canStandOn = true;
            //Bestämmer hur långt den får gå
            this.maxX = maxX;
            this.minX = minX;
            timeToShoot = 4000;
            health = 5;
        }

        public override void Update(GameTime gameTime)
        {

            //Ändrar position
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

            timeToShoot -= gameTime.ElapsedGameTime.Milliseconds;
            if (timeToShoot <= 0)
            {
                Main.Shoot("regular", new Vector2(0, 5), new Vector2(position.X + texture.Width / 2, position.Y + texture.Height + 1), 1, 100000);
                timeToShoot = 500;
            }
            velocity.Y = 0;
        }

        protected override void Move()
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
        }

        //Om den krockar med ett objekt (OBS INTE PROJECTILES)
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
        //Om objektet blir träffad av en projektil
        public override void ProjectileIntersect(Rectangle collided, int damage)
        {
            health -= damage;
        }
    }

}
