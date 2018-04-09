using te16mono.LevelBuilder;
using System.IO;

namespace te16mono
{
    static class XmlSaver
    {
        public static void Save(string name)
        {
            string document = "";
            document += " <map><MovingObjects>";
            foreach (MovingObjects movingObject in MainLevelBuilder.movingObjects)
            {
                document += "<" + movingObject.name + "><X>" + movingObject.position.X + "</X><Y>" 
                    + movingObject.position.Y + "</Y><WalkLeft>true</WalkLeft><MaxSpeed>" + movingObject.maxSpeed + 
                    "</MaxSpeed><MaxX>" + movingObject.maxX + "</MaxX><MinX>" + movingObject.minX + "</MinX></" + movingObject.name + ">";
            }
            document += "</MovingObjects><Blocks>";
            foreach (Block block in MainLevelBuilder.blocks)
            {
                document += "<" + block.name + "><X>" + block.position.X + "</X><Y>" + block.position.Y + 
                    "</Y><Width>" + block.width + "</Width><Height>" + block.height + "</Height><VelocityX>" + block.velocity.X + 
                    "</VelocityX><VelocityY>" + block.velocity.Y + "</VelocityY></" + block.name + ">";
            }
            document += "</Blocks><Effects>";
            foreach (Point effect in MainLevelBuilder.effects)
            {
                document += "<" + effect.name + "><X>" + effect.position.X + "</X><Y>" + effect.position.Y + "</Y><Worth>" + effect.worth + 
                    "</Worth></" + effect.name + ">";
            }
            document += "</Effects></map>";

            File.WriteAllText(name + ".xml", document);
        }
    }
}
