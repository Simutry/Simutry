using SplashKitSDK;

namespace ShapeDrawer
{
    /// <summary>
    /// CONCRETE STATE: Save state implementation
    /// STATE PATTERN: Handles saving drawing and returning to gallery
    /// </summary>
    public class StatisticsState : BaseMenuState, IObserver
    {
        private StateManager _stateManager;
        private DrawingManager _drawingManager;
        private StatisticsMenu _statisticsMenu;
        public override string StateName
        {
          get
            {
                return "StatisticsMenu";
            }  
        }  

        public StatisticsState(StateManager stateManager, DrawingManager drawingManager)
        {
            _stateManager = stateManager;
            _drawingManager = drawingManager;
            _statisticsMenu = new StatisticsMenu();
            StateMenu = _statisticsMenu;

            StatisticsTracker.Instance.AddObserver(this);
        }

        public override void Enter()
        {
            Console.WriteLine("Entered Statistics Menu");
            StatisticsTracker.Instance.CalculateStatistics(_drawingManager);
            //Console.WriteLine(StatisticsTracker.Instance.ObserverCount);
        }

        public override void Exit()
        {
            Console.WriteLine("Exited Statistics Menu");
        }

        public override void Update(float mouseX, float mouseY, bool mouseClicked)
        {
            string newState = _statisticsMenu.CheckButtonInteractions(mouseX, mouseY, mouseClicked);

            if (newState != null)
            {
                //STATE PATTERN: TRANSITION TO NEW STATE
                _stateManager.ChangeState(newState);
            }
        }

        public override void Draw()
        {
            _statisticsMenu.Draw();
        }

        public void OnStatisticsUpdated(Statistics stats)
        {
            _statisticsMenu.LoadStatistics(stats);
        }   
    }
}