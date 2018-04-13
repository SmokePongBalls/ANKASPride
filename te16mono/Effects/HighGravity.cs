using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{
    class HighGravity : Point
    {
        public HighGravity(Vector2 position, Texture2D texture, int worth) : base(position, texture, worth)
        {
            //Dess namn som används används i Xml loader för att veta vilket object det är
            name = "HighGravity";
        }

        public override Player Intersect(GameTime gameTime, Player player)
        {
            //ser till så att programmet vet att player är under en effekt
            player.underEffect = true;
            //ser till så att programmet vet vilken sorts effect det är
            player.effect = "HighGravity";
            //ser till så att players "underEffect" och "effect" blir faktist ändrad och kan då användas. Den skickar tillbaka player och dess värden som är ändrade
            return player;
        }

    }
}
