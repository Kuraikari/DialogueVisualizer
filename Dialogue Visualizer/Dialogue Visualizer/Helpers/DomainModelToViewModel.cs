using Dialogue_Visualizer.Models;
using Dialogue_Visualizer.ViewModels.Dialogues;

namespace Dialogue_Visualizer.Helpers
{
    public class DomainModelToViewModel
    {
        public static DialogueViewModel ConvertToDialogueViewModel(IEnumerable<Project> projects)
        {
            IList<ProjectViewModel> projectViewModels = new List<ProjectViewModel>();
            foreach (var project in projects)
            {
                projectViewModels.Add(new ProjectViewModel()
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description
                });
                IList<SceneViewModel> sceneViewModels = new List<SceneViewModel>();
                foreach (var scene in project.Scenes)
                {
                    sceneViewModels.Add(new SceneViewModel()
                    {
                        Id = scene.Id,
                        Name = scene.Name,
                        Description = scene.Description
                    });
                    IList<DialogueBlockVM> dialogueBlockVMs = new List<DialogueBlockVM>();
                    foreach (var block in scene.DialogueBlocks)
                    {
                        dialogueBlockVMs.Add(new DialogueBlockVM()
                        {
                            Id = block.Id,
                            Color = block.Color,
                            Height = block.Height,
                            Width = block.Width,
                            X = block.X,
                            Y = block.Y,
                            Dialogue = block.Dialogue,
                        });
                    }
                    sceneViewModels.Last().Blocks = dialogueBlockVMs;
                }
                projectViewModels.Last().Scenes = sceneViewModels;
            }


            var viewModel = new DialogueViewModel()
            {
                Projects = projectViewModels
            };
            return viewModel;
        }
    }
}
