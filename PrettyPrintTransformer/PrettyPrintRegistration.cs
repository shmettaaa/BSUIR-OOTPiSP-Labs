using Figures;

public class PrettyPrintRegistration : IFigureRegistration
{
    public void Register()
    {
        TransformerRegistry.Instance.Register(new PrettyPrintTransformer());
    }
}