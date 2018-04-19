using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace te16mono
{
    //Anton
    public static class GameOver
    {
        public static void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            //Retry
            if (keyboardState.IsKeyDown(Keys.M))
            {
                Main.currentState = Main.State.Meny;
                Main.map = 1;
            }
            if (keyboardState.IsKeyDown(Keys.R))
                Main.currentState = Main.State.Run;

            //Lämna spelet
            if (keyboardState.IsKeyDown(Keys.Q))  //
                Main.currentState = Main.State.Quit;

        }
        public static Rectangle Rectangle(GraphicsDevice graphicsDevice)
        {
            return new Rectangle(graphicsDevice.DisplayMode.Width / 2 - 250, graphicsDevice.DisplayMode.Height / 2 - 250, 500, 500);
        }
    }
}
