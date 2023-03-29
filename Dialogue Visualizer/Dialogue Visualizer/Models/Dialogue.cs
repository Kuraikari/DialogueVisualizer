using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dialogue_Visualizer.Models
{
    [PrimaryKey("Id")]
    public class Dialogue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Speaker { get; set; } = string.Empty;
        [Required]
        public string Text { get; set; } = string.Empty;
        public int Order { get; set; }
        public bool IsQuestion { get; set; } = false;
        public IList<Dialogue> FollowUpDialogue { get; set; } = new List<Dialogue>();
    }
}