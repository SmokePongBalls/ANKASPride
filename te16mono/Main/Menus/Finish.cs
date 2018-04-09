using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static te16mono.Main;
using static te16mono.Meny;

namespace te16mono
{
    static class Finish
    {
        public static void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            //Gå vidare till nästa bana
            if (keyboardState.IsKeyDown(Keys.N))
            {
                Main.currentState = Main.State.Run;
                Main.map++;
            }
            if (keyboardState.IsKeyDown(Keys.R))
            {
                Main.currentState = Main.State.Run;
                
            }
            //Lämna spelet
            if (keyboardState.IsKeyDown(Keys.Q))  //
                Main.currentState = Main.State.Quit;

        }
        public static Rectangle Rectangle(GraphicsDevice graphicsDevice)
        {
            return new Rectangle(graphicsDevice.DisplayMode.Width / 2 - 300, graphicsDevice.DisplayMode.Height / 2 - 300, 600, 600);
        }
    }
}
