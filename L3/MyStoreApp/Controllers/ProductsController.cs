using Microsoft.AspNetCore.Mvc;
using MyStoreApp.Models;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
public class ProductsController : Controller
{
    public IActionResult Index(string search, string category, bool? isAvailable, string sortOrder)
    {
        var products = JsonFileHelper.LoadProducts();

        // Filtrowanie i wyszukiwanie
        if (!string.IsNullOrEmpty(search))
        {
            products = products.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase) || 
                                   p.Id.ToString() == search).ToList();
        }

        if (!string.IsNullOrEmpty(category))
        {
            products = products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (isAvailable.HasValue)
        {
            products = products.Where(p => p.IsAvailable == isAvailable.Value).ToList();
        }

        // Sortowanie według ceny, nazwy lub kategorii, jeśli podano opcję sortowania
    if (!string.IsNullOrEmpty(sortOrder))
    {
        products = sortOrder switch
        {
            "name" => products.OrderBy(p => p.Name).ToList(),
            "name_desc" => products.OrderByDescending(p => p.Name).ToList(),
            "price" => products.OrderBy(p => p.Price).ToList(),
            "price_desc" => products.OrderByDescending(p => p.Price).ToList(),
            "category" => products.OrderBy(p => p.Category).ToList(),
            "category_desc" => products.OrderByDescending(p => p.Category).ToList(),
            _ => products.ToList(),
        };
}


        return View(products);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        var products = JsonFileHelper.LoadProducts();
        product.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;
        products.Add(product);
        JsonFileHelper.SaveProducts(products);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var products = JsonFileHelper.LoadProducts();
        var product = products.FirstOrDefault(p => p.Id == id);
        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        var products = JsonFileHelper.LoadProducts();
        var existingProduct = products.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Category = product.Category;
            existingProduct.Price = product.Price;
            // existingProduct.Quantity = product.Quantity;
            existingProduct.IsAvailable = product.IsAvailable;
            JsonFileHelper.SaveProducts(products);
        }
        return RedirectToAction("Index");
    }

    // GET: /Products/Delete/5
    public IActionResult Delete(int id)
    {
        var products = JsonFileHelper.LoadProducts();
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }
    public IActionResult DeleteConfirmed(int id)
    {
        var products = JsonFileHelper.LoadProducts();
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            products.Remove(product);
            JsonFileHelper.SaveProducts(products);
        }
        return RedirectToAction("Index");
    }

        public async Task<IActionResult> AveragePricePerCategory()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "products.json");
        var jsonData = await System.IO.File.ReadAllTextAsync(filePath);
        var products = JsonConvert.DeserializeObject<List<Product>>(jsonData);

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
