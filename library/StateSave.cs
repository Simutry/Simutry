using SplashKitSDK;

namespace ShapeDrawer
{
    /// <summary>
    /// CONCRETE STATE: Save state implementation
    /// STATE PATTERN: Handles saving drawing and returning to gallery
    /// </summary>
    public class SaveState : BaseMenuState
    {
        private StateManager _stateManager;
        private Drawing _drawing;
        private bool _hasSaved;

        public override string StateName
        {
          get
            {
                return "Save";
            }  
        } 

        public SaveState(StateManager stateManager, Drawing drawing)
        {
            _stateManager = stateManager;
            _drawing = drawing;
            _hasSaved = false;
        }

        public Drawing drawing
        {
            set
            {
                _drawing = value;
            }
        }
        public override void Enter()
        {
            base.Enter();
            Console.WriteLine($"SaveState: Saving drawing '{_drawing?.Drawingname}'");
            
            // PERFORM SAVE OPERATION IMMEDIATELY ON ENTER
            SaveDrawing();
        }

        public override void Update(float mouseX, float mouseY, bool mouseClicked)
        {
            // ONCE SAVE IS COMPLETE, RETURN TO GALLERY
            if (_hasSaved)
            {
                _stateManager.ChangeState("GalleryMenu");
            }
        }

        public override void Draw()
        {
            // SHOW SAVING MESSAGE
            SplashKit.ClearScreen(Color.LightGray);
            
            if (_hasSaved)
            {
                Console.WriteLine("Drawing Saved Successfully!");
            }
            else
            {
                Console.WriteLine("Saving Drawing...");
            }
        }

        public override void Exit()
        {
            base.Exit();
            Console.WriteLine("SaveState: Exiting save state");
        }

        /// <summary>
        /// SAVES THE CURRENT DRAWING TO FILE
        /// </summary>
        private void SaveDrawing()
        {
            if (_drawing != null)
            {
                string filename = $"Drawings/{_drawing.Drawingname}.txt";
                _drawing.Save(filename);
                Console.WriteLine($"SaveState: Drawing saved to {filename}");
                _hasSaved = true;
            }
            else
            {
                Console.WriteLine("SaveState: Cannot save - drawing or drawing name is null");
                _hasSaved = true; 
            }
        }
    }
}