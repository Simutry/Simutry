using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyGame;
using SplashKitSDK;

namespace ShapeDrawer
{
    public class StatisticsTracker
    {
        private static StatisticsTracker _instance;
        private static readonly object _lock = new object();
        private List<IObserver> _observers = new List<IObserver>();

        public static StatisticsTracker Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new StatisticsTracker();
                }
            }
        }
        public int ObserverCount
        {
            get { return _observers.Count;}
        }
        public static void Reset()
        {
            _instance = null;
        }

        private StatisticsTracker() { }

        // PASS DrawingManager AS PARAMETER TO METHODS THAT NEED IT
        public void CalculateStatistics(DrawingManager drawingManager)
        {
            Statistics stats = new Statistics();

            try
            {
                CalculateFromDrawingManager(drawingManager, stats);
                NotifyObservers(stats);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating statistics: {ex.Message}");
            }
        }

        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        private void CalculateFromDrawingManager(DrawingManager drawingManager, Statistics stats)
        {
            stats.TotalDrawings = drawingManager.DrawingsList.Count;

            foreach (Drawing drawing in drawingManager.DrawingsList)
            {
                stats.TotalShapes += drawing.ShapeCount;
                foreach (Shape shape in drawing.GetShape) // This requires the public Shapes property
                {
                    switch (shape)
                    {
                        case MyRectangle:
                            stats.Rectangles++;
                            break;
                        case MyCircle:
                            stats.Circles++;
                            break;
                        case MyLine:
                            stats.Lines++;
                            break;
                    }
                }                // Add shape type counting
            }
        }

        private void NotifyObservers(Statistics stats)
        {
            // DEMO: Notify all observers when stats change
            foreach (IObserver observer in _observers)
            {
                observer.OnStatisticsUpdated(stats);
            }
        }
    }
}