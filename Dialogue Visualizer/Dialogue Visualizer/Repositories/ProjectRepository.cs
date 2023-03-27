using Dialogue_Visualizer.Helpers;
using Dialogue_Visualizer.Models;

namespace Dialogue_Visualizer.Repositories
{
    public class ProjectRepository : GenericRepository<Project>
    {
        public ProjectRepository(DialogueDbContext context) : base(context)
        {
        }

        public IEnumerable<Project> GetAll()
        {
            return Get();
        }
    }
}
