using Figures;

public class HtmlReportRegistration : IFigureRegistration
{
    public void Register()
    {
        TransformerRegistry.Instance.Register(new HtmlReportTransformer());
    }
}