using Figures;

public class HtmlReportRegistration : IFigureRegistration
{
    public void Register()
    {
        TransformerRegistry.Register(new HtmlReportTransformer());
    }
}