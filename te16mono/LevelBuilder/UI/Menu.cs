using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te16mono.LevelBuilder.UI
{
    //Anton
    public static class Menu
    {
        public static MenuType menu = MenuType.Selection;
        static Texture2D square;
        static ValueChanging valueChanging;

        public static void Load(ContentManager Content)
        {
            square = Content.Load<Texture2D>("square");
            valueChanging = new MovingObjectChanging(MainLevelBuilder.player);

        }

        public static void Update()
        {
            if (menu == MenuType.Selection)
            {
                Selection.Update();
            }
            else if (menu == MenuType.ValueChanging)
            {
                valueChanging.Update();
            }
                
        }

        public static void ChangeMovingObject(MovingObjects input)
        {
            valueChanging = new MovingObjectChanging(input);
            menu = MenuType.ValueChanging;
        }

        public static void DoneWithMovingObject()
        {
            menu = MenuType.Selection;
            MainLevelBuilder.movingObjects.Add(MainLevelBuilder.selectedMovingObject);
            MainLevelBuilder.placementAllowed = true;
            LevelBuilderDummy.DummyValues();
        }
        public static void ChangeBlock(Block input)
        {
            valueChanging = new BlockChanging(input);
            menu = MenuType.ValueChanging;
        }
        public static void DoneWithBlock()
        {
            menu = MenuType.Selection;
            MainLevelBuilder.blocks.Add(MainLevelBuilder.selectedBlock);
            MainLevelBuilder.placementAllowed = true;
            LevelBuilderDummy.DummyValues();
        }
        public static void ChangeEffect(Point input)
        {
            valueChanging = new EffectChanging(input);
            menu = MenuType.ValueChanging;
        }
        public static void DoneWithEffect()
        {
            menu = MenuType.Selection;
            MainLevelBuilder.effects.Add(MainLevelBuilder.selectedEffect);
            MainLevelBuilder.placementAllowed = true;
            LevelBuilderDummy.DummyValues();
        }
        public static void DeleteValueChanging()
        {
            menu = MenuType.Selection;
            MainLevelBuilder.placementAllowed = true;
            LevelBuilderDummy.DummyValues();
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            if (menu == MenuType.Selection)
                Selection.Draw(spriteBatch);
            else if (menu == MenuType.ValueChanging)
                valueChanging.Draw(spriteBatch);
        }


        public static Rectangle MenuRectangle
        { 
            get
            {
                return new Rectangle(1440, 0, 480, 1080);
            }
        }

        public static Texture2D Square
        {
            get
            {
                return square;
            }
        }

    }
}
