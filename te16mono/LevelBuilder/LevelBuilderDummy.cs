using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            dummyMovingObjects = new MovingObjectsDummy(MainLevelBuilder.bird, new Vector2(0), true, 0, 0, 0);
            dummyBlock = new BlockDummy(new Vector2(0), 0, 0, new Vector2(0), MainLevelBuilder.bird);
            dummyEffect = new EffectDummy(new Vector2(0), MainLevelBuilder.bird, 0);
        }
        //Ger MainLevelBuilder objecten dummy värdena
        public static void DummyValues()
        {
            MainLevelBuilder.selectedMovingObject = DummyMovingObject;
            MainLevelBuilder.selectedBlock = DummyBlock;
            MainLevelBuilder.selectedEffect = DummyEffect;
        }
        //Komma åt de olika dummyvärdena
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
