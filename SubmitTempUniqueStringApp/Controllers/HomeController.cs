using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STUSA.Data;
using SubmitTempUniqueStringApp.Models;

namespace SubmitTempUniqueStringApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly TopScoreDataContext _dataContext;

        public HomeController(ILogger<HomeController> logger, TopScoreDataContext context)
        {
            _logger = logger;
            _dataContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string stringInput)
        {
            var data = new StringViewModel(stringInput);
            var validWords = data.GetValidWords();

            if (validWords.Count == 0)
            {
                ViewBag.Message = $"No valid words have been found in the text box. Please follow instructions.";
            }
            else
            {
                var chosenWord = data.GetChosenValidWord(validWords);
                var result = await AddTopScoreWord(chosenWord);
                ViewBag.Message = result;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchQuery)
        {
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                var result = await _dataContext.TopScoreWords.SingleOrDefaultAsync(x => x.Content == searchQuery);
                ViewBag.Message = (result == null) ? $"{searchQuery} was NOT found." : $"{searchQuery} has been FOUND.";
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Browse()
        {
            var lastTenWords = await _dataContext.TopScoreWords.OrderByDescending(x => x.Id).Take(10).ToListAsync();
            if (lastTenWords.Count > 0)
                ViewBag.Message = String.Join(",", lastTenWords.Select(x => x.Content).ToArray<string>());
            return View("Search");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<string> AddTopScoreWord(string word)
        {
            try
            {
                var alreadyExists = await _dataContext.TopScoreWords.SingleOrDefaultAsync(x => x.Content == word);
                if (alreadyExists != null)
                {
                    return $"{word} has already been submitted.";
                }

                _dataContext.TopScoreWords.Add(new TopScoreWord { Content = word });
                await _dataContext.SaveChangesAsync();
                return $"{word} has been added successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddTopScoreWord failed.");
                return $"An unkonwn error has occured. {ex.Message}";
            }
        }
    }
}
