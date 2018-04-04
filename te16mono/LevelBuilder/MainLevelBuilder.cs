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

    public enum LevelBuilderState {Main, Saving}
    static class MainLevelBuilder
    {

        static ContentManager Content;
        static Vector2 position;
        static SpriteBatch spriteBatch;
        static bool showError;

        public static bool placementAllowed, selectionAllowed;
        public static MouseState mouse, lastMouse;
        public static KeyboardState keyboardState, lastKeyboardState;
        public static SelectedObject selectedObject;
        public static SpriteFont spriteFont;
        public static LevelBuilderState state;
        public static MovingObjects selectedMovingObject;
        public static Block selectedBlock;
        public static Point selectedEffect;
        public static Player player;
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

            state = LevelBuilderState.Main;
            selectedObject = SelectedObject.Hedgehog;
            Menu.Load(Content);
            LoadAllTextures();
            placementAllowed = true;
            LevelBuilderDummy.SetDummyValues();
            LevelBuilderDummy.DummyValues();
        }

        static public void Update(GraphicsDevice graphicsDevice)
        {
            keyboardState = Keyboard.GetState();
            mouse = Mouse.GetState();
            showError = ObjectPlacing.CheckPosition();

            

            //Ska endast ifall muspekaren inte är över menyn
            if (mouse.X < 1440)
            {
                //Kollar om man väljer ett objekt
                if (selectionAllowed)
                {
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
                }                

                //Placerar ut block
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

            Menu.Update();
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
            if (Menu.menu != MenuType.ValueChanging)
            {
                selectedMovingObject = input;
                movingObjects.Remove(input);
                Menu.ChangeMovingObject(selectedMovingObject);
                placementAllowed = false;
            }
            else
            {
                SaveValue();
                selectedMovingObject = input;
                movingObjects.Remove(input);
                Menu.ChangeMovingObject(selectedMovingObject);
                placementAllowed = false;
            }
        }

        static void BlockSelected(Block input)
        {
            if (Menu.menu != MenuType.ValueChanging)
            {
                selectedBlock = input;
                blocks.Remove(input);
                Menu.ChangeBlock(selectedBlock);
                placementAllowed = false;
            }
            else
            {
                SaveValue();
                selectedBlock = input;
                blocks.Remove(input);
                Menu.ChangeBlock(selectedBlock);
                placementAllowed = false;
            }
            
        }

        static void EffectSelected(Point input)
        {
            if (Menu.menu != MenuType.ValueChanging)
            {
                selectedEffect = input;
                effects.Remove(input);
                Menu.ChangeEffect(selectedEffect);
                placementAllowed = false;
            }
            else
            {
                SaveValue();
                selectedEffect = input;
                effects.Remove(input);
                Menu.ChangeEffect(selectedEffect);
                placementAllowed = false;
            }
            
        }

        static void SaveValue()
        {
            if (selectedBlock != LevelBuilderDummy.DummyBlock)
            {
                blocks.Add(selectedBlock);
                selectedBlock = LevelBuilderDummy.DummyBlock;
            }
            else if (selectedEffect != LevelBuilderDummy.DummyEffect)
            {
                effects.Add(selectedEffect);
                selectedEffect = LevelBuilderDummy.DummyEffect;
            }
            else if (selectedMovingObject != LevelBuilderDummy.DummyMovingObject)
            {
                movingObjects.Add(selectedMovingObject);
                selectedMovingObject = LevelBuilderDummy.DummyMovingObject;
            }
        }

        public static void Reset()
        {
            movingObjects = new List<MovingObjects>();
            effects = new List<Point>();
            blocks = new List<Block>();
            LevelBuilderDummy.DummyValues();
            position = new Vector2(0);
        }
        
    }
}
