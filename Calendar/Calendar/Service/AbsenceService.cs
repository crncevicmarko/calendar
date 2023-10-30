using Calendar.Model;
using Calendar.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Service
{
    public class AbsenceService : IAbsenceService
    {
        private IAbsenceRepository absenceRepository;

        public AbsenceService()
        {
            absenceRepository = new AbsenceRepository();
        }

        public ObservableCollection<Absence> GetAll(bool isAdmin)
        {
            return absenceRepository.GetAll(isAdmin);
        }
        
        public ObservableCollection<Absence> GetAllForDate(DateTime date)
        {
            return absenceRepository.GetAllForDate(date);
        }

        public ObservableCollection<Absence> GetAllByUserIdAndDate(int id, DateTime date)
        {
            return absenceRepository.GetAllByUserIdAndDate(id, date);
        }

        public int Add(Absence absence)
        {
            return absenceRepository.Add(absence);
        }

        public void Update(int id, Absence absence)
        {
            absenceRepository.Update(id, absence);
        }
    }
}
