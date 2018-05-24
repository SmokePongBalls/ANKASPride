using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace te16mono
{
    static class Background
    {
        static ContentManager Content;
        static Texture2D mountainTexture;
        static List<Vector2> backgroundPosition;        
        static Vector2 temp;
        

       

        static public void Initialize(ContentManager content)
        {

            Content = content;
            mountainTexture = Content.Load<Texture2D>("mountainBackground");
            
            
            
            backgroundPosition = new List<Vector2>();
            backgroundPosition.Add(new Vector2(-mountainTexture.Width - mountainTexture.Width / 2, -1000));
            backgroundPosition.Add(new Vector2(backgroundPosition[0].X + mountainTexture.Width, backgroundPosition[0].Y));
            backgroundPosition.Add(new Vector2(backgroundPosition[1].X + mountainTexture.Width, backgroundPosition[1].Y));



        }
        static private Rectangle Ground(Vector2 position)
        {

            return new Rectangle((int)position.X, (int)position.Y, mountainTexture.Width, 5000);
        }
        static private Rectangle Sky(Vector2 position)
        {

            return new Rectangle((int)position.X, (int)position.Y, mountainTexture.Width, 5000);
        }

        static public void Draw(SpriteBatch spriteBatch, Player player)
        {
            //ritar ut alla backgrunder.
            for (int i = 0; i < backgroundPosition.Count; i++)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("square"), Sky(new Vector2(backgroundPosition[i].X, backgroundPosition[i].Y - 5000)), new Color(0, 157, 236));
                spriteBatch.Draw(Content.Load<Texture2D>("square"), Ground(new Vector2(backgroundPosition[i].X, backgroundPosition[i].Y + mountainTexture.Height)), new Color(81, 156, 0));
                spriteBatch.Draw(mountainTexture, backgroundPosition[i], null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 1f);
               
            }
        }

        static public void Update(Player player)
        {

            UpdatePosition(player);

            CheckPlayerSide(player);

        }

        private static void UpdatePosition(Player player)
        {
    
            for (int i = 0; i < backgroundPosition.Count; i++)
            {               
                
                //Denna uträckning med "player.velocity.X / 2" är här för att simulera en paralax. 
                //Om det inte redan är förstått så rör den sig med hälften av player hastighet för att det ser coolt ut.
                backgroundPosition[i] += new Vector2(player.velocity.X/2, 0);
            }
            
            
        }

        static private void CheckPlayerSide(Player player)
        {
           
            //ser till så att det alltid finns en backgrund men så att du inte behöver skapa oändligt många --
            //Om spelare går tillräckligt åt höger så flyttas backgrounden längst åt vänster till längst åt höger. 
            if (player.position.X > backgroundPosition[2].X)
            {
                backgroundPosition[0] = backgroundPosition[1];
                backgroundPosition[1] = backgroundPosition[2];
                backgroundPosition[2] = new Vector2(backgroundPosition[1].X + mountainTexture.Width, backgroundPosition[1].Y);

                //backgroundPosition.Add(new Vector2(backgroundPosition[1].X + mountainTexture.Width, backgroundPosition[1].Y));

            }
            //Om spelare går tillräckligt åt vänster så flyttas backgrounden längst åt höger till längst åt vänster. 
            else if (player.position.X < backgroundPosition[0].X + mountainTexture.Width)
            {
                backgroundPosition[2] = backgroundPosition[1];
                backgroundPosition[1] = backgroundPosition[0];
                backgroundPosition[0] = new Vector2(backgroundPosition[1].X - mountainTexture.Width, backgroundPosition[1].Y);
            }
            //--
        }

        //Reset:ar mountainbackgroundposition och den används när player ramlar för långt ner och innan den här metoden fanns så var backgrunden där 
        //ursprunglien player föll och inte där han spawna.
        static public void ResetPosition()
        {
            backgroundPosition.RemoveRange(0, 3);
            backgroundPosition.Add(new Vector2(-mountainTexture.Width - mountainTexture.Width / 2, -100));
            backgroundPosition.Add(new Vector2(backgroundPosition[0].X + mountainTexture.Width, backgroundPosition[0].Y));
            backgroundPosition.Add(new Vector2(backgroundPosition[1].X + mountainTexture.Width, backgroundPosition[1].Y));

        }
    }
}
