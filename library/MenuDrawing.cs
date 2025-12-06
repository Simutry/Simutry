using System;
using System.Collections.Generic;
using SplashKitSDK;
using MyGame;

namespace ShapeDrawer
{
    public class DrawingMenu : Menu
    {
        private Drawing _drawing;

        public DrawingMenu(Drawing drawing)
        {
            _drawing = drawing;
            InitializeButtons();
        }

        public override void InitializeButtons()
        {
            Button save = new Button(Color.DarkGreen, 790, 5, 200, 40, "SaveButton", "Save");
            save.Buttontext = "Save & Exit";
            AddButton(save);
        }

        public override void Draw()
        {
            // Draw the main drawing
            _drawing.Draw();
            DrawAllButtons();
            
            // Draw shape previews
        }
    }
}