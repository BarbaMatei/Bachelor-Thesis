using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Thesis.Services
{
    public class FileReadingService<T>
    {
        private string _filePath;
        private int _timeOffSet;

        public FileReadingService(string path, int timeOffSet = 1000)
        {
            _filePath = path;
            _timeOffSet = timeOffSet;
        }

        public void ReadFromFileJson(Queue<T> elements)
        {
            using(JsonDocument reader = JsonDocument.Parse(File.OpenRead(_filePath)))
            {
                JsonElement root = reader.RootElement;
                foreach(JsonElement element in root.EnumerateArray())
                {
                    var jsonElement = JsonSerializer.Deserialize<T>(element.GetRawText());
                    if(jsonElement != null)
                    {
                        elements.Enqueue(jsonElement);
                    }
                    Thread.Sleep(_timeOffSet);
                }
            }
        }
    }
}
