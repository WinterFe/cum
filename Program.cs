using System;
using System.Security.Principal;
using System.Configuration;
using System.Collections.Specialized;

namespace cum
{
    class Program
    {
        static void Main(string[] args)
        {
            // store setup flag
            string setup;
            // Store boolean flag
            bool isAdmin;

            setup = ConfigurationManager.AppSettings.Get("setup");

            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);

                // If is administrator, the variable updates from False to True
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            // Check with a simple condition whether you are admin or not
            if (setup == "false")
            {
                if (isAdmin)
                {
                    var name = "PATH";
                    var scope = EnvironmentVariableTarget.Machine; // or User
                    var oldValue = Environment.GetEnvironmentVariable(name, scope);
                    var newValue = oldValue + @$";C:\Program Files (x86)\AceFifi\Cum";
                    Environment.SetEnvironmentVariable(name, newValue, scope);

                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings["setup"].Value = "true";
                    config.Save(ConfigurationSaveMode.Full, true);
                    ConfigurationManager.RefreshSection("appSettings");

                    Console.WriteLine("Finished cum setup, close this and run it again for the time of your life :)");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Run this shit as admin. Why? Because, I wanna add this to your Path environment variable, so u can run this shit in command prompt by typing \"cum\"");
                    Console.ReadKey();
                }
            } else
            {
                string text = System.IO.File.ReadAllText(@"C:\Program Files (x86)\AceFifi\Cum\assets\cum.txt");
                foreach (string line in System.IO.File.ReadLines(@"C:\Program Files (x86)\AceFifi\Cum\assets\cum.txt"))
                {
                    System.Console.WriteLine(line);
                    System.Threading.Thread.Sleep(550);
                }
                Console.ReadKey();
            }        
        }
    }
}
