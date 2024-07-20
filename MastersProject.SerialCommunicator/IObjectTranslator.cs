namespace MastersProject.Serial
{
    public interface IObjectTranslator<TData>
    {
        TData Translate(string data);
    }
}
