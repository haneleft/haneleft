#include "profile.h"
#include "ui_profile.h"
#include "QStandardItem"

profile::profile(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::profile)
{
    ui->setupUi(this);

    connect(this->ui->backButton, SIGNAL(clicked()), this, SLOT(returnButtonClick()));
    connect(this->ui->songsView, SIGNAL(clicked(const QModelIndex &)), this, SLOT(songsTableClicked(const QModelIndex &)));
    connect(this->ui->albumView, SIGNAL(clicked(const QModelIndex &)), this, SLOT(albumsTableClicked(const QModelIndex &)));
    connect(this->ui->addButtonSong, SIGNAL(clicked()), this, SLOT(addButtonClickedSong()));
    connect(this->ui->saveButtonSong, SIGNAL(clicked()), this, SLOT(saveButtonClickedSong()));
    connect(this->ui->deleteButtonSong, SIGNAL(clicked()), this, SLOT(deleteButtonClickedSong()));
    connect(this->ui->addButtonAlbum, SIGNAL(clicked()), this, SLOT(addButtonClickedAlbum()));
    connect(this->ui->saveButtonAlbum, SIGNAL(clicked()), this, SLOT(saveButtonClickedAlbum()));
    connect(this->ui->deleteButtonAlbum, SIGNAL(clicked()), this, SLOT(deleteButtonClickedAlbum()));
    connect(this->ui->saveButton, SIGNAL(clicked()), this, SLOT(saveButtonClicked()));
    connect(this->ui->showButton, SIGNAL(clicked()), this, SLOT(showButtonClicked()));


}

profile::~profile()
{
    delete ui;
}

void profile::returnButtonClick()
{
    emit comeBack();
}

void profile::loadData()
{
    QList<QString> values;

    values.push_back("Name");
    values.push_back("Live");
    values.push_back("Played");
    values.push_back("Album");
    values.push_back("Genre");

    QStandardItemModel *songsModel = new QStandardItemModel( 0, values.size() );
    for(int i = 0; i < values.size(); i++){
        songsModel->setHorizontalHeaderItem(i, new QStandardItem( values[i]));
    }

    for(int i = 0; i < this->currentSongs.size(); i++){
        QStandardItem *name = new QStandardItem(QString(this->currentSongs[i].name.c_str()));
        QStandardItem *live = new QStandardItem(QString(to_string(this->currentSongs[i].live).c_str()));
        QStandardItem *played = new QStandardItem(QString(to_string(this->currentSongs[i].played).c_str()));
        QStandardItem *album = new QStandardItem(QString(this->findAlbum(this->currentSongs[i]).c_str() ));
        QStandardItem *genre = new QStandardItem(QString(this->getGenreString(this->currentSongs[i].genre).c_str() ));

        name->setFlags(name->flags() & (~Qt::ItemIsEditable));
        live->setFlags(live->flags() & (~Qt::ItemIsEditable));
        played->setFlags(played->flags() & (~Qt::ItemIsEditable));
        genre->setFlags(genre->flags() & (~Qt::ItemIsEditable));


        QList<QStandardItem*> row(5);
        row = {name, live, played, album, genre};
        songsModel->appendRow(row);
    }

    this->ui->songsView->setModel(songsModel);
    this->ui->songsView->setSelectionBehavior(QAbstractItemView::SelectRows);
    this->ui->songsView->resizeColumnsToContents();
    this->ui->songsView->horizontalHeader()->setSectionResizeMode(QHeaderView::Stretch);


    values.clear();

    values.push_back("Name");
    values.push_back("Date of release");
    values.push_back("Genre");

    QStandardItemModel *albumModel = new QStandardItemModel( 0, values.size() );
    for(int i = 0; i < values.size(); i++){
        albumModel->setHorizontalHeaderItem(i, new QStandardItem( values[i]));
    }

    for(int i = 0; i < this->currentAlbums.size(); i++){
        QStandardItem *name = new QStandardItem(QString(currentAlbums[i].name.c_str()));

        string year = to_string(currentAlbums[i].date.year);
        string month = to_string(currentAlbums[i].date.month);
        string day = to_string(currentAlbums[i].date.day);

        string dat = year + "-" + month + "-" + day;

        QStandardItem *date = new QStandardItem(QString(dat.c_str()));
        QStandardItem *genre = new QStandardItem(QString(this->getGenreString(currentAlbums[i].genre).c_str() ));

        name->setFlags(name->flags() & (~Qt::ItemIsEditable));
        date->setFlags(date->flags() & (~Qt::ItemIsEditable));
        genre->setFlags(genre->flags() & (~Qt::ItemIsEditable));


        QList<QStandardItem*> row(3);
        row = {name,date,genre};
        albumModel->appendRow(row);
    }

    this->ui->albumView->setModel(albumModel);
    this->ui->albumView->setSelectionBehavior(QAbstractItemView::SelectRows);
    this->ui->albumView->resizeColumnsToContents();
    this->ui->albumView->horizontalHeader()->setSectionResizeMode(QHeaderView::Stretch);

    this->ui->albumBox->clear();

    foreach(ContainerAlbum a, userAlbums){
        this->ui->albumBox->addItem(a.name.c_str(), a.id);
    }
}

