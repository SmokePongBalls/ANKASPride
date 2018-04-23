using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{
    //ändrade så att alla effekter ärver av Effekt nu istället. Hugo F
    class FinishLine : Effect
    {

        public FinishLine(Vector2 position, Texture2D texture, int worth) : base(position, texture, worth)
        {
            name = "Finish";
        }
        //Körs ifall den krockar med player Anton
        public override Player PlayerIntersect(Player player)
        {
            Main.currentState = Main.State.Finish;
            return player;
        }

        public override void Update(GameTime gameTime)
        {

        }

        

        public override Rectangle Hitbox
        {
            get
            {
                Rectangle hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width * 3, texture.Height * 3);
                return hitbox;
            }
        }
    }
}
