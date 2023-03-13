using Dialogue_Visualizer.Models;

namespace Dialogue_Visualizer.ViewModels.Structs
{
    public class DialogueBlockVM
    {
        public Dialogue Dialogue { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Color { get; set; } = string.Empty;

        public DialogueBlockVM() {
            Dialogue = new Dialogue();
        }
    }
}
