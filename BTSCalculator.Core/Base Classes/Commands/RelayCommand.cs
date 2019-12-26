using System;
using System.Windows.Input;

namespace BTSCalculator.Core
{
    public class RelayCommand : ICommand
    {
        #region Private Members 

        /// <summary>
        /// Private backing field for <see cref="Action"/> to be executed 
        /// </summary>
        private Action Action;

        /// <summary>
        /// Private backing field for <see cref="CanExecute(object)"/>
        /// </summary>
        private bool CanExecuteAction;

        #endregion

        #region Constructors 

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="action">Action to be executed. CanExecute is always true</param>
        public RelayCommand(Action action)
        {
            Action = action;
            CanExecuteAction = true;
        }

        /// <summary>
        /// Secondary Constructor
        /// NOTE: CanExecute determines whether the <see cref="Action"/> can execute 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="canExecute"></param>
        public RelayCommand(Action action, bool canExecute)
        {
            Action = action;
            CanExecuteAction = canExecute;
        }

        #endregion

        #region Public Methods 

        /// <summary>
        /// Returns a <see cref="bool"/> that determines whether or not the <see cref="Action"/> can execute 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return CanExecuteAction;
        }

        /// <summary>
        /// Executes the <see cref="Action"/>
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            Action();
        }

        #endregion

        #region Events 

        /// <summary>
        /// Event fired when <see cref="CanExecuteAction"/> is changed 
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion 
    }
}
