using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Thesis.Services
{
    public class FileReadingService
    {
        private string _filePath;
        private int _timeOffSet;

        public FileReadingService(string path, int timeOffSet = 1000)
        {
            _filePath = path;
            _timeOffSet = timeOffSet;
        }

        public void ReadFromFile(Queue<string> elements)
        {
            using(StreamReader reader = new StreamReader(_filePath))
            {
                string? line = string.Empty;
                while (true)
                {
                    line = reader.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        elements.Enqueue(line);
                    }
                    Thread.Sleep(_timeOffSet);
                }
            }
        }
    }
}
