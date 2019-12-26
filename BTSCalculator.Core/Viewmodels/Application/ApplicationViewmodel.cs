using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTSCalculator.Core
{
    public class ApplicationViewmodel : BaseViewmodel
    {
        public ApplicationViewmodel()
        {
            if(TestConnectionState())
            {
                DatabaseConstructor DC = new DatabaseConstructor();
                DC.ConstructTables(new ApplicationConnectionStringSystem().ConnectionString);
            }
        }

        /// <summary>
        /// Indicates the current page that the application is displaying
        /// </summary>
        public ApplicationPageTypes CurrentPage { get; set; } = ApplicationPageTypes.MainMenu;

        private bool TestConnectionState()
        {
            ApplicationConnectionStringSystem css = new ApplicationConnectionStringSystem();
            switch (css.TextConnection())
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
    }
}
