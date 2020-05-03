using GalaSoft.MvvmLight;
using halal_bead.Solvers;
using halal_bead.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace halal_bead.ViewModels
{
    public class FunctionViewModel : ViewModelBase
    {
        private static Random rnd = new Random();
        private GeneticAlgorithm geneticAlgorithm;

        private ChartDataModel _targetFunctionChart;
        public ChartDataModel TargetfunctionChart
        {
            get => this._targetFunctionChart;
            set => this.Set(ref this._targetFunctionChart, value);
        }

        private ChartDataModel _approxFunctionChart;
        public ChartDataModel ApproxFunctionChart
        {
            get => this._approxFunctionChart;
            set => this.Set(ref this._approxFunctionChart, value);
        }


        public DispatcherTimer Timer { get; set; }

        private double _functionFitness;
        public double FunctionFitness
        {
            get => this._functionFitness;
            set => this.Set(ref this._functionFitness, value);
        }

        public FunctionViewModel()
        {
            this._targetFunctionChart = new ChartDataModel();
            this._approxFunctionChart = new ChartDataModel();
            this.geneticAlgorithm = new GeneticAlgorithm();

            ObservableCollection<KeyValue> targetValues = new ObservableCollection<KeyValue>();

            for (int i = 0; i < this.geneticAlgorithm.Problem.TargetFunc.Count(); i++)
            {
                double x = this.geneticAlgorithm.Problem.TargetFunc[i].Keys.FirstOrDefault();
                double y = this.geneticAlgorithm.Problem.TargetFunc[i].Values.FirstOrDefault();
                targetValues.Add(new KeyValue() { Key = x, Value = y });
            }
            this.TargetfunctionChart.DataList = targetValues;
            this.ApproxFunctionChart = new ChartDataModel();

            // Setup Timer: 
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(500);
            Timer.Tick += Repeat;
            Timer.Start();
        }

        public void Repeat(object sender, EventArgs e)
        {
                new Task(() =>
                {
                    RedrawChart();

                    geneticAlgorithm.FitnessSelection();

                    RedrawChart();

                    geneticAlgorithm.Crossing();
                    geneticAlgorithm.Mutate();
                    this.FunctionFitness = geneticAlgorithm.globalFitness;

                }, TaskCreationOptions.LongRunning).Start();
        }

        private void RedrawChart()
        {
            ObservableCollection<KeyValue> currentValues = new ObservableCollection<KeyValue>();
            for (int i = 0; i < this.geneticAlgorithm.Problem.TargetFunc.Count(); i++)
            {
                double x = this.geneticAlgorithm.Problem.TargetFunc[i].Keys.FirstOrDefault();
                double y = GetYValue(x);
                currentValues.Add(new KeyValue() { Key = x, Value = y });
            }
            this.ApproxFunctionChart.DataList = currentValues;
        }

        public double GetYValue(double x)
        {
            var approxFunc = this.geneticAlgorithm.GetBestChromosome();
            double y = (approxFunc.parameters[0] * Math.Pow(x - approxFunc.parameters[1], 3)) + (approxFunc.parameters[2] * Math.Pow(x - approxFunc.parameters[3], 2)) + approxFunc.parameters[4];
            return y;
        }
    }
}
