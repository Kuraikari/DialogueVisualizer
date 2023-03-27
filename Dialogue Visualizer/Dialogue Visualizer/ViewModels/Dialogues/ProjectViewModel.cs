namespace Dialogue_Visualizer.ViewModels.Dialogues
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IList<SceneViewModel> Scenes { get; set; } = new List<SceneViewModel>();
    }
}
