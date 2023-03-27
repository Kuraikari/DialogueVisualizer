using Dialogue_Visualizer.Helpers;
using Dialogue_Visualizer.Models;

namespace Dialogue_Visualizer.Repositories
{
    public class DialogueBlockRepository : GenericRepository<DialogueBlock>
    {
        public DialogueBlockRepository(DialogueDbContext context) : base(context)
        {
        }

        public IEnumerable<DialogueBlock> GetAll()
        {
            return Get();
        }
    }
}
