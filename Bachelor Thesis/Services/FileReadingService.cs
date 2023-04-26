using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Bachelor_Thesis.Services
{
    public class FileReadingService<T>
    {
        private string _filePath;
        private int _timeOffSet;

        public FileReadingService(string path, int timeOffSet = 1)
        {
            _filePath = path;
            _timeOffSet = timeOffSet;
        }

        public void ReadFromFileJson(Queue<T> elements)
        {
            using(JsonDocument reader = JsonDocument.Parse(File.OpenRead(_filePath)))
            {
                var sw = new Stopwatch();
                sw.Start();
                JsonElement root = reader.RootElement;
                T? jsonElement;
                foreach(JsonElement element in root.EnumerateArray())
                {
                    jsonElement = JsonSerializer.Deserialize<T>(element.GetRawText());
                    if(jsonElement != null)
                    {
                        jsonElement.GetType().GetProperty("Timestamp").SetValue(jsonElement, DateTime.Now);
                        elements.Enqueue(jsonElement);
                    }
                    //Thread.Sleep(_timeOffSet);
                }
                Console.WriteLine("sync: Running for {0} seconds", sw.Elapsed.TotalSeconds);
                Console.WriteLine("capacity: {0}", elements.Count);
            }
        }
    }
}
