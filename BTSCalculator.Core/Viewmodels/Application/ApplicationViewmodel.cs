namespace BTSCalculator.Core
{
    /// <summary>
    /// Viewmodel that serves as the background layer for the application, controlling much of the navigation and base properties that will be
    /// used throughout
    /// </summary>
    public class ApplicationViewmodel : BaseViewmodel
    {
        #region Constructor 

        /// <summary>
        /// Default constructor 
        /// </summary>
        public ApplicationViewmodel()
        {
            if(TestConnectionState())
            {
                Setup();
            }
        }

        #endregion

        #region Properties 

        /// <summary>
        /// Indicates the current page that the application is displaying
        /// </summary>
        public ApplicationPageTypes CurrentPage { get; set; } = ApplicationPageTypes.MainMenu;

        /// <summary>
        /// Property that indicates the current dialog that is being requested for viewing
        /// </summary>
        public IDialog CurrentDialog { get; set; }

        /// <summary>
        /// Property that contains the current county that is pulled from the system database 
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// Property that contains the current default court costs that is pulled from the system database 
        /// </summary>
        public decimal DefaultCosts { get; set; }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method that performs setup operations for the database and populates some of the properties necessary for operation of the application 
        /// </summary>
        private void Setup()
        {
            DatabaseMigrationSystem.CheckMigrationStatus();
            ExclusionDateCleanup.PerformCleanup(); 
            County = CountyNameManagement.GetCountyName();
            DefaultCosts = CostsManager.GetCosts(); 
        }

        #endregion

        #region Public Methods 

        /// <summary>
        /// Method that generates a new dialog for viewing
        /// </summary>
        /// <param name="dialog">Accepts any model that inherits <see cref="IDialog"/> such as <see cref="DialogModel"/></param>
        public void ShowDialog(IDialog dialog)
        {
            CurrentDialog = dialog;
        }

        #endregion

        #region Private Methods 

        /// <summary>
        /// Tests the connection state of the database
        /// </summary>
        /// <returns><see cref="bool"/> representing connected or failed respectively</returns>
        private bool TestConnectionState()
        {
            switch (ApplicationConnectionStringSystem.TextConnection())
            {
                case System.Data.ConnectionState.Closed:
                    return false;
                case System.Data.ConnectionState.Open:
                    return true;
                case System.Data.ConnectionState.Connecting:
                    return false;
                case System.Data.ConnectionState.Executing:
                    return false;
                case System.Data.ConnectionState.Fetching:
                    return false;
                case System.Data.ConnectionState.Broken:
                    return false;
                default:
                    return false;
            }
        }

        #endregion

        #region Commands 

        /// <summary>
        /// Command that requests that the current dialog be closed by changing the current <see cref="IDialog"/> <see cref="DialogTypes"/> to "None"
        /// </summary>
        public RelayCommand CloseDialog_COMMAND => new RelayCommand(() =>
        {
            IDialog currentDialog = StaticAccessSystem.ApplicationVM.CurrentDialog;
            currentDialog.DialogType = DialogTypes.None;
            CurrentDialog.DialogYes = true;
            // Forces property changed on current dialog
            CurrentDialog = null;
            // Populates current dialog with result of last dialog so that properties can be accessed
            CurrentDialog = currentDialog;
        });

        #endregion 
    }
}
