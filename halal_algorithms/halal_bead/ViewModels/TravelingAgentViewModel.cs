using GalaSoft.MvvmLight;
using halal_bead.Solvers;
using halal_bead.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;

namespace halal_bead.ViewModels
{
    public class TravelingAgentViewModel : ViewModelBase
    {
        private static Random rnd = new Random();
        private HillClimbingStochastic algorithm;

        private ObservableCollection<Point> _routePath;
        public ObservableCollection<Point> RoutePath
        {
            get => this._routePath;
            set => this.Set(ref this._routePath, value);
        }

        public DispatcherTimer Timer { get; set; }

        private int _passedSeconds;

        public int PassedSeconds
        {
            get => this._passedSeconds;
            set => this.Set(ref this._passedSeconds, value);
        }


        private double _globalFitness;
        public double GlobalFitness
        {
            get => this._globalFitness;
            set => this.Set(ref this._globalFitness, value);
        }

        public TravelingAgentViewModel()
        {
            this._routePath = new ObservableCollection<Point>();
            this.algorithm = new HillClimbingStochastic();

            ObservableCollection<Point> targetValues = new ObservableCollection<Point>();

            for (int i = 0; i < this.algorithm.Problem.Route.Count(); i++)
            {
                
                targetValues.Add(this.algorithm.Problem.Route[i].Position);
            }
            this.RoutePath = targetValues;

            //Setup Timer: 
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(100);
            Timer.Tick += Repeat; 
            Timer.Start();
        }

        public void Repeat(object sender, EventArgs e)
        {
            if (this.algorithm.StopCondition == false)
            {
                new Task(() =>
                {
                    this.RedrawRoute();

                    this.algorithm.Repeat();

                    this.RedrawRoute();

                }, TaskCreationOptions.LongRunning).Start();
                this.PassedSeconds++;
            }
        }

        private void RedrawRoute()
        {
            ObservableCollection<Point> currentValues = new ObservableCollection<Point>();

            for (int i = 0; i < this.algorithm.CurrentRoute.Count(); i++)
            {
                currentValues.Add(this.algorithm.CurrentRoute[i].Position);
            }
            this.RoutePath = currentValues;
            this.GlobalFitness = this.algorithm.globalFitness;
        }
    }

    [ValueConversion(typeof(ObservableCollection<Point>), typeof(Geometry))]
    public class PointsToPathConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ObservableCollection<Point> points = (ObservableCollection<Point>)value;
            if (points.Count() > 0)
            {
                Point start = points[0];
                List<LineSegment> segments = new List<LineSegment>();
                for (int i = 1; i < points.Count(); i++)
                {
                    segments.Add(new LineSegment(points[i], true));
                }
                PathFigure figure = new PathFigure(start, segments, false);
                PathGeometry geometry = new PathGeometry();
                geometry.Figures.Add(figure);
                return geometry;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
