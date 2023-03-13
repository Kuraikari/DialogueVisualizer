using Microsoft.EntityFrameworkCore;

namespace Dialogue_Visualizer.Models
{
    [PrimaryKey("Id")]
    public class DialogueBlock
    {
        public int Id { get; set; }
        public Dialogue Dialogue { get; set; } = new Dialogue();
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Color { get; set; } = string.Empty;
    }
}
