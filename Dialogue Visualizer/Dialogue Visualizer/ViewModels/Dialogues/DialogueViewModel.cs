using Dialogue_Visualizer.ViewModels.Structs;

namespace Dialogue_Visualizer.ViewModels.Dialogues
{
    public class DialogueViewModel
    {
        public IEnumerable<DialogueBlockVM> DialogueBlocks { get; set; } = new List<DialogueBlockVM>();
    }
}
