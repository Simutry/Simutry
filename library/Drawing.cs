using System.Collections.Generic;
using System.IO;
using SplashKitSDK;
using MyGame;

namespace ShapeDrawer
{
    public class Drawing
    {
        //PRIVATE FIELDS//
        private readonly List<Shape> _shapes;   //STORES ALL SHAPES IN THE DRAWING
        private Color _background;              //STORES BACKGROUND COLOR
        private string _drawingname;           //STORES DRAWING NAME FOR SAVING/LOADING
        private int _drawingsid;               //STORES DRAWING ID FOR IDENTIFICATION

        //END REGION FOR PRIVATE FIELDS//


        //CONSTRUCTORS//
        public Drawing(Color background, string drawingname = null)
        {
            _shapes = new List<Shape>();
            _background = background;
            _drawingname = drawingname;
            _drawingsid = 0; //DEFAULT ID
        }

        public Drawing() : this(Color.White) //DEFAULT CONSTRUCTOR WITH WHITE BACKGROUND
        { }

        //END REGION FOR CONSTRUCTORS//


        //PUBLIC PROPERTIES//
        public string Drawingname
        {
            get
            {
                return _drawingname;
            }
            set
            {
                _drawingname = value;
            }
        }

        public int DrawingsId
        {
            get
            {
                return _drawingsid;
            }
            set
            {
                _drawingsid = value;
            }
        }

        public Color Background
        {
            get
            {
                return _background;
            }
            set
            {
                _background = value;
            }
        }

        public int ShapeCount
        {
            get
            {
                return _shapes.Count;
            }
        }

        public List<Shape> GetShape
        {
            get
            {
                return _shapes;
            }
        }

        //END REGION FOR PUBLIC PROPERTIES//


        //PUBLIC METHODS//
        public void AddShape(Shape shape)
        {
            //ADD SHAPE TO DRAWING COLLECTION
            _shapes.Add(shape);
        }

        public void RemoveShape(Shape shape)
        {
            //REMOVE SHAPE FROM DRAWING COLLECTION
            _ = _shapes.Remove(shape);
        }

        public void Draw()
        {
            //DRAW ENTIRE DRAWING: BACKGROUND + ALL SHAPES
            SplashKit.ClearScreen(_background);
            foreach (Shape shape in _shapes)
            {
                shape.Draw();
            }
        }

        public void SelectShapesAt(float x, float y)
        {
            //SELECT SHAPES AT SPECIFIED COORDINATES
            foreach (Shape shape in _shapes)
            {
                shape.Selected = shape.IsAt(x, y);
            }
        }

        //END REGION FOR PUBLIC METHODS//


        //SELECTED SHAPES PROPERTY//
        public List<Shape> SelectedShapes
        {
            get
            {
                //RETURN LIST OF ALL SELECTED SHAPES
                List<Shape> result = new List<Shape>();
                foreach (Shape shape in _shapes)
                {
                    if (shape.Selected)
                    {
                        result.Add(shape);
                    }
                }
                return result;
            }
        }

        //END REGION FOR SELECTED SHAPES PROPERTY//


        //SAVE AND LOAD METHODS//
        public void Save(string filename)
        {
            //will close automatically when finished
            using StreamWriter writer = new StreamWriter(filename);
            
            // Save basic drawing info
            writer.WriteColor(_background);
            writer.WriteLine(_shapes.Count);
            writer.WriteLine(_drawingname);

            // Save each shape
            foreach (Shape shape in _shapes)
                shape.SaveTo(writer);
        }

        public void Load(string filename)
        {
            //will close automatically when finished
            using StreamReader reader = new StreamReader(filename);
            
            // Load basic drawing info
            _background = reader.ReadColor();
            int shapeCount = reader.ReadInteger();
            _drawingname = reader.ReadLine();
            
            for (int i = 0; i < shapeCount; i++)
            {
                string shapeType = reader.ReadLine();
                Shape newShape = null;
                switch (shapeType)
                {
                    case "Rectangle":
                        newShape = new MyRectangle();
                        break;
                    case "Circle":
                        newShape = new MyCircle();
                        break;
                    case "Line":
                        newShape = new MyLine();
                        break;
                    default:
                        Console.WriteLine("Unknown shape type: " + shapeType);
                        break;
                }
                newShape.LoadFrom(reader);
                _shapes.Add(newShape);
            }
        }
        //END REGION FOR SAVE AND LOAD METHODS//
    }
}