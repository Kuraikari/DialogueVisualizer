using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dialogue_Visualizer.Models
{
    [PrimaryKey("Id")]
    public class DialogueBlock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Dialogue Dialogue { get; set; } = new Dialogue();
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Color { get; set; } = string.Empty;
    }
}
