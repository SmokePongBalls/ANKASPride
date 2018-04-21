using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te16mono
{
    public enum Oriantations { Left, Right, Up, Down }
    public abstract class ObjectsBase
    {
        public string name;
        public Vector2 velocity, position;
        protected Texture2D texture;
        public int health, damage;
        public bool canStandOn, canJump, walkLeft, solid;
        protected float acceleration = (float)0.5;
        

        //All de olika update är så olika varandra att de alla måste skriva över
        public abstract void Update(GameTime gameTime);


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Hitbox, Color.White);
        }
        public virtual Rectangle Hitbox
        {
            get
            {
                return new Rectangle((int)Math.Round(position.X), (int)Math.Round(position.Y), texture.Width, texture.Height);
            }
        }

        public virtual void ProjectileIntersect(int damage, Oriantations oriantation)
        {
            if (oriantation == Oriantations.Left)
                velocity.X += 20 * damage;
            //Om den är till höger
            if (oriantation == Oriantations.Right)
                velocity.X -= 20 * damage;
            //Om den är över
            if (oriantation == Oriantations.Up)
                velocity.Y += 20 * damage;
            //Om den är under
            if (oriantation == Oriantations.Down)
                velocity.Y -= 20 * damage;

            health -= damage;
        }
        //Standard intersectmetod 
        public virtual ObjectsBase Intersect(ObjectsBase collided)
        {
            //Ser till så att den inte krockat med sig själv
            //Är mest ett failsafe ifall alla movingObjects ligger i samma lista
            if (Hitbox != collided.Hitbox && collided.solid)
            {
                //Får reda på vilken sida objektet krockade ifrån (Upp ner höger vänster)
                Oriantations oriantation = CheckCollision(collided.Hitbox);

                if (oriantation == Oriantations.Up)
                {
                    //Får samma y velocity som objektet det krockar med
                    //Vi kanske kan göra fungerande hissar med det här
                    position.Y -= velocity.Y;
                    velocity.Y = collided.velocity.Y;
                    //Ser till så att objekten inte längre är innuti varandra
                    canJump = true;

                }
                else if (oriantation == Oriantations.Down)
                {
                    //Ifall det åker upp i objektet
                    if (velocity.Y < 0)
                        position.Y -= velocity.Y;

                    //Återstället velocity
                    velocity.Y = 0;
                }
                else if (oriantation == Oriantations.Right)
                {
                    //Ser till så att objekten inte längre är innuti varandra
                    position.X -= velocity.X;
                    //Säger ut fienden att gå åt andra hållet
                    walkLeft = false;
                    //Återställer acceleration och velocity så den inte fortsätter in i objektet
                    acceleration = 0;
                    velocity.X = acceleration;
                }
                else if (oriantation == Oriantations.Left)
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
            return collided;
        }

        protected Oriantations CheckCollision(Rectangle collided)
        {
            //Om den är till vänster
            if (Hitbox.Intersects(new Rectangle(collided.X - collided.Width, collided.Y + (int)velocity.Y + 1, collided.Width, collided.Height)))
                return Oriantations.Left;
            //Om den är till höger
            else if (Hitbox.Intersects(new Rectangle(collided.X + collided.Width, collided.Y + (int)velocity.Y + 1, collided.Width, collided.Height)))
                return Oriantations.Right;
            //Om den är över
            else if (Hitbox.Intersects(new Rectangle(collided.X, collided.Y - collided.Height, collided.Width, collided.Height)))
                return Oriantations.Up;
            //Om den är under
            else
                return Oriantations.Down;
        }


        //Tar reda på vilken sida utav objektet som hitboxen befinner sig
        //Fungerar hyfsat bra men kollisionen underifrån kan göras bättre
        //Anton
        protected virtual Oriantations CheckPlayerCollision(Rectangle collided, Vector2 collidedVelocity)
        {
            //Om den är till vänster
            if (collided.Intersects(new Rectangle((int)position.X - Width, (int)position.Y + (int)collidedVelocity.Y + (int)velocity.Y + 1, Width, Height)))
                return Oriantations.Left;
            //Om den är till höger
            else if (collided.Intersects(new Rectangle((int)position.X + Width, (int)position.Y + (int)collidedVelocity.Y + 1, Width, Height)))
                return Oriantations.Right;
            //Om den är över
            else if (collided.Intersects(new Rectangle((int)position.X, (int)position.Y - Height, Width, Height)))
                return Oriantations.Up;
            //Om den är under
            else
                return Oriantations.Down;
        }



        //Main delen för intersect Anton
        public virtual Player PlayerIntersect(Player player)
        {

            //Får reda på vilken sida objektet krockade ifrån (Upp ner höger vänster)
            Oriantations oriantation = CheckPlayerCollision(player.Hitbox, player.velocity);

            //Om objektet har en damage
            if (damage > 0 && player.canBeDamaged)
            {
                player = DamageFunction(player, oriantation);
            }

            //Om objektet inte har någon damage (plattformar)
            else
            {
                player = NoDamageFunction(player, oriantation);
            }
            return player;
        }
        //Ifall objektet har krockat med något men inte tar skada på player körs dem här Anton
        private Player NoDamageFunction(Player player, Oriantations oriantation)
        {
            if (oriantation == Oriantations.Up)
            {
                //Får samma y velocity som objektet det krockar med
                //Vi kanske kan göra fungerande hissar med det här
                player.velocity.Y = velocity.Y;
                //Ser till så att objekten inte längre är innuti varandra
                player.position.Y = position.Y - player.Hitbox.Height + 0.5f;
                //Står på solid mark så man får hoppa igen
                player.SetCanJump(Oriantations.Up);
                player.lastTouchedSurface = Oriantations.Up;
            }
            else if (oriantation == Oriantations.Down)
            {
                //Ifall player åker upp i objektet
                if (player.velocity.Y < 0)
                    player.position.Y -= player.velocity.Y;

                //Återstället velocity
                player.velocity.Y = 0;
            }
            else if (oriantation == Oriantations.Right)
            {
                //Ser till så att objekten inte längre är innuti varandra
                player.position.X -= player.velocity.X;

                //position.X = collided.X + collided.Width - velocity.X;
                //Återställer velocity så den inte fortsätter in i objektet
                player.velocity.X = 0;

                //Om man inte rörde en högervägg senast
                player.SetCanJump(Oriantations.Right);

                player.lastTouchedSurface = Oriantations.Right;

            }
            else if (oriantation == Oriantations.Left)
            {
                //Ser till så att objekten inte längre är innuti varandra
                player.position.X -= player.velocity.X;

                //position.X = collided.X - velocity.X - texture.Width;
                //Återställer velocity så den inte fortsätter in i objektet
                player.velocity.X = 0;


                //Om inte rörde en vänstervägg senast
                player.SetCanJump(Oriantations.Left);

                player.lastTouchedSurface = Oriantations.Left;
            }
            return player;
        }
        //Ifall objektet har krockat med något och tar skada på player körs den här. Den körs också ifall objektet som krockats med tar skada men kan stås på Anton
        private Player DamageFunction(Player player, Oriantations oriantation)
        {
            if (oriantation == Oriantations.Up && canStandOn)
            {
                //Får samma y velocity som objektet det krockar med
                //Vi kanske kan göra fungerande hissar med det här
                player.velocity.Y = velocity.Y;
                //Ser till så att objekten inte längre är innuti varandra
                player.position.Y = position.Y - player.Hitbox.Height;
                //Står på solid mark så man får hoppa igen
                player.SetCanJump(oriantation);

                player.lastTouchedSurface = Oriantations.Up;
            }
            else if (oriantation == Oriantations.Up && canStandOn == false)
            {
                //Slänger upp player i luften
                player.velocity.Y = -25;
                player.velocity.X = 10;

                player.health -= damage;
                //Ser till så att objekten inte längre är innuti varandra
                player.position += player.velocity;
            }
            else if (oriantation == Oriantations.Down)
            {

                //Ifall player åker upp i objektet
                if (player.velocity.Y < 0)
                    player.position.Y -= player.velocity.Y;

                //Återstället velocity
                player.velocity.Y = 0;

                //Ser till så att objekten inte längre är innuti varandra

            }
            else if (oriantation == Oriantations.Right)
            {
                //Ser till så att objekten inte längre är innuti varandra
                player.position.X = position.X + texture.Width - velocity.X;
                //Ger den en slänger den åt sidan skadad
                player.velocity.X = 25;
                player.velocity.Y = 10;
                player.health -= damage;

            }
            else if (oriantation == Oriantations.Left)
            {
                //Ser till så att objekten inte längre är innuti varandra
                player.position.X = position.X - player.velocity.X - player.texture.Width;
                //Återställer velocity så den inte fortsätter in i objektet
                player.velocity.X = -25;
                player.velocity.Y = 10;
                player.health -= damage;
            }
            return player;
        }


        public virtual int Width
        {
            get
            {
                return texture.Width;
            }
        }
        public virtual int Height
        {
            get
            {
                return texture.Height;
            }
        }
    }
}
