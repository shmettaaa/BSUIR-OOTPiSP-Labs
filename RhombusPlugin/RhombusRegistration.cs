using Figures;

namespace RhombusPlugin
{
    public class RhombusRegistration : IFigureRegistration
    {
        public void Register()
        {
            FigureRegistry.Register(
                "Ромб",                              
                new FigureHandler(
                    new RhombusFactory(),
                    new RhombusRenderer()
                )
            );
        }
    }
}