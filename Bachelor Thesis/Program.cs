// See https://aka.ms/new-console-template for more information
using Bachelor_Thesis.Services;

string filePath = "../../../Files/fruits.txt";

Queue<string> elements = new();

var readingTask = Task.Factory.StartNew(() =>
{
    new FileReadingService(filePath).ReadFromFile(elements);
});


var writingTask = Task.Factory.StartNew(() =>
{
    while (true)
    {
        if (elements.Any())
        {
            Console.WriteLine(elements.Dequeue());
        }
    }
});

Task.WaitAll(readingTask, writingTask);
