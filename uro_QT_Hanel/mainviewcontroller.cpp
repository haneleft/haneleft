#include "mainviewcontroller.h"
#include "./ui_mainwindow.h"


mainViewController::mainViewController(Ui::MainWindow *mwindow, vector<ContainerArtist> &artists,vector<ContainerAlbum> &albums,vector<ContainerSong> &songs, vector<ContainerGenre> &genres, vector<ContainerPlaylist> *playlists)
{
    this->mw = mwindow;

    this->songs.clear();
    this->artists.clear();
    this->albums.clear();
    this->currentSongs.clear();
    this->currentAlbums.clear();
    this->currentArtists.clear();

    this->playlists = playlists;
    this->songs = songs;
    this->currentSongs = songs;
    this->albums = albums;
    this->currentAlbums = albums;
    this->artists = artists;
    this->currentArtists = artists;
    this->genres = genres;


    spl = new SongsInPlaylist();
    this->spl->hide();

    QStandardItemModel *combobox = new QStandardItemModel(0, 1);

    foreach(ContainerGenre genre, genres){
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

    this->mw->genreBox->setModel(combobox);


    this->loadData();
}

void mainViewController::loadData()
{


    QList<QString> values;

    values.push_back("Name");
    values.push_back("Live");
    values.push_back("Played");
    values.push_back("Album");
    values.push_back("Artist");

    this->songsModel = new QStandardItemModel( 0, values.size() );
    for(int i = 0; i < values.size(); i++){
        songsModel->setHorizontalHeaderItem(i, new QStandardItem( values[i]));
    }

    for(int i = 0; i < this->currentSongs.size(); i++){
        QStandardItem *name = new QStandardItem(QString(currentSongs[i].name.c_str()));
        QStandardItem *live = new QStandardItem(QString(to_string(currentSongs[i].live).c_str()));
        QStandardItem *played = new QStandardItem(QString(to_string(currentSongs[i].played).c_str()));
        QStandardItem *album = new QStandardItem(QString(this->findAlbum(currentSongs[i]).c_str() ));
        QStandardItem *artist = new QStandardItem(QString(this->findArtist(currentSongs[i]).c_str() ));

        name->setFlags(name->flags() & (~Qt::ItemIsEditable));
        live->setFlags(live->flags() & (~Qt::ItemIsEditable));
        played->setFlags(played->flags() & (~Qt::ItemIsEditable));
        album->setFlags(album->flags() & (~Qt::ItemIsEditable));
        artist->setFlags(artist->flags() & (~Qt::ItemIsEditable));


        QList<QStandardItem*> row(5);
        row = {name, live, played, album, artist};
        songsModel->appendRow(row);
    }

    this->mw->songTable->setModel(songsModel);
    this->mw->songTable->setSelectionBehavior(QAbstractItemView::SelectRows);
    this->mw->songTable->resizeColumnsToContents();
    this->mw->songTable->horizontalHeader()->setSectionResizeMode(QHeaderView::Stretch);

    values.clear();

    values.push_back("Name");
    values.push_back("Date of release");
    values.push_back("Genre");
    values.push_back("Artist");

    this->albumModel = new QStandardItemModel( 0, values.size() );
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

        QStandardItem *artist = new QStandardItem(QString(this->findArtist(currentAlbums[i]).c_str()));

        QStandardItem *genre = new QStandardItem(QString(this->getGenreString(currentAlbums[i].genre).c_str() ));

        name->setFlags(name->flags() & (~Qt::ItemIsEditable));
        date->setFlags(date->flags() & (~Qt::ItemIsEditable));
        artist->setFlags(artist->flags() & (~Qt::ItemIsEditable));
        genre->setFlags(genre->flags() & (~Qt::ItemIsEditable));


        QList<QStandardItem*> row(4);
        row = {name,date,genre, artist};
        albumModel->appendRow(row);
    }

    this->mw->albumTable->setModel(albumModel);
    this->mw->albumTable->setSelectionBehavior(QAbstractItemView::SelectRows);
    this->mw->albumTable->resizeColumnsToContents();
    this->mw->albumTable->horizontalHeader()->setSectionResizeMode(QHeaderView::Stretch);

    values.clear();

    values.push_back("Name");
    values.push_back("Genres");

    this->artistModel = new QStandardItemModel(0 , values.size() );
    for(int i = 0; i < values.size(); i++){
        artistModel->setHorizontalHeaderItem(i, new QStandardItem( values[i]));
    }

    for(int i = 0; i < this->currentArtists.size(); i++){
        QStandardItem *name = new QStandardItem(QString(currentArtists[i].name.c_str()));

        QStandardItem *genre = new QStandardItem(QString(this->getGenreString(currentArtists[i].genre).c_str() ));

        genre->setFlags(genre->flags() & (~Qt::ItemIsEditable));
        name->setFlags(name->flags() & (~Qt::ItemIsEditable));

        QList<QStandardItem*> row(2);
        row = {name, genre};
        artistModel->appendRow(row);
    }

    this->mw->artistTable->setModel(artistModel);
    this->mw->artistTable->setSelectionBehavior(QAbstractItemView::SelectRows);
    this->mw->artistTable->resizeColumnsToContents();
    this->mw->artistTable->horizontalHeader()->setSectionResizeMode(QHeaderView::Stretch);

    values.clear();

    values.push_back("Name");

    QStandardItemModel *playlistModel = new QStandardItemModel(0 , values.size() );
    for(int i = 0; i < values.size(); i++){
        playlistModel->setHorizontalHeaderItem(i, new QStandardItem( values[i]));
    }

    currentPlaylists.clear();

    foreach(ContainerPlaylist p , *playlists){
        if(p.artId == 0){
            currentPlaylists.push_back(p);
        }
    }

    for(int i = 0; i < this->currentPlaylists.size(); i++){
        QStandardItem *name = new QStandardItem(QString(currentPlaylists[i].name.c_str()));

        name->setFlags(name->flags() & (~Qt::ItemIsEditable));

        QList<QStandardItem*> row(1);
        row = {name};
        playlistModel->appendRow(row);
    }

    this->mw->playlistTable->setModel(playlistModel);
    this->mw->playlistTable->setSelectionBehavior(QAbstractItemView::SelectRows);
    this->mw->playlistTable->resizeColumnsToContents();
    this->mw->playlistTable->horizontalHeader()->setSectionResizeMode(QHeaderView::Stretch);

}

