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
    public partial class UserPlaylists : Form
    {
        private Collection<Song> songs;
        private int currentId;
        private int playing;
        private Database db;
        private int userId;
        private Form form;
        public UserPlaylists(Database db, int userId, Form form)
        {
            this.db = db;
            this.playing = 0;
            this.userId = userId;
            this.form = form;

            InitializeComponent();
            this.listBox1.SelectedIndexChanged += this.listBox1_SelectedIndexChanged;
            loadTable();
            this.dataGridView2.CellClick += this.dataGridView2_CellClick;

            label2.Text = songs[0].Name;
            this.currentId = songs[0].IdSong;

            listBox1.Items.Add("User manager");
            listBox1.Items.Add("Advanced options");
            listBox1.Items.Add("Playlists");

            listBox1.SetSelected(2, true);
        }

        public void loadTable()
        {

            dataGridView2.ColumnCount = 3;
            dataGridView2.Columns[0].Name = "Song Name";
            dataGridView2.Columns[1].Name = "Artist";
            dataGridView2.Columns[2].Name = "Album";

            songs = SongTable.SelectByPlaylist(2, this.db);

            foreach (Song s in songs)
            {
                string[] row = new string[] { s.Name, s.Artist, s.Album};
                dataGridView2.Rows.Add(row);
                
            }



            DataGridViewButtonColumn deleteB = new DataGridViewButtonColumn();
            dataGridView2.Columns.Add(deleteB);
            dataGridView2.Columns[3].Width = 50;
            deleteB.HeaderText = "Edit";
            deleteB.Text = "Delete";
            deleteB.Name = "deleteB";
            deleteB.UseColumnTextForButtonValue = true;


        }

        private void addSongButton(object sender, EventArgs e)
        {
            searchSongs search = new searchSongs(this.db, "", this.userId);
            search.Location = this.Location;
            search.StartPosition = FormStartPosition.Manual;
            search.Show();
            this.Hide();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < songs.Count)
            {

                if (e.ColumnIndex == 4)
                {
                    /*SongTable.Delete(songs[e.RowIndex].IdSong, db);

                    dataGridView2.Rows.Clear();
                    this.loadTable();*/

                }

                label2.Text = songs[e.RowIndex].Name;
                this.currentId = songs[e.RowIndex].IdSong;

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // Get the currently selected item in the ListBox.
            int listId = listBox1.SelectedIndex;

            if (listId == 0)
            {
                ArtistOptions artist = new ArtistOptions(this.db, this.userId, form);
                artist.Location = this.Location;
                artist.StartPosition = FormStartPosition.Manual;
                artist.Show();
                this.Hide();
            }
        }

        private void playClicked(object sender, EventArgs e)
        {
            if (this.playing == 0)
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


        }

        private void cancelBu(object sender, EventArgs e)
        {
            this.Close();
            this.form.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
