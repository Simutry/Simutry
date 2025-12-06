namespace ShapeDrawer
{
    public class Statistics
    {
        public int TotalShapes { get; set; }
        public int TotalDrawings { get; set; }
        public int Rectangles { get; set; }
        public int Circles { get; set; }
        public int Lines { get; set; }

        public Statistics()
        {
            TotalShapes = 0;
            TotalDrawings = 0;
            Rectangles = 0;
            Circles = 0;
            Lines = 0;
        }
    }
}