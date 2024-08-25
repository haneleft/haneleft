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
    public class GenreTable
    {
        public static String SQL_SELECT = "SELECT * FROM Genre";
        public static String SQL_SELECT_S = "SELECT (SELECT name FROM Genre WHERE genreId = SongsGenre.genreId) FROM SongsGenre WHERE songId = @songId";
        public static String SQL_INSERT = "INSERT INTO Genre (name)" +
                                          "VALUES (@name)";
        public static String SQL_INSERT_S = "INSERT INTO SongsGenre (songID, genreID)" +
                                          "VALUES (@songId, @genreId)";
        public static String SQL_DELETE = "DELETE FROM UserGenre WHERE genreID = @genreID; DELETE FROM SongsGenre WHERE genreID = @genreID; DELETE FROM Genre WHERE genreID = @genreID;";
        public static String SQL_UPDATE_S = "UPDATE SongsGenre SET genreID = @genreId WHERE songID = @songId";
        public static String SQL_UPDATE = "UPDATE Genre SET name = @name WHERE genreID = @genreID";
        public static int Insert(Genre genre, Database pDb = null) //6.1
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
            PrepareCommand(command, genre);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int InsertS(int songId, int genreId, Database pDb = null) //6.1
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

            SqlCommand command = db.CreateCommand(SQL_INSERT_S);
            command.Parameters.AddWithValue("@songId", songId);
            command.Parameters.AddWithValue("@genreId", genreId);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int UpdateS(int songId, int genreId, Database pDb = null) //6.1
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

            SqlCommand command = db.CreateCommand(SQL_UPDATE_S);
            command.Parameters.AddWithValue("@songId", songId);
            command.Parameters.AddWithValue("@genreId", genreId);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int Update(Genre genre, Database pDb = null) //6.2
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
            PrepareCommand(command, genre);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int Delete(int genreID, Database pDb = null) //6.3
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
            SqlCommand command = db.CreateCommand(SQL_DELETE);

            command.Parameters.AddWithValue("@genreID", genreID);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static Collection<Genre> Select(Database pDb = null) //6.4
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
            SqlCommand command = db.CreateCommand(SQL_SELECT);

            reader = db.Select(command);


            Collection<Genre> genres= Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return genres;
        }

        public static Collection<Genre> SelectBySong(int id, Database pDb = null) //6.4
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
            SqlCommand command = db.CreateCommand(SQL_SELECT_S);
            command.Parameters.AddWithValue("@songId", id);

            reader = db.Select(command);


            Collection<Genre> genres = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return genres;
        }

        private static Collection<Genre> Read(SqlDataReader reader)
        {
            Collection<Genre> genres = new Collection<Genre>();

            while (reader.Read())
            {
                int i = -1;
                Genre genre = new Genre();
                genre.IdGenre = reader.GetInt32(++i);
                genre.Name = reader.GetString(++i);


                genres.Add(genre);
            }

            return genres;
        }

        private static void PrepareCommand(SqlCommand command, Genre genre)
        {
            command.Parameters.AddWithValue("@genreID", genre.IdGenre);
            command.Parameters.AddWithValue("@name", genre.Name);
        }
    }
}
