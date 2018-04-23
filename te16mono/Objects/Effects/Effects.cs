using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{
    //Hugo F
    //Tog koden från point som alla effekter ärvde ifrån hit istället så att det är mer tydligt
    public abstract class Effect : ObjectsBase
    {
        public int worth;

        public Effect(Vector2 position, Texture2D texture, int worth)
        {
            this.position = position;
            this.texture = texture;
            this.worth = worth;
            solid = false;
        }
        // Detta gör att effekterna inte kan bli skjutna och förstörda men om man skulle vilja gör så att en effekt kan bli skadad
        // så kan man bara lägga till genom att ta denna metod och i just den effekten och lägga till koden så att den kan bli skadad
        // (ta bara koden från ursprungliga "ProjectileIntersect" koden).
        public override bool ProjectileIntersect(int damage, Oriantations oriantation)
        {
            return false;
        }
    }


}
