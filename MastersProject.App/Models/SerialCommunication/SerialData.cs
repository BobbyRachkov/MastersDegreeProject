namespace MastersProject.App.Models.SerialCommunication
{
    public sealed class SerialData
    {
        public int Pitch { get; set; }
        public int Roll { get; set; }
        public long Timestamp { get; set; }
        public int Index { get; set; }
    }
}
