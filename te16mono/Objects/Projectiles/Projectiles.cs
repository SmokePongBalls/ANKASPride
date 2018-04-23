using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{
    //Anton har gjort allt i den här klassen

    abstract class Projectiles : ObjectsBase
    {
        //Hur många milisekunder till som det ska leva

        public override Player PlayerIntersect(Player player)
        {
            //overridear projectile intersect för player så att Immortality effecten kan användas. Hugo F
            if (player.canBeDamaged)
            {
                Intersect(player);
            }
            health = -1;
            return player;
        }
        public override ObjectsBase Intersect(ObjectsBase collided)
        {
            Oriantations oriantation = CheckPlayerCollision(collided.Hitbox, collided.velocity);

            if(collided.ProjectileIntersect(damage, oriantation))
            health = -1;

            return collided;
        }

    }
}
