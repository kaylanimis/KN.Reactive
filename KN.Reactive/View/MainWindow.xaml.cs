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
                this.Bind(ViewModel, x => x.HelloText, x => x.HelloWorldLabel.Content),
                this.Bind(ViewModel, x => x.IncrementQuantity, x => x.IncrementButton.Command),
                this.Bind(ViewModel, x => x.Quantity, x => x.CounterTextBlock.Text),
                this.Bind(ViewModel, x => x.IsBusy, x => x.IsBusy.IsChecked)
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
