using GalaSoft.MvvmLight;
using halal_bead.Solvers;
using halal_bead.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace halal_bead.ViewModels
{
    public class WorkAssignmentViewModel : ViewModelBase
    {
        private static Random rnd = new Random();
        private int timeInterval = 500;

        public SimulatedAnnealing algorithm { get; set; }

        private ChartDataModel _workAssignmentChart;
        public ChartDataModel WorkAssignmentChart
        {
            get => this._workAssignmentChart;
            set => this.Set(ref this._workAssignmentChart, value);
        }

        public DispatcherTimer Timer { get; set; }

        private int _timeRemaining;
        public int TimeRemaining
        {
            get => this._timeRemaining;
            set => this.Set(ref this._timeRemaining, value);
        }

        private double _workAssignFitness;
        public double WorkAssignFitness
        {
            get => this._workAssignFitness;
            set => this.Set(ref this._workAssignFitness, value);
        }

        public WorkAssignmentViewModel()
        {
            this._workAssignmentChart = new ChartDataModel();
            this.algorithm = new SimulatedAnnealing();
            ObservableCollection<KeyValue> targetValues = new ObservableCollection<KeyValue>();

            this.WorkAssignmentChart.DataList = targetValues;
            // Setup timer:
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(timeInterval);
            Timer.Tick += Repeat;
            Timer.Start();
        }

        private void Repeat(object sender, EventArgs e)
        {
            this.algorithm.TimePassed += timeInterval;

            new Task(() =>
            {
                this.RedrawChart();
                this.algorithm.Repeat();
                this.RedrawChart();

            }, TaskCreationOptions.LongRunning).Start();

            if (this.algorithm.TimePassed >= this.algorithm.tMax * 1000)
            {
                Timer.Stop();
            }
        }

        private void RedrawChart()
        {
            this.WorkAssignFitness = this.algorithm.globalFitness;
            this.TimeRemaining = (this.algorithm.tMax * 1000 - this.algorithm.TimePassed) / 1000;

            ObservableCollection<KeyValue> currentValues = new ObservableCollection<KeyValue>();
            for (int i = 0; i < this.algorithm.OptimalSolution.Count(); i++)
            {
                currentValues.Add(new KeyValue() { Key = i, Value = this.algorithm.OptimalSolution[i] });
            }
            this.WorkAssignmentChart.DataList = currentValues;
        }
    }
}
