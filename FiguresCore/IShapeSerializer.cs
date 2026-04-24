using System.Xml;

namespace Figures
{
    public interface IShapeSerializer
    {
        void Write(XmlWriter writer, Shape shape);
        Shape Read(XmlReader reader);
    }
}