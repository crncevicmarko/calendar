using Calendar.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {

        public ObservableCollection<Appointment> GetAllForUserByDate(int id, DateTime date)
        {
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                string query = "SELECT * FROM Appointments WHERE userId = @id AND isDeleted = 0 AND creationDate = @date";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@date", date);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Appointment appointment = new Appointment
                            {
                                Id = (int)reader["id"],
                                UserId = reader["userId"] is DBNull ? (int?)null : (int)reader["userId"],
                                Title = (string)reader["title"],
                                Date = (DateTime)reader["creationDate"],
                                StartOfTheAppointment = (TimeSpan)reader["startOfTheAppointment"],
                                EndOfTheAppointment = (TimeSpan)reader["endOfTheAppointment"],
                                IsDeleted = (bool)reader["isDeleted"]
                            };
                            appointments.Add(appointment);
                        }
                    }
                }
            }
            return appointments;
        }
        public int Add(Appointment appointment)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into Appointments(userId, title, creationDate, startOfTheAppointment, endOfTheAppointment, isDeleted)
                output inserted.id
                values (@userId, @title, @creationDate, @startOfTheAppointment, @endOfTheAppointment, @isDeleted)";

                command.Parameters.Add(new SqlParameter("userId", appointment.User.Id));
                command.Parameters.Add(new SqlParameter("title", appointment.Title));
                command.Parameters.Add(new SqlParameter("creationDate", appointment.Date));
                command.Parameters.Add(new SqlParameter("startOfTheAppointment", appointment.StartOfTheAppointment));
                command.Parameters.Add(new SqlParameter("endOfTheAppointment", appointment.EndOfTheAppointment));
                command.Parameters.Add(new SqlParameter("isDeleted", appointment.IsDeleted));
                return (int)command.ExecuteScalar();
            }
        }
    }
}
