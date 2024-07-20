using MastersProject.App.Models;
using MastersProject.Serial;

namespace MastersProject.App.Translators
{
    internal class DefaultTranslator : IObjectTranslator<SerialData>
    {
        public SerialData Translate(string data)
        {
            string[] parts = data.Trim().Split(';');
            return new()
            {
                Pitch = int.Parse(parts[1]),
                Roll = int.Parse(parts[2]),
                Timestamp = int.Parse(parts[3]),
                Index = int.Parse(parts[4])
            };
        }
    }
}
