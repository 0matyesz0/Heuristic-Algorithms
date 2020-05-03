using GalaSoft.MvvmLight;
using Problems.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Mvvm;
using System.Windows.Controls.DataVisualization.Charting;
using System.Threading;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using Solvers.Genetic;

namespace Problems.FunctionApproximation
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

            for (int i = 0; i < this.geneticAlgorithm.TargetFunc.Count(); i++)
            {
                double x = this.geneticAlgorithm.TargetFunc[i].Keys.FirstOrDefault();
                double y = this.geneticAlgorithm.TargetFunc[i].Values.FirstOrDefault();
                targetValues.Add(new KeyValue() { Key = x, Value = y });
            }
            this.TargetfunctionChart.DataList = targetValues;
            this.ApproxFunctionChart = new ChartDataModel();

            // Setup timer:
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(500);
            Timer.Tick += DoIteration;
            Timer.Start();

            
        }

        private void DoIteration(object sender, EventArgs e)
        {
            new Task(() =>
             {
                 RedrawChart();

                 geneticAlgorithm.FitnessSelection();

                 RedrawChart();

                 geneticAlgorithm.Crossing();
                 geneticAlgorithm.Mutate();
                 this.FunctionFitness = geneticAlgorithm.globalFitness;

                 if (this.FunctionFitness <= 1000)
                 {
                     Timer.Stop();
                 }
             }, TaskCreationOptions.LongRunning).Start();
        }

        private void RedrawChart()
        {
            ObservableCollection<KeyValue> currentValues = new ObservableCollection<KeyValue>();
            for (int i = 0; i < this.geneticAlgorithm.TargetFunc.Count(); i++)
            {
                double x = this.geneticAlgorithm.TargetFunc[i].Keys.FirstOrDefault();
                double y = this.geneticAlgorithm.getYValue(x);
                currentValues.Add(new KeyValue() { Key = x, Value = y });
            }
            this.ApproxFunctionChart.DataList = currentValues;
        }
    }
}
