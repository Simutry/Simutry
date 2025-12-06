using SplashKitSDK;

namespace ShapeDrawer
{
    public class DrawingState : BaseMenuState
    {
        private DrawingMenu _drawingMenu;
        private StateManager _stateManager;
        private Drawing _currentDrawing;
        private StandardShapeFactory _factory;
        private ColourMenu _colourMenu;

        // Input handling state
        private float _locked_x;
        private float _locked_y;
        private bool _islocked;
        private bool _drawingLine;
        private Point2D? _lineStart;
        private ShapeKind _currentShapeKind;
        private int _size;
        private Color _currentColor;
        private Button save;

        public override string StateName
        {
            get
            {
                return "Drawing";
            }
        }

        public DrawingState(StateManager stateManager, Drawing drawing = null)
        {
            _stateManager = stateManager;
            _currentDrawing = drawing;
            _drawingMenu = new DrawingMenu(drawing);
            _factory = new StandardShapeFactory();
            _colourMenu = new ColourMenu();
            
            // Initialize input state
            _locked_x = 0;
            _locked_y = 0;
            _islocked = false;
            _drawingLine = false;
            _lineStart = null;
            _currentShapeKind = ShapeKind.Circle;
            _size = 50;
            _currentColor = Color.Black;
            save = new Button(Color.DarkGreen, 790, 5, 200, 40, "SaveButton", "Save");
        }

        public override void Enter()
        {
            base.Enter();
            Console.WriteLine($"Started drawing: {_currentDrawing?.Drawingname}");
            Console.WriteLine("Press 1,2,3 to Change Shape");
            Console.WriteLine("Press - or + to Change Size");
            Console.WriteLine("Use LeftClick to place and RightClick to Select");
            Console.WriteLine("Use Arrow Keys to move Selected Shapes");
            Console.WriteLine("Press BackSpace to delete Selected Shapes");
            Console.WriteLine("Press Escape when drawing Line to stop Drawing Line");
            Console.WriteLine("Press Ctrl or Shift Key to Lock Axis.");
            Console.WriteLine("Press Spacebar to Randomly Change the Background Colour!.");
            Console.WriteLine("Enjoy Drawing! :D");
        }

        public override void Update(float mouseX, float mouseY, bool mouseClicked)
        {
            // Apply locking for input handling
            LockXY(ref mouseX, ref mouseY);

            // Handle shape creation (only if not over UI)
            if (mouseClicked && !_colourMenu.IsAt(mouseX,mouseY) && !save.IsAt(mouseX,mouseY))
            {
                DrawShapesAtPoint(mouseX, mouseY);
            }

            // Handle right click for selection (only if not over UI)
            if (SplashKit.MouseClicked(MouseButton.RightButton) && !_colourMenu.IsAt(mouseX, mouseY))
            {
                _currentDrawing.SelectShapesAt(mouseX, mouseY);
            }

            // Handle menu input (always process for color selection)
            HandleColorSelection(mouseX, mouseY,mouseClicked, ref _currentColor);
            // Handle keyboard input
            HandleKeyboardInput();

            // Check for state transitions
            string newState = _drawingMenu.CheckButtonInteractions(mouseX, mouseY, mouseClicked);
            if (newState != null)
            {
                _stateManager.ChangeState(newState);
            }
        }

        public override void Draw()
        {
            float mouseX = SplashKit.MouseX();
            float mouseY = SplashKit.MouseY();
            LockXY(ref mouseX, ref mouseY);
            
            _drawingMenu.Draw();

            _colourMenu.Draw();
            DrawShapePreviews(mouseX,mouseY);
        }

        public override void Exit()
        {
            base.Exit();
            if (_currentDrawing != null)
            {
                Console.WriteLine($"Exiting Drawing: {_currentDrawing.Drawingname}");

            }
        }

        // INPUT HANDLING METHODS

        private void HandleColorSelection(float mouseX, float mouseY, bool mouseclick, ref Color currentColor)
        {
            string colorName = _colourMenu.CheckButtonInteractions(mouseX, mouseY, mouseclick);
            if (colorName != null && _colourMenu._colorwheel.ContainsKey(colorName))
            {
                _currentColor = _colourMenu._colorwheel[colorName];
                Console.WriteLine($"Color changed to: {colorName}");
            }
        }

        private void HandleKeyboardInput()
        {
            HandleSize();
            HandleShapeSelection();
            HandleDrawingActions();
            HandleShapeMove();
        }

        private void DrawShapesAtPoint(float mouseX, float mouseY)
        {
            Shape newShape = null;
            switch (_currentShapeKind)
            {
                case ShapeKind.Circle:
                    newShape = _factory.CreateCircle(_currentColor, mouseX, mouseY, _size);
                    _currentDrawing.AddShape(newShape);

                    Console.WriteLine("Circle Added");
                    break;
                case ShapeKind.Line:
                    if (!_drawingLine)
                    {
                        _lineStart = new Point2D() { X = mouseX, Y = mouseY };
                        _drawingLine = true;
                        Console.WriteLine("Start Drawing Line");
                        break;
                    }
                    else
                    {
                        if (_lineStart.Value.X != mouseX || _lineStart.Value.Y != mouseY)
                        {
                            _drawingLine = false;
                            newShape = _factory.CreateLine(_currentColor, (float)_lineStart.Value.X, (float)_lineStart.Value.Y, mouseX, mouseY, _size);
                            _currentDrawing.AddShape(newShape);

                            _lineStart = null;
                            Console.WriteLine("End Drawing Line, Line added");
                        }
                        else
                        {
                            Console.WriteLine("Starting point and Ending point cannot be the same");
                        }
                        break;
                    }
                case ShapeKind.Rectangle:
                    newShape = _factory.CreateRectangle(_currentColor, mouseX, mouseY, _size, _size);
                    newShape.X = mouseX - _size / 2;
                    newShape.Y = mouseY - _size / 2;
                    _currentDrawing.AddShape(newShape);

                    Console.WriteLine("Rectangle added");
                    break;
            }
        }

