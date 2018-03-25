using Microsoft.Xna.Framework;
namespace te16mono
{
    //Anton
    static class Camera
    {

        public static Matrix Position(Player player, int screenWidth, int screenHeight)
        {
            Matrix position = Matrix.CreateTranslation(-player.position.X  - (player.Hitbox.Width/ 2), -player.position.Y - (player.Hitbox.Height / 2), 0);

            Matrix screenSize = Matrix.CreateTranslation((float)screenWidth / 3, (float)screenHeight / (float)1.5, 0);

            return position * screenSize;
        }
        public static Matrix LevelBuilderPosition(Vector2 position, int screenWidth, int screenHeight)
        {
            Matrix matrixPosition = Matrix.CreateTranslation(-position.X, -position.Y, -1);

            Matrix screenSize = Matrix.CreateTranslation((float)screenWidth / 2, (float)screenHeight / (float)2, 1f);

            return matrixPosition * screenSize;
        }

        public static Rectangle Rectangle(Rectangle player)
        {
            return new Rectangle(player.X - 2000, player.Y - 2000, player.Width + 4000, player.Height + 4000);
        }
    }
}
