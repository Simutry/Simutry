using SplashKitSDK;

namespace ShapeDrawer
{
    /// <summary>
    /// CONCRETE STATE: Intro menu state implementation
    /// STATE PATTERN: Encapsulates intro menu behavior
    /// </summary>
    public class IntroState : BaseMenuState
    {
        private IntroMenu _introMenu;
        private StateManager _stateManager;

        public override string StateName
        {
          get
            {
                return "IntroMenu";
            }  
        } 

        public IntroState(StateManager stateManager)
        {
            _stateManager = stateManager;
            _introMenu = new IntroMenu();
            StateMenu = _introMenu;
        }

        public override void Update(float mouseX, float mouseY, bool mouseClicked)
        {
            //HANDLE INTRO MENU INTERACTIONS
            string newState = _introMenu.CheckButtonInteractions(mouseX, mouseY, mouseClicked);
            
            if (newState != null)
            {
                //STATE PATTERN: TRANSITION TO NEW STATE
                _stateManager.ChangeState(newState);
            }
        }

        public override void Draw()
        {
            _introMenu.Draw();
        }
    }
}