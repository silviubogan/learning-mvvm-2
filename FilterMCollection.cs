using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_timed_silver
{
    public class FilterMCollection : BindableBase
    {
        public ObservableCollection<FilterM> Ms;

        internal ClockMCollection MyClocks;

        public FilterMCollection(ClockMCollection c)
        {
            MyClocks = c;

            Ms = new ObservableCollection<FilterM>();
        }
    }
}
