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
    }


}
