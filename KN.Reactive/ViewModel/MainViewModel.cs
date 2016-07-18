﻿using System;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;

namespace KN.Reactive.ViewModel
{
    public class MainViewModel : ReactiveObject
    {
        private int _currQty = 0;
        private readonly ReactiveCommand<Unit> _incrementQuantity;
        public MainViewModel()
        {
            var commandCanExecute = this.WhenAny(vm => vm.Quantity, qty => qty.Value < 15);
            _incrementQuantity = ReactiveCommand.CreateAsyncTask(commandCanExecute, async _ =>
            {
                await Task.Delay(TimeSpan.FromSeconds(4));
            });
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