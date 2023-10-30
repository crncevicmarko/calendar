using Calendar.Model;
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
    class UserRepository : IUserRepository
    {
        public int Add(User user)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into Users(firstName, lastName, email, userName, password, isAdmin, isDeleted)
                output inserted.id
                values (@firstName, @lastName, @email, @userName, @password, @isAdmin, @isDeleted)";

                command.Parameters.Add(new SqlParameter("firstName", user.FirstName));
                command.Parameters.Add(new SqlParameter("lastName", user.LastName));
                command.Parameters.Add(new SqlParameter("email", user.Email));
                command.Parameters.Add(new SqlParameter("userName", user.UserName));
                command.Parameters.Add(new SqlParameter("password", user.Password));
                command.Parameters.Add(new SqlParameter("isAdmin", user.IsAdmin));
                command.Parameters.Add(new SqlParameter("isDeleted", user.IsDeleted));
                return (int)command.ExecuteScalar();
            }
        }

        public ObservableCollection<User> GetAll()
        {
            ObservableCollection<User> users = new ObservableCollection<User>();
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string command = "select * from Users";
                SqlDataAdapter adapter = new SqlDataAdapter(command, conn);

                DataSet ds = new DataSet();

                adapter.Fill(ds, "Users");
                foreach(DataRow row in ds.Tables["Users"].Rows)
                {
                    var user = new User
                    {
                        Id = (int)row["id"],
                        FirstName = (string)row["firstName"],
                        LastName = (string)row["lastName"],
                        Email = (string)row["email"],
                        UserName = (string)row["userName"],
                        Password = (string)row["password"],
                        IsAdmin = (bool)row["isAdmin"],
                        IsDeleted = (bool)row["isDeleted"],
                    };
                    users.Add(user);
                }
            }
            return users;
        }
        public User GetOneById(int? id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = "SELECT * FROM Users WHERE id = @Id";
                SqlCommand command = new SqlCommand(commandText, conn);
                command.Parameters.AddWithValue("@Id", id);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Users");
                if (ds.Tables["Users"].Rows.Count > 0)
                {
                    var row = ds.Tables["Users"].Rows[0];

                    var user = new User
                    {
                        Id = (int)row["id"],
                        FirstName = (string)row["firstName"],
                        LastName = (string)row["lastName"],
                        Email = (string)row["email"],
                        UserName = (string)row["userName"],
                        Password = (string)row["password"],
                        IsAdmin = (bool)row["isAdmin"],
                        IsDeleted = (bool)row["isDeleted"],
                    };
                    return user;
                }
            }

            return null;
        }


        public User GetUserByUserNameAndPassword(string userName, string password)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                string command = "SELECT * FROM Users WHERE userName = @UserName AND password = @Password";

                using (SqlCommand cmd = new SqlCommand(command, conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "Users");
                        if (ds.Tables["Users"].Rows.Count > 0)
                        {
                            var row = ds.Tables["Users"].Rows[0];

                            var user = new User
                            {
                                Id = (int)row["id"],
                                FirstName = (string)row["firstName"],
                                LastName = (string)row["lastName"],
                                Email = (string)row["email"],
                                UserName = (string)row["userName"],
                                Password = (string)row["password"],
                                IsAdmin = (bool)row["isAdmin"],
                                IsDeleted = (bool)row["isDeleted"],
                            };

                            return user;
                        }
                    }
                }
            }
            return null;
        }


        public void Update(int id, User user)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "update Users set firstName = @firstName, lastName = @lastName, email = @email, userName = @userName, password = @password where id = @id";

                command.Parameters.Add(new SqlParameter("firstName", user.FirstName));
                command.Parameters.Add(new SqlParameter("lastName", user.LastName));
                command.Parameters.Add(new SqlParameter("email", user.Email));
                command.Parameters.Add(new SqlParameter("userName", user.UserName));
                command.Parameters.Add(new SqlParameter("password", user.Password));
                command.Parameters.Add(new SqlParameter("id", id));
                command.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = "update Users set isDeleted = 1 where id=@id";

                command.Parameters.Add(new SqlParameter("id", id));
                command.ExecuteNonQuery();
            }
        }

    }
}
