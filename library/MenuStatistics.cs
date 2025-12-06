using System.Globalization;
using System.Numerics;
using System.IO;
using MyGame;
using SplashKitSDK;
using System.Diagnostics.Contracts;
using System.ComponentModel;

namespace ShapeDrawer
{
    public class StatisticsMenu : Menu
    {
        private string _text = "Default";

        public StatisticsMenu(Color color, float x, float y, int width, int height, string Title) 
            : base(color, x, y, width, height, Title)
        {
            // Initialize buttons using the base class method
            InitializeButtons();    
        }

        public StatisticsMenu() : this(Color.Green, 0.0f, 0.0f, 992, 558, "StatisticsMenu")
        { }

        public override void InitializeButtons()
        {
            // Use the AddButton method from base class
            Button backToIntro = new Button(Color.DarkBlue, 20, 480, 200, 40, "IntroButton", "IntroMenu");
            backToIntro.Buttontext = "To Intro";
            Button backToGallery = new Button(Color.DarkBlue, 760, 480, 200, 40, "GalleryButton", "GalleryMenu");
            backToGallery.Buttontext = "To Gallery";          
            
            AddButton(backToIntro);
            AddButton(backToGallery);
        }

        public override void Draw()
        {
            base.Draw(); // Draw the background
            SplashKit.FillRectangle(Color.Gray, 150, 80, 700, 380);
            SplashKit.DrawText("Statistics", Color.Black, "test.otf", 28, 150+700-400, 90);
            float textY = 120;
            foreach (string line in _text.Split('\n'))
            {
                SplashKit.DrawText(line, Color.White, "test.otf", 42, 200, textY);
                textY += 60;
            }
            DrawAllButtons();
        }

        public void LoadStatistics(Statistics stats)
        {
            _text = 
                    $"Total Drawings: {stats.TotalDrawings}\n" +
                    $"Total Shapes: {stats.TotalShapes}\n" +
                    $"Rectangles: {stats.Rectangles}\n" +
                    $"Circles: {stats.Circles}\n" +
                    $"Lines: {stats.Lines}\n";
            Console.WriteLine(_text);
        }
    }
}