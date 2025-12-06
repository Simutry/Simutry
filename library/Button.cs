using System.Globalization;
using System.Numerics;
using MyGame;
using SplashKitSDK;

namespace ShapeDrawer
{
    public class Button : Shape
    {
        private string _buttonid;
        private int _width;
        private int _height;
        private string _text;
        private bool _ishovered;
        private Color _textcolor;
        private string _value;
        public Button (Color color, float x, float y, int width, int height, string id, string value) : base(color)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            _buttonid = id;
            _value = value;
            _ishovered = false;
        }

        public Button() : this(Color.Black, 0.0f, 0.0f, 30, 10, "default_id", "Default")
        { }
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
        public string Buttontext
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }

        }
        public string ButtonID
        {
            get
            {
                return _buttonid;
            }
            set
            {
                _buttonid = value;
            }
        }
        public string Value
        {
            set { _value = value; }
        }
        public Color TextColor
        {
            get { return _textcolor; }
            set { _textcolor = value; }
        }
        public bool IsHovered
        {
            get
            {
                return _ishovered;
            }
        }
        public override void Draw()
        {
            bool hover = IsAt(SplashKit.MouseX(), SplashKit.MouseY());
            
            if (hover)
            {
                // DRAW OUTLINE WHEN HOVERED
                DrawOutline();
            }
            else
            {
                // DRAW NORMAL BUTTON
                SplashKit.FillRectangle(Color, X, Y, Width, Height);
                
                if (Buttontext != null)
                {
                    SplashKit.DrawText(Buttontext, TextColor, "test.otf", 20, X + Width / 2 - SplashKit.TextWidth(Buttontext, "test.otf", 20) / 2, Y + (Height - SplashKit.TextHeight(Buttontext, "test.otf", 20)) / 2);
                }
            }
        }

        public override bool IsAt(float xInput, float yInput)
        {
            return xInput <= X + Width && xInput >= X && yInput <= Height + Y && yInput >= Y;
        }

        public override void DrawOutline()
        {
            SplashKit.FillRectangle(Color.Black, X - 4, Y - 4, Width + 8, Height + 8);
            SplashKit.FillRectangle(Color, X, Y, Width, Height);
            SplashKit.DrawText(Buttontext, Color.White, "test.otf", 20, X + Width/2 - SplashKit.TextWidth(Buttontext, "test.otf", 20)/2, Y + (Height - SplashKit.TextHeight(Buttontext, "test.otf", 20)) / 2);
        }

        public string ReturnValue()
        {
            return _value;
        }
    }
}