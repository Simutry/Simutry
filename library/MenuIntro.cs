using System.Globalization;
using System.Numerics;
using System.IO;
using MyGame;
using SplashKitSDK;
using System.Diagnostics.Contracts;
using System.ComponentModel;

namespace ShapeDrawer
{
    public class IntroMenu : Menu
    {
        public IntroMenu(Color color, float x, float y, int width, int height, string Title) 
            : base(color, x, y, width, height, Title)
        {   
            // Initialize buttons using the base class method
            InitializeButtons();    
        }

        public IntroMenu() : this(Color.Green, 0.0f, 0.0f, 992, 558, "IntroMenu")
        { }

        public override void InitializeButtons()
        {
            // Use the AddButton method from base class
            Button startGame = new Button(Color.DarkBlue, 400, 350-40, 200, 40, "StartButton", "GalleryMenu");
            startGame.Buttontext = "Start Game!";
            Button stat = new Button(Color.DarkBlue, 400, 380-20, 200, 40, "StatButton", "StatisticsMenu");
            stat.Buttontext = "Statistics!";
            Button quitGame = new Button(Color.DarkBlue, 400, 410, 200, 40, "QuitButton", "Exit");
            quitGame.Buttontext = "Quit!";            
            
            AddButton(startGame);
            AddButton(stat);
            AddButton(quitGame);
        }

        public override void Draw()
        {
            base.Draw(); // Draw the background
            SplashKit.DrawText("Welcome to Skrible!", Color.Black, "test.otf", 64, 220, 200);
            DrawAllButtons();
        }
    }
}