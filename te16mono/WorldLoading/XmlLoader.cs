using Microsoft.Xna.Framework.Content;
using System;
using System.Xml;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Globalization;

namespace te16mono
{
    //Anton
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
        static void LoadMovingObjects(ContentManager Content, XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                //Om det är en igelkott
                if (child.Name == "Hedgehog")
                {
                    //Standardvärden ifall något går fel
                    string texture = "hedgehog";
                    float X = 0, Y = 0, maxSpeed = 0, minX = 0, maxX = 0;
                    bool walkLeft = true;
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

                    Main.testObjects.Add(new Hedgehog(Content.Load<Texture2D>(texture), new Vector2(X, Y), walkLeft, maxSpeed, maxX, minX));
                }
                //Om det är en fågel
                if (child.Name == "Bird")
                {
                    //Standardvärden ifall något går fel
                    string texture = "bird";
                    float X = 500, Y = 0, maxSpeed = 0, maxX = 0, minX = 0;
                    bool walkLeft = true;
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

                    Main.testObjects.Add(new Bird(Content.Load<Texture2D>(texture), new Vector2(X, Y), walkLeft, maxSpeed, maxX, minX));
                }
                //Ifall det är en groda
                if (child.Name == "Frog")
                {
                    //Standardvärden ifall något går fel
                    string texture = "frog";
                    float X = 0, Y = 0, maxSpeed = 0, maxX = 0, minX = 0;
                    bool walkLeft = true;
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

                    Main.testObjects.Add(new Frog(Content.Load<Texture2D>(texture), new Vector2(X, Y), walkLeft, maxSpeed, maxX, minX));
                }
                //Ifall det är en groda
                if (child.Name == "Katt")
                {
                    //Standardvärden ifall något går fel
                    string texture = "katt";
                    float X = 0, Y = 0, maxSpeed = 0, maxX = 0, minX = 0;
                    bool walkLeft = true;
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

                    Main.testObjects.Add(new Katt(Content.Load<Texture2D>(texture), new Vector2(X, Y), walkLeft, maxSpeed, maxX, minX));
                }
            }
        }

        static void LoadBlocks(ContentManager Content, XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                //Om det är ett vanligt block
                if (child.Name == "Block")
                {
                    string texture = "square";
                    float X = 0, Y = 0, velocityY = 0, velocityX = 0;
                    int width = 0, height = 0;
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
                    Main.testBlocks.Add(new Block(new Vector2(X, Y), width, height, new Vector2(velocityX, velocityY), Content.Load<Texture2D>(texture)));
                }
            }
        }

        static void LoadPoints(ContentManager Content, XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name == "Health")
                {
                    int worth = 1;
                    string texture = "pear";
                    float X = 0, Y = 0;
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


                    
                    Main.effects.Add(new Health(new Vector2(X, Y), Content.Load<Texture2D>(texture), worth));

                }

                if (child.Name == "Immortality")
                {
                    int worth = 1;
                    string texture = "shield";
                    float X = 0, Y = 0;
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



                    Main.effects.Add(new Immortality(new Vector2(X, Y), Content.Load<Texture2D>(texture), worth));

                }

                if (child.Name == "Whammy")
                {
                    int worth = 1;
                    string texture = "iceblock";
                    float X = 0, Y = 0;
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



                    Main.effects.Add(new Whammy(new Vector2(X, Y), Content.Load<Texture2D>(texture), worth));

                }

                if (child.Name == "HighGravity")
                {
                    int worth = 1;
                    string texture = "weight";
                    float X = 0, Y = 0;
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



                    Main.effects.Add(new HighGravity(new Vector2(X, Y), Content.Load<Texture2D>(texture), worth));

                }

                if (child.Name == "Point")
                {
                    int worth = 100;
                    string texture = "goldbag";
                    float X = 0, Y = 0;
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
                    Main.effects.Add(new Point(new Vector2(X, Y), Content.Load<Texture2D>(texture), worth));
                }
                if (child.Name == "Finish")
                {
                    int worth = 0;
                    string texture = "finishFlag";
                    float X = 0, Y = 0;
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
                    Main.effects.Add(new FinishLine(new Vector2(X, Y), Content.Load<Texture2D>(texture), worth));
                }
            }
        }
    }
}
