﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te16mono
{

    //Anton, Hugo F
    public class Block
    {
        public string name;
        protected Texture2D texture;
        public int width, height;
        public int damage = 0;
        public bool canStandOn;
        public Vector2 position, velocity;

        
        public Block(Vector2 position, int width, int height, Vector2 velocity, Texture2D texture)
        {
            name = "Block";
            this.velocity = velocity;
            this.position = position;
            this.width = width;
            this.height = height;
            this.texture = texture;
            canStandOn = true;
        }

        public virtual Rectangle Hitbox
        {
            get
            {
                Rectangle hitbox = new Rectangle((int)position.X, (int)position.Y, width, height);
                return hitbox;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Hitbox, Color.Pink);
        }


    }
}