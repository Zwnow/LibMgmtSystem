using System.Configuration;
using System.Collections.Specialized;
using System.Data.SQLite;
using System.Runtime.CompilerServices;

namespace Library
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Get Data from App.config
            var key_user = ConfigurationManager.AppSettings["KeyUser"];
            var path = ConfigurationManager.AppSettings["dbPath"];
            DBHandler db_ = new DBHandler();
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            

            //Check if Database Exists, if not, creates it.
            if (!Directory.EnumerateFileSystemEntries(path).Any())
            {   
                config.AppSettings.Settings.Remove("data_db_path");
                config.AppSettings.Settings.Add("data_db_path", $"{path}\\data.db");
                config.Save(ConfigurationSaveMode.Full, true);
                ConfigurationManager.RefreshSection("appSettings");

                DBHandler.CreateDatabase("data", path);
                DBHandler.Setup(path);

                ApplicationConfiguration.Initialize();
                Loadup_Form loadup = new Loadup_Form();
                Application.Run(new MultiFormContext(loadup, new Config_Form()));
            }
            else
            {
                ApplicationConfiguration.Initialize();
                Application.Run(new Loadup_Form());
            }


        }  
    }
}