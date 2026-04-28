using Figures;
using NewGraphicEditor.Data;

namespace FiguresApp.Adapters
{
    public class TrapeziumShapeWrapper : ClosedShape
    {
        public Shapes InnerShape { get; }

        public TrapeziumShapeWrapper(Shapes inner)
        {
            InnerShape = inner;
        }
    }
}