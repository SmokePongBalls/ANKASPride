using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{
    //Hugo F
    //Tog koden från point som alla effekter ärvde ifrån hit istället så att det är mer tydligt
    public class Effect
    {
        public string name;
        public Vector2 position;
        protected Texture2D texture;
        public int worth;

        public Effect(Vector2 position, Texture2D texture, int worth)
        {
            this.position = position;
            this.texture = texture;
            this.worth = worth;
        }

        public virtual Rectangle Hitbox
        {
            get
            {
                Rectangle hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                return hitbox;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, Hitbox, Color.White);
        }
        public virtual Player Intersect(GameTime gameTime, Player player)
        {           
            return player;
        }
    }


}
