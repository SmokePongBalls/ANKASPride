using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{
    //Hugo F
    //ändrade så att alla effekter ärver av Effekt nu istället. Hugo F
    class Health : Effect
    {
        public Health(Vector2 position, Texture2D texture, int worth) : base(position, texture, worth)
        {
            // Dess namn som används används i Xml loader för att veta vilket object det är
             name = "Health"; 
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override Player PlayerIntersect(Player player)
        {
            health = -4;
            //ser till så att players health inte kan gå över "maxHealth"
            if (player.health < player.maxHealth)
                //"worth" är då en int som används för alla objekten i XmlLoader. Just för health så används det för att öka player health med det värde worth har.
                player.health += worth;
            //ser till så att players "health" blir faktist ändrad. Den skickar tillbaka "player" och dess värden och dess värden som är ändrade
            return player;
        }
    }
}