namespace Figures
{
    public interface IDataTransformer
    {
        string Name { get; }

        string TransformBeforeSave(string data);

        string TransformAfterLoad(string data);
    }
}