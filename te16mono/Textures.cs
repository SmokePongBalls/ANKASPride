using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace te16mono
{
    class Textures
    {

        GraphicsDeviceManager graphics;
        Block plattform;
        Player player;

        public void Initialize()
        {

           

           
           
            player = new Player(1, content.Load<Texture2D>("square"));
           

        }
        public void LoadContent()
        {

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            plattform.texture = Content.Load<Texture2D>("square");

            //Testkatten
            testKatt = new Katt(1, Content.Load<Texture2D>("kattModel"), new Vector2(100, 100), false, (float)0.5, 1700, 400);

            font = Content.Load<SpriteFont>("Font");

            music = Content.Load<Song>("megaman2");
            MediaPlayer.Play(music);

        }
    }
}
