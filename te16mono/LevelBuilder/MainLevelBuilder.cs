using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using te16mono.LevelBuilder.UI;
using te16mono.LevelBuilder.ObjectEditing;

namespace te16mono.LevelBuilder
{
    //Anton
    static class MainLevelBuilder
    {

        static ContentManager Content;
        public static Player player;
        static Vector2 position;
        static SpriteBatch spriteBatch;
        static bool showError;

        public static bool placementAllowed;
        public static MouseState mouse, lastMouse;
        public static KeyboardState keyboardState, lastKeyboardState;
        public static SelectedObject selectedObject;
        public static SpriteFont spriteFont;

        public static MovingObjects selectedMovingObject;
        public static Block selectedBlock;
        public static Point selectedEffect;

        public static List<Block> blocks;
        public static List<MovingObjects> movingObjects;
        public static List<Point> effects;
        public static Texture2D
            cat,
            pear,
            bird,
            hedgehog,
            square,
            frog,
            finishFlag;

        public static void Initialize(ContentManager Content, GraphicsDevice graphicsDevice)
        {
            MainLevelBuilder.Content = Content;
            player = new Player(1, Content.Load<Texture2D>("square"));
            spriteBatch = new SpriteBatch(graphicsDevice);
            movingObjects = new List<MovingObjects>();
            effects = new List<Point>();
            blocks = new List<Block>();
            Vector2 position = new Vector2(0);
            mouse = new MouseState();
            lastMouse = new MouseState();
            keyboardState = new KeyboardState();
            lastKeyboardState = new KeyboardState();
            spriteFont = Content.Load<SpriteFont>("font");
            lastMouse = Mouse.GetState();
            lastKeyboardState = Keyboard.GetState();

            selectedObject = SelectedObject.Hedgehog;
            Menu.Load(Content);
            LoadAllTextures();
            placementAllowed = true;
            DummyValues();
        }

        static public void Update(GraphicsDevice graphicsDevice)
        {
            keyboardState = Keyboard.GetState();
            mouse = Mouse.GetState();
            showError = ObjectPlacing.CheckPosition();

            Menu.Update();

            //Ska endast ifall muspekaren inte är över menyn
            if (mouse.X < 1440)
            {
                //Placerar ut block
                foreach (MovingObjects movingObject in movingObjects.ToArray())
                {
                    if (movingObject.Hitbox.Intersects(AbsoluteMouseHitbox) && mouse.LeftButton == ButtonState.Pressed && lastMouse.LeftButton == ButtonState.Released)
                    {
                        MovingObjectSelected(movingObject);
                    }
                }
                foreach (Block block in blocks.ToArray())
                {
                    if (block.Hitbox.Intersects(AbsoluteMouseHitbox) && mouse.LeftButton == ButtonState.Pressed && lastMouse.LeftButton == ButtonState.Released)
                    {
                        BlockSelected(block);
                    }
                }
                foreach (Point effect in effects.ToArray())
                {
                    if (effect.Hitbox.Intersects(AbsoluteMouseHitbox) && mouse.LeftButton == ButtonState.Pressed && lastMouse.LeftButton == ButtonState.Released)
                    {
                        EffectSelected(effect);
                    }
                }

                if (mouse.LeftButton == ButtonState.Pressed && lastMouse.LeftButton == ButtonState.Released && placementAllowed)
                    ObjectPlacing.Create();

                //Flyttar runt kameran
                if (mouse.RightButton == ButtonState.Pressed)
                {
                    if (lastMouse.RightButton == ButtonState.Pressed)
                    {
                        position.X += lastMouse.X - mouse.X;
                        position.Y += lastMouse.Y - mouse.Y;
                    }

                }
            }
            

            lastMouse = mouse;
            lastKeyboardState = keyboardState;
        }

        static public void Draw(GraphicsDevice graphicsDevice)
        {
            //Allting som är del utav banan
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Camera.LevelBuilderPosition(position, graphicsDevice.DisplayMode.Width, graphicsDevice.DisplayMode.Height));
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

            if (showError && placementAllowed)
                spriteBatch.DrawString(spriteFont, "X", MousePosition, Color.Red);

            selectedBlock.Draw(spriteBatch);
            selectedEffect.Draw(spriteBatch);
            selectedMovingObject.Draw(spriteBatch);

            spriteBatch.End();
            //Allting som är en del utav UI
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, Convert.ToString(blocks.Count), new Vector2(0), Color.Wheat);
            Menu.Draw(spriteBatch);

                
            spriteBatch.End();
        }


        public static Rectangle MouseHitbox
        {
            get
            {
                return new Rectangle(mouse.X, mouse.Y + 10, 1, 1);
            }
        }

        public static Rectangle AbsoluteMouseHitbox
        {
            get
            {
                return new Rectangle(Convert.ToInt32(mouse.X - 960 + position.X), Convert.ToInt32(mouse.Y - 540 + position.Y), 1, 1);
            }
        }

        static void LoadAllTextures()
        {
            cat = Content.Load<Texture2D>("katt");
            pear = Content.Load<Texture2D>("pear");
            bird = Content.Load<Texture2D>("bird");
            hedgehog = Content.Load<Texture2D>("hedgehog");
            square = Content.Load<Texture2D>("square");
            finishFlag = Content.Load<Texture2D>("finishFlag");
            frog = Content.Load<Texture2D>("frog");
        }

        public static Vector2 MousePosition
        {
            get
            {
                return new Vector2(mouse.X - 960 + position.X, mouse.Y - 540 + position.Y);
            }
        }

        static void MovingObjectSelected(MovingObjects input)
        {
            selectedMovingObject = input;
            movingObjects.Remove(input);
            Menu.ChangeMovingObject(selectedMovingObject);
            placementAllowed = false;
        }

        static void BlockSelected(Block input)
        {
            selectedBlock = input;
            blocks.Remove(input);
            Menu.ChangeBlock(selectedBlock);
            placementAllowed = false;
        }

        static void EffectSelected(Point input)
        {
            selectedEffect = input;
            effects.Remove(input);
            Menu.ChangeEffect(selectedEffect);
            placementAllowed = false;
        }

        public static void DummyValues()
        {
            selectedMovingObject = new MovingObjectsDummy(cat, new Vector2(0), true, 64564, 6454, 545454);
            selectedBlock = new BlockDummy(new Vector2(0), 0, 0, new Vector2(0), cat);
            selectedEffect = new EffectDummy(new Vector2(0), cat, 56556);
        }
    }
}
