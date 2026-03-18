namespace sport
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool exitProgram = false;

            while (!exitProgram)
            {
                using (var formLogin = new Form1())
                {
                    if (formLogin.ShowDialog() == DialogResult.OK)
                    {
                        using (var formProducts = new Form2(
                        formLogin.CurrentUser,
                        formLogin.IsGuest))
                        {
                            if (formProducts.ShowDialog() == DialogResult.Cancel)
                            {
                                continue;
                            }   
                            else
                            {
                                exitProgram = true;
                            }
                        }
                    }
                    else
                    {
                        exitProgram = true;
                    }
                }
            }
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}