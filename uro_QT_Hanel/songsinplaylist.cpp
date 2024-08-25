#include "songsinplaylist.h"
#include "ui_songsinplaylist.h"

SongsInPlaylist::SongsInPlaylist(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::SongsInPlaylist)
{
    ui->setupUi(this);

    connect(this->ui->songTable, SIGNAL(clicked(const QModelIndex &)), this, SLOT(onTableClickedSong(const QModelIndex &)));
    connect(this->ui->add, SIGNAL(clicked()), this, SLOT(addSongButtonClick()));
    connect(this->ui->exit, SIGNAL(clicked()), this, SLOT(exitButtonClick()));
    connect(this->ui->search, SIGNAL(clicked()), this, SLOT(search()));
}

SongsInPlaylist::~SongsInPlaylist()
{
    delete ui;
}

void SongsInPlaylist::setData(vector<ContainerSong> songs, vector<ContainerSong> playlistSongs, vector<ContainerPlaylist> *playlist, int id){
    this->songs = songs;
    this->playlistSongs = playlistSongs;
    this->playlist = playlist;
    playlistId = id;

    this->loadData();

}

void SongsInPlaylist::onTableClickedSong(const QModelIndex &index){
    if(index.isValid()){
        cSongId = currentSongs[index.row()].id;
        cSongRow = index.row();
        qInfo("row %d", cSongRow);
    }
}

void SongsInPlaylist::exitButtonClick(){
    this->hide();
}

void SongsInPlaylist::addSongButtonClick(){

    if(cSongRow < this->currentSongs.size()){
        vector<ContainerPlaylist>::iterator p = playlist->begin();

        for(int i = 0; i < playlist->size(); i++){
            if(p->id == this->playlistId){
                qInfo("id %d", p->id);
                p->songs.push_back(currentSongs[cSongRow]);
                this->playlistSongs.push_back(currentSongs[cSongRow]);
                this->currentSongs.erase(this->currentSongs.begin() + cSongRow);
                break;
            }
            p++;
        }

    }

    this->loadData();

}

void SongsInPlaylist::search(){
    if(this->ui->lineEdit->text().toStdString() == ""){
        this->loadData();
    }
    else{
        QList<QString> values;

        values.push_back("Name");
        values.push_back("Live");
        values.push_back("Played");

        QStandardItemModel *songsPModel = new QStandardItemModel( 0, values.size() );
        for(int i = 0; i < values.size(); i++){
            songsPModel->setHorizontalHeaderItem(i, new QStandardItem( values[i]));
        }

        vector<ContainerSong> s;

        this->currentSongs.clear();

        for(int i = 0; i < this->songs.size(); i++){
            bool in = false;

            foreach(ContainerSong s, playlistSongs){
                if(this->songs[i].id == s.id){
                    in = true;
                    break;
                }
            }

            if(in == false && songs[i].name == this->ui->lineEdit->text().toStdString()){
                this->currentSongs.push_back(this->songs[i]);

                QStandardItem *name = new QStandardItem(QString(this->songs[i].name.c_str()));
                QStandardItem *live = new QStandardItem(QString(to_string(this->songs[i].live).c_str()));
                QStandardItem *played = new QStandardItem(QString(to_string(this->songs[i].played).c_str()));

                name->setFlags(name->flags() & (~Qt::ItemIsEditable));
                live->setFlags(live->flags() & (~Qt::ItemIsEditable));
                played->setFlags(played->flags() & (~Qt::ItemIsEditable));


                QList<QStandardItem*> row(3);
                row = {name, live, played};
                songsPModel->appendRow(row);
            }

            this->ui->songTable->setModel(songsPModel);
            this->ui->songTable->setSelectionBehavior(QAbstractItemView::SelectRows);
            this->ui->songTable->resizeColumnsToContents();
            this->ui->songTable->horizontalHeader()->setSectionResizeMode(QHeaderView::Stretch);

        }
    }
}

void SongsInPlaylist::loadData(){
    QList<QString> values;

    values.push_back("Name");
    values.push_back("Live");
    values.push_back("Played");

    QStandardItemModel *songsPModel = new QStandardItemModel( 0, values.size() );
    for(int i = 0; i < values.size(); i++){
        songsPModel->setHorizontalHeaderItem(i, new QStandardItem( values[i]));
    }

    vector<ContainerSong> s;

    this->currentSongs.clear();

    for(int i = 0; i < this->songs.size(); i++){
        bool in = false;

        foreach(ContainerSong s, playlistSongs){
            if(this->songs[i].id == s.id){
                in = true;
                break;
            }
        }

        if(in == false){
            this->currentSongs.push_back(this->songs[i]);

            QStandardItem *name = new QStandardItem(QString(this->songs[i].name.c_str()));
            QStandardItem *live = new QStandardItem(QString(to_string(this->songs[i].live).c_str()));
            QStandardItem *played = new QStandardItem(QString(to_string(this->songs[i].played).c_str()));

            name->setFlags(name->flags() & (~Qt::ItemIsEditable));
            live->setFlags(live->flags() & (~Qt::ItemIsEditable));
            played->setFlags(played->flags() & (~Qt::ItemIsEditable));


            QList<QStandardItem*> row(3);
            row = {name, live, played};
            songsPModel->appendRow(row);
        }

        this->ui->songTable->setModel(songsPModel);
        this->ui->songTable->setSelectionBehavior(QAbstractItemView::SelectRows);
        this->ui->songTable->resizeColumnsToContents();
        this->ui->songTable->horizontalHeader()->setSectionResizeMode(QHeaderView::Stretch);

    }
}
