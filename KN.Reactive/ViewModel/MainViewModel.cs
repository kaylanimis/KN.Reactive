using System;
using System.Reactive.Subjects;
using System.Windows.Input;
using ReactiveUI;

namespace KN.Reactive.ViewModel
{
    public class MainViewModel : ReactiveObject
    {
        private int _currQty = 0;
        private readonly ReactiveCommand<object> _incrementQuantity;
        public MainViewModel()
        {
            var commandCanExecute = this.WhenAny(vm => vm.Quantity, qty => qty.Value < 15);
            _incrementQuantity = ReactiveCommand.Create(commandCanExecute);
            _incrementQuantity.Subscribe(_ => Quantity++);
        }

        public string HelloText => "Hello, World!";

        public ICommand IncrementQuantity => _incrementQuantity;

        public int Quantity
        {
            get { return _currQty; }
            set { this.RaiseAndSetIfChanged(ref _currQty, value); }
        }
    }
}