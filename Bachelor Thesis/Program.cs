// See https://aka.ms/new-console-template for more information
using Bachelor_Thesis.Domain;
using Bachelor_Thesis.Services;

string filePath = "../../../Files/fruits.txt";
string inventoryFilePath = "../../../Files/Inventory.json";

Queue<InventoryData> customObjects = new();

var readingTask = Task.Factory.StartNew(() =>
{
    new FileReadingService<InventoryData>(inventoryFilePath).ReadFromFileJson(customObjects);
});


var writingTask = Task.Factory.StartNew(() =>
{
    while (true)
    {
        if (customObjects.Any())
        {
            Console.WriteLine(customObjects.Dequeue());
        }
    }
});

Task.WaitAll(readingTask, writingTask);

