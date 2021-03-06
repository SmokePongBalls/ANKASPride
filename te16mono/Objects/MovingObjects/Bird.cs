﻿using Microsoft.Xna.Framework;
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
            damage = 1;
            canStandOn = true;
            //Bestämmer hur långt den får gå
            this.maxX = maxX;
            this.minX = minX;
            timeToShoot = 4000; //Tiden innan första skottet skjuts (milisekunder)
            health = 5;
            solid = true;
        }
        //Anton
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
            //När timeToShoot är 0 eller mindre ska den skjuta ett skott neråt
            timeToShoot -= gameTime.ElapsedGameTime.Milliseconds;
            if (timeToShoot <= 0)
            {
                Main.Shoot("regular", new Vector2(0, 5), new Vector2(position.X + texture.Width / 2, position.Y + texture.Height + 1), 1, 100000, false);
                timeToShoot = 500;
            }
            velocity.Y = 0;
        }
        //Metoden som ser till att objektet rör på sig Anton
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

        //Om den krockar med ett objekt Anton
        public override ObjectsBase Intersect(ObjectsBase collided)
        {
            //Ser till så att den inte krockat med sig själv
            //Är mest ett failsafe ifall alla movingObjects ligger i samma lista
            if (Hitbox != collided.Hitbox)
            {
                Oriantations oriantation = CheckCollision(collided.Hitbox);

                if (oriantation == Oriantations.Right)
                {
                    //Ser till så att objekten inte längre är innuti varandra
                    position.X -= velocity.X;
                    //Säger ut objektet att inte längre åka åt vänster
                    walkLeft = false;
                    //Återställer acceleration och velocity så den inte fortsätter in i objektet
                    acceleration = 0;
                    velocity.X = acceleration;
                }
                else if (oriantation == Oriantations.Left)
                {
                    //Ser till så att objekten inte längre är innuti varandra
                    position.X -= velocity.X;
                    //Säger ut objektet att inte längre åka åt höger
                    walkLeft = true;
                    //Återställer acceleration och velocity så den inte fortsätter in i objektet
                    acceleration = 0;
                    velocity.X = acceleration;
                }
            }
            return collided;
        }
        //Om objektet blir träffad av en projektil Anton
        public override bool ProjectileIntersect(int damage, Oriantations oriantation)
        {
           
            health -= damage;
            return true;
        }
    }

}
