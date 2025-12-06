using System;
using SplashKitSDK;

namespace ShapeDrawer
{
    public class Program
    {
        public static void Main()
        {
            //INITIALIZE MANAGERS
            DrawingManager drawingManager = new DrawingManager();
            Window window = new Window("ShapeDrawer", 992, 558);
            
            //STATE PATTERN: CREATE MENU MANAGER
            StateManager stateManager = new StateManager(drawingManager, window);
            
            //MAIN LOOP - SIMPLIFIED
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                //GET INPUT
                float mouseX = SplashKit.MouseX();
                float mouseY = SplashKit.MouseY();
                bool mouseClicked = SplashKit.MouseClicked(MouseButton.LeftButton);

                //STATE PATTERN: UPDATE AND DRAW CURRENT STATE
                stateManager.Update(mouseX, mouseY, mouseClicked);
                stateManager.Draw();

                SplashKit.RefreshScreen();
            }
            while (!window.CloseRequested && stateManager.CurrentStateName != "Exit");
        }
    }
}