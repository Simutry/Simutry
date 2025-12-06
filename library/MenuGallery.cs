using System.Globalization;
using System.Numerics;
using System.IO;
using MyGame;
using SplashKitSDK;
using System.Diagnostics.Contracts;
using System.ComponentModel;

namespace ShapeDrawer
{
    public class GalleryMenu : Menu
    {
        private DrawingManager _drawingManager;
        public GalleryMenu(Color color, float x, float y, int width, int height, string Title, DrawingManager drawingmanager) 
            : base(color, x, y, width, height, Title)
        {
            _drawingManager = drawingmanager;

            // Initialize buttons using the base class method
            InitializeButtons();
            InitializeDrawingButtons();
        }

        public GalleryMenu(DrawingManager drawingmg) : this(Color.Green, 0.0f, 0.0f, 992, 558, "GalleryMenu", drawingmg)
        {
            InitializeButtons();
            InitializeDrawingButtons();
        }

        public override void InitializeButtons()
        {
        
            Button quitGame = new Button(Color.DarkBlue, 830, 510, 130, 40, "QuitButton", "Exit");
            quitGame.Buttontext = "Quit!";  //Return exit 
            quitGame.TextColor = Color.Black;

            Button backToIntro = new Button(Color.DarkBlue, 20, 510, 130, 40, "IntroButton", "IntroMenu");
            backToIntro.Buttontext = "To Intro";

            AddButton(backToIntro);
            AddButton(quitGame); // use the AddButton method from base class
        }

        private void InitializeDrawingButtons()
        {
            // Grid layout: 3 columns x 3 rows = 9 buttons per page
            int buttonsPerPage = 6;
            int startIndex = 0;
            
            float startX = 100;
            float startY = 110;
            int buttonWidth = 230;
            int buttonHeight = 140;
            float horizontalSpacing = 55;
            float verticalSpacing = 45;

            for (int i = 0; i < buttonsPerPage; i++)
            {
                int drawingIndex = startIndex + i;
                
                // Stop if we've run out of drawings
                if (drawingIndex >= _drawingManager.DrawingsList.Count)
                    break;

                // Calculate grid position to give position in grid (0,0) to (2,2)
                int row = i / 3; // 3 columns
                int column = i % 3; // 3 columns
                
                float x = startX + column * (buttonWidth + horizontalSpacing);
                float y = startY + row * (buttonHeight + verticalSpacing);

                // Get drawing name or use "Empty"
                string drawingName = _drawingManager.DrawingsList[drawingIndex].Drawingname;

                // Create button for this drawing
                Button drawingButton = new Button(Color.LightGray, x, y, buttonWidth, buttonHeight, 
                    "DrawingButton", $"{drawingName}");
                drawingButton.Buttontext = drawingName;
                drawingButton.TextColor = Color.Black;
                
                AddButton(drawingButton);
            }
        }

        public override void Draw() 
        {
            base.Draw(); // Draw the background
            SplashKit.DrawText($"Page 1", Color.Black, "test.otf", 24, 480, 520);
            DrawAllButtons();
        }

    }
}