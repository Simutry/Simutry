using SplashKitSDK;

namespace ShapeDrawer
{
    // STATE PATTERN: Interface for all menu states, this defines the contract that all menu states must implement

    /// ABSTRACT BASE STATE: Provides common functionality for all menu states
    public abstract class BaseMenuState : IMenuState
    {
        public abstract string StateName 
        { 
            get; 
        }

        protected Menu StateMenu 
        { 
            get; 
            set; 
        }

        public virtual void Enter()
        {
            Console.WriteLine($"Entering state: {StateName}");
        }

        public virtual void Exit()
        {
            Console.WriteLine($"Exiting state: {StateName}");
        }

        public abstract void Update(float mouseX, float mouseY, bool mouseClicked);
        public abstract void Draw();
    }

    //END FOR STATE PATTERN INTERFACES//
}