
namespace Forms
{
    partial class searchSongs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.liveCount = new System.Windows.Forms.Label();
            this.playedCount = new System.Windows.Forms.Label();
            this.plAdd = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button5 = new System.Windows.Forms.Button();
            this.artistName = new System.Windows.Forms.Label();
            this.albumName = new System.Windows.Forms.Label();
            this.songName = new System.Windows.Forms.Label();
            this.searchSong = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(-3, 64);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(691, 332);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(683, 306);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Songs";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(684, 311);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(683, 306);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Albums";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 25;
            this.dataGridView2.Size = new System.Drawing.Size(687, 305);
            this.dataGridView2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataGridView3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(683, 306);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "Users";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(0, 0);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowTemplate.Height = 25;
            this.dataGridView3.Size = new System.Drawing.Size(687, 305);
            this.dataGridView3.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(10, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(180, 20);
            this.textBox1.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.liveCount);
            this.panel1.Controls.Add(this.playedCount);
            this.panel1.Controls.Add(this.plAdd);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.artistName);
            this.panel1.Controls.Add(this.albumName);
            this.panel1.Controls.Add(this.songName);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Location = new System.Drawing.Point(343, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(343, 391);
            this.panel1.TabIndex = 6;
            this.panel1.Visible = false;
            // 
            // liveCount
            // 
            this.liveCount.AutoSize = true;
            this.liveCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.liveCount.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.liveCount.Location = new System.Drawing.Point(248, 352);
            this.liveCount.Name = "liveCount";
            this.liveCount.Size = new System.Drawing.Size(13, 13);
            this.liveCount.TabIndex = 17;
            this.liveCount.Text = "0";
            // 
            // playedCount
            // 
            this.playedCount.AutoSize = true;
            this.playedCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.playedCount.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.playedCount.Location = new System.Drawing.Point(248, 314);
            this.playedCount.Name = "playedCount";
            this.playedCount.Size = new System.Drawing.Size(13, 13);
            this.playedCount.TabIndex = 16;
            this.playedCount.Text = "0";
            // 
            // plAdd
            // 
            this.plAdd.AutoSize = true;
            this.plAdd.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.plAdd.Location = new System.Drawing.Point(109, 324);
            this.plAdd.Name = "plAdd";
            this.plAdd.Size = new System.Drawing.Size(52, 26);
            this.plAdd.TabIndex = 15;
            this.plAdd.Text = "Add song\r\nto playlist";
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button6.Location = new System.Drawing.Point(16, 324);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(87, 34);
            this.button6.TabIndex = 14;
            this.button6.Text = "Comments";
            this.button6.UseVisualStyleBackColor = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(33, 290);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(279, 11);
            this.progressBar1.TabIndex = 13;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(149, 221);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(48, 45);
            this.button5.TabIndex = 12;
            this.button5.Text = "Play";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.playClicked);
            // 
            // artistName
            // 
            this.artistName.AutoSize = true;
            this.artistName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.artistName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.artistName.Location = new System.Drawing.Point(212, 177);
            this.artistName.MaximumSize = new System.Drawing.Size(133, 60);
            this.artistName.Name = "artistName";
            this.artistName.Size = new System.Drawing.Size(119, 17);
            this.artistName.TabIndex = 11;
            this.artistName.Text = "Artist: Artist name";
            // 
            // albumName
            // 
            this.albumName.AutoSize = true;
            this.albumName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.albumName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.albumName.Location = new System.Drawing.Point(13, 177);
            this.albumName.MaximumSize = new System.Drawing.Size(133, 60);
            this.albumName.Name = "albumName";
            this.albumName.Size = new System.Drawing.Size(133, 17);
            this.albumName.TabIndex = 10;
            this.albumName.Text = "Album: Album name";
            // 
            // songName
            // 
            this.songName.AutoSize = true;
            this.songName.BackColor = System.Drawing.Color.Black;
            this.songName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.songName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.songName.Location = new System.Drawing.Point(96, 63);
            this.songName.MaximumSize = new System.Drawing.Size(200, 100);
            this.songName.Name = "songName";
            this.songName.Size = new System.Drawing.Size(145, 31);
            this.songName.TabIndex = 9;
            this.songName.Text = "songName";
            this.songName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // searchSong
            // 
            this.searchSong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchSong.Image = global::ORM_Projekt_HAN0334.Properties.Resources.search;
            this.searchSong.Location = new System.Drawing.Point(197, 23);
            this.searchSong.Name = "searchSong";
            this.searchSong.Size = new System.Drawing.Size(21, 19);
            this.searchSong.TabIndex = 7;
            this.searchSong.UseVisualStyleBackColor = true;
            this.searchSong.Click += new System.EventHandler(this.searchButton);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Image = global::ORM_Projekt_HAN0334.Properties.Resources.starsB1;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(99, 147);
            this.label4.MaximumSize = new System.Drawing.Size(160, 0);
            this.label4.MinimumSize = new System.Drawing.Size(0, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 30);
            this.label4.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Image = global::ORM_Projekt_HAN0334.Properties.Resources.addB;
            this.label3.Location = new System.Drawing.Point(167, 330);
            this.label3.MinimumSize = new System.Drawing.Size(30, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 20);
            this.label3.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Image = global::ORM_Projekt_HAN0334.Properties.Resources.live;
            this.label2.Location = new System.Drawing.Point(211, 345);
            this.label2.MaximumSize = new System.Drawing.Size(30, 20);
            this.label2.MinimumSize = new System.Drawing.Size(30, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 20);
            this.label2.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Image = global::ORM_Projekt_HAN0334.Properties.Resources.listened;
            this.label1.Location = new System.Drawing.Point(212, 314);
            this.label1.MinimumSize = new System.Drawing.Size(30, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 20);
            this.label1.TabIndex = 18;
            // 
            // button3
            // 
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Image = global::ORM_Projekt_HAN0334.Properties.Resources.settingsB;
            this.button3.Location = new System.Drawing.Point(223, 11);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(52, 49);
            this.button3.TabIndex = 8;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Image = global::ORM_Projekt_HAN0334.Properties.Resources.profileB;
            this.button4.Location = new System.Drawing.Point(280, 11);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(52, 49);
            this.button4.TabIndex = 7;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.profileButton);
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::ORM_Projekt_HAN0334.Properties.Resources.settings;
            this.button1.Location = new System.Drawing.Point(566, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 49);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = global::ORM_Projekt_HAN0334.Properties.Resources.profile;
            this.button2.Location = new System.Drawing.Point(623, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(52, 49);
            this.button2.TabIndex = 2;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.profileButton);
            // 
            // searchSongs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 390);
            this.Controls.Add(this.searchSong);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Name = "searchSongs";
            this.Text = "searchSongs";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label artistName;
        private System.Windows.Forms.Label albumName;
        private System.Windows.Forms.Label songName;
        private System.Windows.Forms.Label plAdd;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label playedCount;
        private System.Windows.Forms.Label liveCount;
        private System.Windows.Forms.Button searchSong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}