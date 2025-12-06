using SplashKitSDK;

namespace ShapeDrawer
{
    /// <summary>
    /// CONCRETE STATE: Exit state implementation
    /// STATE PATTERN: Handles application termination
    /// </summary>
    public class ExitState : BaseMenuState
    {
        private Window _window;

        public override string StateName
        {
          get
            {
                return "Exit";
            }  
        }

        public ExitState(Window window)
        {
            _window = window;
        }

        public override void Update(float mouseX, float mouseY, bool mouseClicked)
        {
            //CLOSE WINDOW ON ENTERING EXIT STATE
            _window.Close();
        }

        public override void Draw()
        {
            //OPTIONAL: DRAW EXIT MESSAGE
            SplashKit.ClearScreen(Color.Black);
        }
    }
}