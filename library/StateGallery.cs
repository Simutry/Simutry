using SplashKitSDK;

namespace ShapeDrawer
{
    /// <summary>
    /// CONCRETE STATE: Gallery menu state implementation
    /// STATE PATTERN: Encapsulates gallery menu behavior
    /// </summary>
    public class GalleryState : BaseMenuState
    {
        private GalleryMenu _galleryMenu;
        private StateManager _stateManager;
        private DrawingManager _drawingManager;

        public override string StateName
        {
          get
            {
                return "GalleryMenu";;
            }  
        }

        public GalleryState(StateManager stateManager, DrawingManager drawingManager)
        {
            _stateManager = stateManager;
            _drawingManager = drawingManager;
            _galleryMenu = new GalleryMenu(drawingManager);
            StateMenu = _galleryMenu;
        }

        public override void Update(float mouseX, float mouseY, bool mouseClicked)
        {
            //HANDLE GALLERY MENU INTERACTIONS
            string newState = _galleryMenu.CheckButtonInteractions(mouseX, mouseY, mouseClicked);

            if (newState != null)
            {
                //STATE PATTERN: TRANSITION TO DRAWING STATE
                bool found = false;
                foreach (Drawing drawing in _drawingManager.DrawingsList)
                {
                    if (drawing.Drawingname == newState)
                    {
                        _drawingManager.CurrentDrawing = drawing;

                        //STATE PATTERN: TRANSITION WITH DATA
                        _stateManager.ChangeState("Drawing", drawing);
                        found = true;
                        break;
                    }
                }

                if (found == false)
                {
                    _stateManager.ChangeState(newState);                            
                }
                
            }
        }

        public override void Draw()
        {
            _galleryMenu.Draw();
        }
    }
}