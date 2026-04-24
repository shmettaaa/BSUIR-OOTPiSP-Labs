using System;
using System.IO;
using System.Xml;

class Program
{
    static void Main(string[] args)
    {
        var path = args.Length>0?args[0]:"D:\\vsProjects\\BSUIR OOTPiSP Labs\\OOTPiSP Lab4\\pretty.xml";
        var xml = File.ReadAllText(path);
        Console.WriteLine("XML length: " + xml.Length);
        Console.WriteLine("---BEGIN XML---");
        Console.WriteLine(xml);
        Console.WriteLine("---END XML---");

        var settings = new XmlReaderSettings { IgnoreWhitespace = true };
        using (var reader = XmlReader.Create(new StringReader(xml), settings))
        {
            while (reader.Read())
            {
                Console.WriteLine($"NodeType={reader.NodeType} Name={reader.Name} Value={reader.Value}");
                if (reader.NodeType==XmlNodeType.Element && reader.Name=="Shape")
                {
                    Console.WriteLine("IsEmptyElement=" + reader.IsEmptyElement);
                    if (reader.HasAttributes)
                    {
                        while (reader.MoveToNextAttribute())
                        {
                            Console.WriteLine($"  Attr: {reader.Name}='{reader.Value}'");
                        }
                        reader.MoveToElement();
                    }
                }
            }
        }
    }
}
