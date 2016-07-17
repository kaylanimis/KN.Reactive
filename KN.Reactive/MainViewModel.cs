using System;
using System.Reactive.Subjects;
using System.Windows.Input;
using ReactiveUI;

namespace KN.Reactive
{
    public class MainViewModel : ReactiveObject
    {
        private int _currQty = 0;
        private readonly IObserver<int> _qty = new Subject<int>();
        private readonly ReactiveCommand _command;
        public MainViewModel()
        {
            var commandCanExecute = this.WhenAny(x => x.Quantity, x => x.Value < 15);
            _command = new ReactiveCommand(commandCanExecute);
            _command.Subscribe(x => _qty.OnNext(Quantity++));
        }

        public string HelloText => "Hello, World!";

        public ICommand Command => _command;

        public int Quantity
        {
            get { return _currQty; }
            set { this.RaiseAndSetIfChanged(ref _currQty, value); }
        }
    }
}