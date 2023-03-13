using System.Dynamic;

namespace Dialogue_Visualizer.Models
{
    public class SettingsModel
    {
        public dynamic ConnectionStrings { get; set; } = new ExpandoObject();
        public SettingsModel() {
            ConnectionStrings.WebApiDatabase = "Data Source=LocalDatabase.db";
        }
    }
}
