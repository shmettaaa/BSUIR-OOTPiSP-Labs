using Figures;
using NewGraphicEditor.Controls;
using NewGraphicEditor.Data;
using TrapeziumLibrary;

namespace FiguresApp.Adapters
{
    public class TrapeziumRegistration : IFigureRegistration
    {
        public void Register()
        {
            FigureRegistry.Register(
                "Трапеция",
                new FigureHandler(
                    new TrapeziumHostFactory(),
                    new TrapeziumDrawableAdapter(new TrapeziumFactory())
                )
            );
        }
    }
}