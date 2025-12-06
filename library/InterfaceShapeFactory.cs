using SplashKitSDK;

namespace ShapeDrawer
{
    public interface ShapeFactory
    {
        Shape CreateCircle(Color color, float x, float y, int radius);
        Shape CreateRectangle(Color color, float x, float y, int width, int height);
        Shape CreateLine(Color color, float startX, float startY, float endX, float endY, int size);
    }
}