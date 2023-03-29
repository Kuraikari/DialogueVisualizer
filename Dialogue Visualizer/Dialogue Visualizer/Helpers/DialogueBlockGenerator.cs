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

            Size size = new(250, 150);

            Point frame = new()
            {
                X = (int)(size.Width * 2.5),
                Y = (int)(size.Height * 2.5)
            };

            var block = new DialogueBlock()
            {
                Color = color,
                Width = size.Width,
                Height = size.Height,
                X = point.X - frame.X,
                Y = point.Y - frame.Y,
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
            Size vp = new(1920, 1080);
            Size hvp = new()
            {
                Width = (vp.Width / 2),
                Height = (vp.Height / 2)
            };

            int x, y;
            Random random = new Random();
            x = random.Next(hvp.Width, vp.Width);
            y = random.Next(hvp.Height, vp.Height);
            Point point = new(x, y);
            return point;
        }
    }
}
