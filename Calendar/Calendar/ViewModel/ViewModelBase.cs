using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // odkomentarisati za logovanje, u jednom trenutku radi u jednom ne
        //public void Log(string userString)
        //{
        //    string logsPath = @"../../Resources/Logs.txt";
        //    using (StreamWriter writer = new StreamWriter(logsPath))
        //    {
        //        writer.WriteLine(userString);
        //    }
        //}
    }
}
