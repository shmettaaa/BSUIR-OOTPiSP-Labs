using Figures;
using System.Windows.Media;
using System.Xml;
using System.Globalization;

public class EllipseShapeSerializer : ShapeSerializerBase<EllipseShape>
{
    protected override void WriteInternal(XmlWriter writer, EllipseShape shape)
    {
        writer.WriteAttributeString("Cx", shape.Cx.ToString());
        writer.WriteAttributeString("Cy", shape.Cy.ToString());
        writer.WriteAttributeString("Width", shape.Width.ToString());
        writer.WriteAttributeString("Height", shape.Height.ToString());
        writer.WriteAttributeString("Stroke", BrushHelper.ToArgbString(shape.Stroke as SolidColorBrush));
        writer.WriteAttributeString("Fill", BrushHelper.ToArgbString(shape.Fill as SolidColorBrush));
        writer.WriteAttributeString("StrokeThickness", shape.StrokeThickness.ToString());
    }

    protected override EllipseShape ReadInternal(XmlReader reader)
    {
        double cx = double.Parse(reader.GetAttribute("Cx"), CultureInfo.InvariantCulture);
        double cy = double.Parse(reader.GetAttribute("Cy"), CultureInfo.InvariantCulture);
        double w = double.Parse(reader.GetAttribute("Width"), CultureInfo.InvariantCulture);
        double h = double.Parse(reader.GetAttribute("Height"), CultureInfo.InvariantCulture);
        var stroke = BrushHelper.FromArgbString(reader.GetAttribute("Stroke"));
        var fill = BrushHelper.FromArgbString(reader.GetAttribute("Fill"));
        double thickness = double.Parse(reader.GetAttribute("StrokeThickness"));
        return new EllipseShape(cx, cy, w, h, stroke, fill, thickness);
    }
}