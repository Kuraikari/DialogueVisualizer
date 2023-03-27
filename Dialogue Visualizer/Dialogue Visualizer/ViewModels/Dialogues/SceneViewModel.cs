namespace Dialogue_Visualizer.ViewModels.Dialogues
{
    public class SceneViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IList<DialogueBlockVM> Blocks { get; set; } = new List<DialogueBlockVM>();
    }
}
