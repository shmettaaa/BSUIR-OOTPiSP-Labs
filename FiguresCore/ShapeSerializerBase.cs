using System.Xml;

namespace Figures
{
    public abstract class ShapeSerializerBase<T> : IShapeSerializer where T : Shape
    {
        public void Write(XmlWriter writer, Shape shape)
        {
            WriteInternal(writer, (T)shape);
        }

        public Shape Read(XmlReader reader)
        {
            return ReadInternal(reader);
        }

        protected abstract void WriteInternal(XmlWriter writer, T shape);
        protected abstract T ReadInternal(XmlReader reader);
    }
}