using System.Globalization;
using System.Numerics;
using System.IO;
using MyGame;
using SplashKitSDK;
using System.Diagnostics.Contracts;
using System.ComponentModel;
using System.Reflection.Metadata;

namespace ShapeDrawer
{
    public class ColourMenu : Menu
    {
        private Button red, orangeRed, orange, gold, yellow, yellowGreen, limeGreen, green;
        private Button teal, dodgerBlue, blue, blueViolet, purple, magenta, hotPink, brown, black, white;
        public ColourMenu(Color color, float x, float y, int width, int height, string Title) 
            : base(color, x, y, width, height, Title)
        {
            // Initialize buttons using the base class method
            InitializeButtons();
        }

        public ColourMenu() : this(Color.Gray, 200, 0, 460, 40, "ColorMenu")
        { }

        public override void InitializeButtons()
        {
            red = new Button(Color.Red, 205, 5, 20, 30, "RedButton", "red");
            orangeRed = new Button(Color.OrangeRed, 230, 5, 20, 30, "OrangeRedButton", "orangered");
            orange = new Button(Color.Orange, 255, 5, 20, 30, "OrangeButton", "orange");
            gold = new Button(Color.Gold, 280, 5, 20, 30, "GoldButton", "gold");
            yellow = new Button(Color.Yellow, 305, 5, 20, 30, "YellowButton", "yellow");
            yellowGreen = new Button(Color.YellowGreen, 330, 5, 20, 30, "YellowGreenButton", "yellowgreen");
            limeGreen = new Button(Color.LimeGreen, 355, 5, 20, 30, "LimeGreenButton", "limegreen");
            green = new Button(Color.Green, 380, 5, 20, 30, "GreenButton", "green");
            teal = new Button(Color.Teal, 405, 5, 20, 30, "TealButton", "teal");
            dodgerBlue = new Button(Color.DodgerBlue, 430, 5, 20, 30, "DodgerBlueButton", "dodgerblue");
            blue = new Button(Color.Blue, 455, 5, 20, 30, "BlueButton", "blue");
            blueViolet = new Button(Color.BlueViolet, 480, 5, 20, 30, "BlueVioletButton", "blueviolet");
            purple = new Button(Color.Purple, 505, 5, 20, 30, "PurpleButton", "purple");
            magenta = new Button(Color.Magenta, 530, 5, 20, 30, "MagentaButton", "magenta");
            hotPink = new Button(Color.HotPink, 555, 5, 20, 30, "HotPinkButton", "hotpink");
            brown = new Button(Color.Brown, 580, 5, 20, 30, "BrownButton", "brown");
            black = new Button(Color.Black, 605, 5, 20, 30, "BlackButton", "black");
            white = new Button(Color.White, 630, 5, 20, 30, "WhiteButton", "white");
        // Use the AddButton method from base class
            AddButton(red);
            AddButton(orangeRed);
            AddButton(orange);
            AddButton(gold);
            AddButton(yellow);
            AddButton(yellowGreen);
            AddButton(limeGreen);
            AddButton(green);
            AddButton(teal);
            AddButton(dodgerBlue);
            AddButton(blue);
            AddButton(blueViolet);
            AddButton(purple);
            AddButton(magenta);
            AddButton(hotPink);
            AddButton(brown);
            AddButton(black);
            AddButton(white);
        }
        public override void Draw()
        {
            base.Draw(); // Draw the background
            DrawAllButtons();
        }

        public Dictionary<string, Color> _colorwheel = new Dictionary<string, Color>
        {
            {"red", Color.Red},
            {"orangered", Color.OrangeRed},
            {"orange", Color.Orange},
            {"gold", Color.Gold},
            {"yellow", Color.Yellow},
            {"yellowgreen", Color.YellowGreen},
            {"limegreen", Color.LimeGreen},
            {"green", Color.Green},
            {"teal", Color.Teal},
            {"dodgerblue", Color.DodgerBlue},
            {"blue", Color.Blue},
            {"blueviolet", Color.BlueViolet},
            {"purple", Color.Purple},
            {"magenta", Color.Magenta},
            {"hotpink", Color.HotPink},
            {"brown", Color.Brown},
            {"black", Color.Black},
            {"white", Color.White}
        };
    }
}