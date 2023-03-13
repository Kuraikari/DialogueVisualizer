using Dialogue_Visualizer.Models;
using Dialogue_Visualizer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogue_Visualizer.Services
{
    public class DialogueService
    {
        private readonly DialogueRepository _dialogRepo;

        public DialogueService(DialogueRepository dialogRepo)
        {
            _dialogRepo = dialogRepo;
        }

        public List<Dialogue> GetAllDialogues() => _dialogRepo.GetAll().ToList();
    }
}
