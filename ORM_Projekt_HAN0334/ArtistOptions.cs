using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ORM_Projekt_HAN0334;
using ORM_Projekt_HAN0334.ORM;
using ORM_Projekt_HAN0334.ORM.DAO;

namespace Forms
{
    public partial class ArtistOptions : Form
    {
        private Collection<Song> songs;
        private Collection<Album> albums;
        private string input;
        private Database db;
        private int userId;
        private Form form;
        public ArtistOptions(Database db, int userId, Form form)
        {
            this.db = db;
            this.input = "";
            this.userId = userId;
            this.form = form;
            
            InitializeComponent();
            this.dataGridView1.CellClick += this.dataGridView1_CellClick;
            this.listBox1.SelectedIndexChanged += this.listBox1_SelectedIndexChanged;
            loadTable();

            foreach(Album a in albums)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = a.Name;
                item.Value = a.IdAlbum;
                comboBox1.Items.Add(item);
            }

            foreach(Genre g in GenreTable.Select(db))
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = g.Name;
                item.Value = g.IdGenre;
                comboBox2.Items.Add(item);
            }

            listBox1.Items.Add("User manager");
            listBox1.Items.Add("Advanced options");
            listBox1.Items.Add("Playlists");

            listBox1.SetSelected(0, true);
        }

        public void loadTable()
        {
            dataGridView1.Rows.Clear();

            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "Name";
            dataGridView1.Columns[1].Name = "Live Listeners";
            dataGridView1.Columns[2].Name = "Played";
            dataGridView1.Columns[3].Name = "Album";

            songs = SongTable.SelectByArtist(userId, this.db);

            foreach (Song s in songs)
            {
                string[] row = new string[] { s.Name, s.Live.ToString(), s.Listened.ToString(), s.Album};
                dataGridView1.Rows.Add(row);
            }


            DataGridViewButtonColumn editB = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(editB);
            dataGridView1.Columns[4].Width = 30;
            editB.HeaderText = "Edit";
            editB.Text = "Edit";
            editB.Name = "editB";
            editB.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn deleteB = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(deleteB);
            dataGridView1.Columns[5].Width = 50;
            deleteB.HeaderText = "Edit";
            deleteB.Text = "Delete";
            deleteB.Name = "deleteB";
            deleteB.UseColumnTextForButtonValue = true;

            albums = AlbumTable.SelectByUser(userId, this.db);

        }

        private void addSongButton(object sender, EventArgs e)
        {
            ComboboxItem item = new ComboboxItem();
            

            Song s = new Song();
            s.Name = textBox2.Text;
            s.IdOrder = Int32.Parse(textBox3.Text);

            item = (ComboboxItem)comboBox1.SelectedItem;
            s.IdAlbum = item.Value ;

            TimeSpan x;
            TimeSpan.TryParseExact("2:30.0", "ss\\.fff", null, out x);
            s.Length = x;

            SongTable.Insert(s);

            dataGridView1.Rows.Clear();
            this.loadTable();

            Collection<Song> so = SongTable.SelectByArtist(this.userId, db);

            s.IdSong = so[so.Count - 1].IdSong;

            item = (ComboboxItem)comboBox2.SelectedItem;
            int genre = item.Value;

            GenreTable.InsertS(s.IdSong, genre);


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.RowIndex < songs.Count)
            {

                if (e.ColumnIndex == 5)
                {
                    SongTable.Delete(songs[e.RowIndex].IdSong, db);

                    dataGridView1.Rows.Clear();
                    this.loadTable();

                }
                if (e.ColumnIndex == 4)
                {
                    UpdateSong usong = new UpdateSong(songs[e.RowIndex], albums, db, this);
                    usong.Location = this.Location;
                    usong.StartPosition = FormStartPosition.Manual;
                    usong.Show();
                }

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // Get the currently selected item in the ListBox.
            int listId = listBox1.SelectedIndex;

            if(listId == 2)
            {
                UserPlaylists playlists = new UserPlaylists(this.db, this.userId, form);
                playlists.Location = this.Location;
                playlists.StartPosition = FormStartPosition.Manual;
                playlists.Show();
                this.Hide();
            }
        }

        private void cancelBu(object sender, EventArgs e)
        {
            this.Close();
            this.form.Show();
        }

        private void searchB(object sender, EventArgs e)
        {
            songs.Clear();

            if(textBox1.Text != "")
            {
                dataGridView1.Rows.Clear();

                dataGridView1.ColumnCount = 4;
                dataGridView1.Columns[0].Name = "Name";
                dataGridView1.Columns[1].Name = "Live Listeners";
                dataGridView1.Columns[2].Name = "Played";
                dataGridView1.Columns[3].Name = "Album";


                Collection<Song> so = SongTable.SelectByArtist(userId, this.db);

                foreach (Song s in so)
                {
                    Console.WriteLine(s.Name + " " + textBox1.Text);
                    if (s.Name.Contains(textBox1.Text) || s.Album.Contains(textBox1.Text))
                    {
                        songs.Add(s);
                        Console.WriteLine("Added");
                    }
                        
                }

                foreach (Song s in songs)
                {
                    string[] row = new string[] { s.Name, s.Live.ToString(), s.Listened.ToString(), s.Album };
                    dataGridView1.Rows.Add(row);
                }


                DataGridViewButtonColumn editB = new DataGridViewButtonColumn();
                dataGridView1.Columns.Add(editB);
                dataGridView1.Columns[4].Width = 30;
                editB.HeaderText = "Edit";
                editB.Text = "Edit";
                editB.Name = "editB";
                editB.UseColumnTextForButtonValue = true;

                DataGridViewButtonColumn deleteB = new DataGridViewButtonColumn();
                dataGridView1.Columns.Add(deleteB);
                dataGridView1.Columns[5].Width = 50;
                deleteB.HeaderText = "Edit";
                deleteB.Text = "Delete";
                deleteB.Name = "deleteB";
                deleteB.UseColumnTextForButtonValue = true;
            }
            else
            {
                loadTable();
            }
            
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
