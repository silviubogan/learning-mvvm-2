using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace cs_timed_silver
{
    public class FilterVMCollection : BindableBase
    {
        public ObservableCollection<FilterVM> VMs { get; set; }

        internal FilterMCollection MyModel;

        internal ClockMCollection MyClocks;

        public FilterVMCollection(ClockMCollection c)
        {
            MyClocks = c;

            MyModel = new FilterMCollection(MyClocks);
            MyModel.Ms.CollectionChanged += MyModel_Ms_CollectionChanged;

            VMs = new ObservableCollection<FilterVM>();
            VMs.CollectionChanged += VMs_CollectionChanged;

            MyClocks.GroupsVM.VMs.CollectionChanged += GroupsVMs_CollectionChanged;

            OcPropertyChangedListener<ClockGroupVM> l1 =
                OcPropertyChangedListener.Create(MyClocks.GroupsVM.VMs);
            l1.PropertyChanged += l1_PropertyChanged;
        }

        private void l1_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //var vm = sender as ClockGroupVM;

            //FilterM m = ClockGroupVMToFilterM(vm);

            //m.GroupNames = new List<string>() { vm.Name };

            //prepareDeletion: null,
            //equalsWithinTargetTo: new Func<ClockGroupVM, FilterM, bool>
            //    (ClockGroupVMEqualsWithinTargetToFilterM),
            //toTarget: new Func<ClockGroupVM, FilterM>(ClockGroupVMToFilterM),
        }

        public void Init()
        {
            VMs.Add(new FilterVM()
            {
                MyFilter = new FilterM(MyClocks.MyDataFile.ClockVMCollection.Model, "")
                {
                    ShowActive = true,
                    ShowInactive = true,
                    ShowTimers = true,
                    ShowAlarms = true
                },
                DisplayString = "All",
                MyConstantImageSource = new BitmapImage(
                    new Uri("/Resources/show-all-icon.png", UriKind.Relative))
            });
            VMs.Add(new FilterVM()
            {
                MyFilter = new FilterM(MyClocks.MyDataFile.ClockVMCollection.Model, "")
                {
                    ShowActive = true,
                    ShowInactive = false,
                    ShowTimers = true,
                    ShowAlarms = true
                },
                DisplayString = "Active",
                MyConstantImageSource = new BitmapImage(
                    new Uri("/Resources/on filter.ico", UriKind.Relative))
            });
            VMs.Add(new FilterVM()
            {
                MyFilter = new FilterM(MyClocks.MyDataFile.ClockVMCollection.Model, "")
                {
                    ShowActive = false,
                    ShowInactive = true,
                    ShowTimers = true,
                    ShowAlarms = true
                },
                DisplayString = "Inactive",
                MyConstantImageSource = new BitmapImage(
                    new Uri("/Resources/off filter.ico", UriKind.Relative))
            });

            VMs.Add(new FilterVM()
            {
                MyFilter = new FilterM(MyClocks.MyDataFile.ClockVMCollection.Model, "")
                {
                    ShowActive = true,
                    ShowInactive = true,
                    ShowTimers = true,
                    ShowAlarms = false
                },
                DisplayString = "Timers",
                MyConstantImageSource = new BitmapImage(
                    new Uri("/Resources/timers filter (clepsidra 4).ico", UriKind.Relative))
            });
            VMs.Add(new FilterVM()
            {
                MyFilter = new FilterM(MyClocks.MyDataFile.ClockVMCollection.Model, "")
                {
                    ShowActive = true,
                    ShowInactive = true,
                    ShowTimers = false,
                    ShowAlarms = true
                },
                DisplayString = "Alarms",
                MyConstantImageSource = (DrawingImage)App.Current.MainWindow.FindResource("alarmClockDrawingImage")
            });
        }

        private void GroupsVMs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (SynchDisabled2)
            {
                return;
            }

            SynchDisabled2 = true;

            Utils.SynchronizeCollectionChange(MyClocks.GroupsVM.VMs, e, MyModel.Ms,
                afterAddition: null,
                prepareDeletion: null,
                equalsWithinTargetTo: new Func<ClockGroupVM, FilterM, bool>
                    (ClockGroupVMEqualsWithinTargetToFilterM),
                toTarget: new Func<ClockGroupVM, FilterM>(ClockGroupVMToFilterM),
                startingIndexInSource: 0,
                startingIndexInTarget: 5);

            SynchDisabled2 = false;
        }

        public static bool ClockGroupVMEqualsWithinTargetToFilterM(ClockGroupVM vm, FilterM m)
        {
            return m.GroupNames.Count > 0 && m.ShowsGroup(vm.Name);
        }

        public FilterM ClockGroupVMToFilterM(ClockGroupVM vm)
        {
            var f = new FilterM(MyClocks.MyDataFile.ClockVMCollection.Model,
                $"{MyClocks.GroupsVM.VMs.IndexOf(vm) + 1}")
            {
                ShowActive = true,
                ShowInactive = true,
                ShowAlarms = true,
                ShowTimers = true
            };

            return f;
        }

        private void VMs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (SynchDisabled3)
            {
                return;
            }

            SynchDisabled3 = true;

            Utils.SynchronizeCollectionChange(VMs, e, MyModel.Ms,
                afterAddition: null,
                prepareDeletion: null,
                equalsWithinTargetTo: new Func<FilterVM, FilterM, bool>((FilterVM vm, FilterM m) =>
                {
                    return vm.MyFilter == m;
                }),
                toTarget: new Func<FilterVM, FilterM>((FilterVM vm) =>
                {
                    return vm.MyFilter;
                }));

            SynchDisabled3 = false;
        }

        public bool SynchDisabled2 = false,
            SynchDisabled3 = false;

        private void MyModel_Ms_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!SynchDisabled3)
            {
                SynchDisabled3 = true;

                Utils.SynchronizeCollectionChange(MyModel.Ms, e, VMs,
                    afterAddition: null,
                    prepareDeletion: null,
                    equalsWithinTargetTo: new Func<FilterM, FilterVM, bool>((FilterM m, FilterVM vm) =>
                    {
                        return vm.MyFilter == m;
                    }),
                    toTarget: new Func<FilterM, FilterVM>((FilterM m) =>
                    {
                        return new FilterVM()
                        {
                            DisplayString = m.GroupNames[0], // TODO: what if there are more groups in the filter?
                            MyFilter = m,
                            MyEmptyImageSource = new BitmapImage(
                                new Uri("/Resources/pictograma folder 2.png", UriKind.Relative)),
                            MyNonEmptyImageSource = new BitmapImage(
                                new Uri("/Resources/pictograma folder cu continut.png", UriKind.Relative))
                        };
                    }));

                SynchDisabled3 = false;
            }

            if (!SynchDisabled2)
            {
                SynchDisabled2 = true;

                Utils.SynchronizeCollectionChange(MyModel.Ms, e, MyClocks.GroupsVM.VMs,
                    afterAddition: null,
                    prepareDeletion: null,
                    equalsWithinTargetTo: new Func<FilterM, ClockGroupVM, bool>((FilterM fm, ClockGroupVM gm) =>
                    {
                        return fm.SearchString == gm.Name;

                    }),
                    toTarget: new Func<FilterM, ClockGroupVM>((FilterM m) =>
                    {
                        if (m.GroupNames.Count == 0)
                        {
                            return null;
                        }

                        //return new FilterVM()
                        //{
                        // //DisplayString = m.
                        // MyFilter = m
                        //};
                        var gm = new ClockGroupVM()
                        {
                            DisplayString = m.GroupNames[0],
                            Name = m.GroupNames[0]
                        };

                        //var f = new FilterM(MyClocks.MyDataFile.ClockVMCollection.Model,
                        //    $"{MyModel.Ms.IndexOf(m) + 1}")
                        //{
                        //    ShowActive = true,
                        //    ShowInactive = true,
                        //    ShowAlarms = true,
                        //    ShowTimers = true
                        //};

                        return gm;
                    }),
                    startingIndexInSource: 5,
                    startingIndexInTarget: 0);

                SynchDisabled2 = false;
            }
        }
    }
}
