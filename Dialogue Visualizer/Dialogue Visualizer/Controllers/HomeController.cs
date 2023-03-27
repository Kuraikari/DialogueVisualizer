using Dialogue_Visualizer.Helpers;
using Dialogue_Visualizer.Models;
using Dialogue_Visualizer.Services;
using Dialogue_Visualizer.ViewModels;
using Dialogue_Visualizer.ViewModels.Dialogues;
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
                AddDialogueBlock(DialogueBlockGenerator.GenerateDialogueBlock("You", "Hello fellow young person. How are you doing?"));
                AddDialogueBlock(DialogueBlockGenerator.GenerateDialogueBlock("George", "Young? I lived for 75 years!"));
                AddDialogueBlock(DialogueBlockGenerator.GenerateDialogueBlock("You", "I see. I lived for thousands of years. Everyone is young to me."));
            }

            var projects = await _context.Projects
                .Include(project => project.Scenes)
                .ToListAsync();
            var viewModel = DomainModelToViewModel.ConvertToDialogueViewModel(projects);

            return View(viewModel);
        }

        public IActionResult ContextView(string func = "", DialogueBlockVM? dialogueBlock = null)
        {
            var dialogues = _context.Dialogue.ToList();
            var blocks = _context.DialogueBlocks.ToList();
            var test = new { func, model = dialogueBlock};

            return PartialView("_DialogueBlockForm", test);
        }

        public async Task<IActionResult> DialogueBlueprints()
        {
            var projects = await _context.Projects
                .Include(project => project.Scenes)
                .ThenInclude(scenes => scenes.Select(scene => scene.DialogueBlocks.Select(block => block.Dialogue)))
                .ToListAsync();
            var viewModel = DomainModelToViewModel.ConvertToDialogueViewModel(projects);

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
                    _context.DialogueBlocks.Add(block);
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
            finally
            {
                _context.SaveChanges();
            }
        }

        [HttpPost]
        public IActionResult AddProjectAndScene(Project project, Scene scene)
        {
            try
            {
                if (project != null && scene != null)
                {
                    project.Id = _context.Projects.Any() ? _context.Projects.Max(b => b.Id) + 1 : 1;
                    scene.Id = _context.Scene.Any() ? _context.Scene.Max(b => b.Id) + 1 : 1;
                    project.Scenes.Add(scene);
                    _context.Projects.Add(project);
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
            finally
            {
                _context.SaveChanges();
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