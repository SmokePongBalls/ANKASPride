using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{
    class RegularProjectile : Projectiles
    {
        public RegularProjectile(int health, int damage, Vector2 velocity, Vector2 position, Texture2D texture)
        {
            this.health = health;
            this.damage = damage;
            this.velocity = velocity;
            this.position = position;
            this.texture = texture;
        }


        public override void Update()
        {
            position += velocity;
        }
        
    }
}
