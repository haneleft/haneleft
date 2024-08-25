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
    public class PaymentTable
    {
        public static String SQL_SELECT = "SELECT * FROM Payment";
        public static String SQL_UPDATE = "UPDATE Payment SET dateToPay = @date WHERE paymentID = @paymentID";
        public static String SQL_DELETE = "DELETE FROM Payment WHERE datetopay < GETDATE() - year(5)";
        public static String SQL_SELECT_EMAIL = "SELECT u.* FROM[User] u JOIN Payment p on p.userID = u.userID " +
                                                "WHERE(DATEDIFF(day, GETDATE(), p.dateToPay) > 1) AND(DATEDIFF(day, GETDATE(), p.dateToPay) < 2)";
        public static int Insert(Payment payment, int months, int credit, Database pDb = null) //7.1, na jak dlouho bude predplatne, byly zadane platebni udeaje spravne 0/1 (ne/ano)
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

            SqlCommand command = db.CreateCommand("PNovaPlatba");
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter input = new SqlParameter();
            input.ParameterName = "@userID";
            input.DbType = DbType.Int32;
            input.Value = payment.IdUser;
            input.Direction = ParameterDirection.Input;
            command.Parameters.Add(input);

            SqlParameter input2 = new SqlParameter();
            input2.ParameterName = "@money";
            input2.DbType = DbType.Double;
            input2.Value = (double)payment.Cost;
            input2.Direction = ParameterDirection.Input;
            command.Parameters.Add(input2);

            SqlParameter input3 = new SqlParameter();
            input3.ParameterName = "@toDate";
            input3.DbType = DbType.Int32;
            input3.Value = months;
            input3.Direction = ParameterDirection.Input;
            command.Parameters.Add(input3);

            SqlParameter input4 = new SqlParameter();
            input4.ParameterName = "@creditData";
            input4.DbType = DbType.Int32;
            input4.Value = credit;
            input4.Direction = ParameterDirection.Input;
            command.Parameters.Add(input4);


            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int Update(Payment payment, Database pDb = null) //7.2
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
            PrepareCommand(command, payment);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int SendEmail(Database pDb = null) //7.3
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

            SqlCommand command1 = db.CreateCommand(SQL_DELETE); //smazani plateb starsich 5 let
            int ret = db.ExecuteNonQuery(command1);

            SqlCommand command = db.CreateCommand(SQL_SELECT_EMAIL); //selectnuti uzivatelu, kterym se ma zaslat email
            SqlDataReader reader = db.Select(command);
            Collection<User> users = ReadUser(reader);
            reader.Close();

            foreach(User u in users)
            {
                //zasleme email na tuto adresu: u.Email
            }

            if (pDb == null)
            {
                db.Close();
            }

            return 0;
        }


        public static Collection<Payment> Select(Database pDb = null) //7.4
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


            Collection<Payment> payments = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return payments;
        }

        private static Collection<Payment> Read(SqlDataReader reader)
        {
            Collection<Payment> payments = new Collection<Payment>();

            while (reader.Read())
            {
                int i = -1;
                Payment payment = new Payment();
                payment.IdPayment = reader.GetInt32(++i);
                payment.Cost = (float)reader.GetDouble(++i);
                payment.PayDate = reader.GetDateTime(++i);
                payment.PayToDate = reader.GetDateTime(++i);
                payment.IdUser = reader.GetInt32(++i);

                payments.Add(payment);
            }

            return payments;
        }




        private static Collection<User> ReadUser(SqlDataReader reader)
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

        private static void PrepareCommand(SqlCommand command, Payment payment)
        {
            command.Parameters.AddWithValue("@paymentID", payment.IdPayment);
            command.Parameters.AddWithValue("@date", payment.PayToDate);
        }
    }
}
