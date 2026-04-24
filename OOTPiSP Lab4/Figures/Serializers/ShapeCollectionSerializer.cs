using System.Collections.Generic;
using System.IO;
using System.Xml;
using Figures;

public static class ShapeCollectionSerializer
{
    public static string SaveToString(IEnumerable<Shape> shapes)
    {
        using (var sw = new StringWriter())
        {
            var settings = new XmlWriterSettings { Indent = true, Encoding = System.Text.Encoding.UTF8 };
            using (var writer = XmlWriter.Create(sw, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Shapes");
                foreach (var shape in shapes)
                {
                    var serializer = ShapeSerializerRegistry.GetSerializer(shape.GetType());
                    if (serializer == null)
                        throw new System.InvalidOperationException($"No serializer for {shape.GetType().Name}");

                    writer.WriteStartElement("Shape");
                    writer.WriteAttributeString("Type", shape.GetType().AssemblyQualifiedName);
                    serializer.Write(writer, shape);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            return sw.ToString();
        }
    }

    public static List<Shape> LoadFromString(string xmlData)
    {
        var shapes = new List<Shape>();
        var settings = new XmlReaderSettings { IgnoreWhitespace = true };
        using (var reader = XmlReader.Create(new StringReader(xmlData), settings))
        {
            reader.MoveToContent();
            reader.ReadStartElement("Shapes");
            while (reader.IsStartElement("Shape"))
            {
                string typeName = reader.GetAttribute("Type");
                if (string.IsNullOrEmpty(typeName))
                {
                    reader.Skip();
                    continue;
                }
                var shapeType = System.Type.GetType(typeName);
                if (shapeType == null)
                {
                    reader.Skip();
                    continue;
                }
                var serializer = ShapeSerializerRegistry.GetSerializer(shapeType);
                if (serializer == null)
                {
                    reader.Skip();
                    continue;
                }
                var shape = serializer.Read(reader);
                shapes.Add(shape);
                reader.Read();
            }
            reader.ReadEndElement();
        }
        return shapes;
    }
}