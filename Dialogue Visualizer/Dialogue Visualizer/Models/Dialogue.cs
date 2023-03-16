using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Dialogue_Visualizer.Models
{
    [PrimaryKey("Id")]
    public class Dialogue
    {    
        public int Id { get; set; }
        [Required]
        public string Speaker { get; set; } = string.Empty;
        [Required]
        public string Text { get; set; } = string.Empty;
        public int Order { get; set; }
        public bool IsQuestion { get; set; }
        public int FollowUpTextId { get; set; }
        public Scene Scene { get; set; } = new Scene();
    }
}