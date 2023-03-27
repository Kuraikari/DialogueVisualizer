using Microsoft.EntityFrameworkCore;

namespace Dialogue_Visualizer.Models
{
    [PrimaryKey("Id")]
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IList<Scene> Scenes { get; set; } = new List<Scene>();
    }
}
