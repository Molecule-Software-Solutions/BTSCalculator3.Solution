using System;
using System.Windows.Input;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Allows for a parameterized relay command to be executed
    /// </summary>
    public class ParamRelayCommand : ICommand
    {
        /// <summary>
        /// Fired when <see cref="CanExecuteCommand"/> property is changed 
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        /// <summary>
        /// Action to be executed 
        /// </summary>
        Action<object> RelayAction;

        /// <summary>
        /// Property that determines whether or not the command can be fired 
        /// </summary>
        bool CanExecuteCommand;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="action"></param>
        /// <param name="canExecute"></param>
        public ParamRelayCommand(Action<object> action, bool canExecute)
        {
            RelayAction = action;
            CanExecuteCommand = true; 
        }

        /// <summary>
        /// Returns the <see cref="CanExecuteCommand"/> property to the caller
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return CanExecuteCommand; 
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            RelayAction(parameter); 
        }
    }
}
