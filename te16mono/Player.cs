using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te16mono
{
    class Player : MovingObjects
    {
        public int points;
        float acceleration = (float)0.5;
        public Keys up, down, left, right;
        KeyboardState pressedKeys;

        

        public Player(int seed, Texture2D texture)
        {
            position = new Vector2();
            velocity = new Vector2();
            //Initiera värden
        }

        public void Update()
        {

            velocity = velocity * (float)0.95;
            velocity.Y += gravity;
            //Spellogik
            pressedKeys = Keyboard.GetState();

            
            if (pressedKeys.IsKeyDown(left))
                velocity.X -= acceleration;
            if (pressedKeys.IsKeyDown(down))
                velocity.Y += acceleration;
            if (pressedKeys.IsKeyDown(right))
                velocity.X += acceleration;
           
            //Själva: Ordna styrning för a, s, d också

            position += velocity;

            if(position.X<0)
            {
                position.X = 0;
                velocity.X = -velocity.X;
            }

            if (position.Y < 0)
            {
                position.Y = 0;
                velocity.Y = -velocity.Y;
            }

            if (position.X+texture.Width >1920)
            {
                position.X = 1920-texture.Width;
                velocity.X = -velocity.X;
            }

            if (position.Y + texture.Height> 1080)
            {
                position.Y = 1080- texture.Height;
                velocity.Y = -velocity.Y;
            }
        }

       
    }
    

}
