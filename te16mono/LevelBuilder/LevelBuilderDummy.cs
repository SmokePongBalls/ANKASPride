using Microsoft.Xna.Framework;

namespace te16mono.LevelBuilder
{
    //Anton har gjort allt i den här klassen
    public static class LevelBuilderDummy
    {
        static MovingObjects dummyMovingObjects;
        static Block dummyBlock;
        static Effect dummyEffect;
        //Standard dummyvärdena
        public static void SetDummyValues()
        {
            //Dummy objekten kommer aldrig målas ut så det kvittar vilka texture de har
            dummyMovingObjects = new MovingObjectsDummy(MainLevelBuilder.GetTexture(), new Vector2(0), true, 0, 0, 0);
            dummyBlock = new BlockDummy(new Vector2(0), 0, 0, new Vector2(0), MainLevelBuilder.GetTexture());
            dummyEffect = new EffectDummy(new Vector2(0), MainLevelBuilder.GetTexture(), 0);
        }
        //Ger MainLevelBuilder objecten dummy värdena
        public static void DummyValues()
        {
            MainLevelBuilder.selectedMovingObject = DummyMovingObject;
            MainLevelBuilder.selectedBlock = DummyBlock;
            MainLevelBuilder.selectedEffect = DummyEffect;
        }
        //Användas för att kunna komma åt de olika dummy värdena utan att kunna skriva över det
        public static MovingObjects DummyMovingObject
        {
            get
            {
                return dummyMovingObjects;
            }
        }
        public static Block DummyBlock
        {
            get
            {
                return dummyBlock;
            }
        }
        public static Effect DummyEffect
        {
            get
            {
                return dummyEffect;
            }
        }
    }
}
