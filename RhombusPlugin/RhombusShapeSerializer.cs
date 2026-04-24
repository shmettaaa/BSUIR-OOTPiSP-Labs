using Figures;
using RhombusPlugin;
using System.Windows.Media;
using System.Xml;
using System.Globalization;

public class RhombusShapeSerializer : ShapeSerializerBase<RhombusShape>
{
    protected override void WriteInternal(XmlWriter writer, RhombusShape shape)
    {
        writer.WriteAttributeString("CenterX", shape.CenterX.ToString(CultureInfo.InvariantCulture));
        writer.WriteAttributeString("CenterY", shape.CenterY.ToString(CultureInfo.InvariantCulture));
        writer.WriteAttributeString("Width", shape.Width.ToString(CultureInfo.InvariantCulture));
        writer.WriteAttributeString("Height", shape.Height.ToString(CultureInfo.InvariantCulture));
        writer.WriteAttributeString("Stroke", BrushHelper.ToArgbString(shape.Stroke as SolidColorBrush));
        writer.WriteAttributeString("Fill", BrushHelper.ToArgbString(shape.Fill as SolidColorBrush));
        writer.WriteAttributeString("StrokeThickness", shape.StrokeThickness.ToString());
    }

    protected override RhombusShape ReadInternal(XmlReader reader)
    {
        double cx = double.Parse(reader.GetAttribute("CenterX"), CultureInfo.InvariantCulture);
        double cy = double.Parse(reader.GetAttribute("CenterY"), CultureInfo.InvariantCulture);
        double w = double.Parse(reader.GetAttribute("Width"), CultureInfo.InvariantCulture);
        double h = double.Parse(reader.GetAttribute("Height"), CultureInfo.InvariantCulture);
        var stroke = BrushHelper.FromArgbString(reader.GetAttribute("Stroke"));
        var fill = BrushHelper.FromArgbString(reader.GetAttribute("Fill"));
        double thickness = double.Parse(reader.GetAttribute("StrokeThickness"), CultureInfo.InvariantCulture);
        return new RhombusShape(cx, cy, w, h, stroke, fill, thickness);
    }
}