using Calendar.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Service
{
    interface IAbsenceService
    {
        ObservableCollection<Absence> GetAll(bool isAdmin);
        ObservableCollection<Absence> GetAllForDate(DateTime date);
        ObservableCollection<Absence> GetAllByUserIdAndDate(int id, DateTime date);
        int Add(Absence absence);
        void Update(int id, Absence absence);
    }
}
