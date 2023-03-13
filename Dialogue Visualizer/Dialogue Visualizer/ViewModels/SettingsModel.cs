using System.Dynamic;

namespace Dialogue_Visualizer.ViewModels
{
    public class SettingsModel
    {
        public dynamic ConnectionStrings { get; set; } = new ExpandoObject();
        public SettingsModel() {
            ConnectionStrings.WebApiDatabase = "Data Source=LocalDatabase.db";
        }
    }
}
