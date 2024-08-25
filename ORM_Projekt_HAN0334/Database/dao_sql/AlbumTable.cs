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
    public class AlbumTable
    {
        public static String SQL_SELECT_ART = "SELECT a.albumID, a.name, a.dateA, (SELECT artistN FROM [User] WHERE userID = " +
        "a.userID) as art ,AVG(c.STARS) as stars " +
        "FROM Album a " +
        "LEFT JOIN Comments c on c.albumID = a.albumID WHERE (SELECT userID FROM [User] WHERE userID = a.userID) = @userID " +
        "GROUP BY a.albumID, a.name, a.dateA, a.userID ";
        public static String SQL_SELECT_DETAIL = "SELECT a.albumID, a.name, a.dateA, (SELECT artistN FROM [User] WHERE userID = " +
        "a.userID) as art ,AVG(c.STARS) as stars " +
        "FROM Album a " +
        "LEFT JOIN Comments c on c.albumID = a.albumID WHERE a.albumID = @albumID " +
        "GROUP BY a.albumID, a.name, a.dateA, a.userID ";
        public static String SQL_INSERT = "INSERT INTO Album (name, dateA, userID)" +
                                          "VALUES (@name, GETDATE(), @userID)";
        public static String SQL_DELETE = "DELETE FROM Album WHERE albumID = @albumID";
        public static String SQL_UPDATE = "UPDATE Album SET name = @name, dateA = GETDATE(), userID = @userID WHERE albumID = @albumID";
        public static int Insert(Album album, Database pDb = null) //4.1
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
            PrepareCommand(command, album);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int Update(Album album, Database pDb = null) //4.2
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
            PrepareCommand(command, album);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int Delete(int albumID, Database pDb = null) //4.3
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

            foreach (Comments c in CommentsTable.Select(albumID, "Album", db))
            {
                CommentsTable.Delete(c.IdAlbum.Value, db);
            }

            foreach (Song s in SongTable.SelectByAlbum(albumID, db))
            {
                SongTable.Delete(s.IdSong, db);
            }

            SqlCommand command = db.CreateCommand(SQL_DELETE);

            command.Parameters.AddWithValue("@albumID", albumID);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static Collection<Album> Select(string name, Database pDb = null) //4.4
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

            SqlCommand command = db.CreateCommand("PSeznamAlb");
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter input = new SqlParameter();
            input.ParameterName = "@name";
            input.DbType = DbType.String;
            input.Value = name;
            input.Direction = ParameterDirection.Input;
            command.Parameters.Add(input);

            SqlDataReader reader = db.Select(command);

            Collection<Album> albums = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return albums;
        }

        public static Collection<Album> SelectDetail(int id,Database pDb = null) //4.5
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
            SqlCommand command = db.CreateCommand(SQL_SELECT_DETAIL);

            command.Parameters.AddWithValue("@albumID", id);
            SqlDataReader reader = db.Select(command);

            Collection<Album> albums = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return albums;
        }

        public static Collection<Album> SelectByUser(int id, Database pDb = null) //4.6
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
            SqlCommand command = db.CreateCommand(SQL_SELECT_ART);

            command.Parameters.AddWithValue("@userID", id);
            SqlDataReader reader = db.Select(command);

            Collection<Album> albums = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return albums;
        }

        private static Collection<Album> Read(SqlDataReader reader)
        {
            Collection<Album> albums = new Collection<Album>();

            while (reader.Read())
            {
                int i = -1;
                Album album = new Album();
                album.IdAlbum = reader.GetInt32(++i);
                album.Name = reader.GetString(++i);
                album.AlbumDate = reader.GetDateTime(++i);
                album.User = reader.GetString(++i);
                if (!reader.IsDBNull(++i))
                    album.Stars = reader.GetInt32(i);

                albums.Add(album);
            }

            return albums;
        }

        private static void PrepareCommand(SqlCommand command, Album album)
        {
            command.Parameters.AddWithValue("@albumID", album.IdAlbum);
            command.Parameters.AddWithValue("@name", album.Name);
            command.Parameters.AddWithValue("@dateA", album.AlbumDate == null ? DBNull.Value : (object)album.AlbumDate);
            command.Parameters.AddWithValue("@userID", album.IdUser);
        }
    }
}
