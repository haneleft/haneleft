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
    public partial class Songs : Form
    {
        private Database db;
        private Collection<Song> songs;
        private int userId;
        public Songs(Database db, int userId)
        {
            this.db = db;
            this.userId = userId;
            InitializeComponent();
            loadTable();
        }

        public void loadTable()
        {
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Name";
            dataGridView1.Columns[1].Name = "Live Listeners";
            dataGridView1.Columns[2].Name = "Played";
            dataGridView1.Columns[3].Name = "Artist";
            dataGridView1.Columns[4].Name = "Album";

            songs = SongTable.Select("", this.db);

            for(int i = 0; i < songs.Count; i++)
            {
                for (int j = 0; j < songs.Count; j++)
                {
                    if(songs[i].Live > songs[j].Live)
                    {
                        Song tmp = songs[i];
                        songs[i] = songs[j];
                        songs[j] = tmp;
                    }
                }
            }

            foreach (Song s in songs)
            {
                string[] row = new string[] { s.Name, s.Live.ToString(), s.Listened.ToString(), s.Artist, s.Album };
                dataGridView1.Rows.Add(row);
            }
        }

        public void searchButton(object sender, EventArgs e)
        {
            searchSongs search = new searchSongs(this.db, this.textBox1.Text, this.userId);
            search.Location = this.Location;
            search.StartPosition = FormStartPosition.Manual;
            search.Show();
            this.Hide();
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
