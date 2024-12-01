using Microsoft.AspNetCore.Mvc;
using MyStoreApp.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class ChartsController : Controller
{
    public async Task<IActionResult> AveragePricePerCategory()
    {
        var products = JsonFileHelper.LoadProducts();

        var categoryPrices = products
            .GroupBy(p => p.Category)
            .ToDictionary(g => g.Key, g => g.Select(p => p.Price).ToList());

        var categories = categoryPrices.Keys.ToList();
        var averagePrices = categories.Select(category =>
        {
            var prices = categoryPrices[category];
            var total = prices.Sum();
            return prices.Count > 0 ? (total / prices.Count).ToString("F2") : "0";
        }).ToList();

        ViewData["Categories"] = categories;
        ViewData["AveragePrices"] = averagePrices;

        return View();
    }
}

