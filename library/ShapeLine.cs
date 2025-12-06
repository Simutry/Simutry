using System.IO;
using SplashKitSDK;
using MyGame;

namespace ShapeDrawer
{
    public class MyLine : Shape
    {
        //PRIVATE FIELDS//
        private float _endX;
        private float _endY;

        //END REGION FOR PRIVATE FIELDS//


        //CONSTRUCTORS//
        public MyLine(Color color, float startX, float startY, float endX, float endY, int size) : base(color)
        {
            X = startX;
            Y = startY;
            EndX = endX;
            EndY = endY;
            Size = size; //LINE THICKNESS
        }

        public MyLine() : this(Color.Red, 0.0f, 0.0f, 0.0f, 0.0f, 2) //DEFAULT CONSTRUCTOR
        { }

        //END REGION FOR CONSTRUCTORS//


        //PUBLIC PROPERTIES//
        public float EndX
        {
            get
            {
                return _endX;
            }
            set
            {
                _endX = value;
            }
        }

        public float EndY
        {
            get
            {
                return _endY;
            }
            set
            {
                _endY = value;
            }
        }

        //END REGION FOR PUBLIC PROPERTIES//


        //BASIC METHODS//
        public override void DrawPreview(float startX, float startY)
        {
            //DRAW SEMI-TRANSPARENT LINE PREVIEW FROM START POINT TO CURRENT END POINT
            SplashKit.DrawLine(Color.Gray, startX, startY, EndX, EndY, SplashKit.OptionLineWidth(Size));
        }

        //END REGION FOR BASIC METHODS//


        //OVERRIDDEN METHODS//
        public override void Draw()
        {
            if (Selected)       //DRAW OUTLINE IF SELECTED
            {
                DrawOutline();
            }
            //DRAW MAIN LINE WITH SPECIFIED THICKNESS
            SplashKit.DrawLine(Color, X, Y, EndX, EndY, SplashKit.OptionLineWidth(Size));
        }

        public override bool IsAt(float xInput, float yInput)
        {
            float boundary = Size;
            
            // Check if point is within square boundary around start point
            if (xInput >= X - boundary && xInput <= X + boundary &&
                yInput >= Y - boundary && yInput <= Y + boundary)
                return true;
            
            // Check if point is within square boundary around end point
            if (xInput >= EndX - boundary && xInput <= EndX + boundary &&
                yInput >= EndY - boundary && yInput <= EndY + boundary)
                return true;
            
            return false;
        }

        public override void DrawOutline()
        {
            //DRAW BLUE CIRCLES AT BOTH ENDPOINTS TO SHOW SELECTION
            int rad = 5;
            SplashKit.FillCircle(Color.Blue, X, Y, rad);
            SplashKit.FillCircle(Color.Blue, EndX, EndY, rad);
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Line");       //SAVE SHAPE IDENTIFIER
            base.SaveTo(writer);            //SAVE COMMON PROPERTIES FROM SHAPE.CS
            writer.WriteLine(EndX);
            writer.WriteLine(EndY);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);      //LOAD COMMON PROPERTIES FROM SHAPE.CS
            EndX = reader.ReadSingle();
            EndY = reader.ReadSingle();
            
        }

        //END REGION FOR OVERRIDDEN METHODS//
    }
}