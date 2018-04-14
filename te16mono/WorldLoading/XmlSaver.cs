using te16mono.LevelBuilder;
using System.IO;

namespace te16mono
{
    static class XmlSaver
    {
        //Anton

        public static void Save(string name)
        {
            string document = ""; //Stringen som ska sparas

            //startar både map och movingobject taggen
            document += " <map><MovingObjects>";
            document = SaveMovingObjects(document);
            document = SaveBlocks(document);
            document = SaveEffect(document);

            //Sparar filen i mappen programmet körs ifrån med namnet [name].xml
            File.WriteAllText(name + ".xml", document);
        }
        //Lägger till alla efffects i dokument
        private static string SaveEffect(string document)
        {
            foreach (Point effect in MainLevelBuilder.effects)
            {
                document += "<" + effect.name + "><X>" + effect.position.X + "</X><Y>" + effect.position.Y + "</Y><Worth>" + effect.worth +
                    "</Worth></" + effect.name + ">";
            }
            //Stänger effect och map taggarna
            document += "</Effects></map>";
            return document;
        }

        //Lägger till alla blocks i dokumentet
        private static string SaveBlocks(string document)
        {
            foreach (Block block in MainLevelBuilder.blocks)
            {
                document += "<" + block.name + "><X>" + block.position.X + "</X><Y>" + block.position.Y +
                    "</Y><Width>" + block.width + "</Width><Height>" + block.height + "</Height><VelocityX>" + block.velocity.X +
                    "</VelocityX><VelocityY>" + block.velocity.Y + "</VelocityY></" + block.name + ">";
            }
            //Stänger block taggen och startar effects
            document += "</Blocks><Effects>";
            return document;
        }

        //Lägger till alla movingobjects i dokumentet
        static string SaveMovingObjects(string document)
        {
            foreach (MovingObjects movingObject in MainLevelBuilder.movingObjects)
            {
                document += "<" + movingObject.name + "><X>" + movingObject.position.X + "</X><Y>"
                    + movingObject.position.Y + "</Y><WalkLeft>true</WalkLeft><MaxSpeed>" + movingObject.maxSpeed +
                    "</MaxSpeed><MaxX>" + movingObject.maxX + "</MaxX><MinX>" + movingObject.minX + "</MinX></" + movingObject.name + ">";
            }
            //Stänger movingobject taggen och startar blocks
            document += "</MovingObjects><Blocks>";
            return document;
        }
    }
}
