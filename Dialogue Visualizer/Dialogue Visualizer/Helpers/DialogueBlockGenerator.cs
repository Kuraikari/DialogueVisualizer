using Dialogue_Visualizer.Models;
using System.Drawing;
using System.Dynamic;

namespace Dialogue_Visualizer.Helpers
{
    public class DialogueBlockGenerator
    {
        public static DialogueBlock GenerateDialogueBlock(string speaker, string text)
        {
            Point point = GenerateRandomPoint();
            string color = GenerateRandomHexCode();
            var block = new DialogueBlock()
            {
                Color = color,
                Height = 150,
                Width = 250,
                X = point.X - 250,
                Y = point.Y - 150,
                Dialogue = new Dialogue()
                {
                    Text = text,
                    Speaker = speaker,
                    IsQuestion = false,
                    Order = 0,
                }
            };
            return block;
        }

        private static string GenerateRandomHexCode()
        {
            Random random = new Random();
            int red = random.Next(256);
            int green = random.Next(256);
            int blue = random.Next(256);
            return $"#{red:X2}{green:X2}{blue:X2}";
        }

        private static Point GenerateRandomPoint()
        {
            int x, y;
            Random random = new Random();
            x = random.Next(1920);
            y = random.Next(1080);
            Point point = new(x, y);
            return point;
        }
    }
}
