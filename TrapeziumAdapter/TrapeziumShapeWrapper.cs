using Figures;
using System.Windows.Media;
using static System.Net.WebRequestMethods;

namespace TrapeziumAdapter
{
    public class TrapeziumShapeWrapper : Shape
    {
        public object FriendShape { get; set; }

        public TrapeziumShapeWrapper(object friendShape, Brush stroke, Brush fill, double thickness)
        {
            FriendShape = friendShape;
            Stroke = stroke;
            Fill = fill;
            StrokeThickness = thickness;
        }
    }
}