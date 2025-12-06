using System.IO;
using SplashKitSDK;
using MyGame;

namespace ShapeDrawer
{
    public class MyRectangle : Shape
    {
        //PRIVATE FIELDS//
        private int _width;
        private int _height;

        //END REGION FOR PRIVATE FIELDS//


        //CONSTRUCTORS//
        public MyRectangle(Color color, float x, float y, int width, int height) : base(color)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Size = width;
        }

        public MyRectangle() : this(Color.Green, 0.0f, 0.0f, 144, 150) //DEFAULT CONSTRUCTOR
        { }

        //END REGION FOR CONSTRUCTORS//


        //PUBLIC PROPERTIES//
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        //END REGION FOR PUBLIC PROPERTIES//


        //BASIC METHODS//
        public override void DrawPreview(float mouseX, float mouseY)
        {
            //DRAW SEMI-TRANSPARENT RECTANGLE PREVIEW AT MOUSE POSITION
            SplashKit.DrawRectangle(Color.Gray, mouseX, mouseY, Width, Height);
        }

        //END REGION FOR BASIC METHODS//


        //OVERRIDDEN METHODS//
        public override void Draw()
        {
            if (Selected)       //DRAW OUTLINE IF SELECTED
            {
                DrawOutline();
            }
            SplashKit.FillRectangle(Color, X, Y, Width, Height); //DRAW MAIN RECTANGLE
        }

        public override bool IsAt(float xInput, float yInput)
        {
            //CHECK IF POINT IS WITHIN RECTANGLE BOUNDARIES
            return xInput <= X + _width && xInput >= X && yInput <= _height + Y && yInput >= Y;
        }

        public override void DrawOutline()
        {
            //DRAW GREEN OUTLINE AROUND RECTANGLE TO SHOW SELECTION
            SplashKit.DrawRectangle(Color.Green, X - 2, Y - 2, Width + 5, Height + 5);
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Rectangle");      //SAVE SHAPE IDENTIFIER
            base.SaveTo(writer);                //SAVE COMMON PROPERTIES FROM SHAPE.CS
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);      //LOAD COMMON PROPERTIES FROM SHAPE.CS
            Width = Size;
            Height = Size;
        }

        //END REGION FOR OVERRIDDEN METHODS//
    }
}