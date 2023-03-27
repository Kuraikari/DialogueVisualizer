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
        private readonly DialogueBlockRepository _blockRepo;
        private readonly SceneRepository _sceneRepo;
        private readonly ProjectRepository _projectRepo;

        public DialogueService(DialogueRepository dialogRepo, DialogueBlockRepository blockRepo, SceneRepository sceneRepo, ProjectRepository projectRepo)
        {
            _dialogRepo = dialogRepo;
            _blockRepo = blockRepo;
            _sceneRepo = sceneRepo;
            _projectRepo = projectRepo;
        }

        public List<Dialogue> GetAllDialogues() => _dialogRepo.GetAll().ToList();
        public List<DialogueBlock> GetAllDialogueBlocks() => _blockRepo.GetAll().ToList();
        public List<Project> GetAllProjects() => _projectRepo.GetAll().ToList();
        public List<Scene> GetAllScenes() => _sceneRepo.GetAll().ToList();
    }
}
