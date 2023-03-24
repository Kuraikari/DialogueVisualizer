using Dialogue_Visualizer.Helpers;
using Dialogue_Visualizer.Models;
using Dialogue_Visualizer.Services;
using Dialogue_Visualizer.ViewModels;
using Dialogue_Visualizer.ViewModels.Dialogues;
using Dialogue_Visualizer.ViewModels.Structs;
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

        public async Task<IActionResult> Index(bool seed = false)
        {
            if (seed)
            {
                AddDialogueBlock(new DialogueBlock()
                {
                    Color = "#444",
                    Width = 250,
                    Height = 150,
                    Dialogue = new Dialogue()
                    {
                        Text = "This is a another new text",
                        Speaker = "They",
                        Order = 0,
                        Scene = new Scene()
                        {
                            Name = "NULL",
                            Description = "NULL",
                        },
                        IsQuestion = false,
                        FollowUpTextId = -1,
                    },
                    X = 800,
                    Y = 350
                });
            }

            var dialogues = await _context.Dialogue.ToListAsync();

            List<DialogueBlockVM> blockVM = new();
            foreach (var item in dialogues)
            {
                blockVM.Add(new()
                {
                    Dialogue = item,
                    Color = "#F08090",
                    Width = 250,
                    Height = 100,
                    X = 0,
                    Y = 0,
                });
            }

            var viewModel = new DialogueViewModel()
            {
                DialogueBlocks = blockVM
            };

            return View(viewModel);
        }

        public IActionResult ContextView(string func = "", DialogueBlockVM? dialogueBlock = null)
        {
            var dialogues = _context.Dialogue.ToList();
            var blocks = _context.DialogueBlocks.ToList();
            var test = new { func = func, model = dialogueBlock};

            return PartialView("_DialogueBlockForm", test);
        }

        public async Task<IActionResult> DialogueBlueprints()
        {
            var dialogues = await _context.Dialogue.ToListAsync();
            var dialogueblocks = await _context.DialogueBlocks.ToListAsync();

            List<DialogueBlockVM> blockVM = new();
            foreach (var item in dialogueblocks)
            {
                blockVM.Add(new()
                {
                    Dialogue = item.Dialogue,
                    Color = item.Color,
                    Width = item.Width,
                    Height = item.Height,
                    X = item.X,
                    Y = item.Y,
                });
            }

            var viewModel = new DialogueViewModel()
            {
                DialogueBlocks = blockVM
            };

            return View(viewModel);
        }

        public async Task<IActionResult> GetDialogueBlocks()
        {
            var dialogues = await _context.Dialogue.ToListAsync();
            var blocks = await _context.DialogueBlocks.ToListAsync();
            return Json(blocks);
        }

        [HttpPost]
        public IActionResult AddDialogueBlock(DialogueBlock block)
        {
            try
            {
                if (block != null)
                {
                    block.Id = _context.DialogueBlocks.Any() ? _context.DialogueBlocks.Max(b  => b.Id) + 1 : 1;
                    block.Dialogue.Id = _context.Dialogue.Any() ? _context.Dialogue.Max(b => b.Id) + 1 : 1;

                    _context.Dialogue.Add(block.Dialogue);
                    _context.DialogueBlocks.Add(block);
                    _context.SaveChanges();
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
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