void mainViewController::search(){
    string searchName = this->mw->searchLine->text().toStdString();

    vector<string> gen;
    int alb;
    vector<ContainerGenre>::iterator it = this->genres.begin();

    for(int i = 0; i < this->genres.size(); i++){
        if(this->mw->genreBox->model()->index(i,0).data().toString() == it->name.c_str()
                && (this->mw->genreBox->model()->index(i,0).data(Qt::CheckStateRole).toInt() == 2) ){
            gen.push_back(it->name);
        }
        it++;
    }

    if(gen.size() <=0 && searchName == ""){
        this->currentSongs = songs;
        this->currentAlbums = albums;
        this->currentArtists = artists;

        this->loadData();
    }
    else if (gen.size() > 0 && searchName == ""){
        qInfo("halo");
        this->currentSongs.clear();
        this->currentAlbums.clear();
        this->currentArtists.clear();

        foreach(ContainerSong s, this->songs){
            if(s.genre == gen){
                this->currentSongs.push_back(s);
            }
            else{
                foreach(string g, gen){
                    foreach(string g2, s.genre){
                        if(g == g2){
                            this->currentSongs.push_back(s);
                            break;
                        }
                    }
                }
            }
        }

        foreach(ContainerAlbum a, this->albums){
            if(a.genre == gen){
                this->currentAlbums.push_back(a);
            }
            else{
                foreach(string g, gen){
                    foreach(string g2, a.genre){
                        if(g == g2){
                            this->currentAlbums.push_back(a);
                            break;
                        }
                    }
                }
            }
        }

        foreach(ContainerArtist a, this->artists){
            if(a.genre == gen){
                this->currentArtists.push_back(a);
            }
            else{
                foreach(string g, gen){
                    foreach(string g2, a.genre){
                        if(g == g2){
                            this->currentArtists.push_back(a);
                            break;
                        }
                    }
                }
            }
        }

        this->loadData();
    }
    else if(gen.size() <= 0 && searchName != ""){
        std::transform(searchName.begin(), searchName.end(), searchName.begin(),[](unsigned char c){ return std::tolower(c); });
        this->currentSongs.clear();
        this->currentAlbums.clear();
        this->currentArtists.clear();

        foreach(ContainerSong s, this->songs){
            string name = "";
            string album = "";
            string artist = "";

            foreach(char c , s.name){
                name += tolower(c);
            }

            foreach(char c , this->findAlbum(s)){
                album += tolower(c);
            }

            foreach(char c , this->findArtist(s)){
                artist += tolower(c);
            }

            if( name == searchName || album == searchName || artist == searchName){
                if(s.genre == gen){
                    this->currentSongs.push_back(s);
                }
                else{
                    foreach(string g, gen){
                        foreach(string g2, s.genre){
                            if(g == g2){
                                this->currentSongs.push_back(s);
                                break;
                            }
                        }
                    }
                }
            }
        }

        foreach(ContainerAlbum a, this->albums){
            string name = "";
            string artist = "";

            foreach(char c , a.name){
                name += tolower(c);
            }

            foreach(char c , this->findArtist(a)){
                artist += tolower(c);
            }


            if(name == searchName || artist == searchName){
                if(a.genre == gen){
                    this->currentAlbums.push_back(a);
                }
                else{
                    foreach(string g, gen){
                        foreach(string g2, a.genre){
                            if(g == g2){
                                this->currentAlbums.push_back(a);
                                break;
                            }
                        }
                    }
                }
            }
        }

        foreach(ContainerArtist a, this->artists){
            string name = "";

            foreach(char c , a.name){
                name += tolower(c);
            }

            if(name == searchName){
                if(a.genre == gen){
                    this->currentArtists.push_back(a);
                }
                else{
                    foreach(string g, gen){
                        foreach(string g2, a.genre){
                            if(g == g2){
                                this->currentArtists.push_back(a);
                                break;
                            }
                        }
                    }
                }
            }
        }

        this->loadData();
    }
    else{
        std::transform(searchName.begin(), searchName.end(), searchName.begin(),[](unsigned char c){ return std::tolower(c); });
        this->currentSongs.clear();
        this->currentAlbums.clear();
        this->currentArtists.clear();

        foreach(ContainerSong s, this->songs){
            string name = "";
            string album = "";
            string artist = "";

            foreach(char c , s.name){
                name += tolower(c);
            }

            foreach(char c , this->findAlbum(s)){
                album += tolower(c);
            }

            foreach(char c , this->findArtist(s)){
                artist += tolower(c);
            }

            if( name == searchName || album == searchName || artist == searchName){
                this->currentSongs.push_back(s);
            }
        }

        foreach(ContainerAlbum a, this->albums){
            string name = "";
            string artist = "";

            foreach(char c , a.name){
                name += tolower(c);
            }

            foreach(char c , this->findArtist(a)){
                artist += tolower(c);
            }


            if(name == searchName || artist == searchName){
                this->currentAlbums.push_back(a);
            }
        }

        foreach(ContainerArtist a, this->artists){
            string name = "";

            foreach(char c , a.name){
                name += tolower(c);
            }

            if(name == searchName){
                this->currentArtists.push_back(a);
            }
        }

        this->loadData();
    }


}


