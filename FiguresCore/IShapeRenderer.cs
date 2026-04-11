using System.Windows.Controls;

namespace Figures.Renderers
{
    public interface IShapeRenderer
    {
        void Render(Shape shape, Canvas canvas);
    }
}