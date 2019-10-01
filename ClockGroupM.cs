using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace cs_timed_silver
{
    public class ClockGroupM : BindableBase
    {
        internal string _Name = "";
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        internal bool _IsSelected = false;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                if (_IsSelected != value)
                {
                    _IsSelected = value;
                    RaisePropertyChanged("IsSelected");
                }
            }
        }

        internal System.Drawing.Bitmap _Icon = null;
        public System.Drawing.Bitmap Icon
        {
            get
            {
                return _Icon;
            }
            set
            {
                if (_Icon != value)
                {
                    if (value != null)
                    {
                        // resize while switching to new aspect ratio (square 200 x 200)

                        float maxHeight = 200;
                        float maxWidth = 200;

                        var r = new Rectangle(0,
                            0,
                            (int)Math.Round(maxWidth),
                            (int)Math.Round(maxHeight)
                        );

                        Bitmap bmp = Utils.ResizeToFitBoundingBox(value, r);

                        _Icon = bmp;
                    }
                    else
                    {
                        _Icon = null;
                    }

                    // TODO: inspiration: *efficiently* resize image:
                    //ico = Utils.ResizeImage(ico, ico.Width, ico.Height);

                    RaisePropertyChanged("Icon");
                }
            }
        }

        public override bool Equals(object obj)
        {
            var o = obj as ClockGroupM;

            if (ReferenceEquals(o, null))
            {
                return false;
            }

            return base.Equals(obj) &&
                Name == o.Name &&
                Icon == o.Icon;
        }

        public static bool operator ==(ClockGroupM m1,
            ClockGroupM m2)
        {
            if (ReferenceEquals(m1, null))
            {
                return ReferenceEquals(m2, null);
            }
            return m1.Equals(m2);
        }

        public static bool operator !=(ClockGroupM m1,
            ClockGroupM m2)
        {
            return !(m1 == m2);
        }
    }
}
