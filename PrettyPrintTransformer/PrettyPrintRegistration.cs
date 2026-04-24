using Figures;

public class PrettyPrintRegistration : IFigureRegistration
{
    public void Register()
    {
        TransformerRegistry.Register(new PrettyPrintTransformer());
    }
}