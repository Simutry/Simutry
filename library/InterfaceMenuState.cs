    public interface IMenuState
    {
        void Enter();
        void Update(float mouseX, float mouseY, bool mouseClicked);
        void Draw();
        void Exit();
        string StateName 
        { 
            get; 
        }
    }