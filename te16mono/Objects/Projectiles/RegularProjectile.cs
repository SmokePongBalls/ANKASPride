using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{
    //Anton har gjort allt i den här klassen

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

        //Ändrar position åt det hållet den ska och drar ner health. Ifall health < 0 tas den bort ur objects listan 
        public override void Update(GameTime gameTime)
        {
            position += velocity;
            health -= gameTime.ElapsedGameTime.Milliseconds;
        }
        
    }
}
