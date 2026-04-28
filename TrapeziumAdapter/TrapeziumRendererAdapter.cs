using System.Reflection;
using System.Windows.Controls;
using Figures;
using Figures.Renderers;

namespace TrapeziumAdapter
{
    public class TrapeziumRendererAdapter : IShapeRenderer
    {
        private readonly object _friendFactory;
        private readonly MethodInfo _drawMethod;

        public TrapeziumRendererAdapter(object friendFactory)
        {
            _friendFactory = friendFactory;
            _drawMethod = friendFactory.GetType().GetMethod("Draw");
        }

        public void Render(Shape shape, Canvas canvas)
        {
            if (shape is TrapeziumShapeWrapper wrapper)
            {
                // Just call friend's Draw method!
                _drawMethod?.Invoke(_friendFactory, new object[] { canvas, wrapper.FriendShape });
            }
        }
    }
}