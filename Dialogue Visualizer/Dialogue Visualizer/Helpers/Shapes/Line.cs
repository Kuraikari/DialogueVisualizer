namespace Dialogue_Visualizer.Helpers.Shapes
{
    public class Line : BaseShape
    {
        public int StartX { get; set; }
        public int StartY { get; set; }

        public int DistanceX { get; set; }
        public int DistanceY { get; set; }

        public Line() : base()
        {
            this.Points.Add("A", new Size2DInt(StartX, StartY));
            this.Points.Add("B", new Size2DInt(StartX + DistanceX, StartY + DistanceY));
        }
    }
}
