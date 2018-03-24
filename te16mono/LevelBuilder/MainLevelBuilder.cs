using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace te16mono.LevelBuilder
{
    public static class MainLevelBuilder
    {

        static ContentManager Content;
        static List<Block> blocks;
        static List<Point> effects;
        static List<MovingObjects> movingObjects;
        static Player player;
        static SpriteBatch spriteBatch;
        static MouseState mouse;
        static Vector2 position;
        static KeyboardState keyboardState;
        static SpriteFont spriteFont;

        static public void Initialize(ContentManager Content, GraphicsDevice graphicsDevice)
        {
            MainLevelBuilder.Content = Content;
            player = new Player(1, Content.Load<Texture2D>("square"));
            spriteBatch = new SpriteBatch(graphicsDevice);
            movingObjects = new List<MovingObjects>();
            effects = new List<Point>();
            blocks = new List<Block>();
            Vector2 position = new Vector2(0);
            mouse = new MouseState();
            keyboardState = new KeyboardState();
            spriteFont = Content.Load<SpriteFont>("font");
            
        }

            static public void Update(GraphicsDevice graphicsDevice)
        {
            keyboardState = Keyboard.GetState();
            mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed)
                blocks.Add(new Block(new Vector2(Convert.ToInt32(mouse.X - graphicsDevice.DisplayMode.Width/2 + position.X), Convert.ToInt32(mouse.Y - graphicsDevice.DisplayMode.Height/2 + position.Y)), 50, 50, new Vector2(0), Content.Load<Texture2D>("square")));
        }
        static public void Draw(GraphicsDevice graphicsDevice)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Camera.LevelBuilderPosition(position, graphicsDevice.DisplayMode.Width, graphicsDevice.DisplayMode.Height));
            spriteBatch.DrawString(spriteFont, Convert.ToString(blocks.Count), new Vector2(-500, -500), Color.Wheat);
            foreach (MovingObjects movingObject in movingObjects)
            {
                movingObject.Draw(spriteBatch);
            }
            foreach (Point effect in effects)
            {
                effect.Draw(spriteBatch);
            }
            foreach (Block block in blocks)
            {
                block.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
            
            spriteBatch.End();
        }
    }
}
