using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{
    //ändrade så att alla effekter ärver av Effekt nu istället. Hugo F
    class Point : Effect
    {
        public Point(Vector2 position, Texture2D texture, int worth) : base(position, texture, worth)
        {
            name = "Point";
        }

        public override void Update(GameTime gameTime){}

        //Körs ifall den krockar med  player Anton
        public override Player PlayerIntersect(Player player)
        {
            player.points += worth;
            return player;
        }
    }

    
}
