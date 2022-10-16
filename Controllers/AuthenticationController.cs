using FridgesWebApp.Models;
using Google;
using Google.Apis.Admin.Directory.directory_v1.Data;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.PowerBI.Api.Models;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web.Helpers;

namespace FridgesWebApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        string baseURL = "http://localhost:8080/";

        public AuthenticationController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Authorize(UserEntity user)
        {
            UserEntity obj = new UserEntity
            {
                Username = user.Username,
                Password = user.Password
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL + "api/authentication/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.PostAsJsonAsync<UserEntity>("login", obj);
                if (getData.IsSuccessStatusCode)
                {
                    string token = getData.Content.ReadAsStringAsync().Result;
                    token = new string(token[10..]);
                    token = new string(token[..^2]);

                    HttpContext.Session.SetString("Token", token);

                    var identity = new ClaimsIdentity("Custom");
                    HttpContext.User = new ClaimsPrincipal(identity);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _logger.LogInformation("Error calling web API");
                }
            }

            return View();
        }

        public async Task<IActionResult> Registration(UserForRegistration user)
        {
            UserForRegistration obj = new UserForRegistration
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = new List<string>() { "User" }
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL + "api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.PostAsJsonAsync<UserForRegistration>("authentication", obj);

                if (getData.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _logger.LogInformation("Error calling web API");
                }
            }

            return View();
        }
    }
}