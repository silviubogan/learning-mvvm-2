using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace cs_timed_silver
{
    public class ClockGroupVM : ClockGroupM
    {
        internal string _DisplayString = "";
        public string DisplayString
        {
            get
            {
                return _DisplayString;
            }
            set
            {
                if (_DisplayString != value)
                {
                    _DisplayString = value;
                    RaisePropertyChanged("DisplayString");
                }
            }
        }

        internal Brush _Foreground = Brushes.Black;
        public Brush Foreground
        {
            get
            {
                return _Foreground;
            }
            set
            {
                if (_Foreground != value)
                {
                    _Foreground = value;
                    RaisePropertyChanged("Foreground");
                }
            }
        }

        internal FontStyle _FontStyle = FontStyles.Normal;
        public FontStyle FontStyle
        {
            get
            {
                return _FontStyle;
            }
            set
            {
                if (_FontStyle != value)
                {
                    _FontStyle = value;
                    RaisePropertyChanged("FontStyle");
                }
            }
        }

        public ClockGroupVM() : base()
        {
            // inheritance from ClockGroupM does this ctor's job
        }

        public ClockGroupVM(ClockGroupM model)
        {
            Icon = model.Icon;
            Name = model.Name;
            DisplayString = model.Name;
            IsSelected = model.IsSelected;
        }

        public override bool Equals(object obj)
        {
            var o = obj as ClockGroupVM;

            if (ReferenceEquals(o, null))
            {
                return false;
            }

            return base.Equals(obj) &&
                DisplayString == o.DisplayString &&
                FontStyle == o.FontStyle &&
                Foreground == o.Foreground;
        }

        public static bool operator ==(ClockGroupVM m1,
            ClockGroupVM m2)
        {
            if (ReferenceEquals(m1, null))
            {
                return ReferenceEquals(m2, null);
            }
            return m1.Equals(m2);
        }

        public static bool operator !=(ClockGroupVM m1,
            ClockGroupVM m2)
        {
            return !(m1 == m2);
        }

        public override string ToString()
        {
            return DisplayString;
        }
    }
}
