using Dialogue_Visualizer.Helpers;
using Dialogue_Visualizer.Models;
using Dialogue_Visualizer.Services;
using Dialogue_Visualizer.ViewModels;
using Dialogue_Visualizer.ViewModels.Dialogues;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

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
                await AddDialogueBlock(DialogueBlockGenerator.GenerateDialogueBlock("You", "Hello fellow young person. How are you doing?"), true);
                await AddDialogueBlock(DialogueBlockGenerator.GenerateDialogueBlock("George", "Young? I lived for 75 years!"), false);
                await AddDialogueBlock(DialogueBlockGenerator.GenerateDialogueBlock("You", "I see. I lived for thousands of years. Everyone is young to me."), false);
            }

            var projects = await _context.Projects
                .Include(project => project.Scenes)
                .ThenInclude(scene => scene.DialogueBlocks)
                .ThenInclude(block => block.Dialogue)
                .ToListAsync();
            var viewModel = DomainModelToViewModel.ConvertToDialogueViewModel(projects);

            return View(viewModel);
        }

        public IActionResult ContextView(string func = "", int blockId = 0)
        {
            try
            {
                var blocks = _context.DialogueBlocks
                .Include(block => block.Dialogue)
                .Single(x => x.Id == blockId);

                var result = new DialogueBlockVM()
                {
                    Color = blocks.Color,
                    Id = blocks.Id,
                    Dialogue = blocks.Dialogue,
                    Height = blocks.Height,
                    Width = blocks.Width,
                    X = blocks.X,
                    Y = blocks.Y
                };

                dynamic test = new { func, model = result };

                return PartialView("_DialogueBlockForm", test);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(e.ToString());
            }
            
        }

        public async Task<IActionResult> DialogueBlueprints()
        {
            var projects = await _context.Projects
                .Include(project => project.Scenes)
                .ThenInclude(scene => scene.DialogueBlocks)
                .ThenInclude(block => block.Dialogue)
                .ToListAsync();
            var viewModel = DomainModelToViewModel.ConvertToDialogueViewModel(projects);

            return View(viewModel);
        }

        public async Task<IActionResult> GetDialogueBlocks(int projectId = 1)
        {
            var project = await _context.Projects
                .Where(p => p.Id == projectId)
                .OrderBy(x => x.Id)
                .Include(project => project.Scenes)
                .ThenInclude(scene => scene.DialogueBlocks)
                .ThenInclude(block => block.Dialogue)
                .FirstOrDefaultAsync();
            IList<Scene>? scenes = null;
            if (project != null)
            {
                scenes = project.Scenes;
            }
               
            return Json(scenes);
        }

        [HttpPost]
        public async Task<IActionResult> AddDialogueBlock(DialogueBlock block, bool newProject)
        {
            try
            {
                if (block != null)
                {
                    if (newProject)
                    {
                        IList<Scene> scenes = new List<Scene>();
                        scenes.Add(new Scene()
                        {
                            Name = "Default Scene",
                            Description = "This is the default Scene",
                            DialogueBlocks = new List<DialogueBlock>() { block }
                        });

                        var project = new Project()
                        {
                            Name = "Default Project",
                            Description = "This is the default project",
                            Scenes = scenes
                        };

                        await AddProject(project);
                    }
                    else
                    {
                        var defaultProject = await _context.Projects
                            .Include(p => p.Scenes)
                            .ThenInclude(s => s.DialogueBlocks)
                            .ThenInclude(d => d.Dialogue)
                            .OrderBy(x => x.Id)
                            .LastOrDefaultAsync(project => project.Name == "Default Project");
                            
                        if (defaultProject != null)
                        {
                            if (defaultProject.Scenes.Any())
                            {
                                defaultProject.Scenes.Where(s => s.Name == "Default Scene").First().DialogueBlocks.Add(block);
                                _context.Entry(defaultProject).State = EntityState.Modified;
                            }
                        }

                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
            finally
            {
                await _context.SaveChangesAsync();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(Project project)
        {
            try
            {
                if (project != null)
                {
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
                await _context.SaveChangesAsync();
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