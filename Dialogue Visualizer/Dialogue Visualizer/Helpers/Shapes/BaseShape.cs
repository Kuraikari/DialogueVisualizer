namespace Dialogue_Visualizer.Helpers.Shapes
{
    public abstract class BaseShape
    {
        public IDictionary<string, Size2DInt> Points { get; set; }

        public BaseShape()
        {
            Points = new Dictionary<string, Size2DInt>();
        }
    }
}
