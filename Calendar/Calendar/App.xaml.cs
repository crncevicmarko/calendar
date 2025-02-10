using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Calendar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("test.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.CloseAndFlush(); // Ensure all logs are written before the app closes
            base.OnExit(e);
        }
    }
}
