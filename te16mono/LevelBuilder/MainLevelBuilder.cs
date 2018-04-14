using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using te16mono.LevelBuilder.UI;
using te16mono.LevelBuilder.ObjectEditing;
using te16mono.Input;

namespace te16mono.LevelBuilder
{
    //Anton

    public enum LevelBuilderState { Main, Saving }
    static class MainLevelBuilder
    {

        static ContentManager Content; //Contentmanagern för att ladda in filer
        static SpriteBatch spriteBatch; //Spritebatchen för att rita ut saker
        static bool showError; // Är true ifall ett kryss ska visas vid muspekaren
        static Saving saving; //Saving klassen som används när filer ska sparas

        public static Vector2 position; //Används för att veta vilken position kameran och muspekaren har
        public static bool placementAllowed, selectionAllowed; // Ifall man får placera ut och ifall man får välja objekt
        public static MouseState mouse, lastMouse;
        public static KeyboardState keyboardState { get; private set; }
        public static KeyboardState lastKeyboardState { get; private set; }
        public static SelectedObject selectedObject; //Vilken typ utav objekt som är valt
        public static SpriteFont spriteFont { get; private set; }
        public static LevelBuilderState state; //Vilken state det körs i just nu 
        //Ett objekt flyttas till en av de här när det ska redigeras
        public static MovingObjects selectedMovingObject;
        public static Block selectedBlock;
        public static Point selectedEffect;

        public static Player player; //Används så att användaren ska se vart spelaren börjar

        //Listorna med saker man har placerat ut 
        public static List<Block> blocks;
        public static List<MovingObjects> movingObjects;
        public static List<Point> effects;
        //Alla textures som används
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

            selectionAllowed = true;
            saving = new Saving();
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

            if (state == LevelBuilderState.Main)
            {
                UpdateMain();
            }
            //Om den är i sparar stadiet
            else if (state == LevelBuilderState.Saving)
            {
                saving.Update(keyboardState, lastKeyboardState);
            }

            //Sparar mus och keyboard staten till sina laststate
            lastMouse = mouse;
            lastKeyboardState = keyboardState;
        }

        //Update ifall state == Main
        private static void UpdateMain()
        {
            //Ska endast ifall muspekaren inte är över menyn
            if (mouse.X < 1440)
            {
                //Saker som endast ska köras ifall vänster musknapp trycks ner för första gånger
                if (LeftClick())
                {
                    //Ifall man får välja ett utav objekten
                    if (selectionAllowed)
                    {
                        CheckForSelection();
                    }

                    //Placerar ut block
                    if (placementAllowed)
                        ObjectPlacing.Create();
                }
                //Flyttar runt kameran
                CameraMovement();
            }

            Menu.Update();
        }
        //Om man håller ner högermusknapp så flyttas kameran
        private static void CameraMovement()
        {
            if (mouse.RightButton == ButtonState.Pressed && lastMouse.RightButton == ButtonState.Pressed)
            {
                position.X += lastMouse.X - mouse.X; //X += Delta X
                position.Y += lastMouse.Y - mouse.Y; // Y += Delta Y
            }
        }

        //Kollar ifall användaren klickar på någon utav objekten
        private static void CheckForSelection()
        {
            //Går igenom alla movingobjects 
            foreach (MovingObjects movingObject in movingObjects.ToArray())
            {
                if (movingObject.Hitbox.Intersects(AbsoluteMouseHitbox))
                {
                    MovingObjectSelected(movingObject);
                }
            }
            //Går igenom alla blocks
            foreach (Block block in blocks.ToArray())
            {
                if (block.Hitbox.Intersects(AbsoluteMouseHitbox))
                {
                    BlockSelected(block);
                }
            }
            //Går igenom alla effekter 
            foreach (Point effect in effects.ToArray())
            {
                if (effect.Hitbox.Intersects(AbsoluteMouseHitbox))
                {
                    EffectSelected(effect);
                }
            }
        }

        static public void Draw(GraphicsDevice graphicsDevice)
        {
            //Allting som är del utav banan
            if (state == LevelBuilderState.Main)
            {
                MainDraw(graphicsDevice);
            }
            //Allting som är del utav saving
            else if (state == LevelBuilderState.Saving)
            {
                SavingDraw();
            }

        }
        //Ska målas om State == Saving
        private static void SavingDraw()
        {
            spriteBatch.Begin();
            saving.Draw(spriteBatch);
            spriteBatch.End();
        }

        //Ska målas om State == Main
        private static void MainDraw(GraphicsDevice graphicsDevice)
        {
            //Allting som blir ska flyttas med kameran
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
            //Allting som är en del utav UI och ska ha samma position
            spriteBatch.Begin();
            Menu.Draw(spriteBatch);

            spriteBatch.End();
        }
        //Laddar in alla textures som kommer behövas 
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

        
        // Om man har tryck på ett moving object
        static void MovingObjectSelected(MovingObjects input)
        {
            //Sparar det gamla valda värdet ifall det finns ett och byter sedan till det valda movingObject
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
        //Om man har tryck på ett block
        static void BlockSelected(Block input)
        {
            //Sparar det gamla valda värdet ifall det finns ett och byter sedan till det valda blocket
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

        //Om man har tryck på en effekt
        static void EffectSelected(Point input)
        {
            //Sparar det gamla valda värdet ifall det finns ett och byter sedan till den valda effekten
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

        //Kollar ifall det finns några objekt som behöver flyttas till sin rätta lista
        static void SaveValue()
        {
            //Sparas endast ifall det inte är dummy värde
            if (selectedBlock != LevelBuilderDummy.DummyBlock)
            {
                blocks.Add(selectedBlock);
                selectedBlock = LevelBuilderDummy.DummyBlock;
            }
            //Sparas endast ifall det inte är dummy värde
            else if (selectedEffect != LevelBuilderDummy.DummyEffect)
            {
                effects.Add(selectedEffect);
                selectedEffect = LevelBuilderDummy.DummyEffect;
            }
            //Sparas endast ifall det inte är dummy värde
            else if (selectedMovingObject != LevelBuilderDummy.DummyMovingObject)
            {
                movingObjects.Add(selectedMovingObject);
                selectedMovingObject = LevelBuilderDummy.DummyMovingObject;
            }
        }
        //Återställer så att det blir en tom bana
        public static void Reset()
        {
            movingObjects = new List<MovingObjects>();
            effects = new List<Point>();
            blocks = new List<Block>();
            LevelBuilderDummy.DummyValues();
            position = new Vector2(0);
        }
        //Retunerar true ifall man trycker ner vänstermusknapp. Ifall man håller ner den retuneras false
        public static bool LeftClick()
        {
            if (mouse.LeftButton == ButtonState.Pressed && lastMouse.LeftButton == ButtonState.Released)
                return true;
            else
                return false;
        }

        //Musens position justerat efter kameran
        public static Vector2 MousePosition
        {
            get
            {
                return new Vector2(mouse.X - 960 + position.X, mouse.Y - 540 + position.Y);
            }
        }

        //Alla rektanglar som används i klassen
        public static Rectangle MouseHitbox
        {
            get
            {
                return new Rectangle(mouse.X, mouse.Y + 5, 1, 1);
            }
        }

        public static Rectangle AbsoluteMouseHitbox
        {
            get
            {
                return new Rectangle(Convert.ToInt32(mouse.X - 960 + position.X), Convert.ToInt32(mouse.Y - 540 + position.Y), 1, 1);
            }
        }
    }
}