string profile::findAlbum(ContainerSong s){
    foreach(ContainerAlbum al, *this->albums){
        if(al.id == s.albumId){
            return al.name;
        }
    }

    return "";
}

string profile::getGenreString(vector<string> genres){
    string genreBuilder = "";

    foreach(string g, genres){
        if(g != genres[genres.size() - 1]){
            genreBuilder += g + ",";
        }
        else{
            genreBuilder += g;
        }

    }

    return genreBuilder;
}

void profile::songsTableClicked(const QModelIndex &index){
    if (index.isValid()) {
           this->ui->nameEditSong->setText( this->currentSongs[index.row()].name.c_str());
           int album = 0;

           for(int i = 0; i < currentAlbums.size(); i++){
               if(currentAlbums[i].id == this->ui->albumBox->itemData(i, 1)){
                   album = i;
                   break;
               }
           }
           int i = 0;



           /*for(int j =0; j < this->ui->genresBoxSong->count(); j++){
               if(this->currentSongs[index.row()].genre.size() > i){
                   if(this->ui->genresBoxSong->model()->index(j , 0).data().toString() == this->currentSongs[index.row()].genre[i].c_str()){
                       qDebug(this->ui->genresBoxSong->model()->index(j , 0).data().toString().toLatin1() );
                       this->ui->genresBoxSong->model()->data()->;
                       i++;
                   }
                   else{
                       this->ui->genresBoxSong->model()->setData(this->ui->genresBoxSong->model()->index(j , 1),Qt::Unchecked);
                   }
               }
               else{
                   this->ui->genresBoxSong->model()->setData(this->ui->genresBoxSong->model()->index(j , 1),Qt::Unchecked);
               }

           }*/


           this->ui->albumBox->setCurrentIndex(album);
           this->cSongId = this->currentSongs[index.row()].id;
           this->cSongRow = index.row();

    }
}

void profile::albumsTableClicked(const QModelIndex &index){
    if (index.isValid()) {
           this->ui->nameEditAlbum->setText( this->currentAlbums[index.row()].name.c_str());

           /*for(int j =0; j < this->ui->genresBoxSong->count(); j++){
               if(this->currentSongs[index.row()].genre.size() > i){
                   if(this->ui->genresBoxSong->model()->index(j , 0).data().toString() == this->currentSongs[index.row()].genre[i].c_str()){
                       qDebug(this->ui->genresBoxSong->model()->index(j , 0).data().toString().toLatin1() );
                       this->ui->genresBoxSong->model()->data()->;
                       i++;
                   }
                   else{
                       this->ui->genresBoxSong->model()->setData(this->ui->genresBoxSong->model()->index(j , 1),Qt::Unchecked);
                   }
               }
               else{
                   this->ui->genresBoxSong->model()->setData(this->ui->genresBoxSong->model()->index(j , 1),Qt::Unchecked);
               }

           }*/


           this->cAlbumId = this->currentAlbums[index.row()].id;
           this->cAlbumRow = index.row();
           this->ui->dateEdit->setDate(QDate(currentAlbums[index.row()].date.year, currentAlbums[index.row()].date.month, currentAlbums[index.row()].date.day));

    }
}

