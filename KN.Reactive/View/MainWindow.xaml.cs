using System;
using System.Collections.Generic;
using System.Windows;
using KN.Reactive.ViewModel;
using ReactiveUI;

namespace KN.Reactive.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewFor<MainViewModel>
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = ViewModel = viewModel;
            this.WhenActivated(GenerateBindings);
        }

        private IEnumerable<IDisposable> GenerateBindings()
        {
            return new[]
            {
                this.Bind(ViewModel, vm => vm.HelloText, v => v.HelloWorldLabel.Content),
                this.Bind(ViewModel, vm => vm.IncrementQuantity, v => v.IncrementButton.Command),
                this.Bind(ViewModel, vm => vm.Quantity, v => v.CounterTextBlock.Text),
                this.Bind(ViewModel, vm => vm.IsBusy, v => v.IsBusy.IsChecked),
                (IDisposable) this.OneWayBind(ViewModel, vm => vm.Values, v => v.ListBox.ItemsSource)
            };
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MainViewModel) value; }
        }

        public MainViewModel ViewModel { get; set; }
    }
}
