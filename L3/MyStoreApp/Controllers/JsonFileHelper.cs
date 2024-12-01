using System.Collections.Generic;
using System.IO;
using MyStoreApp.Models;
using Newtonsoft.Json;

public static class JsonFileHelper
{
    private static string filePath = "Data/products.json";

    public static List<Product> LoadProducts()
    {
        if (!File.Exists(filePath))
        {
            return new List<Product>();
        }

        var json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<Product>>(json);
    }

    public static void SaveProducts(List<Product> products)
    {
        var json = JsonConvert.SerializeObject(products, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }
}
