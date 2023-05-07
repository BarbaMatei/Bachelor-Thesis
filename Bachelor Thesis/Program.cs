// See https://aka.ms/new-console-template for more information
using Bachelor_Thesis.Constants;
using Bachelor_Thesis.Domain;
using Bachelor_Thesis.Indexes;
using Bachelor_Thesis.Services;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;

string filePath = "../../../Files/fruits.txt";
string inventoryFilePath = "../../../Files/Inventory.json";

ConcurrentQueue<InventoryData> customObjects = new();

var readingTask = Task.Factory.StartNew(() =>
{
    new FileReadingService<InventoryData>(inventoryFilePath, StreamGenerationMillisecondsOffset.OneSecondOffset).ReadFromFileJson(customObjects, 10);
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

var hashIndex = new HashIndex<InventoryData>(new List<PropertyInfo> { new InventoryData().GetType().GetProperty(PropertyNames.ProductId) });
hashIndex.SetTimestampProperty(new InventoryData().GetType().GetProperty(PropertyNames.Timestamp));
for (int i = 0; i < 1000 && customObjects.Any(); i++)
{
    if (customObjects.TryDequeue(out var customObject))
    {
        hashIndex.AddElement(customObject);
    }
}

Console.WriteLine("********** Before Removal **********");
hashIndex.PrintIndexList();

Console.WriteLine("********** After Removal **********");
hashIndex.RemoveElementsBeforeDate(DateTime.Now.AddSeconds(-5));
hashIndex.PrintIndexList();

int sdsad = 0;


