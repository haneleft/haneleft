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
    public class SongTable
    {
        public static String SQL_SELECT_ALB = "SELECT s.songID, s.name, s.length, s.live, s.listened, (SELECT artistN FROM [User] WHERE userID = " +
        "(SELECT userID FROM album WHERE albumID = s.albumID)) as art ,(SELECT name FROM "+
        "album WHERE albumID = s.albumID) as alb ,AVG(c.STARS) as stars "+
        "FROM Song s "+
        "LEFT JOIN Comments c on c.songID = s.songID  WHERE s.albumID = @albumID "  +
        "GROUP BY s.songID, s.name, s.length, s.live, s.listened, s.albumID ";
        public static String SQL_SELECT_ART = "SELECT s.songID, s.name, s.length, s.live, s.listened, (SELECT artistN FROM [User] WHERE userID = " +
        "(SELECT userID FROM album WHERE albumID = s.albumID)) as art ,(SELECT name FROM "+
        "album WHERE albumID = s.albumID) as alb ,AVG(c.STARS) as stars "+
        "FROM Song s "+
        "LEFT JOIN Comments c on c.songID = s.songID  WHERE (SELECT userID FROM Album WHERE s.albumID = albumID) = @userID " +
        "GROUP BY s.songID, s.name, s.length, s.live, s.listened, s.albumID ORDER BY s.albumID";
        public static String SQL_SELECT_PL = "SELECT s.songID, s.name, s.length, s.live, s.listened, (SELECT artistN FROM [User] WHERE userID = " +
        "(SELECT userID FROM album WHERE albumID = s.albumID)) as art ,(SELECT name FROM " +
        "album WHERE albumID = s.albumID) as alb ,AVG(c.STARS) as stars " +
        "FROM songInPL spl " +
        "LEFT JOIN Song s on s.songID = spl.songID "+
        "LEFT JOIN Comments c on c.songID = s.songID  WHERE spl.playlistID = @playlistID " +
        "GROUP BY s.songID, s.name, s.length, s.live, s.listened, s.albumID ";
        public static String SQL_SELECT_DETAIL = "SELECT s.songID, s.name, s.length, s.live, s.listened, (SELECT artistN FROM [User] WHERE userID = " +
        "(SELECT userID FROM album WHERE albumID = s.albumID)) as art ,(SELECT name FROM " +
        "album WHERE albumID = s.albumID) as alb ,AVG(c.STARS) as stars " +
        "FROM Song s " +
        "LEFT JOIN Comments c on c.songID = s.songID  WHERE s.songID = @songID " +
        "GROUP BY s.songID, s.name, s.length, s.live, s.listened, s.albumID ";
        public static String SQL_INSERT = "INSERT INTO Song (name, length, orderID, listened, live, albumID) " +
                                          "VALUES (@name, @length, @orderID, 0, 0, @albumID) ";
        public static String SQL_INSERT_G = "INSERT INTO UserGenre (genreID, songID)" +
                                          "VALUES (@genreID, @songID)";
        public static String SQL_DELETE = "DELETE FROM Song WHERE songID = @songID ";
        public static String SQL_DELETE_G = "DELETE FROM Collab WHERE songID = @songID ";
        public static String SQL_DELETE_C = "DELETE FROM SongsGenre WHERE songID = @songID ";
        public static String SQL_UPDATE = "UPDATE Song SET name = @name, length = @length, orderID = @orderID, albumID = @albumID WHERE songID = @songID ";
        public static String SQL_DELETE_PL = "DELETE FROM songInPL WHERE songID = @songID ";
        public static int Insert(Song song, Database pDb = null) //3.1
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
            PrepareCommand(command, song);
            int ret = db.ExecuteNonQuery(command);
            if (song.Genres != null)
            {
                foreach (Genre g in song.Genres)
                {
                    SqlCommand command1 = db.CreateCommand(SQL_INSERT_G);
                    command.Parameters.AddWithValue("@songID", song.IdSong);
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

        public static int Update(Song song, Database pDb = null) //3.2
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
            PrepareCommand(command, song);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int Delete(int songID, Database pDb = null) //3.3
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

            SqlCommand command2 = db.CreateCommand(SQL_DELETE_G);

            command2.Parameters.AddWithValue("@songID", songID);
            int ret = db.ExecuteNonQuery(command2);

            SqlCommand command3 = db.CreateCommand(SQL_DELETE_C);

            command3.Parameters.AddWithValue("@songID", songID);
            ret = db.ExecuteNonQuery(command3);

            SqlCommand command1 = db.CreateCommand(SQL_DELETE_PL);

            command1.Parameters.AddWithValue("@songID", songID);
            ret = db.ExecuteNonQuery(command1);
            
            foreach(Comments c in CommentsTable.Select(songID, "Song", db))
            {
                CommentsTable.Delete(c.IdComment, db);
            }

            SqlCommand command = db.CreateCommand(SQL_DELETE);

            command.Parameters.AddWithValue("@songID", songID);
            ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static Collection<Song> Select(string name, Database pDb = null) //3.4
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

            SqlCommand command = db.CreateCommand("PSeznamSkladeb");
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter input = new SqlParameter();
            input.ParameterName = "@name";
            input.DbType = DbType.String;
            input.Value = name;
            input.Direction = ParameterDirection.Input;
            command.Parameters.Add(input);

            SqlDataReader reader = db.Select(command);

            Collection<Song> songs = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return songs;
        }
        public static Collection<Song> SelectDetail(int id, Database pDb = null) //3.5
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
            command.Parameters.AddWithValue("@songID", id);

            reader = db.Select(command);


            Collection<Song> songs = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return songs;
        }

        public static Collection<Song> SelectByAlbum(int id, Database pDb = null) //3.6
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

            command = db.CreateCommand(SQL_SELECT_ALB);
            command.Parameters.AddWithValue("@albumID", id);

            reader = db.Select(command);


            Collection<Song> songs = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return songs;
        }

        public static Collection<Song> SelectByArtist(int id, Database pDb = null) //3.7
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

            command = db.CreateCommand(SQL_SELECT_ART);
            command.Parameters.AddWithValue("@userID", id);

            reader = db.Select(command);


            Collection<Song> songs = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return songs;
        }

        public static Collection<Song> SelectByPlaylist(int id, Database pDb = null) //3.8
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

            command = db.CreateCommand(SQL_SELECT_PL);
            command.Parameters.AddWithValue("@playlistID", id);
            
            reader = db.Select(command);


            Collection<Song> songs = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return songs;
        }

        

        public static int Listening(int id, int playing, Database pDb = null) //3.9 a 3.10 dohromady, parametr id je id skladby a playing = 0 / 1, znamena skladba nehraje/hraje
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

            SqlCommand command = db.CreateCommand("PPridaniPoslechnutiNaSkladbe");
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter input = new SqlParameter();
            input.ParameterName = "@skladba";
            input.DbType = DbType.Int32;
            input.Value = id;
            input.Direction = ParameterDirection.Input;
            command.Parameters.Add(input);

            SqlParameter input2 = new SqlParameter();
            input2.ParameterName = "@playing";
            input2.DbType = DbType.Int32;
            input2.Value = playing;
            input2.Direction = ParameterDirection.Input;
            command.Parameters.Add(input2);

            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        


        private static Collection<Song> Read(SqlDataReader reader)
        {
            Collection<Song> songs = new Collection<Song>();

            while (reader.Read())
            {
                int i = -1;
                Song song = new Song();
                song.IdSong = reader.GetInt32(++i);
                song.Name = reader.GetString(++i);
                song.Length = reader.GetTimeSpan(++i);
                if (!reader.IsDBNull(++i))
                    song.Live = reader.GetInt32(i);
                if (!reader.IsDBNull(++i))
                    song.Listened = reader.GetInt32(i);
                song.Artist = reader.GetString(++i);
                song.Album = reader.GetString(++i);
                if (!reader.IsDBNull(++i))
                    song.Stars = reader.GetInt32(i);

                songs.Add(song);
            }

            return songs;
        }

        private static void PrepareCommand(SqlCommand command, Song song)
        {
            command.Parameters.AddWithValue("@songID", song.IdSong);
            command.Parameters.AddWithValue("@name", song.Name);
            command.Parameters.AddWithValue("@length", song.Length);
            command.Parameters.AddWithValue("@orderID", song.IdOrder);
            command.Parameters.AddWithValue("@albumID", song.IdAlbum);
        }
    }
}
