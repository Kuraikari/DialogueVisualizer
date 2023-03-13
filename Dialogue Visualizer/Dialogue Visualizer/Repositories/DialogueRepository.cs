using Dialogue_Visualizer.Helpers;
using Dialogue_Visualizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogue_Visualizer.Repositories
{
    public class DialogueRepository : GenericRepository<Dialogue>
    {
        public DialogueRepository(DialogueDbContext context) : base(context)
        {
        }

        public IEnumerable<Dialogue> GetAll()
        {
            return Get();
        }
    }
}
