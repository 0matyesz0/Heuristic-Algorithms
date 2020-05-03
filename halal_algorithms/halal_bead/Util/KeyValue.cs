using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace halal_bead.Util
{
    public class KeyValue : BindableBase
    {
		private double _key;

		public double Key
		{
			get { return _key; }
			set { SetProperty(ref this._key, value); }
		}

		private double _value;

		public double Value
		{
			get { return _value; }
			set { SetProperty(ref this._value, value); }
		}

	}
}
