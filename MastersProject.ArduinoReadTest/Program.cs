using MastersProject.SerialCommunicator;
using System.Diagnostics;
using System.IO.Ports;

namespace MastersProject.ArduinoReadTest
{
    internal class Program
    {
        private static SerialPort serial = new SerialPort("COM8", 115200);//9600;115200
        private static List<SerialData> data = new();
        private static List<string> lines = new();
        private static int skip = 2;
        private static int count = 0;
        private static bool _continue = true;
        private static string text = string.Empty;
        private static HandoverData pointer = new();

        private static ISerialCommunicator<SerialData> _serialCommunicator = null!;

        public static string Text
        {
            get => text;
            set
            {
                Console.WriteLine(value);
                text = value;
            }
        }
        static void Main(string[] args)
        {
            //OldTest();
            _serialCommunicator = new SerialPortCommunicator<SerialData>("COM7", 2000000, new SerialTranslator());
            _serialCommunicator.StartAsync((d) =>
            {
                //Console.WriteLine(d);
                data.Add(d);
            });
            int millis = 5 * 1000;
            Task.Delay(millis).Wait();
            _serialCommunicator.StopAsync();

            Console.WriteLine(data.Count);
            Console.WriteLine((data.Count * 1.0 / millis) * 1000 + " per second");



        }

        private static void OldTest()
        {
            Action<string> print = (s) => Text = s;

            Thread readThread = new Thread(() => Read(print));
            Stopwatch sw = new();
            serial.Open();
            //serial.DataReceived += Serial_DataReceived;
            sw.Start();
            readThread.Start();
            Console.ReadKey();
            _continue = false;
            readThread.Join();
            sw.Stop();
            //serial.DataReceived -= Serial_DataReceived;
            serial.Close();

            Console.WriteLine(data.Count);
            Console.WriteLine($"Local timer elapsed millis: {sw.ElapsedMilliseconds}");
            Console.WriteLine($"Remote timer elapsed millis: {data.Last().Timestamp - data.First().Timestamp}");
            Console.WriteLine($"Started at: {data.First().Timestamp}");
            Console.WriteLine($"Ended at: {data.Last().Timestamp}");

            Console.WriteLine($"{data.First()}\n{data.Last()}");

            Console.WriteLine((data.Count * 1.0 / sw.ElapsedMilliseconds) * 1000);

            foreach (var element in data)
            {
                Console.WriteLine(element);
            }
        }

        private static void Read(Action<string> callback)
        {
            while (_continue)
            {
                try
                {
                    string line = serial.ReadLine();
                    data.Add(new SerialData(line));
                    lines.Add(line);
                    callback(line);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (count < skip)
            {
                count++;
                serial.ReadLine();
                return;
            }
            //if (!_continue)
            //{
            //    return;
            //}
            try
            {
                string line = serial.ReadLine();
                data.Add(new(line));
                lines.Add(line);
                //Console.WriteLine(line);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}