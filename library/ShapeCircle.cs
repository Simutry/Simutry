using System.IO;
using SplashKitSDK;
using MyGame;
namespace ShapeDrawer
{
    public class MyCircle : Shape
    {
        //PRIVATE FIELDS//
        private int _radius;

        //END REGION FOR PRIVATE FIELDS//


        //CONSTRUCTORS//
        public MyCircle(Color color, float x, float y, int radius) : base(color)
        {
            X = x;
            Y = y;
            Radius = radius;
            Size = radius;
        }

        public MyCircle() : this(Color.Blue, 0.0f, 0.0f, 94) //DEFAULT CONSTRUCTOR
        { }

        //END REGION FOR CONSTRUCTORS//


        //PUBLIC PROPERTIES//
        public int Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                _radius = value;
            }
        }

        //END REGION FOR PUBLIC PROPERTIES//


        //BASIC METHODS//
        public override void DrawPreview(float mouseX, float mouseY)
        {
            //DRAW SEMI-TRANSPARENT CIRCLE PREVIEW AT MOUSE POSITION
            SplashKit.DrawCircle(Color.Gray, mouseX, mouseY, Radius);
        }

        //END REGION FOR BASIC METHODS//


        //OVERRIDDEN METHODS//
        public override void Draw()
        {
            if (Selected)       //DRAW OUTLINE IF SELECTED
            {
                DrawOutline();
            }
            SplashKit.FillCircle(Color, X, Y, Radius); //DRAW MAIN CIRCLE
        }

        public override bool IsAt(float xInput, float yInput)
        {
            //CHECK IF POINT IS WITHIN CIRCLE USING DISTANCE FORMULA
            //USES: (X - CENTER_X)² + (Y - CENTER_Y)² ≤ RADIUS²
            return (xInput - X) * (xInput - X) + (yInput - Y) * (yInput - Y) <= _radius * _radius;
        }

        public override void DrawOutline()
        {
            //DRAW PURPLE OUTLINE AROUND CIRCLE TO SHOW SELECTION
            SplashKit.DrawCircle(Color.Purple, X - 1, Y - 1, Radius + 2);
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Circle");     //SAVE SHAPE IDENTIFIER
            base.SaveTo(writer);            //SAVE COMMON PROPERTIES FROM SHAPE.CS
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);      //LOAD COMMON PROPERTIES FROM SHAPE.CS
            Radius = Size;  
        }

        //END REGION FOR OVERRIDDEN METHODS//
    }
}