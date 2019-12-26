using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTSCalculator.Core
{
    public class ApplicationViewmodel : BaseViewmodel
    {
        /// <summary>
        /// Indicates the current page that the application is displaying
        /// </summary>
        public ApplicationPageTypes CurrentPage { get; set; } = ApplicationPageTypes.MainMenu;
    }
}
