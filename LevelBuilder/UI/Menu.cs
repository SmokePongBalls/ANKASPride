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
        static MenuType menu = MenuType.Selection;
        static Texture2D square;

        public static void Load(ContentManager Content)
        {
            square = Content.Load<Texture2D>("square");
        }

        public static void Update()
        {
            if (menu == MenuType.Selection)
                Selection.Update();
            //else if (menu == MenuType.ValueChanging)
                //ValueChanging.Update();
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            if (menu == MenuType.Selection)
                Selection.Draw(spriteBatch);
            //else if (menu == MenuType.ValueChanging)
                // ValueChanging.Draw(spritebatch);
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
