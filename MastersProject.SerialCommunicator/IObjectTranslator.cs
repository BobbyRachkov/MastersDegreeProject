namespace MastersProject.SerialCommunicator
{
    public interface IObjectTranslator<TData>
    {
        TData Translate(string data);
    }
}
