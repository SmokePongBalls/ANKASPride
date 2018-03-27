using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static te16mono.Main;

namespace te16mono
{
    public static class GameOver
    {
        public static void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            //Retry
            if (keyboardState.IsKeyDown(Keys.M))
            {
                Main.currentState = State.Meny;
                map = 1;
            }
            if (keyboardState.IsKeyDown(Keys.R))
                Main.currentState = State.Run;

            //Lämna spelet
            if (keyboardState.IsKeyDown(Keys.Q))  //
                Main.currentState = State.Quit;

        }
        public static Rectangle Rectangle(GraphicsDevice graphicsDevice)
        {
            return new Rectangle(graphicsDevice.DisplayMode.Width / 2 - 250, graphicsDevice.DisplayMode.Height / 2 - 250, 500, 500);
        }
    }
}
