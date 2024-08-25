using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Forms;

using ORM_Projekt_HAN0334.ORM;
using ORM_Projekt_HAN0334.ORM.DAO;

namespace ORM_Projekt_HAN0334
{
    static class Program
    {
        /// <summary>
        /// Hlavní vstupní bod aplikace.
        /// </summary>
        //[STAThread]
        static void Main()
        {
            Database db = new Database();
            db.Connect();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Songs(db, 6));

            

            /*User u = new User();
            u.IdUser = 11;
            u.FirstName = "lul";
            u.LastName = "hmm";
            u.Email = "v@v.c";
            u.Address = "XXXXXXXX";
            u.Role = "Interpret";
            u.Password = "alooo";
            u.Telephone = "666666666";
            u.ArtistName = "Haha";
            u.Status = "Inactive";

            UserTable.Insert(u, db); //1.1 
            int count = UserTable.Select("Admin",db).Count; //1.2
            Console.WriteLine("Pocet uzivatelu: " + count);
            UserTable.Update(u, db); //1.3
            UserTable.Delete(1, db); //1.4
            UserTable.AutoStateChange(db); //1.5
            string name = UserTable.SelectDetail(2, db)[0].FirstName; //1.6
            string nstatus = UserTable.SelectDetail(2, db)[0].Status; //1.6
            Console.WriteLine("Name: " + name + " status: " +nstatus);
            u.IdUser = 2; //id uzivatele u ktereho status menime
            u.Status = "Active"; //zmena statusu
            UserTable.StateChange(u, db); //1.7
            nstatus = UserTable.SelectDetail(2, db)[0].Status; //1.6
            Console.WriteLine("Status: " + nstatus);

            count = UserTable.Select("Admin", db).Count; //1.2
            Console.WriteLine("Pocet uzivatelu po smazani: " + count);

            Console.WriteLine();

            Comments c = new Comments();
            c.IdComment = 27;
            c.Text = "eeej";
            c.Stars = 2;
            c.IdSong = 2;
            c.IdUser = 2;

            CommentsTable.Insert(c, db);  //2.1
            CommentsTable.Update(c, db); //2.2
            CommentsTable.Delete(1, db); //2.3
            count = CommentsTable.Select(1,"Song",db).Count; //2.4
            Console.WriteLine("Pocet vsech komentaru u skladby 1: " + count);
            count = CommentsTable.Select(1, "Album", db).Count; //2.4
            Console.WriteLine("Pocet vsech komentaruu alba 1: " + count);
            count = CommentsTable.SelectAll(db).Count; //2.5
            Console.WriteLine("Pocet vsech komentaru: " + count);
            //2.6 je trigger a je soucasti prilozeneho sql souboru

            Console.WriteLine();

            Song s = new Song();
            s.IdSong = 13;
            s.Name = "fbnsaxxxxxxxx";
            TimeSpan x;
            TimeSpan.TryParseExact("2:30.0", "ss\\.fff", null, out x);
            s.Length = x;
            s.IdOrder = 4;
            s.IdAlbum = 2;

            SongTable.Insert(s, db); //3.1
            SongTable.Update(s, db); //3.2
            SongTable.Delete(1, db); //3.3
            count = SongTable.Select("", db).Count; //3.4 (argument string znaci text podle ktereho vyhledavame skladbu (jmeno skladby, umelce, alba)
            Console.WriteLine("Pocet vsech skladeb: " + count);
            name = SongTable.SelectDetail(2, db)[0].Name; //3.5
            Console.WriteLine("Nazev skladby s id 2: " + name);
            count = SongTable.SelectByAlbum(2, db).Count; //3.6
            Console.WriteLine("Pocet skladeb v albu 2: " + count);
            count = SongTable.SelectByArtist(5, db).Count; //3.7
            Console.WriteLine("Pocet skladeb od artisty 5: " + count);
            count = SongTable.SelectByPlaylist(2, db).Count; //3.8
            Console.WriteLine("Pocet skladeb v playlistu 2: " + count);
            SongTable.Listening(2, 1, db); //3.9 a 3.10 dohromady (prvni parametr je id skladby, druhy parametr znaci 0 = nehraje, 1 = hraje)

            Console.WriteLine();

            Album a = new Album();
            a.IdAlbum = 8;
            a.Name = "fdimnsaons";
            a.IdUser = 5;

            AlbumTable.Insert(a, db); //4.1
            AlbumTable.Update(a, db); //4.2
            AlbumTable.Delete(1, db); //4.3
            count = AlbumTable.Select("", db).Count; //4.4 stejna podminka jako funkce 3.4
            Console.WriteLine("Pocet vsech alb: " + count);
            name = AlbumTable.SelectDetail(2, db)[0].Name; //4.5
            Console.WriteLine("Nazev alba 2: " + name);
            count = AlbumTable.SelectByUser(5, db).Count; //4.6
            Console.WriteLine("Pocet vsech alb od artisty 5: " + count);

            Console.WriteLine();
            
            Playlist p = new Playlist();
            p.IdPlaylist = 3;
            p.Name = "necoxxxx";
            TimeSpan y;
            TimeSpan.TryParseExact("2:30.0", "ss\\.fff", null, out y);
            p.IdUser = 2;
            p.Length = y;

            PlaylistTable.Insert(p, db); //5.1
            PlaylistTable.UpdatePlaylist(2, 3, db); //5.2 + neosetrena vyjimka
            PlaylistTable.Delete(2, db); //5.3
            count = PlaylistTable.Select(2, db).Count; //5.4
            Console.WriteLine("Pocet vsech playlistu od uzivatele 2: " + count);
            PlaylistTable.Update(p, db); //5.5

            Console.WriteLine();

            Genre g = new Genre(); 
            g.IdGenre = 19;
            g.Name = "Drum and Bass";

            GenreTable.Insert(g, db); //6.1
            GenreTable.Update(g, db); //6.2
            GenreTable.Delete(1, db); //6.3
            count = GenreTable.Select(db).Count; //6.4
            Console.WriteLine("Pocet vsech zanru: " + count);

            Console.WriteLine();

            Payment py = new Payment();
            py.IdPayment = 2;
            py.IdUser = 2;
            py.PayToDate = new DateTime(2024, 11, 10);
            py.Cost = (float)500.0;

            PaymentTable.Insert(py, 6, 1, db); //7.1
            PaymentTable.Update(py, db); //7.2
            PaymentTable.SendEmail(db); //7.3
            count = PaymentTable.Select(db).Count; //7.4
            

            */
            //u funkce 5.2 je neosetrena vyjimka, ktera by byla osetrena az v uzivatelskem rozhrani, funguje tedy jen pri prvni spusteni
        }
    }
}
