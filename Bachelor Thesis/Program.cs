// See https://aka.ms/new-console-template for more information
using Bachelor_Thesis.Domain;
using Bachelor_Thesis.Indexes;
using Bachelor_Thesis.Services;
using System.Diagnostics;
using System.Reflection;

string filePath = "../../../Files/fruits.txt";
string inventoryFilePath = "../../../Files/Inventory.json";

Queue<InventoryData> customObjects = new();

var readingTask = Task.Factory.StartNew(() =>
{
    new FileReadingService<InventoryData>(inventoryFilePath).ReadFromFileJson(customObjects);
});


/*var writingTask = Task.Factory.StartNew(() =>
{
    while (true)
    {
        if (customObjects.Any())
        {
            Console.WriteLine(customObjects.Dequeue());
        }
    }
});*/

Task.WaitAll(readingTask);

var hashIndex = new HashIndex<InventoryData>(new List<PropertyInfo> { new InventoryData().GetType().GetProperty("ProductId") });
for (int i = 0; i < 1000; i++)
{
    hashIndex.AddElement(customObjects.Dequeue());
}

//hashIndex.RemoveFirstElementAdded();

hashIndex.PrintIndex();

int sdsad = 0;


