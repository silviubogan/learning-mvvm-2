using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;

namespace cs_timed_silver
{
    public class FilterVM : BindableBase
    {
        internal FilterM _MyFilter = null;
        public FilterM MyFilter
        {
            get { return _MyFilter; }
            set  { SetProperty(ref _MyFilter, value); }
        }

        internal string _DisplayString = null;
        public string DisplayString
        {
            get { return _DisplayString; }
            set { SetProperty(ref _DisplayString, value); }
        }

        internal ImageSource _MyConstantImageSource = null;
        public ImageSource MyConstantImageSource
        {
            get { return _MyConstantImageSource; }
            set
            {
                SetProperty(ref _MyConstantImageSource, value, new Action(() =>
                {
                    RaisePropertyChanged("MyImageSource");
                }));
            }
        }

        internal ImageSource _MyEmptyImageSource = null;
        public ImageSource MyEmptyImageSource
        {
            get { return _MyEmptyImageSource; }
            set
            {
                SetProperty(ref _MyEmptyImageSource, value, new Action(() =>
                {
                    RaisePropertyChanged("MyImageSource");
                }));
            }
        }

        internal ImageSource _MyNonEmptyImageSource = null;
        public ImageSource MyNonEmptyImageSource
        {
            get { return _MyNonEmptyImageSource; }
            set
            {
                SetProperty(ref _MyNonEmptyImageSource, value, new Action(() =>
                {
                    RaisePropertyChanged("MyImageSource");
                }));
            }
        }

        public ImageSource MyImageSource
        {
            get
            {
                if (MyConstantImageSource != null)
                {
                    return MyConstantImageSource;
                }
                return HasContent ? MyNonEmptyImageSource :
                    MyEmptyImageSource;
            }
        }

        internal bool _IsSelected = false;
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { SetProperty(ref _IsSelected, value); }
        }

        internal int _Items = 0;
        public int Items
        {
            get { return _Items; }
            set
            {
                SetProperty(ref _Items, value, new Action(() =>
                {
                    RaisePropertyChanged("HasContent");
                    RaisePropertyChanged("MyImageSource");
                }));
            }
        }

        public bool HasContent
        {
            get
            {
                return Items > 0;
            }
        }

        internal void UpdateItemCount()
        {
            int c = MyFilter.Clocks.OneTimeFilter(MyFilter).Count();
            Items = c;
        }

        public FilterVM()
        {

        }
    }
}