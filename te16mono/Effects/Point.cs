using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{
    //ändrade så att alla effekter ärver av Effekt nu istället. Hugo F
    public class Point : Effect
    {
        public Point(Vector2 position, Texture2D texture, int worth) : base(position, texture, worth)
        {
            name = "Point";
        }

             
        public override Player Intersect(GameTime gameTime, Player player)
        {

            player.points += worth;
            return player;
        }
    }

    
}
