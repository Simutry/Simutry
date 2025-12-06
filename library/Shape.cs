using System.IO;
using SplashKitSDK;
using MyGame;

namespace ShapeDrawer
{
    public abstract class Shape
    {
        //PRIVATE FIELDS//
        private Color _color;
        private float _x;
        private float _y;
        private int _size;
        private bool _selected;

        //END REGION FOR PRIVATE FIELDS//


        //CONSTRUCTORS//
        public Shape(Color color) //NON DEFAULT CONSTRUCTOR WITH COLOR PARAMETER
        {
            _color = color;
            _x = 0.0f;
            _y = 0.0f;
            _size = 10; //SET DEFAULT SIZE
        }

        public Shape() : this(Color.Yellow)     //DEFAULT CONSTRUCTOR CREATES YELLOW SHAPE
        { }

        //END REGION FOR CONSTRUCTORS//


        //FACTORY DESIGN PATTERN//

        //PUBLIC PROPERTIES//
        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        public float X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public float Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public int Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }

        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
            }
        }

        //END REGION FOR PUBLIC PROPERTIES//


        //ABSTRACT METHODS - MUST BE IMPLEMENTED BY DERIVED CLASSES//
        public abstract void Draw();                    //DRAW THE SHAPE TO SCREEN
        public abstract bool IsAt(float x, float y);    //CHECK IF POINT IS WITHIN SHAPE BOUNDARIES
        public abstract void DrawOutline();             //DRAW SELECTION OUTLINE AROUND SHAPE

        //END REGION FOR ABSTRACT METHODS//


        //VIRTUAL METHODS - CAN BE OVERRIDDEN BY DERIVED CLASSES//
        public virtual void DrawPreview(float mouseX, float mouseY)
        {
            //DEFAULT PREVIEW IMPLEMENTATION - DERIVED CLASSES SHOULD OVERRIDE THIS
            //THIS PROVIDES A COMMON INTERFACE FOR ALL SHAPES TO DRAW PREVIEWS
        }

        public virtual void SaveTo(StreamWriter writer)
        {
            //SAVE COMMON SHAPE PROPERTIES TO STREAM
            writer.WriteColor(Color);
            writer.WriteLine(X);
            writer.WriteLine(Y);
            writer.WriteLine(Size);
        }

        public virtual void LoadFrom(StreamReader reader)
        {
            //LOAD COMMON SHAPE PROPERTIES FROM STREAM
            Color = reader.ReadColor();
            X = reader.ReadSingle();
            Y = reader.ReadSingle();
            Size = reader.ReadInteger();
        }

        //END REGION FOR VIRTUAL METHODS//
    }
}