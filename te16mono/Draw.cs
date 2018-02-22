using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono
{

    class Draw
    {
        GraphicsDeviceManager graphics;
        public void Initialize()
        {             
            //Fullscreen --
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            //--

            testblock = new Block();
            testblock.position.X = 200;
            testblock.position.Y = 900;
            testblock.isAlive = true;
            testblock.type = TypeOfBlock.plattform;

            // TODO: Add your initialization logic here
            player = new Player(1, Content.Load<Texture2D>("square"));
            player.up = Keys.W;
            player.down = Keys.S;
            player.left = Keys.A;
            player.right = Keys.D;

            base.Initialize();
        }
    }
}
