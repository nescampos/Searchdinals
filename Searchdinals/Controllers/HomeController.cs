using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Searchdinals.Models;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Searchdinals.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SearchByText(string text)
        {
            SearchViewModel model = new SearchViewModel();
            model.Text = text;
            if (string.IsNullOrEmpty(text))
            {
                return View(model);
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Realizar la llamada a la API
                    HttpResponseMessage response = await client.GetAsync(_configuration["OrdinalsBotEndpoint"]+"?text="+ text);

                    // Verificar si la respuesta es exitosa
                    if (response.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como una cadena de texto
                        string json = await response.Content.ReadAsStringAsync();

                        // Deserializar el JSON en un objeto utilizando Newtonsoft.Json
                        SearchResult result = JsonConvert.DeserializeObject<SearchResult>(json);

                        // Acceder a los datos del objeto result
                        model.Result = result;

                    }
                    else
                    {
                        ModelState.AddModelError("", "Error in the search. Please try again in a couple of minutes.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error in the search. Please try again in a couple of minutes.");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> SearchByImage(string hash)
        {
            SearchViewModel model = new SearchViewModel();
            model.Hash = hash;
            if (string.IsNullOrEmpty(hash))
            {
                return View(model);
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Realizar la llamada a la API
                    HttpResponseMessage response = await client.GetAsync(_configuration["OrdinalsBotEndpoint"] + "?hash=" + hash);

                    // Verificar si la respuesta es exitosa
                    if (response.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como una cadena de texto
                        string json = await response.Content.ReadAsStringAsync();

                        // Deserializar el JSON en un objeto utilizando Newtonsoft.Json
                        SearchResult result = JsonConvert.DeserializeObject<SearchResult>(json);

                        // Acceder a los datos del objeto result
                        model.Result = result;

                    }
                    else
                    {
                        ModelState.AddModelError("", "Error in the search. Please try again in a couple of minutes.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error in the search. Please try again in a couple of minutes.");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SearchByImage(IFormFile file)
        {
            if(file == null)
            {
                return RedirectToAction("SearchByImage");
            }
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        byte[] hashBytes = sha256.ComputeHash(memoryStream.ToArray());
                        string hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                        return RedirectToAction("SearchByImage", new { hash = hash });
                    }
                }
            }
            catch (FileNotFoundException)
            {
                return RedirectToAction("SearchByImage");
            }
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}