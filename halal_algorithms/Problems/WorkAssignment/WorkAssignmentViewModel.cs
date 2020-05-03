using GalaSoft.MvvmLight;
using Problems.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Problems.WorkAssignment
{
    public class WorkAssignmentViewModel : ViewModelBase
    {
        private static Random rnd = new Random();
        //Simulated Annealing algorithm

        private ChartDataModel _workAssignmentChart;
        public ChartDataModel WorkAssignmentChart
        {
            get => this._workAssignmentChart;
            set => this.Set(ref this._workAssignmentChart, value);
        }

        public DispatcherTimer Timer { get; set; }

        private double _functionFitness;
        public double FunctionFitness
        {
            get => this._functionFitness;
            set => this.Set(ref this._functionFitness, value);
        }

        public WorkAssignmentViewModel()
        {
            this._workAssignmentChart = new ChartDataModel();
            //Simulated Annealing algorithm instance

            ObservableCollection<KeyValue> targetValues = new ObservableCollection<KeyValue>();

            this.WorkAssignmentChart.DataList = targetValues;
            // Setup timer:
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(500);
            Timer.Tick += DoIteration;
            Timer.Start();


        }

        private void DoIteration(object sender, EventArgs e)
        {
            //new Task(() =>
            //{
            //    // MAGIC

            //    if (this.FunctionFitness <= 1000)
            //    {
            //        Timer.Stop();
            //    }
            //}, TaskCreationOptions.LongRunning).Start();
        }

        private void RedrawChart()
        {
            ObservableCollection<KeyValue> currentValues = new ObservableCollection<KeyValue>();
            // MAGIC
            
        }
    }
}
