using Dialogue_Visualizer.Models;

namespace Dialogue_Visualizer.ViewModels.Dialogues
{
    public class DialogueViewModel
    {
        public IEnumerable<ProjectViewModel> Projects { get; set; } = new List<ProjectViewModel>();
        public int SelectedProjectId { get; set; }
        public int SelectedSceneId { get; set; }
    }
}
