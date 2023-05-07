using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Bachelor_Thesis.Constants;
using System.Collections.Concurrent;

namespace Bachelor_Thesis.Services
{
    public class FileReadingService<T>
    {
        private string _filePath;
        private int _timeOffSet;

        public FileReadingService(string path, int timeOffSet = 20)
        {
            _filePath = path;
            _timeOffSet = timeOffSet < 20 ? 20 : timeOffSet;
        }

        public void ReadFromFileJson(ConcurrentQueue<T> elements, int maximumElements = 0)
        {
            using (JsonDocument reader = JsonDocument.Parse(File.OpenRead(_filePath)))
            {
                JsonElement root = reader.RootElement;
                T? jsonElement;
                foreach (JsonElement element in root.EnumerateArray())
                {
                    jsonElement = JsonSerializer.Deserialize<T>(element.GetRawText());
                    if (jsonElement != null)
                    {
                        jsonElement.GetType().GetProperty(PropertyNames.Timestamp).SetValue(jsonElement, DateTime.Now);
                        elements.Enqueue(jsonElement);
                    }
                    Task.Delay(_timeOffSet).Wait();
                    if(maximumElements != 0 && elements.Count == maximumElements)
                    {
                        break;
                    }
                }
            }
        }
    }
}
