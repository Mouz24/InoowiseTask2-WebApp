using FridgesWebApp.Models;
using FridgesWebApp.ProductUpdateRepository;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;

namespace FridgesWebApp.Controllers
{
    public class FridgesController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        string baseURL = "http://localhost:8080/";

        public FridgesController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> GetFridges(IEnumerable<FridgeEntity> fridge)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var token = HttpContext.Session.GetString("Token");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");

                if (token != null)
                {
                    HttpResponseMessage getData = await client.GetAsync("api/fridge");

                    if (getData.StatusCode.Equals(System.Net.HttpStatusCode.Forbidden))
                    {
                        return RedirectToAction("AccessDenied");
                    }
                    if (getData.IsSuccessStatusCode)
                    {
                        string results = getData.Content.ReadAsStringAsync().Result;

                        fridge = JsonConvert.DeserializeObject<IList<FridgeEntity>>(results);
                    }
                }
                else
                {
                    _logger.LogInformation("Error calling web API");
                    
                    return RedirectToAction("Unauthorized");
                }

                ViewData.Model = fridge;
            }

            return View();
        }

        public async Task<IActionResult> FillFridgesWithDefaultQuantity()
        {
            DataTable datatable = new DataTable();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                var token = HttpContext.Session.GetString("Token");
                
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");
                
                if (token != null)
                {
                    HttpResponseMessage getData = await client.GetAsync("api/fridge/productswithzeroquantity");
                    if (getData.StatusCode.Equals(System.Net.HttpStatusCode.Forbidden))
                    {
                        return RedirectToAction("AccessDenied");
                    }

                    string results = getData.Content.ReadAsStringAsync().Result;
                    
                    datatable = JsonConvert.DeserializeObject<DataTable>(results);
                }
                else
                {
                    _logger.LogInformation("Error calling web API");
                    
                    return RedirectToAction("Unauthorized");
                }

                ViewData.Model = datatable;
            }

            return View();
        }

        public async Task<IActionResult> CreateFridge(FridgeEntity fridge)
        {
            FridgeEntity obj = new FridgeEntity
            {
                Id = fridge.Id,
                Name = fridge.Name,
                Owner_name = fridge.Owner_name,
                NameOfModel = fridge.NameOfModel,
                Year = fridge.Year,
                FirstProduct = fridge.FirstProduct,
                SecondProduct = fridge.SecondProduct 
            };

            ProductUpdateEntityRepository Product = new ProductUpdateEntityRepository();

            ProductUpdateEntity ProductToAddFirst = Product.DefineFirstProduct(obj);
            ProductUpdateEntity ProductToAddSecond = Product.DefineSecondProduct(obj);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL + "api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var token = HttpContext.Session.GetString("Token");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");

                if (token != null)
                {
                    HttpResponseMessage getData = await client.PostAsJsonAsync<FridgeEntity>("fridge", obj);
                    if (getData.StatusCode.Equals(System.Net.HttpStatusCode.Forbidden))
                    {
                        return RedirectToAction("AccessDenied");
                    }
                    else if (getData.IsSuccessStatusCode)
                    {
                        getData = await client.GetAsync("fridge");

                        string results = getData.Content.ReadAsStringAsync().Result;

                        var CreatedFridge = JsonConvert.DeserializeObject<IList<FridgeEntity>>(results);

                        string FridgeId = (from i in CreatedFridge where (obj.Owner_name == i.Owner_name) select i.Id).First().ToString();

                        await client.PostAsJsonAsync<ProductUpdateEntity>($"fridge/{FridgeId}/products", ProductToAddFirst);
                        await client.PostAsJsonAsync<ProductUpdateEntity>($"fridge/{FridgeId}/products", ProductToAddSecond);

                        return RedirectToAction("GetFridges");
                    }
                }
                else
                {
                    _logger.LogInformation("Error calling web API");

                    return RedirectToAction("Unauthorized");
                }
            }

            return View();
        }

        public async Task<IActionResult> EditFridge(FridgeEntity fridge)
        {
            FridgeEntity obj = new FridgeEntity
            {
                Id = fridge.Id,
                Name = fridge.Name,
                Owner_name = fridge.Owner_name,
                NameOfModel = fridge.NameOfModel,
                Year = fridge.Year
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL + "api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var token = HttpContext.Session.GetString("Token");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");

                if (token != null)
                {
                    HttpResponseMessage getData = await client.PutAsJsonAsync<FridgeEntity>($"fridge/{fridge.Id}", obj);
                    if (getData.StatusCode.Equals(System.Net.HttpStatusCode.Forbidden))
                    {
                        return RedirectToAction("AccessDenied");
                    }
                    else if (getData.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetFridges");
                    }
                }
                else
                {
                    _logger.LogInformation("Error calling web API");

                    return RedirectToAction("Unauthorized");
                }
            }

            return View();
        }

        public async Task<IActionResult> DeleteFridge(FridgeEntity fridge)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL + "api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var token = HttpContext.Session.GetString("Token");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");

                if (token != null)
                {
                    HttpResponseMessage getData = await client.DeleteAsync($"fridge/{fridge.Id}");
                    if (getData.StatusCode.Equals(System.Net.HttpStatusCode.Forbidden))
                    {
                        return RedirectToAction("AccessDenied");
                    }
                    else if (getData.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetFridges");
                    }
                }
                else
                {
                    _logger.LogInformation("Error calling web API");
                    
                    return RedirectToAction("Unauthorized");
                }
            }

            return View();
        }

        public async Task<IActionResult> GetFridgeProducts(FridgeEntity fridge)
        {
            IEnumerable<FridgeProductsEntity> products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL + "api/fridge/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var token = HttpContext.Session.GetString("Token");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");

                if (token != null)
                {
                    HttpResponseMessage getData = await client.GetAsync($"{fridge.Id}/products");

                    if (getData.StatusCode.Equals(System.Net.HttpStatusCode.Forbidden))
                    {
                        return RedirectToAction("AccessDenied");
                    }
                    else if (getData.IsSuccessStatusCode)
                    {
                        string results = getData.Content.ReadAsStringAsync().Result;

                        products = JsonConvert.DeserializeObject<IList<FridgeProductsEntity>>(results);
                    }
                }
                else
                {
                    _logger.LogInformation("Error calling web API");

                    return RedirectToAction("Unauthorized");
                }

                if (products is null)
                {
                    ViewData.Add("Fridge_Id", fridge.Id.ToString());
                }

                ViewData.Model = products;
            }

            return View();
        }

        public async Task<IActionResult> EditFridgeProduct(FridgeProductsEntity fridge_products, ProductUpdateEntity product)
        {
            ProductUpdateEntity ProductToUpdate = new ProductUpdateEntity
            {
                Name = product.Name,
                Quantity = product.Quantity,
                Default_quantity = product.Default_quantity
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL + $"api/fridge/{fridge_products.Fridge_Id}/products/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var token = HttpContext.Session.GetString("Token");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");

                if (token != null)
                {
                    HttpResponseMessage getData = await client.PutAsJsonAsync<ProductUpdateEntity>($"{fridge_products.Product_Id}", ProductToUpdate);
                    if (getData.StatusCode.Equals(System.Net.HttpStatusCode.Forbidden))
                    {
                        return RedirectToAction("AccessDenied");
                    }
                    else if (getData.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetFridges");
                    }
                }
                else
                {
                    _logger.LogInformation("Error calling web API");

                    return RedirectToAction("Unauthorized");
                }
            }

            return View();
        }

        public async Task<IActionResult> DeleteFridgeProduct(FridgeProductsEntity fridge_products)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL + $"api/fridge/{fridge_products.Fridge_Id}/products/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var token = HttpContext.Session.GetString("Token");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");

                if (token != null)
                {
                    HttpResponseMessage getData = await client.DeleteAsync($"{fridge_products.Product_Id}");
                    if (getData.StatusCode.Equals(System.Net.HttpStatusCode.Forbidden))
                    {
                        return RedirectToAction("AccessDenied");
                    }
                    else if (getData.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetFridges");
                    }
                }
                else
                {
                    _logger.LogInformation("Error calling web API");

                    return RedirectToAction("Unauthorized");
                }
            }

            return View();
        }

        public async Task<IActionResult> CreateFridgeProduct(FridgeProductsEntity fridge_products, ProductUpdateEntity product)
        {
            ProductUpdateEntity ProductToCreate = new ProductUpdateEntity
            {
                Name = product.Name,
                Quantity = product.Quantity,
                Default_quantity = product.Default_quantity,
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL + $"api/fridge/{fridge_products.Fridge_Id}/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var token = HttpContext.Session.GetString("Token");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");

                if (token != null)
                {
                    HttpResponseMessage getData = await client.PostAsJsonAsync<ProductUpdateEntity>("products", ProductToCreate);
                    if (getData.StatusCode.Equals(System.Net.HttpStatusCode.Forbidden))
                    {
                        return RedirectToAction("AccessDenied");
                    }
                    else if (getData.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetFridges");
                    }
                }
                else
                {
                    _logger.LogInformation("Error calling web API");

                    return RedirectToAction("Unauthorized");
                }
            }

            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Unauthorized()
        {
            return View();
        }
    }
}
