using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Forms;
using ORM_Projekt_HAN0334.ORM;
using ORM_Projekt_HAN0334.ORM.DAO;

namespace ORM_Projekt_HAN0334
{
    public partial class UpdateSong : Form
    {
        private Song song;
        private Collection<Album> albums;
        private Database db;
        private ArtistOptions form;
        public UpdateSong(Song song, Collection<Album> albums, Database db, ArtistOptions form)
        {
            this.song = song;
            this.albums = albums;
            this.db = db;
            InitializeComponent();

            this.textBox1.Text = song.Name;
            this.textBox2.Text = song.IdOrder.ToString();

            foreach (Album a in albums)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = a.Name;
                item.Value = a.IdAlbum;
                comboBox1.Items.Add(item);
            }

            comboBox1.SelectedIndex = comboBox1.FindStringExact(song.IdAlbum.ToString());

            foreach (Genre g in GenreTable.Select(db))
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = g.Name;
                item.Value = g.IdGenre;
                comboBox2.Items.Add(item);
            }


        }

        private void cancelBu(object sender, EventArgs e)
        {
            this.Close();

        }

        private void saveButton(object sender, EventArgs e)
        {
            Song s = new Song();

            s.IdSong = song.IdSong;
            s.Name = textBox1.Text;
            s.IdOrder = Int32.Parse(textBox2.Text);

            ComboboxItem item = new ComboboxItem();

            item = (ComboboxItem)comboBox1.SelectedItem;
            s.IdAlbum = item.Value;

            TimeSpan x;
            TimeSpan.TryParseExact("2:30.0", "ss\\.fff", null, out x);
            s.Length = x;

            SongTable.Update(s, db);

            item = (ComboboxItem)comboBox2.SelectedItem;
            int genre = item.Value;

            GenreTable.UpdateS(s.IdSong, genre, db);

            this.Close();

        }

    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