void profile::addButtonClickedSong(){
    bool error = false;
    string errorString = "";
    if(this->ui->nameEditSong->text().toStdString() == ""){
        error= true;
        errorString += "Name is missing\n";

    }
    if(this->currentAlbums.size() <= 0){
        error= true;
        errorString += "Song needs to have album\n(insert album)";

        QMessageBox reply;
        reply.setWindowTitle("Vaulues missing");
        reply.setText(errorString.c_str());
        reply.setStandardButtons(QMessageBox::Ok);
        if(reply.exec() == QMessageBox::Ok){
        }
    }
    if(error == false){


        vector<string> gen;
        int alb;
        vector<ContainerGenre>::iterator it = this->genres->begin();

        for(int i = 0; i < this->genres->size(); i++){
            if(this->ui->genresBoxSong->model()->index(i,0).data().toString() == it->name.c_str()
                    && (this->ui->genresBoxSong->model()->index(i,0).data(Qt::CheckStateRole).toInt() == 2) ){
                gen.push_back(it->name);
            }
            it++;
        }

        alb = this->currentAlbums[this->ui->albumBox->currentIndex()].id;

        vector<ContainerSong>::iterator its = this->songs->begin();
        for(int i = 0; i < this->songs->size(); i++){
            if(i == this->songs->size() - 1){
                ContainerSong s ={its->id + 1, this->currentArtist.id, alb, this->ui->nameEditSong->text().toStdString(), 0, 0, gen, "path" };

                this->currentSongs.push_back(s);
                this->songs->push_back(s);

                break;
            }
            its++;
        }

    }





    this->loadData();
}

void profile::deleteButtonClickedSong(){
    int i = 0;

    if(cSongRow < currentSongs.size()){
        foreach(ContainerSong s , *songs){
            if(s.id == cSongId){
                songs->erase(songs->begin() + i);
                this->deleteInPlaylists(s.id);
                break;
            }
            i++;
        }

        this->currentSongs.erase(this->currentSongs.begin() + cSongRow);

        this->loadData();
    }


}

void profile::saveButtonClickedSong(){
    if(cSongRow < currentSongs.size()){
        bool error = false;
        string errorString = "";
        if(this->ui->nameEditSong->text().toStdString() == ""){
            error= true;
            errorString += "Name is missing\n";

        }
        if(this->currentAlbums.size() <= 0){
            error= true;
            errorString += "Song needs to have album\n(insert album)";

            QMessageBox reply;
            reply.setWindowTitle("Vaulues missing");
            reply.setText(errorString.c_str());
            reply.setStandardButtons(QMessageBox::Ok);
            if(reply.exec() == QMessageBox::Ok){
            }
        }
        if(error == false){
            vector<string> gen;
            int alb = 0;
            vector<ContainerGenre>::iterator it = this->genres->begin();

            for(int i = 0; i < this->genres->size(); i++){
                if(this->ui->genresBoxSong->model()->index(i,0).data().toString() == it->name.c_str()
                        && (this->ui->genresBoxSong->model()->index(i,0).data(Qt::CheckStateRole).toInt() == 2) ){
                    gen.push_back(it->name);
                }

                it++;
            }

            alb = this->currentAlbums[this->ui->albumBox->currentIndex()].id;

            vector<ContainerSong>::iterator its = this->songs->begin();

            for(int i = 0; i < this->songs->size(); i++){
                if(its->id == cSongId){
                    its->name = this->ui->nameEditSong->text().toStdString();
                    its->albumId = alb;
                    its->genre.clear();
                    foreach(string g, gen){
                        its->genre.push_back(g);
                    }
                    its->path = "path";

                    break;
                }

                its++;
            }

            this->currentSongs[cSongRow].name = this->ui->nameEditSong->text().toStdString();
            this->currentSongs[cSongRow].albumId = alb;
            this->currentSongs[cSongRow].genre.clear();
            foreach(string g, gen){
                this->currentSongs[cSongRow].genre.push_back(g);
            }
            this->currentSongs[cSongRow].path = "path";

            this->loadData();
        }


    }

}

