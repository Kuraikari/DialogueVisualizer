using Dialogue_Visualizer.Helpers;
using Dialogue_Visualizer.Models;

namespace Dialogue_Visualizer.Repositories
{
    public class SceneRepository : GenericRepository<Scene>
    {
        public SceneRepository(DialogueDbContext context) : base(context)
        {
        }

        public IEnumerable<Scene> GetAll()
        {
            return Get();
        }
    }
}
