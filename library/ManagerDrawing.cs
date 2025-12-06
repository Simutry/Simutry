using System;
using System.Collections.Generic;
using System.IO;
using SplashKitSDK;

namespace ShapeDrawer
{
    public class DrawingManager    //MANAGES ALL DRAWINGS IN THE APPLICATION
    {
        //PRIVATE FIELDS//
        private List<Drawing> _drawings;        //STORES ALL LOADED DRAWINGS
        private Drawing _currentdrawing;        //TRACKS CURRENTLY ACTIVE DRAWING

        //END FOR PRIVATE FIELDS//


        //CONSTRUCTORS//
        public DrawingManager()
        {
            _drawings = new List<Drawing>();
            _currentdrawing = null;             //NO DRAWING SELECTED INITIALLY

            InitializeDrawings();               //LOAD ALL EXISTING DRAWINGS ON CREATION
        }

        //END FOR CONSTRUCTORS//


        //PUBLIC PROPERTIES//
        public List<Drawing> DrawingsList
        {
            get
            {
                return _drawings;
            }
            set
            {
                _drawings = value;
            }
        }

        public Drawing CurrentDrawing
        {
            get
            {
                return _currentdrawing;
            }
            set
            {
                _currentdrawing = value;
            }
        }

        //END FOR PUBLIC PROPERTIES//


        //INITIALIZATION METHODS//
        public void InitializeDrawings()
        {
            //LOAD ALL SAVED DRAWINGS FROM THE DRAWINGS DIRECTORY
            string[] drawingsaves = Directory.GetFiles("Drawings");
            string temp;
            
            for (int i = 0; i < drawingsaves.Length; i++)
            {
                //READ DRAWING FILE TO FIND DRAWING NAME
                StreamReader reader = new StreamReader(drawingsaves[i]);
                temp = reader.ReadLine();   //SKIP BACKGROUND COLOR COMPONENTS
                temp = reader.ReadLine();
                temp = reader.ReadLine();
                temp = reader.ReadLine();   //SKIP SHAPE COUNT
                temp = reader.ReadLine();   //READ DRAWING NAME
                
                //CREATE NEW DRAWING AND SET PROPERTIES
                Drawing newone = new Drawing();
                newone.DrawingsId = i;              //SET UNIQUE IDENTIFIER USING CURRENT I
                newone.Drawingname = temp;          //SET DRAWING NAME FROM FILE
                newone.Load($"Drawings/{newone.Drawingname}.txt"); //LOADS ALL DRAWINGS, BUT DOESNT DISPLAY BECAUSE MENU STARTS FROM INTROMENU.
                DrawingsList.Add(newone);           //ADD TO DRAWINGS COLLECTION
                
                reader.Close();                     //CLOSE FILE READER
            };
        }

        //END REGION FOR INITIALIZATION METHODS//


        //DRAWING METHOD//

        //END DRAWING METHOD//

    }
}