void profile::addButtonClickedAlbum(){
    if(this->ui->nameEditAlbum->text().toStdString() == ""){
        QMessageBox reply;
        reply.setWindowTitle("Vaulues missing");
        reply.setText("Name is missing");
        reply.setStandardButtons(QMessageBox::Ok);
        if(reply.exec() == QMessageBox::Ok){
        }
    }
    else{
        vector<string> gen;
        int alb;
        vector<ContainerGenre>::iterator it = this->genres->begin();

        for(int i = 0; i < this->genres->size(); i++){
            if(this->ui->genresBoxAlbum->model()->index(i,0).data().toString() == it->name.c_str()
                    && (this->ui->genresBoxAlbum->model()->index(i,0).data(Qt::CheckStateRole).toInt() == 2) ){
                gen.push_back(it->name);
            }
            it++;
        }

        DateTime d = {this->ui->dateEdit->date().year(),this->ui->dateEdit->date().month(), this->ui->dateEdit->date().day()};

        vector<ContainerAlbum>::iterator ita = this->albums->begin();
        for(int i = 0; i < this->albums->size(); i++){
            if(i == this->albums->size() - 1){
                ContainerAlbum a = {ita->id + 1, this->currentArtist.id, this->ui->nameEditAlbum->text().toStdString(), d, gen};
                this->currentAlbums.push_back(a);
                this->albums->push_back(a);

                break;
            }
            ita++;
        }
        this->userAlbums.clear();

        foreach(ContainerAlbum a, *albums){
            if(a.artId == 0){
                this->userAlbums.push_back(a);
            }
        }

        this->loadData();
    }

}
void profile::deleteButtonClickedAlbum(){
    int i = 0;

    if(cAlbumRow < currentAlbums.size()){
        foreach(ContainerSong s , *songs){
            if(s.albumId == cAlbumId){
                songs->erase(songs->begin() + i);
                int j = 0;
                foreach(ContainerSong cs, this->currentSongs){
                    if(cs.id == s.id){
                        this->currentSongs.erase(this->currentSongs.begin() + j);
                        this->deleteInPlaylists(s.id);
                    }
                    j++;
                }
                i--;
            }
            i++;
        }

        i = 0;

        foreach(ContainerAlbum a , *albums){
            if(a.id == cAlbumId){
                albums->erase(albums->begin() + i);
                break;
            }
            i++;
        }

        this->currentAlbums.erase(this->currentAlbums.begin() + cAlbumRow);
        this->userAlbums.clear();

        foreach(ContainerAlbum a, *albums){
            if(a.artId == 0){
                this->userAlbums.push_back(a);
            }
        }

        this->loadData();
    }
}

void profile::saveButtonClickedAlbum(){

    if(cAlbumRow < currentAlbums.size()){

        if(this->ui->nameEditAlbum->text().toStdString() == ""){
            QMessageBox reply;
            reply.setWindowTitle("Vaulues missing");
            reply.setText("Name is missing");
            reply.setStandardButtons(QMessageBox::Ok);
            if(reply.exec() == QMessageBox::Ok){
            }
        }
        else{
            vector<string> gen;
            int alb;
            vector<ContainerGenre>::iterator it = this->genres->begin();

            for(int i = 0; i < this->genres->size(); i++){
                if(this->ui->genresBoxAlbum->model()->index(i,0).data().toString() == it->name.c_str()
                        && (this->ui->genresBoxAlbum->model()->index(i,0).data(Qt::CheckStateRole).toInt() == 2) ){
                    gen.push_back(it->name);
                }
                it++;
            }

            DateTime d = {this->ui->dateEdit->date().year(),this->ui->dateEdit->date().month(), this->ui->dateEdit->date().day()};

            vector<ContainerAlbum>::iterator ita = this->albums->begin();
            for(int i = 0; i < this->albums->size(); i++){
                if(i == this->albums->size() - 1){
                    ita->name = this->ui->nameEditAlbum->text().toStdString();
                    ita->date = d;
                    ita->genre.clear();
                    foreach(string g, gen){
                        ita->genre.push_back(g);
                    }

                    break;
                }
                ita++;
            }

            this->currentAlbums[cAlbumRow].name = this->ui->nameEditAlbum->text().toStdString();
            this->currentAlbums[cAlbumRow].date = d;
            this->currentAlbums[cAlbumRow].genre.clear();
            foreach(string g, gen){
                this->currentAlbums[cAlbumRow].genre.push_back(g);
            }

            this->userAlbums.clear();

            foreach(ContainerAlbum a, *albums){
                if(a.artId == 0){
                    this->userAlbums.push_back(a);
                }
            }

            this->loadData();
        }


    }

}

