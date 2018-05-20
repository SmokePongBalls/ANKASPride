using Microsoft.Xna.Framework.Content;
using System;
using System.Xml;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Globalization;

namespace te16mono
{
    //Anton, Hugo F
    static class XmlLoader
    {
        public static void LoadMap(ContentManager Content, string mapName)
        {
            XmlDocument document = new XmlDocument();
            document.Load(mapName);

            foreach (XmlNode node in document.DocumentElement)
            {
                //Om det är ett movingobject som ska laddas in
                if (node.Name == "MovingObjects")
                {
                    LoadMovingObjects(Content, node);
                }
                else if (node.Name == "Blocks")
                {
                    LoadBlocks(Content, node);
                }
                else if (node.Name == "Effects")
                {
                    LoadPoints(Content, node);
                }
            }
        }
        //Går igenom hela movingobject elementet Anton
        static void LoadMovingObjects(ContentManager Content, XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                //Om det är en igelkott
                if (child.Name == "Hedgehog")
                {
                    AddHedgehog(Content, child);
                }
                //Om det är en fågel
                if (child.Name == "Bird")
                {
                    AddBird(Content, child);
                }
                //Ifall det är en groda
                if (child.Name == "Frog")
                {
                    AddFrog(Content, child);
                }
                //Ifall det är en groda
                if (child.Name == "Katt")
                {
                    AddKatt(Content, child);
                }
            }
        }
        //Går igenom hela blocks elementet Anton
        static void LoadBlocks(ContentManager Content, XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name == "Block")
                {
                    AddBlock(Content, child);
                }
            }
        }
        //Går igenom hela point elementet Anton
        static void LoadPoints(ContentManager Content, XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name == "Health")
                {
                    AddHealth(Content, child);
                }
                else if (child.Name == "Immortality")
                {
                    AddImmortality(Content, child);

                }

                if (child.Name == "Whammy")
                {
                    AddWhammy(Content, child);

                }

                if (child.Name == "HighGravity")
                {
                    AddHighGravity(Content, child);

                }

                if (child.Name == "Point")
                {
                    AddPoint(Content, child);
                }
                if (child.Name == "Finish")
                {
                    AddFinish(Content, child);
                }
            }
        }

        //Om det är en finishflagga som ska läggas till Anton
        static void AddFinish(ContentManager Content, XmlNode child)
        {
            //Standardvärden ifall något går fel
            int worth = 0;
            string texture = "finishFlag";
            float X = 0, Y = 0;
            //Kollar om något ska ändras
            foreach (XmlNode childNode in child.ChildNodes)
            {
                if (childNode.Name == "Worth")
                    worth = Convert.ToInt32(childNode.InnerXml);
                else if (childNode.Name == "Texture")
                    texture = childNode.InnerXml;
                else if (childNode.Name == "X")
                    X = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "Y")
                    Y = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
            }
            //Lägger till objektet i listan
            Main.objects.Add(new FinishLine(new Vector2(X, Y), Content.Load<Texture2D>(texture), worth));
        }
        //Om det är ett poäng som ska läggas till Anton
        static void AddPoint(ContentManager Content, XmlNode child)
        {
            int worth = 100;
            string texture = "goldbag";
            float X = 0, Y = 0;
            //Kollar ifall några värden ska ändras
            foreach (XmlNode childNode in child.ChildNodes)
            {
                if (childNode.Name == "Worth")
                    worth = Convert.ToInt32(childNode.InnerXml);
                else if (childNode.Name == "Texture")
                    texture = childNode.InnerXml;
                else if (childNode.Name == "X")
                    X = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "Y")
                    Y = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
            }
            //Lägger till objektet i listan
            Main.objects.Add(new Point(new Vector2(X, Y), Content.Load<Texture2D>(texture), worth));
        }
        //Om det är HighGravity som ska läggas till Hugo F
        static void AddHighGravity(ContentManager Content, XmlNode child)
        {
            //Standard värden ifall något går fel
            int worth = 1;
            string texture = "weight";
            float X = 0, Y = 0;
            //Kollar ifall några värden ska ändras
            foreach (XmlNode childNode in child.ChildNodes)
            {
                if (childNode.Name == "Worth")
                    worth = Convert.ToInt32(childNode.InnerXml);
                else if (childNode.Name == "Texture")
                    texture = childNode.InnerXml;
                else if (childNode.Name == "X")
                    X = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "Y")
                    Y = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
            }
            //Lägger till objektet i listan
            Main.objects.Add(new HighGravity(new Vector2(X, Y), Content.Load<Texture2D>(texture), worth));
        }
        //Om det är whammy som ska läggas till Hugo F
        static void AddWhammy(ContentManager Content, XmlNode child)
        {
            //Standardvärden ifall att något går fel
            int worth = 1;
            string texture = "iceblock";
            float X = 0, Y = 0;
            //Kollar ifall något ska ändras
            foreach (XmlNode childNode in child.ChildNodes)
            {
                if (childNode.Name == "Worth")
                    worth = Convert.ToInt32(childNode.InnerXml);
                else if (childNode.Name == "Texture")
                    texture = childNode.InnerXml;
                else if (childNode.Name == "X")
                    X = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "Y")
                    Y = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
            }
            //Lägger till objektet i listan
            Main.objects.Add(new Whammy(new Vector2(X, Y), Content.Load<Texture2D>(texture), worth));
        }
        //Om det är immortality som ska läggas till Hugo F
        static void AddImmortality(ContentManager Content, XmlNode child)
        {
            //Standardvärden ifall något går fel
            int worth = 1;
            string texture = "sheild";
            float X = 0, Y = 0;
            //Kollar ifall något ska ändras
            foreach (XmlNode childNode in child.ChildNodes)
            {
                if (childNode.Name == "Worth")
                    worth = Convert.ToInt32(childNode.InnerXml);
                else if (childNode.Name == "Texture")
                    texture = childNode.InnerXml;
                else if (childNode.Name == "X")
                    X = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "Y")
                    Y = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
            }
            //Lägger till objektet i listan
            Main.objects.Add(new Immortality(new Vector2(X, Y), Content.Load<Texture2D>(texture), worth));
        }
        //Om det är health som ska läggas till Hugo F
        static void AddHealth(ContentManager Content, XmlNode child)
        {
            //Standard värden ifall något går fel
            int worth = 1;
            string texture = "pear";
            float X = 0, Y = 0;
            //Kollar ifall några värden ska ändras
            foreach (XmlNode childNode in child.ChildNodes)
            {
                if (childNode.Name == "Worth")
                    worth = Convert.ToInt32(childNode.InnerXml);
                else if (childNode.Name == "Texture")
                    texture = childNode.InnerXml;
                else if (childNode.Name == "X")
                    X = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "Y")
                    Y = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
            }
            //Lägger till objektet i listan
            Main.objects.Add(new Health(new Vector2(X, Y), Content.Load<Texture2D>(texture), worth));
        }
        //Om det är ett block som ska läggas till Hugo F
        static void AddBlock(ContentManager Content, XmlNode child)
        {
            //Om det är ett vanligt block
            if (child.Name == "Block")
            {
                //Standard värden ifall att något går fel
                string texture = "square";
                float X = 0, Y = 0, velocityY = 0, velocityX = 0;
                int width = 0, height = 0;
                //Kollar igenom ifall några värden ska ändras
                foreach (XmlNode childNode in child.ChildNodes)
                {
                    if (childNode.Name == "Texture")
                        texture = childNode.InnerXml;
                    else if (childNode.Name == "X")
                        X = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                    else if (childNode.Name == "Y")
                        Y = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                    else if (childNode.Name == "VelocityX")
                        velocityX = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                    else if (childNode.Name == "VelocityY")
                        velocityY = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                    else if (childNode.Name == "Width")
                        width = Convert.ToInt32(childNode.InnerXml);
                    else if (childNode.Name == "Height")
                        height = Convert.ToInt32(childNode.InnerXml);

                }
                //Lägger till objektet i listan
                Main.objects.Add(new Block(new Vector2(X, Y), width, height, new Vector2(velocityX, velocityY), Content.Load<Texture2D>(texture)));
            }
        }
        //Om det är en katt som ska läggas till Anton
        static void AddKatt(ContentManager Content, XmlNode child)
        {
            //Standardvärden ifall något går fel
            string texture = "katt";
            float X = 0, Y = 0, maxSpeed = 0, maxX = 0, minX = 0;
            bool walkLeft = true;
            //Går igenom för att se ifall några värden ska ändras
            foreach (XmlNode childNode in child.ChildNodes)
            {
                if (childNode.Name == "Texture")
                    texture = childNode.InnerXml;
                else if (childNode.Name == "X")
                    X = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "Y")
                    Y = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "MaxSpeed")
                    maxSpeed = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "MaxX")
                    maxX = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "MinX")
                    minX = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "WalkLeft")
                    walkLeft = bool.Parse(childNode.InnerXml);
            }
            //Lägger till objektet i listan
            Main.objects.Add(new Katt(Content.Load<Texture2D>(texture), new Vector2(X, Y), walkLeft, maxSpeed, maxX, minX));
        }
        //Om det är en groda som ska läggas till Anton
        static void AddFrog(ContentManager Content, XmlNode child)
        {
            //Standardvärden ifall något går fel
            string texture = "frog";
            float X = 0, Y = 0, maxSpeed = 0, maxX = 0, minX = 0;
            bool walkLeft = true;
            //Går igenom för att se ifall några värden ska ändras
            foreach (XmlNode childNode in child.ChildNodes)
            {
                if (childNode.Name == "Texture")
                    texture = childNode.InnerXml;
                else if (childNode.Name == "X")
                    X = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "Y")
                    Y = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "MaxSpeed")
                    maxSpeed = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "MaxX")
                    maxX = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "MinX")
                    minX = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "WalkLeft")
                    walkLeft = bool.Parse(childNode.InnerXml);
            }
            //Lägger till objektet i listan
            Main.objects.Add(new Frog(Content.Load<Texture2D>(texture), new Vector2(X, Y), walkLeft, maxSpeed, maxX, minX));
        }
        //Om det är en fågel som ska läggas till Anton
        static void AddBird(ContentManager Content, XmlNode child)
        {
            //Standardvärden ifall något går fel
            string texture = "bird";
            float X = 500, Y = 0, maxSpeed = 0, maxX = 0, minX = 0;
            bool walkLeft = true;
            //går igenom för att se ifall några värden ska ändras
            foreach (XmlNode childNode in child.ChildNodes)
            {
                if (childNode.Name == "Texture")
                    texture = childNode.InnerXml;
                else if (childNode.Name == "X")
                    X = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "Y")
                    Y = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "MaxSpeed")
                    maxSpeed = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "MaxX")
                    maxX = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "MinX")
                    minX = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "WalkLeft")
                    walkLeft = bool.Parse(childNode.InnerXml);
            }
            //Lägger till objektet i listan
            Main.objects.Add(new Bird(Content.Load<Texture2D>(texture), new Vector2(X, Y), walkLeft, maxSpeed, maxX, minX));
        }
        //Om det är en igelkott som ska läggas till Anton
        static void AddHedgehog(ContentManager Content, XmlNode child)
        {
            //Standardvärden ifall något går fel
            string texture = "hedgehog";
            float X = 0, Y = 0, maxSpeed = 0, minX = 0, maxX = 0;
            bool walkLeft = true;
            //Kollar om några värden ska ändras
            foreach (XmlNode childNode in child.ChildNodes)
            {
                if (childNode.Name == "Texture")
                    texture = childNode.InnerXml;
                else if (childNode.Name == "X")
                    X = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "Y")
                    Y = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "MaxSpeed")
                    maxSpeed = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "MaxX")
                    maxX = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "MinX")
                    minX = float.Parse(childNode.InnerXml, CultureInfo.InvariantCulture);
                else if (childNode.Name == "WalkLeft")
                    walkLeft = bool.Parse(childNode.InnerXml);
            }
            //Lägger till objektet i listan
            Main.objects.Add(new Hedgehog(Content.Load<Texture2D>(texture), new Vector2(X, Y), walkLeft, maxSpeed, maxX, minX));
        }
    }
}
