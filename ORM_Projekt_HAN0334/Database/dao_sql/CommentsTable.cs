using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Projekt_HAN0334.ORM.DAO
{
    public class CommentsTable
    {
        public static String SQL_SELECT_SONG = "SELECT * FROM Comments WHERE songID = @songID";
        public static String SQL_SELECT_ALBUM = "SELECT * FROM Comments WHERE albumID = @albumID";
        public static String SQL_SELECT_ALL = "SELECT * FROM Comments";
        public static String SQL_INSERT = "INSERT INTO Comments (text, stars, date, songID, userID, albumID)" +
                                          "VALUES (@text, @stars, GETDATE(), @songID, @userID, @albumID)";
        public static String SQL_DELETE = "DELETE FROM Comments WHERE comID = @commentID";
        public static String SQL_UPDATE = "UPDATE Comments SET text = @text, stars = @stars WHERE comID = @commentID";
        public static int Insert(Comments comment, Database pDb = null) //2.1
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
            PrepareCommand(command, comment);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int Update(Comments comment, Database pDb = null) //2.2
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
            PrepareCommand(command, comment);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int Delete(int commentID, Database pDb = null) //2.3
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

            command.Parameters.AddWithValue("@commentID", commentID);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static Collection<Comments> Select(int id, string type, Database pDb = null) //2.4
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

            if (type == "Song")
            {
                command = db.CreateCommand(SQL_SELECT_SONG);
                command.Parameters.AddWithValue("@songID", id);

            }
            else
            {
                command = db.CreateCommand(SQL_SELECT_ALBUM);
                command.Parameters.AddWithValue("@albumID", id);
            }

            reader = db.Select(command);


            Collection<Comments> comments = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return comments;
        }

        public static Collection<Comments> SelectAll(Database pDb = null) //2.5
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

            command = db.CreateCommand(SQL_SELECT_ALL);

            reader = db.Select(command);


            Collection<Comments> comments = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return comments;
        }

        //funkce 2.6 je transakce viz. prilozeny sql soubor

        private static Collection<Comments> Read(SqlDataReader reader)
        {
            Collection<Comments> comments = new Collection<Comments>();

            while (reader.Read())
            {
                int i = -1;
                Comments comment = new Comments();
                comment.IdComment  = reader.GetInt32(++i);
                comment.Text = reader.GetString(++i);
                comment.Stars = reader.GetInt32(++i);
                comment.Date = reader.GetDateTime(++i);
                if (!reader.IsDBNull(++i))
                {
                    comment.IdSong = reader.GetInt32(i);
                }
                if (!reader.IsDBNull(++i))
                {
                    comment.IdUser = reader.GetInt32(i);
                }
                if (!reader.IsDBNull(++i))
                {
                    comment.IdAlbum = reader.GetInt32(i);
                }

                comments.Add(comment);
            }
            return comments;
        }

        private static void PrepareCommand(SqlCommand command, Comments comment)
        {
            command.Parameters.AddWithValue("@commentID", comment.IdComment);
            command.Parameters.AddWithValue("@text", comment.Text == null ? DBNull.Value : (object)comment.Text);
            command.Parameters.AddWithValue("@stars", comment.Stars);
            command.Parameters.AddWithValue("@date", comment.Date.HasValue ? (object)comment.Date : DBNull.Value);
            command.Parameters.AddWithValue("@songID", comment.IdSong.HasValue ? (object)comment.IdSong : DBNull.Value);
            command.Parameters.AddWithValue("@userID", comment.IdUser.HasValue ? (object)comment.IdUser : DBNull.Value);
            command.Parameters.AddWithValue("@albumID", comment.IdAlbum.HasValue ? (object)comment.IdAlbum : DBNull.Value);
           
        }
    }
}
