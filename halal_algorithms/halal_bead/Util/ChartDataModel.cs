using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace halal_bead.Util
{
    public class ChartDataModel : BindableBase
    {
		private ObservableCollection<KeyValue> _dataList;

		public ObservableCollection<KeyValue> DataList
		{
			get { return _dataList; }
			set { SetProperty(ref _dataList, value); }
		}
	}
}
