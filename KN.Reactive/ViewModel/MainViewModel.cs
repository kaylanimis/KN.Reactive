using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;

namespace KN.Reactive.ViewModel
{
    public class MainViewModel : ReactiveObject
    {
        private int _currQty = 0;
        private readonly ReactiveList<WrapperInt> _values = new ReactiveList<WrapperInt>();
        private readonly ReactiveCommand<IEnumerable<int>> _incrementQuantity;
        private readonly ObservableAsPropertyHelper<bool> _isBusy;
        public MainViewModel()
        {
            var commandCanExecute = this.WhenAny(vm => vm.Quantity, qty => qty.Value < 15);
            _incrementQuantity = ReactiveCommand.CreateAsyncTask(commandCanExecute, async _ =>
            {
                await Task.Delay(TimeSpan.FromSeconds(4));
                return Enumerable.Range(0, 40);
            });
            _isBusy = _incrementQuantity.IsExecuting.ToProperty(this, vm => vm.IsBusy);
            _incrementQuantity.Subscribe(output =>
            {
                Quantity++;
                _values.Clear();
                foreach (var i in output.Select(v => new WrapperInt(v)))
                {
                    _values.Add(i);
                }
            });
        }

        public string HelloText => "Hello, World!";

        public ICommand IncrementQuantity => _incrementQuantity;

        public int Quantity
        {
            get { return _currQty; }
            set { this.RaiseAndSetIfChanged(ref _currQty, value); }
        }

        public ReactiveList<WrapperInt> Values => _values;

        public bool IsBusy
        {
            get { return _isBusy.Value; }
        }

        public class WrapperInt
        {
            private readonly int _val;

            public WrapperInt(int value)
            {
                _val = value;
            }

            public int Value => _val;
        }
    }
}