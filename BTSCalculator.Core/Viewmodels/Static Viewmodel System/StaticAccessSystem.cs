namespace BTSCalculator.Core
{
    public class StaticAccessSystem
    {
        /// <summary>
        /// Static instance of the Application Viewmodel which is accessible to any core class during the lifetime of the application 
        /// </summary>
        public static ApplicationViewmodel ApplicationVM = new ApplicationViewmodel();

        public static FormGeneratorViewmodel FormGeneratorVM = new FormGeneratorViewmodel();

        public static void ClearFormSearchVM()
        {
            FormGeneratorVM = new FormGeneratorViewmodel(); 
        }
    }
}
