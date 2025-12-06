using SplashKitSDK;

namespace ShapeDrawer
{
    //Creates standard shapes //
    public class StandardShapeFactory : ShapeFactory
    {
        public Shape CreateCircle(Color color, float x, float y, int radius)
        {
            return new MyCircle(color, x, y, radius);
        }

        public Shape CreateRectangle(Color color, float x, float y, int width, int height)
        {
            return new MyRectangle(color, x, y, width, height);
        }

        public Shape CreateLine(Color color, float startX, float startY, float endX, float endY, int size)
        {
            return new MyLine(color, startX, startY, endX, endY, size);
        }
    }
}