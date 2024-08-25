using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Projekt_HAN0334.ORM.DAO
{
    public class UserTable
    {
        public static String SQL_SELECT_ADMIN = "SELECT * FROM [User]";
        public static String SQL_SELECT = "SELECT * FROM [User] WHERE role LIKE \'Interpret\'";
        public static String SQL_SELECT_DETAIL = "SELECT * FROM [User] WHERE userID = @userID";
        public static String SQL_INSERT = "INSERT INTO [User] (first_name, last_name, email, address, role, password, telephone, artistN, description, label)" +
                                          "VALUES (@first_name, @last_name, @email, @address, @role, @password, @telephone, @artistN, @description, @label)";
        public static String SQL_INSERT_G = "INSERT INTO UserGenre (genreID, userID)" +
                                          "VALUES (@genreID, @userID)";
        public static String SQL_DELETE_P = "DELETE FROM Payment WHERE userID = @userID";
        public static String SQL_DELETE_C = "DELETE FROM Comments WHERE userID = @userID";
        public static String SQL_DELETE_CO = "DELETE FROM Collab WHERE userID = @userID";
        public static String SQL_DELETE_G = "DELETE FROM UserGenre WHERE userID = @userID";
        public static String SQL_DELETE = "DELETE FROM [User] WHERE userID = @userID";
        public static String SQL_UPDATE_S = "UPDATE [User] SET status = @status WHERE userID = @userID";
        public static String SQL_UPDATE = "UPDATE [User] SET first_name = @first_name, last_name = @last_name, email = @email, address = @address, role = @role, password = @password, telephone = @telephone, artistN = @artistN, description = @description, label = @label, status = @status WHERE userID = @userID";
        public static int Insert(User user, Database pDb = null) //1.1
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, user);
            int ret = db.ExecuteNonQuery(command);
            if(user.Genres != null)
            {
                foreach (Genre g in user.Genres)
                {
                    SqlCommand command1 = db.CreateCommand(SQL_INSERT_G);
                    command.Parameters.AddWithValue("@userID", user.IdUser);
                    command.Parameters.AddWithValue("@genreID", g.IdGenre);

                    ret = db.ExecuteNonQuery(command1);
                }
            }

            

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static Collection<User> Select(string role, Database pDb = null) //1.2 , role se musi rovnat "Admin" pokud chceme zobrazit vsechny vysledky selectu
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlDataReader reader;
            SqlCommand command;

            if (role == "Admin")
            {
                command = db.CreateCommand(SQL_SELECT_ADMIN);

            }
            else
            {
                command = db.CreateCommand(SQL_SELECT);
            }


            reader = db.Select(command);


            Collection<User> users = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return users;
        }

        public static int Update(User user, Database pDb = null) //1.3
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, user);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int Delete(int userID, Database pDb = null) //1.4
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }


            foreach (Album a in AlbumTable.SelectByUser(userID, db))
            {
                AlbumTable.Delete(a.IdAlbum, db);
            }

            foreach (Playlist pl in PlaylistTable.Select(userID, db))
            {
                PlaylistTable.Delete(pl.IdPlaylist, db);
            }

            SqlCommand command5 = db.CreateCommand(SQL_DELETE_G);
            command5.Parameters.AddWithValue("@userID", userID);
            int ret = db.ExecuteNonQuery(command5);

            SqlCommand command4 = db.CreateCommand(SQL_DELETE_CO);
            command4.Parameters.AddWithValue("@userID", userID);
            ret = db.ExecuteNonQuery(command4);

            SqlCommand command = db.CreateCommand(SQL_DELETE_P);
            command.Parameters.AddWithValue("@userID", userID);
            ret = db.ExecuteNonQuery(command);


            SqlCommand command2 = db.CreateCommand(SQL_DELETE_C);
            command2.Parameters.AddWithValue("@userID", userID);
            ret = db.ExecuteNonQuery(command2);

            SqlCommand command3 = db.CreateCommand(SQL_DELETE);
            command3.Parameters.AddWithValue("@userID", userID);
            ret = db.ExecuteNonQuery(command3);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int Read()
        {
            return 0;
        }

        
        public static int AutoStateChange(Database pDb = null) //1.5
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand("TAutoZmenaStavuUzivatele");
            command.CommandType = CommandType.StoredProcedure;

            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static Collection<User> SelectDetail(int id, Database pDb = null) //1.6
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlDataReader reader;
            SqlCommand command;

            command = db.CreateCommand(SQL_SELECT_DETAIL);
            command.Parameters.AddWithValue("@userID", id);


            reader = db.Select(command);


            Collection<User> users = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return users;
        }

        public static int StateChange(User user,Database pDb = null)// 1.7
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }
            SqlCommand command;
            int ret = 0;

            if(user.Status.Equals( "Active" )|| user.Status.Equals( "Inactive" )|| user.Status.Equals( "Blocked"))
            {
                command = db.CreateCommand(SQL_UPDATE_S);
                PrepareCommand(command, user);
                ret = db.ExecuteNonQuery(command);
            }
            else
            {
                Console.WriteLine("Wrong status inserted");
            }
            

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        private static Collection<User> Read(SqlDataReader reader)
        {
            Collection<User> users = new Collection<User>();

            while (reader.Read())
            {
                int i = -1;
                User user = new User();
                user.IdUser = reader.GetInt32(++i);
                user.FirstName = reader.GetString(++i);
                user.LastName = reader.GetString(++i);
                user.Email = reader.GetString(++i);
                user.Address = reader.GetString(++i);
                user.Role = reader.GetString(++i);
                user.Password = reader.GetString(++i);
                if (!reader.IsDBNull(++i))
                {
                    user.Telephone = reader.GetString(i);
                }
                if (!reader.IsDBNull(++i))
                {
                    user.ArtistName = reader.GetString(i);
                }
                if (!reader.IsDBNull(++i))
                {
                    user.Description = reader.GetString(i);
                }
                if (!reader.IsDBNull(++i))
                {
                    user.Label = reader.GetString(i);
                }
                user.Status = reader.GetString(++i);

                users.Add(user);
            }
            return users;
        }

        private static void PrepareCommand(SqlCommand command, User user)
        {
            command.Parameters.AddWithValue("@userID", user.IdUser);
            command.Parameters.AddWithValue("@first_name", user.FirstName);
            command.Parameters.AddWithValue("@last_name", user.LastName);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@address", user.Address);
            command.Parameters.AddWithValue("@role", user.Role);
            command.Parameters.AddWithValue("@password", user.Password);
            if(user.Telephone != null)
            {
                command.Parameters.AddWithValue("@telephone", user.Telephone);
            }
            else
            {
                command.Parameters.AddWithValue("@telephone", DBNull.Value);
            }
            if(user.ArtistName != null)
            {
                command.Parameters.AddWithValue("@artistN", user.ArtistName);
            }
            else
            {
                command.Parameters.AddWithValue("@artistN", DBNull.Value);
            }
            if(user.Description != null)
            {
                command.Parameters.AddWithValue("@description", user.Description);
            }
            else
            {
                command.Parameters.AddWithValue("@description", DBNull.Value);
            }
            if(user.Label != null)
            {
                command.Parameters.AddWithValue("@label", user.Label);
            }
            else
            {
                command.Parameters.AddWithValue("@label", DBNull.Value);
            }

            command.Parameters.AddWithValue("@status", user.Status);

        }

    }
}
