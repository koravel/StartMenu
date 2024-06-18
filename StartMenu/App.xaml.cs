using Microsoft.Extensions.Configuration;
using StartMenu.Model;
using StartMenu.View;
using System.Globalization;
using System.IO;
using System.Windows;

namespace StartMenu
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly string configFileLocation = "appsettings.json";

        private ConfigurationLib.Configuration _configuration;
        private Database _database;

        public App()
        {
            /*_configuration = new ConfigurationLib.Configuration(builder => {
                builder.SetBasePath(Directory.GetCurrentDirectory());
                //builder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                builder.AddJsonFile(configFileLocation);
            });
            _database = new Database(_configuration);*/
        }

        
    }

}