        private void DrawShapePreviews(float mouseX, float mouseY)
        {
            switch (_currentShapeKind)
            {
                case ShapeKind.Circle:
                    Shape circle = _factory.CreateCircle(Color.Gray, mouseX, mouseY, _size);
                    circle.DrawPreview(mouseX, mouseY);
                    break;
                    
                case ShapeKind.Line:
                    //Draw Preview if you are drawing line and line has value
                    if (_drawingLine)
                    {
                        Shape line = _factory.CreateLine(Color.Gray, (float)_lineStart.Value.X, (float)_lineStart.Value.Y, mouseX, mouseY, _size);
                        line.DrawPreview((float)_lineStart.Value.X, (float)_lineStart.Value.Y);
                    }
                    break;

                case ShapeKind.Rectangle:
                    Shape rectangle = _factory.CreateRectangle(Color.Gray, mouseX, mouseY, _size, _size);
                    rectangle.X = mouseX - _size / 2;
                    rectangle.Y = mouseY - _size / 2;
                    rectangle.DrawPreview(rectangle.X,rectangle.Y);
                    break;
            }
        }

        private void LockXY(ref float mouseX, ref float mouseY)
        {
            bool shiftPressed = SplashKit.KeyDown(KeyCode.LeftShiftKey);
            bool controlPressed = SplashKit.KeyDown(KeyCode.LeftCtrlKey);
            
            if (shiftPressed)
            {
                if (!_islocked)
                {
                    _locked_y = mouseY;
                    _islocked = true;
                    Console.WriteLine("Vertical Position locked");
                }
                mouseY = _locked_y;
            }
            else if (controlPressed)
            {
                if (!_islocked)
                {
                    _locked_x = mouseX;
                    _islocked = true;
                    Console.WriteLine("Horizontal Position locked");
                }
                mouseX = _locked_x;
                
            }
            else
            {
                _islocked = false;
            }
        }

        private void CancelLine()
        {
            _drawingLine = false;
            _lineStart = null;
            Console.WriteLine("Line cancelled");
        }

        private void HandleSize()
        {
            if (SplashKit.KeyTyped(KeyCode.EqualsKey))
            {
                _size += 5;
                if (_size > 200) 
                {
                    _size = 200;
                }
                Console.WriteLine($"Size: {_size}");
            }

            if (SplashKit.KeyTyped(KeyCode.MinusKey))
            {
                _size -= 5;
                if (_size < 5) 
                {
                    _size = 5;
                }
                Console.WriteLine($"Size: {_size}");
            }
        }

        private void HandleShapeSelection()
        {
            if (SplashKit.KeyTyped(KeyCode.Num1Key)) 
            {
                _currentShapeKind = ShapeKind.Rectangle;
                Console.WriteLine("Rectangle selected");
            }

            if (SplashKit.KeyTyped(KeyCode.Num2Key)) 
            {
                _currentShapeKind = ShapeKind.Circle;
                Console.WriteLine("Circle selected");
            }

            if (SplashKit.KeyTyped(KeyCode.Num3Key)) 
            {
                _currentShapeKind = ShapeKind.Line;
                Console.WriteLine("Line selected");
            }
        }

        private void HandleDrawingActions()
        {
            if (SplashKit.KeyTyped(KeyCode.SpaceKey))
            {
                _currentDrawing.Background = SplashKit.RandomRGBColor(255);
                Console.WriteLine("Random Background changed");
            }

            if (SplashKit.KeyTyped(KeyCode.DeleteKey) || SplashKit.KeyTyped(KeyCode.BackspaceKey))
            {
                int temp = _currentDrawing.SelectedShapes.Count();
                foreach (Shape shape in _currentDrawing.SelectedShapes)
                {
                    _currentDrawing.RemoveShape(shape);
                }
                Console.WriteLine($"Deleted {temp} Shapes");
            }
            
            if (SplashKit.KeyTyped(KeyCode.EscapeKey) && _drawingLine)
            {
                CancelLine();
            }
        }

        private void HandleShapeMove()
        {
            if (SplashKit.KeyDown(KeyCode.LeftKey)) MoveShapes(-0.2f, 0);
            if (SplashKit.KeyDown(KeyCode.RightKey)) MoveShapes(0.2f, 0);
            if (SplashKit.KeyDown(KeyCode.UpKey)) MoveShapes(0, -0.2f);
            if (SplashKit.KeyDown(KeyCode.DownKey)) MoveShapes(0, 0.2f);
        }

        private void MoveShapes(float X, float Y)
        {
            foreach (Shape shape in _currentDrawing.SelectedShapes)
            {
                if (shape is MyLine myline)
                {
                    myline.X += X;
                    myline.Y += Y;
                    myline.EndX += X;
                    myline.EndY += Y;
                }
                else
                {
                    shape.X += X;
                    shape.Y += Y;
                }
            }
        }

        public enum ShapeKind
        {
            Rectangle,
            Circle,
            Line
        }
    }
}