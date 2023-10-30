using Calendar.Model;
using Calendar.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Repository
{
    public class AbsenceRepository : IAbsenceRepository
    {
        private IUserService userService = new UserService();

        public int Add(Absence absence)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into Absences(userId, reason, startOfTheEvent, endOfTheEvent, isApproved, isDeleted)
                output inserted.id
                values (@userId, @reason, @startOfTheEvent, @endOfTheEvent, @isApproved, @isDeleted)";

                command.Parameters.Add(new SqlParameter("userId", absence.User.Id));
                command.Parameters.Add(new SqlParameter("reason", absence.Event.ToString()));
                command.Parameters.Add(new SqlParameter("startOfTheEvent", absence.StartOfTheEvent));
                command.Parameters.Add(new SqlParameter("endOfTheEvent", absence.EndOfTheEvent));
                command.Parameters.Add(new SqlParameter("isApproved", absence.IsApproved));
                command.Parameters.Add(new SqlParameter("isDeleted", absence.IsDeleted));
                return (int)command.ExecuteScalar();
            }
        }

        public ObservableCollection<Absence> GetAllForDate(DateTime date)
        {
            ObservableCollection<Absence> absences = new ObservableCollection<Absence>();
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                string query = "SELECT * FROM Absences where isDeleted = 0 and @date BETWEEN startOfTheEvent AND endOfTheEvent";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@date", date);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Absence absence = new Absence
                            {
                                Id = (int)reader["id"],
                                UserId = reader["userId"] is DBNull ? (int?)null : (int)reader["userId"],
                                Event = (ETypeOfEvent)Enum.Parse(typeof(ETypeOfEvent), (string)reader["reason"]),
                                StartOfTheEvent = (DateTime)reader["startOfTheEvent"],
                                EndOfTheEvent = (DateTime)reader["endOfTheEvent"],
                                IsApproved = (bool)reader["isApproved"],
                                IsDeleted = (bool)reader["isDeleted"]
                            };
                            absence.User = userService.GetOneById(absence.UserId);
                            absences.Add(absence);
                        }
                    }
                }
            }
            return absences;
        }

        public ObservableCollection<Absence> GetAll(bool isAdmin)
        {
            string query = "";
            ObservableCollection<Absence> absences = new ObservableCollection<Absence>();
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                if (isAdmin)
                {
                    query = "SELECT * FROM Absences where isApproved = 0 and isDeleted = 0";
                }
                else
                {
                    query = "SELECT * FROM Absences where isApproved = 0";
                }
                using (SqlCommand cmd = new SqlCommand(query, conn))
                { 
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Absence absence = new Absence
                            {
                                Id = (int)reader["id"],
                                UserId = reader["userId"] is DBNull ? (int?)null : (int)reader["userId"],
                                Event = (ETypeOfEvent)Enum.Parse(typeof(ETypeOfEvent), (string)reader["reason"]),
                                StartOfTheEvent = (DateTime)reader["startOfTheEvent"],
                                EndOfTheEvent = (DateTime)reader["endOfTheEvent"],
                                IsApproved = (bool)reader["isApproved"],
                                IsDeleted = (bool)reader["isDeleted"]
                            };
                            absence.User = userService.GetOneById(absence.UserId);
                            absences.Add(absence);
                        }
                    }
                }
            }
            return absences;
        }

        public ObservableCollection<Absence> GetAllByUserIdAndDate(int id, DateTime date)
        {
            ObservableCollection<Absence> absences = new ObservableCollection<Absence>();
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                string query = "SELECT * FROM Absences WHERE userId = @id AND isApproved = 1 AND @date BETWEEN startOfTheEvent AND endOfTheEvent";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@date", date);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Absence absence = new Absence
                            {
                                Id = (int)reader["id"],
                                UserId = reader["userId"] is DBNull ? (int?)null : (int)reader["userId"],
                                Event = (ETypeOfEvent)Enum.Parse(typeof(ETypeOfEvent), (string)reader["reason"]),
                                StartOfTheEvent = (DateTime)reader["startOfTheEvent"],
                                EndOfTheEvent = (DateTime)reader["endOfTheEvent"],
                                IsApproved = (bool)reader["isApproved"],
                                IsDeleted = (bool)reader["isDeleted"]
                            };
                            absence.User = userService.GetOneById(absence.UserId);
                            absences.Add(absence);
                        }
                    }
                }
            }
            return absences;
        }

        public void Update(int id, Absence absence)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "update Absences set isApproved = @isApproved, isDeleted = @isDeleted where id = @id";

                command.Parameters.Add(new SqlParameter("isApproved", (object)absence.IsApproved ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("isDeleted", (object)absence.IsDeleted ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("id", id));

                command.ExecuteNonQuery();
            }
        }
    }
}
