using Dialogue_Visualizer.Helpers;
using Dialogue_Visualizer.ViewModels;
using DialoguesServiceLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Dialogue_Visualizer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DialogueDbContext _context;

        private readonly DialogueService _dialogueService;

        public HomeController(ILogger<HomeController> logger, DialogueDbContext context, DialogueService dialogueService)
        {
            _logger = logger;
            _context = context;
            _dialogueService = dialogueService;
        }

        public async Task<IActionResult> Index()
        {
            var dialogues = await _context.Dialogue.ToListAsync();
            return View(dialogues);
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