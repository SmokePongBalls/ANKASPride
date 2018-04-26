﻿

namespace te16mono
{
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
        //Gör skada på objektet som den krockat med. Ifall någon skada blev gjord blir health -1 och objektet blir borttaget ifrån objects listan
        public override ObjectsBase Intersect(ObjectsBase collided)
        {
            Oriantations oriantation = CheckPlayerCollision(collided.Hitbox, collided.velocity);

            if(collided.ProjectileIntersect(damage, oriantation))
            health = -1;

            return collided;
        }

    }
}