using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace ShapeDrawer
{
    public class StateManager
    {
        //PRIVATE FIELDS//
        private Dictionary<string, IMenuState> _states;        
        private IMenuState _currentState;                      
        private DrawingManager _drawingManager;                
        private Window _window;                                
        private Drawing _currentDrawing;  // ADD THIS: Track current drawing for SaveState

        //END REGION FOR PRIVATE FIELDS//

        //PUBLIC PROPERTIES//
        public IMenuState CurrentState
        {
            get
            {
                return _currentState;
            }
        } 
        
        public string CurrentStateName
        {
            get
            {
                return _currentState?.StateName ?? "None";      
            }
        }

        public Drawing CurrentDrawing
        {
            get
            {
                return _currentDrawing;
            }
        }

        //END REGION FOR PUBLIC PROPERTIES//
        
        //CONSTRUCTOR//
        public StateManager(DrawingManager drawingManager, Window window)
        {
            _drawingManager = drawingManager;
            _window = window;
            _states = new Dictionary<string, IMenuState>();
            _currentDrawing = null;  // Initialize as null
            
            //FACTORY PATTERN: INITIALIZE ALL STATES
            InitializeStates();
            
            //STATE PATTERN: START WITH INTRO MENU
            ChangeState("IntroMenu");
        }

        //END REGION FOR CONSTRUCTOR//


        //STATE MANAGEMENT METHODS//
        private void InitializeStates()
        {
            //FACTORY PATTERN: CREATE ALL MENU STATES
            _states["IntroMenu"] = new IntroState(this);
            _states["StatisticsMenu"] = new StatisticsState(this, _drawingManager);
            _states["GalleryMenu"] = new GalleryState(this, _drawingManager);
            _states["Exit"] = new ExitState(_window);
            
            // NOTE: SaveState and DrawingState created dynamically with drawing parameter
            // Don't create SaveState here - it needs a drawing which we don't have yet
        }

        // STATE PATTERN: TRANSITION TO NEW STATE
        // MEDIATOR PATTERN: COORDINATES STATE TRANSITIONS
        public void ChangeState(string newStateName, Drawing stateData = null)
        {
            //EXIT CURRENT STATE
            if (_currentState != null)
            {
                _currentState.Exit();
            }

            //UPDATE CURRENT DRAWING IF PROVIDED
            if (stateData is Drawing drawing)
            {
                _currentDrawing = drawing;
            }

            //CREATE OR GET NEW STATE
            IMenuState newState = CreateState(newStateName, stateData);
            if (newState == null)
            {
                Console.WriteLine($"MenuManager: Cannot create state {newStateName}");
                return;
            }

            //STATE PATTERN: UPDATE CURRENT STATE
            _currentState = newState;
            _currentState.Enter();
        }

        /// <summary>
        /// FACTORY METHOD: CREATES THE APPROPRIATE STATE INSTANCE
        /// </summary>
        private IMenuState CreateState(string stateName, Drawing drawingdata)
        {
            switch (stateName)
            {
                case "Drawing" when drawingdata is Drawing drawing:
                    // MAKE A STATE FOR DRAWING AND PUT THE DRAWING INTO THE STATE
                    DrawingState drawingState = new DrawingState(this, drawing);
                    _states["Drawing"] = drawingState; 
                    return drawingState;

                case "Save":
                    // CREATE SAVE STATE WITH CURRENT DRAWING
                    if (_currentDrawing != null)
                    {
                        SaveState saveState = new SaveState(this, _currentDrawing);
                        _states["Save"] = saveState; 
                        return saveState;
                    }
                    else
                    {
                        Console.WriteLine("Save is not created correctly, drawing is null");
                        return new GalleryState(this, _drawingManager); // return to gallery
                    }

                default:
                    // RETURN EXISTING STATE OR CREATE NEW ONE
                    if (_states.ContainsKey(stateName))
                    {
                        return _states[stateName];
                    }
                    else
                    {
                        Console.WriteLine($"MenuManager: no {stateName} State");
                        return null;
                    }
            }
        }
        //END REGION FOR STATE MANAGEMENT METHODS//


        //MAIN LOOP METHODS//
        public void Update(float mouseX, float mouseY, bool mouseClicked)
        {
            //STATE PATTERN: TELL STATE TO UPDATE ITSELF
            if (_currentState != null)
            {
                _currentState.Update(mouseX, mouseY, mouseClicked);
            }
        }

        public void Draw()
        {
            //STATE PATTERN: TELL STATE TO DRAW ITSELF
            if (_currentState != null)
            {
                _currentState.Draw();
            }
        }

        //END REGION FOR MAIN LOOP METHODS//
    }
}