using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ORM_Projekt_HAN0334.ORM;
using ORM_Projekt_HAN0334.ORM.DAO;

namespace Forms
{
    public partial class searchSongs : Form
    {
        private Database db;
        private string input;
        private Collection<Song> songs;
        private int playing;
        private int currentId;
        private bool songSelected;
        private int userId;
        public searchSongs(Database db, string input, int userId)
        {
            this.db = db;
            this.input = input;
            this.playing = 0;
            this.userId = userId;
            songSelected = false;
            InitializeComponent();
            this.dataGridView1.CellClick += this.dataGridView1_CellClick;
            this.panel1.Visible = false;
            this.loadTable();
        }

        public void loadTable()
        {
            
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Name";
            dataGridView1.Columns[1].Name = "Live Listeners";
            dataGridView1.Columns[2].Name = "Played";
            dataGridView1.Columns[3].Name = "Artist";
            dataGridView1.Columns[4].Name = "Album";

            

            songs = SongTable.Select(this.input, this.db);

            for (int i = 0; i < songs.Count; i++)
            {
                for (int j = 0; j < songs.Count; j++)
                {
                    if (songs[i].Listened > songs[j].Listened)
                    {
                        Song tmp = songs[i];
                        songs[i] = songs[j];
                        songs[j] = tmp;
                    }
                }
            }

            DataGridViewImageColumn img = new DataGridViewImageColumn();
            img.HeaderText = "Stars";
            Image image = Image.FromFile("D:\\DS projekt\\ORM_Projekt_HAN0334\\pictures\\stars.png");
            img.Image = image;
            img.ImageLayout = DataGridViewImageCellLayout.Normal;
            dataGridView1.Columns.Add(img);

            foreach (Song s in songs)
            {
                string[] row = new string[] { s.Name, s.Live.ToString(), s.Listened.ToString(), s.Artist, s.Album };
                dataGridView1.Rows.Add(row);
            }


        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < songs.Count)
            {
                int rowId = e.RowIndex;

                if (songSelected = true && this.playing == 1)
                {
                    this.playing = 0;
                    SongTable.Listening(currentId, this.playing, db);
                    this.button5.Text = "Play";
                }

            
                    Collection<Song> currentSong = SongTable.SelectDetail(songs[rowId].IdSong, db);

                    this.panel1.Visible = true;

                    this.currentId = currentSong[0].IdSong;
                    this.songName.Text = currentSong[0].Name;
                    this.songName.Location = new System.Drawing.Point(171 - this.songName.Width / 2, 63);
                    this.albumName.Text = "Album: " + currentSong[0].Album;
                    this.artistName.Text = "Artist: " + currentSong[0].Artist;
                    this.playedCount.Text = currentSong[0].Listened.ToString();
                    this.liveCount.Text = currentSong[0].Live.ToString();

                    this.label4.MinimumSize = new System.Drawing.Size(currentSong[0].Stars * 32, 30);


                if(songSelected == false)
                {
                    songSelected = true;
                }

            }
        }

        private void playClicked(object sender, EventArgs e)
        {
            if(this.playing == 0)
            {
                this.playing = 1;
                this.button5.Text = "Pause";
            }
            else
            {
                this.playing = 0;
                this.button5.Text = "Play";
            }
            SongTable.Listening(currentId, this.playing, db);

            Collection<Song> currentSong = SongTable.SelectDetail(currentId, db);

            this.playedCount.Text = currentSong[0].Listened.ToString();
            this.liveCount.Text = currentSong[0].Live.ToString();

        }

        private void searchButton(object sender, EventArgs e)
        {
            this.input = this.textBox1.Text;
            dataGridView1.Rows.Clear();
            this.loadTable();
        }

        public void profileButton(object sender, EventArgs e)
        {
            ArtistOptions artist = new ArtistOptions(this.db, this.userId, this);
            artist.Location = this.Location;
            artist.StartPosition = FormStartPosition.Manual;
            artist.Show();
            this.Hide();
        }

    }
}
