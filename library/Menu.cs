using System.Globalization;
using System.Numerics;
using System.IO;
using MyGame;
using SplashKitSDK;
using System.Diagnostics.Contracts;
using System.ComponentModel;
using System.Collections.Generic;

namespace ShapeDrawer
{
    public class Menu      //Use shape for base properties
    {
        private int _width;
        private int _height;
        private Color _color;
        private float _x;
        private float _y;
        private string _title;      //for identification
        protected List<Button> _buttons; // Changed to protected so derived classes can access

        public Menu(Color color, float x, float y, int width, int height, string Title)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            BackgroundColor = color;
            _title = Title;
            _buttons = new List<Button>(); 
        }

        public Menu() : this(Color.Black, 0.0f, 0.0f, 30, 10, "DefaultMenu")
        { }

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        public float X
        {
            get { return _x; }
            set { _x = value; }
        }
        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public string MenuTitle
        {
            get { return _title; }
            set { _title = value; }
        }

        public Color BackgroundColor
        {
            get { return _color; }
            set { _color = value; }
        }

        public virtual void Draw()     //Draw Menu
        {
            SplashKit.FillRectangle(BackgroundColor, X, Y, Width, Height);
        }

        public bool IsAt(float xInput, float yInput)       //Return Position
        {
            return xInput <= X + Width && xInput >= X && yInput <= Height + Y && yInput >= Y;
        }


        // Button handling
        public void AddButton(Button button)        //Not all menus need to add button or update
        {
            _buttons.Add(button);
        }

        public void ClearButtons()
        {
            _buttons.Clear();
        }

        public string CheckButtonInteractions(float mouseX, float mouseY, bool mouseClicked)
        {
            foreach (Button button in _buttons)     //GO through each button and see if mouse is at button.
            {
                if (button.IsAt(mouseX, mouseY))
                {
                    // handle mouse click if button is enabled.
                    if (mouseClicked)
                    {
                        return button.ReturnValue();    //Buttons return a string value;
                    }
                }
            }
            return null; // No button was clicked, return nothing
        }

        public void DrawAllButtons()
        {
            foreach (Button button in _buttons)
            {
                button.Draw();
            }
        }

        public virtual void InitializeButtons()
        {
            Button startGame = new Button(Color.DarkBlue, 400, 350-40, 200, 40, "StartButton", "GalleryMenu");
            startGame.Buttontext = "Start Game!";

            AddButton(startGame);
        }

        public void ClearMenu()
        {
            BackgroundColor = Color.White;
        }
    }
}