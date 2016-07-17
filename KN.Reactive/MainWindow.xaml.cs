using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReactiveUI;

namespace KN.Reactive
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
                this.Bind(ViewModel, x => x.Command, x => x.IncrementButton.Command),
                this.Bind(ViewModel, x => x.Quantity, x => x.CounterTextBlock.Text)
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