void profile::deleteInPlaylists(int id){


    vector<ContainerPlaylist>::iterator itp = this->playlists->begin();

    foreach(ContainerPlaylist p, *this->playlists){
        int i = 0;

        foreach(ContainerSong s, p.songs){
            if(s.id == id){
                itp->songs.erase(itp->songs.begin() + i);
                break;
            }
            i++;
        }
        itp++;
    }

}


void profile::setData(vector<ContainerArtist> *artists,vector<ContainerAlbum> *albums,vector<ContainerSong> *songs, vector<ContainerGenre> *genres, int ar, vector<ContainerPlaylist> *playlists){
    this->currentSongs.clear();
    this->currentAlbums.clear();
    this->userAlbums.clear();


    this->playlists = playlists;
    this->albums = albums;
    this->artists = artists;
    this->songs = songs;
    this->genres = genres;

    foreach(ContainerAlbum a, *albums){
        if(a.artId == 0){
            this->currentAlbums.push_back(a);
            this->userAlbums.push_back(a);
        }
    }

    foreach(ContainerSong s, *songs){
        if(s.artId == 0){
            this->currentSongs.push_back(s);
        }
    }

    foreach(ContainerArtist a, *artists){
        if(a.id == ar){
            this->currentArtist = a;
            break;
        }
    }

    QStandardItemModel *combobox = new QStandardItemModel(0, 1);

    foreach(ContainerGenre genre, *genres){
        QStandardItem* name = new QStandardItem(QString(genre.name.c_str()));
        QStandardItem* id = new QStandardItem(QString(to_string(genre.id).c_str()));

        name->setFlags(Qt::ItemIsUserCheckable | Qt::ItemIsEnabled);
        name->setData(Qt::Unchecked, Qt::CheckStateRole);
        id->setFlags(Qt::ItemIsUserCheckable | Qt::ItemIsEnabled);
        id->setData(Qt::Unchecked, Qt::CheckStateRole);

        QList<QStandardItem*> row(5);
        row = {name, id};
        combobox->appendRow(row);
    }

    this->ui->genresBoxSong->setModel(combobox);
    this->ui->genresBoxAlbum->setModel(combobox);
    this->ui->genreTextEdit->setModel(combobox);

    this->ui->nameTextEdit->setText(this->currentArtist.name.c_str());
    this->ui->descTextEdit->setText(this->currentArtist.description.c_str());
}

void profile::showButtonClicked(){

    this->ui->nameTextEdit->setText(this->currentArtist.name.c_str());
    this->ui->descTextEdit->setText(this->currentArtist.description.c_str());

}

void profile::saveButtonClicked(){

    vector<string> gen;
    vector<ContainerGenre>::iterator it = this->genres->begin();

    for(int i = 0; i < this->genres->size(); i++){
        if(this->ui->genreTextEdit->model()->index(i,0).data().toString() == it->name.c_str()
                && (this->ui->genreTextEdit->model()->index(i,0).data(Qt::CheckStateRole).toInt() == 2) ){
            gen.push_back(it->name);
        }
        it++;
    }

    vector<ContainerArtist>::iterator ita = this->artists->begin();

    foreach(ContainerArtist a, *artists){
        if(a.id == this->currentArtist.id){
            ita->name = this->ui->nameTextEdit->text().toStdString();
            this->currentArtist.name = ita->name;
            ita->description = this->ui->descTextEdit->toPlainText().toStdString();
            this->currentArtist.description = ita->description;
            ita->genre.clear();
            this->currentArtist.genre.clear();
            foreach(string g, gen){
                ita->genre.push_back(g);
                this->currentArtist.genre.push_back(g);
            }

            break;
        }
        ita++;
    }

    QMessageBox reply;
    reply.setWindowTitle("Test");
    reply.setText("Personal data saved");
    reply.setStandardButtons(QMessageBox::Ok);
    if(reply.exec() == QMessageBox::Ok){
    }

}

