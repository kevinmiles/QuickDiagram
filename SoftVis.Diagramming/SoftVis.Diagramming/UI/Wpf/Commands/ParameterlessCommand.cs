﻿using System;
using System.Windows.Input;

namespace Codartis.SoftVis.UI.Wpf.Commands
{
    /// <summary>
    /// A command that executes a delegate with no parameter.
    /// </summary>
    public class ParameterlessCommand : ICommand
    {
        private readonly Action _execute;
        private readonly object _lockObject = new object();

        public event EventHandler CanExecuteChanged;

        public ParameterlessCommand(Action execute)
        {
            _execute = execute;
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { lock (_lockObject) CanExecuteChanged += value; }
            remove { lock (_lockObject) CanExecuteChanged -= value; }
        }

        bool ICommand.CanExecute(object parameter) => true;
        void ICommand.Execute(object parameter) => _execute();

        public bool CanExecute() => ((ICommand)this).CanExecute(null);
        public void Execute() => ((ICommand)this).Execute(null);
    }

    /// <summary>
    /// A command that executes a delegate with one typed parameter.
    /// TODO: implement CanExecuteChanged raising
    /// </summary>
    public abstract class DelegateCommand<T> : ICommand
    {
        private readonly Func<T, bool> _canExecute;
        private readonly Action<T> _execute;
        private readonly object _lockObject = new object();

        public event EventHandler CanExecuteChanged;

        protected DelegateCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { lock (_lockObject) CanExecuteChanged += value; }
            remove { lock (_lockObject) CanExecuteChanged -= value; }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T) parameter);
        }

        void ICommand.Execute(object parameter)
        {
            _execute((T) parameter);
        }

        public bool CanExecute(T parameter) => ((ICommand)this).CanExecute(parameter);
        public void Execute(T parameter) => ((ICommand)this).Execute(parameter);
    }

    /// <summary>
    /// A command that executes a delegate with two typed parameters.
    /// TODO: implement CanExecuteChanged raising
    /// </summary>
    public abstract class DelegateCommand<T1, T2> : ICommand
    {
        private readonly Func<T1, T2, bool> _canExecute;
        private readonly Action<T1, T2> _execute;
        private readonly object _lockObject = new object();

        public event EventHandler CanExecuteChanged;

        protected DelegateCommand(Action<T1, T2> execute, Func<T1, T2, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { lock (_lockObject) CanExecuteChanged += value; }
            remove { lock (_lockObject) CanExecuteChanged -= value; }
        }

        bool ICommand.CanExecute(object parameter)
        {
            var tuple = ToTuple(parameter);
            return _canExecute == null || _canExecute(tuple.Item1, tuple.Item2);
        }

        void ICommand.Execute(object parameter)
        {
            var tuple = ToTuple(parameter);
            _execute(tuple.Item1, tuple.Item2);
        }

        protected bool CanExecuteCore(T1 param1, T2 param2) => ((ICommand)this).CanExecute(Tuple.Create(param1, param2));
        protected void ExecuteCore(T1 param1, T2 param2) => ((ICommand)this).Execute(Tuple.Create(param1, param2));

        private static Tuple<T1, T2> ToTuple(object parameter) => (Tuple<T1, T2>)parameter;
    }

    /// <summary>
    /// A command that executes a delegate with three typed parameters.
    /// TODO: implement CanExecuteChanged raising
    /// </summary>
    public abstract class DelegateCommand<T1, T2, T3> : ICommand
    {
        private readonly Func<T1, T2, T3, bool> _canExecute;
        private readonly Action<T1, T2, T3> _execute;
        private readonly object _lockObject = new object();

        public event EventHandler CanExecuteChanged;

        protected DelegateCommand(Action<T1, T2, T3> execute, Func<T1, T2, T3, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { lock (_lockObject) CanExecuteChanged += value; }
            remove { lock (_lockObject) CanExecuteChanged -= value; }
        }

        bool ICommand.CanExecute(object parameter)
        {
            var tuple = ToTuple(parameter);
            return _canExecute == null || _canExecute(tuple.Item1, tuple.Item2, tuple.Item3);
        }

        void ICommand.Execute(object parameter)
        {
            var tuple = ToTuple(parameter);
            _execute(tuple.Item1, tuple.Item2, tuple.Item3);
        }

        protected bool CanExecuteCore(T1 param1, T2 param2, T3 param3) => ((ICommand)this).CanExecute(Tuple.Create(param1, param2, param3));
        protected void ExecuteCore(T1 param1, T2 param2, T3 param3) => ((ICommand)this).Execute(Tuple.Create(param1, param2, param3));

        private static Tuple<T1, T2, T3> ToTuple(object parameter) => (Tuple<T1, T2, T3>)parameter;
    }
}
