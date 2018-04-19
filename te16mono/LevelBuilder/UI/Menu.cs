using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace te16mono.LevelBuilder.UI
{
    //Anton har gjort allt i den här klassen
    public static class Menu
    {
        public static MenuType menu = MenuType.Selection;
        static Texture2D square;
        static ValueChanging valueChanging;
        public static bool gridEnabled;
        //Sätter allting till sin standard
        public static void Load(ContentManager Content)
        {
            square = Content.Load<Texture2D>("square");
            Options.Initialize();
            gridEnabled = true;
        }
        //Update
        public static void Update()
        {
            //Kollar vilken sorts menu som är vald nu
            if (menu == MenuType.Selection)
            {
                Selection.Update();
            }
            else if (menu == MenuType.ValueChanging)
            {
                valueChanging.Update();
            }
            else if (menu == MenuType.Options)
            {
                Options.Update();
            }
                
        }
        //Målar ut menyerna
        public static void Draw(SpriteBatch spriteBatch)
        {
            // Om grid ska målas ut
            if (gridEnabled)
            HelpLines.Draw(MainLevelBuilder.position, spriteBatch);

            //Målar ut rätt meny
            if (menu == MenuType.Options)
            {
                Options.Draw(spriteBatch);
            }
            else if (menu == MenuType.Selection)
                Selection.Draw(spriteBatch);
            else if (menu == MenuType.ValueChanging)
                valueChanging.Draw(spriteBatch);
        }
        //Initiera och byter meny till MovingObjectsChanging
        public static void ChangeMovingObject(MovingObjects input)
        {
            valueChanging = new MovingObjectChanging(input);
            menu = MenuType.ValueChanging;
        }
        //Återställer till dummyvärdet och byter meny
        public static void DoneWithMovingObject()
        {
            menu = MenuType.Selection;
            MainLevelBuilder.movingObjects.Add(MainLevelBuilder.selectedMovingObject);
            MainLevelBuilder.placementAllowed = true;
            LevelBuilderDummy.DummyValues();
        }
        //Initiera och byter meny till BlockChanging
        public static void ChangeBlock(Block input)
        {
            valueChanging = new BlockChanging(input);
            menu = MenuType.ValueChanging;
        }
        //Återställer till dummyvärdet och byter meny
        public static void DoneWithBlock()
        {
            menu = MenuType.Selection;
            MainLevelBuilder.blocks.Add(MainLevelBuilder.selectedBlock);
            MainLevelBuilder.placementAllowed = true;
            LevelBuilderDummy.DummyValues();
        }
        //Initiera och byter meny till EffectChanging
        public static void ChangeEffect(Effect input)
        {
            valueChanging = new EffectChanging(input);
            menu = MenuType.ValueChanging;
        }
        //Återställer till dummyvärdet och byter meny
        public static void DoneWithEffect()
        {
            menu = MenuType.Selection;
            MainLevelBuilder.effects.Add(MainLevelBuilder.selectedEffect);
            MainLevelBuilder.placementAllowed = true;
            LevelBuilderDummy.DummyValues();
        }
        //Om man väljer att radera objectet
        public static void DeleteValueChanging()
        {
            menu = MenuType.Selection;
            MainLevelBuilder.placementAllowed = true;
            LevelBuilderDummy.DummyValues();
        }
        //Om man vill gå över till optionsmenyn
        public static void StartOptions()
        {
            //Ändrar så att man inte kan välja eller placera ut objekt
            MainLevelBuilder.placementAllowed = false;
            MainLevelBuilder.selectionAllowed = false;
            menu = MenuType.Options;
        }
        //Om man är färdig med Optionmenyn
        public static void DoneWithOptions()
        {
            menu = MenuType.Selection;
            MainLevelBuilder.placementAllowed = true;
            MainLevelBuilder.selectionAllowed = true;
        }
        //Togglar mellan att ha grid på och av
        public static void GridToggle()
        {
            if (gridEnabled)
            {
                gridEnabled = false;
            }
            else
            {
                gridEnabled = true;
            }
        }
        //Rektangeln
        public static Rectangle MenuRectangle
        { 
            get
            {
                return new Rectangle(1440, 0, 480, 1080);
            }
        }
        //Texture som används till de flesta UI klasserna
        public static Texture2D Square
        {
            get
            {
                return square;
            }
        }
    }
}
