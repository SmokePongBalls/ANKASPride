using System;

namespace te16mono
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        //Våran gravity variable standard är 0.5
        static private float gravity = (float)0.5;
        static public float Gravity
        {
            get
            {
                return gravity;
            }
            set
            {
                gravity = value;
            }

        }


        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
#endif
}
