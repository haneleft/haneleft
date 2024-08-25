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
    public class PlaylistTable
    {
        public static String SQL_SELECT = "SELECT p.playlistID, p.userID, p.name, " +
                "length = CAST((SELECT SUM(DATEDIFF(SECOND,'00:00:00', length))" +
                "FROM Song JOIN SongInPL sp on sp.songID = Song.songID " +
                "WHERE p.playlistID = sp.playlistID)/60 as VARCHAR(5))  + ':' + " +
                "CAST((SELECT SUM(DATEDIFF(SECOND,'00:00:00', length))" +
                "FROM Song JOIN SongInPL sp on sp.songID = Song.songID WHERE p.playlistID = sp.playlistID)%60 as VARCHAR(5))" +
                "FROM[User] u JOIN Playlist p on p.userID = u.userID WHERE u.userID = @userID";

        public static String SQL_INSERT = "INSERT INTO Playlist (userID, length, name)" +
                                          "VALUES (@userID, @length, @name)";
        public static String SQL_DELETE = "DELETE FROM Playlist WHERE playlistID = @plID";
        public static String SQL_DELETE_SONGS = "DELETE FROM SongInPl WHERE playlistID = @plID";
        public static String SQL_UPDATE = "UPDATE Playlist SET name = @name WHERE playlistID = @plID";
        public static String SQL_UPDATE_PLAYLIST = "INSERT INTO songInPL(songID, playlistID) " +
                                                   "VALUES(@skladba, @playlist)";
        public static int Insert(Playlist playlist, Database pDb = null) //5.1
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
            PrepareCommand(command, playlist);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int UpdatePlaylist(int songId, int playlistId, Database pDb = null) //5.2 po konzultaci jsem nemohl s dobrym svedomim tuto funkci prevest na transakci tak aby to davalo smysl, transakci se tedy stal trigger (neboli funkce 2.5)
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

            SqlCommand command = db.CreateCommand(SQL_UPDATE_PLAYLIST);
            command.Parameters.AddWithValue("@skladba", songId);
            command.Parameters.AddWithValue("@playlist", playlistId);

            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int Delete(int playlistID, Database pDb = null) //5.3
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
            SqlCommand command = db.CreateCommand(SQL_DELETE_SONGS);

            command.Parameters.AddWithValue("@plID", playlistID);
            int ret = db.ExecuteNonQuery(command);

            command = db.CreateCommand(SQL_DELETE);

            command.Parameters.AddWithValue("@plID", playlistID);
            ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static Collection<Playlist> Select(int userID, Database pDb = null) //5.4
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
            command.Parameters.AddWithValue("@userID", userID);

            reader = db.Select(command);


            Collection<Playlist> playlists = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return playlists;
        }

        

        public static int Update(Playlist playlist, Database pDb = null) //5.5
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
            PrepareCommand(command, playlist);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        private static Collection<Playlist> Read(SqlDataReader reader)
        {
            Collection<Playlist> playlists = new Collection<Playlist>();

            while (reader.Read())
            {
                int i = -1;
                Playlist playlist = new Playlist();
                playlist.IdPlaylist = reader.GetInt32(++i);
                playlist.IdUser = reader.GetInt32(++i);
                if (!reader.IsDBNull(++i))
                    playlist.Name = reader.GetString(i);
                if (!reader.IsDBNull(++i))
                    playlist.FakeTime= reader.GetString(i);
                

                playlists.Add(playlist);
            }

            return playlists;
        }

        private static void PrepareCommand(SqlCommand command, Playlist playlist)
        {
            command.Parameters.AddWithValue("@plID", playlist.IdPlaylist);
            command.Parameters.AddWithValue("@userID", playlist.IdUser);
            command.Parameters.AddWithValue("@length", playlist.Length);
            command.Parameters.AddWithValue("@name", playlist.Name);
        }
    }
}
