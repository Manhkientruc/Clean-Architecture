using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using ProductManagement.Web.Models;

namespace ProductManagement.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductsController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient("API");
        var response = await client.GetAsync("products");

        if (!response.IsSuccessStatusCode)
        {
            return View("Error");
        }

        var content = await response.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<List<Product>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return View(products);
    }
    // GET: Products/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Products/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (!ModelState.IsValid) return View(product);

        var client = _httpClientFactory.CreateClient("API");
        var response = await client.PostAsJsonAsync("products", product);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Lỗi gọi API");
            return View(product);
        }

        return RedirectToAction(nameof(Index));
    }
    // GET: Products/Edit/{id}
    public async Task<IActionResult> Edit(Guid id)
    {
        var client = _httpClientFactory.CreateClient("API");
        var response = await client.GetAsync($"products/{id}");

        if (!response.IsSuccessStatusCode)
        {
            return NotFound();
        }

        var content = await response.Content.ReadAsStringAsync();
        var product = JsonSerializer.Deserialize<Product>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return View(product);
    }

    // POST: Products/Edit/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, Product product)
    {
        if (id != product.Id)
            return BadRequest();

        if (!ModelState.IsValid) return View(product);

        var client = _httpClientFactory.CreateClient("API");
        var response = await client.PutAsJsonAsync($"products/{id}", product);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Lỗi cập nhật API");
            return View(product);
        }

        return RedirectToAction(nameof(Index));
    }
    // GET: Products/Delete/{id}
    public async Task<IActionResult> Delete(Guid id)
    {
        var client = _httpClientFactory.CreateClient("API");
        var response = await client.GetAsync($"products/{id}");

        if (!response.IsSuccessStatusCode)
        {
            return NotFound();
        }

        var content = await response.Content.ReadAsStringAsync();
        var product = JsonSerializer.Deserialize<Product>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return View(product);
    }

    // POST: Products/Delete/{id}
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var client = _httpClientFactory.CreateClient("API");
        var response = await client.DeleteAsync($"products/{id}");

        if (!response.IsSuccessStatusCode)
        {
            return View("Error");
        }

        return RedirectToAction(nameof(Index));
    }
}
