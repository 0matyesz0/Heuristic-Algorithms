using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace halal_bead.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public TravelingAgentViewModel TravelingAgentVM { get; set; }
        public WorkAssignmentViewModel WorkAssignmentVM { get; set; }
        public FunctionViewModel FunctionVM { get; set; }

        public MainViewModel()
        {
            //Parallel.Invoke(
            //    () => this.TravelingAgentVM = new TravelingAgentViewModel(),
            //    () => this.WorkAssignmentVM = new WorkAssignmentViewModel(),
            //    () => this.FunctionVM = new FunctionViewModel()
            //);
            this.TravelingAgentVM = new TravelingAgentViewModel();
            this.WorkAssignmentVM = new WorkAssignmentViewModel();
            this.FunctionVM = new FunctionViewModel();
        }
    }

}