string mainViewController::findArtist(ContainerSong cs){
    foreach(ContainerArtist car, this->artists){
        if(car.id == cs.artId){
            return car.name;
        }
    }

    return "";
}

string mainViewController::findArtist(ContainerAlbum ca){
    foreach(ContainerArtist car, this->artists){
        if(car.id == ca.artId){
            return car.name;
        }
    }

    return "";
}

string mainViewController::findAlbum(ContainerSong cs){
    foreach(ContainerAlbum alb, this->albums){
        if(alb.id == cs.albumId){
            return alb.name;
        }
    }

    return "";
}

string mainViewController::getGenreString(vector<string> genres){
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

void mainViewController::onTableClickedSong(const QModelIndex &index, bool x)
{

    if (index.isValid()) {
        this->mw->detailView1->hide();
        this->mw->detailView2->hide();
        this->mw->detailView->show();
        this->mw->detailView->activateWindow();
        this->mw->detailView->raise();

        ContainerSong s;

        if(x == true){
            s = {this->currentSongs[index.row()].id,
                 this->currentSongs[index.row()].artId,
                 this->currentSongs[index.row()].albumId,
                 this->currentSongs[index.row()].name,
                 this->currentSongs[index.row()].live,
                 this->currentSongs[index.row()].played,
                 this->currentSongs[index.row()].genre,
                 this->currentSongs[index.row()].path
                };
        }
        else{
            s = {this->currentSongsDetail[index.row()].id,
                 this->currentSongsDetail[index.row()].artId,
                 this->currentSongsDetail[index.row()].albumId,
                 this->currentSongsDetail[index.row()].name,
                 this->currentSongsDetail[index.row()].live,
                 this->currentSongsDetail[index.row()].played,
                 this->currentSongsDetail[index.row()].genre,
                 this->currentSongsDetail[index.row()].path
                };
        }

        this->mw->songName->setText(s.name.c_str());
        this->mw->aName->setText(this->findArtist(s).c_str());
        this->mw->alName->setText(this->findAlbum(s).c_str());
        this->mw->liveName->setText(to_string(s.live).c_str());
        this->mw->playedName->setText(to_string(s.played).c_str());
        this->mw->genreName->setText(this->getGenreString(s.genre).c_str());

        string path = "background-image: url( "+ this->currentSongs[index.row()].path + "); background-size: auto auto; background-repeat: no-repeat;";


        this->mw->imgLabel->setStyleSheet(path.c_str());
    }
}

void mainViewController::onTableClickedArtist(const QModelIndex &index)
{

    if (index.isValid()) {
        this->mw->detailView1->hide();
        this->mw->detailView->hide();
        this->mw->detailView2->show();
        this->mw->detailView2->activateWindow();
        this->mw->detailView2->raise();

        this->mw->artistName->setText(this->currentArtists[index.row()].name.c_str());

        this->mw->genreNameArt->setText(this->getGenreString(this->currentArtists[index.row()].genre).c_str());
        this->mw->decriptionName->setText(this->currentArtists[index.row()].description.c_str());


        QList<QString> values;

        values.push_back("Name");
        values.push_back("Date of release");
        values.push_back("Genre");
        values.push_back("Artist");

        this->currentAlbumsDetail.clear();
        this->currentSongsDetail.clear();

        QStandardItemModel *albModel = new QStandardItemModel( 0, values.size() );
        for(int i = 0; i < values.size(); i++){
            albModel->setHorizontalHeaderItem(i, new QStandardItem( values[i]));
        }

        for(int i = 0; i < this->albums.size(); i++){
            if(this->currentArtists[index.row()].id == this->albums[i].artId){
                currentAlbumsDetail.push_back(this->albums[i]);

                QStandardItem *name = new QStandardItem(QString(albums[i].name.c_str()));

                string year = to_string(albums[i].date.year);
                string month = to_string(albums[i].date.month);
                string day = to_string(albums[i].date.day);

                string dat = year + "-" + month + "-" + day;

                QStandardItem *date = new QStandardItem(QString(dat.c_str()));

                QStandardItem *artist = new QStandardItem(QString(this->findArtist(albums[i]).c_str()));

                QStandardItem *genre = new QStandardItem(QString(this->getGenreString(albums[i].genre).c_str() ));

                name->setFlags(name->flags() & (~Qt::ItemIsEditable));
                date->setFlags(date->flags() & (~Qt::ItemIsEditable));
                artist->setFlags(artist->flags() & (~Qt::ItemIsEditable));
                genre->setFlags(genre->flags() & (~Qt::ItemIsEditable));


                QList<QStandardItem*> row(4);
                row = {name,date,genre, artist};
                albModel->appendRow(row);
            }
        }

        this->mw->albumsArtVIew->setModel(albModel);
        this->mw->albumsArtVIew->setSelectionBehavior(QAbstractItemView::SelectRows);
        this->mw->albumsArtVIew->resizeColumnsToContents();
        this->mw->albumsArtVIew->horizontalHeader()->setSectionResizeMode(QHeaderView::Stretch);


        values.clear();

        values.push_back("Name");
        values.push_back("Live");
        values.push_back("Played");
        values.push_back("Album");
        values.push_back("Artist");

        QStandardItemModel *sModel = new QStandardItemModel( 0, values.size() );
        for(int i = 0; i < values.size(); i++){
            sModel->setHorizontalHeaderItem(i, new QStandardItem( values[i]));
        }

        for(int i = 0; i < this->songs.size(); i++){
            if(this->currentArtists[index.row()].id == this->songs[i].artId){
                this->currentSongsDetail.push_back(songs[i]);

                QStandardItem *name = new QStandardItem(QString(songs[i].name.c_str()));
                QStandardItem *live = new QStandardItem(QString(to_string(songs[i].live).c_str()));
                QStandardItem *played = new QStandardItem(QString(to_string(songs[i].played).c_str()));
                QStandardItem *album = new QStandardItem(QString(this->findAlbum(songs[i]).c_str() ));
                QStandardItem *artist = new QStandardItem(QString(this->findArtist(songs[i]).c_str() ));

                name->setFlags(name->flags() & (~Qt::ItemIsEditable));
                live->setFlags(live->flags() & (~Qt::ItemIsEditable));
                played->setFlags(played->flags() & (~Qt::ItemIsEditable));
                album->setFlags(album->flags() & (~Qt::ItemIsEditable));
                artist->setFlags(artist->flags() & (~Qt::ItemIsEditable));


                QList<QStandardItem*> row(5);
                row = {name, live, played, album, artist};
                sModel->appendRow(row);
            }
        }

        this->mw->songsArtView->setModel(sModel);
        this->mw->songsArtView->setSelectionBehavior(QAbstractItemView::SelectRows);
        this->mw->songsArtView->resizeColumnsToContents();
        this->mw->songsArtView->horizontalHeader()->setSectionResizeMode(QHeaderView::Stretch);

    }
}

void mainViewController::update(){
    this->loadData();
    if(this->currentPlaylists.size() > 0){
        this->cPlaylistRow = 0;
        this->loadPlaylistSongs(0);

    }
}

void mainViewController::onTableClickedAlbum(const QModelIndex &index, bool x)
{

    if (index.isValid()) {
        this->mw->detailView->hide();
        this->mw->detailView2->hide();
        this->mw->detailView1->show();
        this->mw->detailView1->activateWindow();
        this->mw->detailView1->raise();

        ContainerAlbum a;

        if(x){
            a = this->currentAlbums[index.row()];
        }
        else{
            a = this->currentAlbumsDetail[index.row()];
        }

        string dat = to_string(a.date.year) + "-"
                + to_string(a.date.month)
                + "-" + to_string(a.date.day);

        this->mw->albumName->setText(a.name.c_str());
        this->mw->aNameAlb->setText(this->findArtist(a).c_str());
        this->mw->dateName->setText(dat.c_str());
        this->mw->genreNameAlb->setText(this->getGenreString(a.genre).c_str());

        QList<QString> values;

        values.push_back("Name");
        values.push_back("Live");
        values.push_back("Played");
        values.push_back("Album");
        values.push_back("Artist");

        this->currentAlbumsDetail.clear();
        this->currentSongsDetail.clear();

        QStandardItemModel *sModel = new QStandardItemModel( 0, values.size() );
        for(int i = 0; i < values.size(); i++){
            sModel->setHorizontalHeaderItem(i, new QStandardItem( values[i]));
        }

        for(int i = 0; i < this->songs.size(); i++){
            if(a.id == this->songs[i].albumId){
                this->currentSongsDetail.push_back(songs[i]);

                QStandardItem *name = new QStandardItem(QString(songs[i].name.c_str()));
                QStandardItem *live = new QStandardItem(QString(to_string(songs[i].live).c_str()));
                QStandardItem *played = new QStandardItem(QString(to_string(songs[i].played).c_str()));
                QStandardItem *album = new QStandardItem(QString(this->findAlbum(songs[i]).c_str() ));
                QStandardItem *artist = new QStandardItem(QString(this->findArtist(songs[i]).c_str() ));

                name->setFlags(name->flags() & (~Qt::ItemIsEditable));
                live->setFlags(live->flags() & (~Qt::ItemIsEditable));
                played->setFlags(played->flags() & (~Qt::ItemIsEditable));
                album->setFlags(album->flags() & (~Qt::ItemIsEditable));
                artist->setFlags(artist->flags() & (~Qt::ItemIsEditable));


                QList<QStandardItem*> row(5);
                row = {name, live, played, album, artist};
                sModel->appendRow(row);
            }
        }

        this->mw->songsAlbView->setModel(sModel);
        this->mw->songsAlbView->setSelectionBehavior(QAbstractItemView::SelectRows);
        this->mw->songsAlbView->resizeColumnsToContents();
        this->mw->songsAlbView->horizontalHeader()->setSectionResizeMode(QHeaderView::Stretch);

    }
}

void mainViewController::updateData( vector<ContainerArtist> &artists,vector<ContainerAlbum> &albums,vector<ContainerSong> &songs, vector<ContainerGenre> &genres, vector<ContainerPlaylist> *playlists){
    this->songs.clear();
    this->artists.clear();
    this->albums.clear();
    this->currentSongs.clear();
    this->currentAlbums.clear();
    this->currentArtists.clear();
    this->currentPlaylists.clear();

    this->songs = songs;
    this->currentSongs = songs;
    this->albums = albums;
    this->currentAlbums = albums;
    this->artists = artists;
    this->playlists = playlists;
    this->currentArtists = artists;

    foreach(ContainerPlaylist pl, *playlists){
        if(pl.artId == 0){
            this->currentPlaylists.push_back(pl);
        }

    }

    this->loadData();
}

void mainViewController::onTableClickedPlaylist(const QModelIndex &index){
    if(index.row() < this->currentPlaylists.size()){
        cPlaylistRow = index.row();
        cPlaylistId = this->currentPlaylists[index.row()].id;
    }
    this->loadPlaylistSongs(index.row());

}
void mainViewController::onTableClickedPlaylistSong(const QModelIndex &index){
    if(index.row() < currentPlaylistSongs.size()){
        this->mw->songPlaylistName->setText(currentPlaylistSongs[index.row()].name.c_str());
        this->cSongId = currentPlaylistSongs[index.row()].id;
        this->cSongRow = index.row();
    }
}

void mainViewController::addToPl(){

    this->spl->setData(this->songs, this->currentPlaylistSongs, playlists, cPlaylistId);

    this->spl->show();
}

void mainViewController::onTableClickedAddSong(const QModelIndex &index){
    this->cWindowId = this->currentWindowSongs[index.row()].id;
    this->cWindowRow = index.row();

}




void mainViewController::addPlButtonClick(){

    vector<ContainerPlaylist>::iterator pl = playlists->begin();

    if(this->playlists->size() == 0){
        vector<ContainerSong> s;
        ContainerPlaylist p = { 0, 0, this->mw->playlistTextEdit->text().toStdString(), s};

        this->playlists->push_back(p);
        this->currentPlaylists.push_back(p);
    }
    else{
        for(int i = 0; i < this->playlists->size(); i++){
            if(i == this->playlists->size() - 1){
                qInfo("%d", i);
                qInfo("Id %d", pl->id);
                vector<ContainerSong> s;
                ContainerPlaylist p = { pl->id, 0, this->mw->playlistTextEdit->text().toStdString(), s};

                this->playlists->push_back(p);
                this->currentPlaylists.push_back(p);

                break;
            }
            pl++;
        }
    }


    this->loadData();

}
void mainViewController::savePlButtonClick(){
    if(cPlaylistRow < this->currentPlaylistSongs.size()){
        vector<ContainerPlaylist>::iterator pl = playlists->begin();

        for(int i = 0; i < this->playlists->size(); i++){
            if(cPlaylistId == pl->id){
                pl->name = this->mw->playlistTextEdit->text().toStdString();
                this->currentPlaylists[cPlaylistRow].name  = this->mw->playlistTextEdit->text().toStdString();
                break;
            }
            pl++;
        }

        this->loadData();
    }
}

void mainViewController::deletePlButtonClick(){
    if(cPlaylistRow < this->currentPlaylistSongs.size()){
        vector<ContainerPlaylist>::iterator pl = playlists->begin();

        int i = 0;

        for(int i = 0; i < this->playlists->size(); i++){
            if(cPlaylistId == pl->id){
                this->playlists->erase(this->playlists->begin() + i);
                this->currentPlaylistSongs.erase(this->currentPlaylistSongs.begin() + cPlaylistRow);
                break;
            }
            pl++;
        }

        this->loadData();
    }
}

void mainViewController::loadPlaylistSongs(int id){
    QList<QString> values;

    this->currentPlaylistSongs.clear();

    this->mw->playlistTextEdit->setText(currentPlaylists[cPlaylistRow].name.c_str());

    values.push_back("Name");
    values.push_back("Live");
    values.push_back("Played");
    values.push_back("Album");
    values.push_back("Artist");

    QStandardItemModel *songsPModel = new QStandardItemModel( 0, values.size() );
    for(int i = 0; i < values.size(); i++){
        songsPModel->setHorizontalHeaderItem(i, new QStandardItem( values[i]));
    }

    for(int i = 0; i < this->currentPlaylists[id].songs.size(); i++){
        this->currentPlaylistSongs.push_back(this->currentPlaylists[id].songs[i]);
        QStandardItem *name = new QStandardItem(QString(this->currentPlaylists[id].songs[i].name.c_str()));
        QStandardItem *live = new QStandardItem(QString(to_string(this->currentPlaylists[id].songs[i].live).c_str()));
        QStandardItem *played = new QStandardItem(QString(to_string(this->currentPlaylists[id].songs[i].played).c_str()));
        QStandardItem *album = new QStandardItem(QString(this->findAlbum(this->currentPlaylists[id].songs[i]).c_str() ));
        QStandardItem *artist = new QStandardItem(QString(this->findArtist(this->currentPlaylists[id].songs[i]).c_str() ));

        name->setFlags(name->flags() & (~Qt::ItemIsEditable));
        live->setFlags(live->flags() & (~Qt::ItemIsEditable));
        played->setFlags(played->flags() & (~Qt::ItemIsEditable));
        album->setFlags(album->flags() & (~Qt::ItemIsEditable));
        artist->setFlags(artist->flags() & (~Qt::ItemIsEditable));


        QList<QStandardItem*> row(5);
        row = {name, live, played, album, artist};
        songsPModel->appendRow(row);
    }

    this->mw->songPlayTable->setModel(songsPModel);
    this->mw->songPlayTable->setSelectionBehavior(QAbstractItemView::SelectRows);
    this->mw->songPlayTable->resizeColumnsToContents();
    this->mw->songPlayTable->horizontalHeader()->setSectionResizeMode(QHeaderView::Stretch);
}
