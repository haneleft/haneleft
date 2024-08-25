#include "mainwindow.h"
#include "./ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    ui->detailView->hide();
    ui->detailView1->hide();
    ui->detailView2->hide();
    ui->searchTable->setCurrentIndex(0);
    ui->playlistWidget->hide();

    op = new options();

    ui->detailView->setStyleSheet("background-color: #52595D;");
    ui->detailView1->setStyleSheet("background-color: #52595D;");
    ui->detailView2->setStyleSheet("background-color: #52595D;");
    ui->ls10->setStyleSheet("color: white");
    ui->ls2->setStyleSheet("color: white");
    ui->ls3->setStyleSheet("color: white");
    ui->ls4->setStyleSheet("color: white");
    ui->ls6->setStyleSheet("color: white");
    ui->ls5->setStyleSheet("color: white");
    ui->ls7->setStyleSheet("color: white");
    ui->ls8->setStyleSheet("color: white");
    ui->ls9->setStyleSheet("color: white");
    ui->ls1->setStyleSheet("color: white");
    ui->artistName->setStyleSheet("color: white");
    ui->songName->setStyleSheet("color: white");
    ui->albumName->setStyleSheet("color: white");
    ui->decriptionName->setStyleSheet("color: white");
    ui->genreName->setStyleSheet("color: white");
    ui->genreNameArt->setStyleSheet("color: white");
    ui->genreNameAlb->setStyleSheet("color: white");
    ui->aNameAlb->setStyleSheet("color: white");
    ui->aName->setStyleSheet("color: white");
    ui->aNameArt->setStyleSheet("color: white");
    ui->dateName->setStyleSheet("color: white");
    ui->alName->setStyleSheet("color: white");
    ui->liveName->setStyleSheet("color: white");
    ui->playedName->setStyleSheet("color: white");
    ui->pushButton->setStyleSheet("color: #52595D; background : white;");
    ui->songsArtView->setStyleSheet("background : white;");
    ui->songsAlbView->setStyleSheet("background : white;");
    ui->playlistSong->setStyleSheet("background: #52595D;");
    ui->previousButton->setStyleSheet("color: #52595D; background : white;");
    ui->playButtonPl->setStyleSheet("color: #52595D; background : white;");
    ui->nextButton->setStyleSheet("color: #52595D; background : white;");
    ui->songPlaylistName->setStyleSheet("color : white;");

    p = new profile();
    c = new Containers();

    this->loadMainData();

    this->mvc = new mainViewController(ui, c->artists, c->albums, c->songs, c->genres, &c->playlists);

    connect(this->ui->songTable, SIGNAL(clicked(const QModelIndex &)), this, SLOT(onTableClickedSong(const QModelIndex &)));
    connect(this->ui->albumTable, SIGNAL(clicked(const QModelIndex &)), this, SLOT(onTableClickedAlbum(const QModelIndex &)));
    connect(this->ui->artistTable, SIGNAL(clicked(const QModelIndex &)), this, SLOT(onTableClickedArtist(const QModelIndex &)));
    connect(this->ui->songsAlbView, SIGNAL(clicked(const QModelIndex &)), this, SLOT(onTableClickedSong2(const QModelIndex &)));
    connect(this->ui->songsArtView, SIGNAL(clicked(const QModelIndex &)), this, SLOT(onTableClickedSong2(const QModelIndex &)));
    connect(this->ui->albumsArtVIew, SIGNAL(clicked(const QModelIndex &)), this, SLOT(onTableClickedAlbum2(const QModelIndex &)));
    connect(this->ui->playlistTable, SIGNAL(clicked(const QModelIndex &)), this, SLOT(onTableClickedPlaylist(const QModelIndex &)));
    connect(this->ui->songPlayTable, SIGNAL(clicked(const QModelIndex &)), this, SLOT(onTableClickedPlaylistSong(const QModelIndex &)));
    connect(this->ui->searchButton, SIGNAL(clicked()), this, SLOT(buttonClickSearch()));
    connect(this->ui->profileButton, SIGNAL(clicked()), this, SLOT(buttonClickProfile()));
    connect(this->ui->playlistButton, SIGNAL(clicked()), this, SLOT(buttonClickPlaylists()));
    connect(this->ui->addPlaylistButton, SIGNAL(clicked()), this, SLOT(buttonClickAddPlaylist()));
    connect(this->ui->savePlaylistButton, SIGNAL(clicked()), this, SLOT(buttonClickSavePlaylist()));
    connect(this->ui->deletePlaylistButton, SIGNAL(clicked()), this, SLOT(buttonClickDeletePlaylist()));
    connect(this->ui->addToPlButton, SIGNAL(clicked()), this, SLOT(buttonClickAddToPl()));
    connect(this->ui->lightMode, SIGNAL(triggered()), this, SLOT(lightMode()));
    connect(this->ui->darkMode, SIGNAL(triggered()), this, SLOT(darkMode()));
    connect(this->ui->font10, SIGNAL(triggered()), this, SLOT(font10Set()));
    connect(this->ui->font15, SIGNAL(triggered()), this, SLOT(font15Set()));
    connect(this->ui->font20, SIGNAL(triggered()), this, SLOT(font20Set()));
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::onTableClickedSong(const QModelIndex &index){
    this->mvc->onTableClickedSong(index, true);
}

void MainWindow::onTableClickedAlbum(const QModelIndex &index){
    this->mvc->onTableClickedAlbum(index, true);
}

void MainWindow::onTableClickedArtist(const QModelIndex &index){
    this->mvc->onTableClickedArtist(index);
}

void MainWindow::buttonClickSearch(){
    this->mvc->search();
}

void MainWindow::buttonClickProfile(){
    p->setData(&c->artists, &c->albums, &c->songs, &c->genres, 0, &c->playlists);
    p->loadData();
    p->show();
    connect(p, SIGNAL(comeBack()), this, SLOT(showThis()));
    this->hide();
}

void MainWindow::showThis(){
    p->hide();

    this->mvc->updateData(c->artists, c->albums, c->songs, c->genres, &c->playlists);
    this->show();
}

void MainWindow::onTableClickedSong2(const QModelIndex &index){
    this->mvc->onTableClickedSong(index, false);
}

void MainWindow::onTableClickedAlbum2(const QModelIndex &index){
    this->mvc->onTableClickedAlbum(index, false);
}

void MainWindow::buttonClickPlaylists(){
    if(this->pl == false){
        this->mvc->updateData(c->artists, c->albums, c->songs, c->genres, &c->playlists);
        this->ui->playlistWidget->show();
        this->pl = true;
    }
    else{
        this->ui->playlistWidget->hide();
        this->pl = false;
    }

}

void MainWindow::buttonClickAddPlaylist(){
    this->mvc->addPlButtonClick();
}

void MainWindow::buttonClickSavePlaylist(){
    this->mvc->savePlButtonClick();
}

void MainWindow::buttonClickDeletePlaylist(){
    this->mvc->deletePlButtonClick();
}

void MainWindow::buttonClickAddToPl(){
    this->mvc->addToPl();
}

void MainWindow::onTableClickedPlaylist(const QModelIndex &index){
    this->mvc->onTableClickedPlaylist(index);
}

void MainWindow::onTableClickedPlaylistSong(const QModelIndex &index){
    this->mvc->onTableClickedPlaylistSong(index);
}

void MainWindow::loadMainData(){
}

void MainWindow::lightMode(){

}

void MainWindow::darkMode(){

}

void MainWindow::font10Set(){
    ui->ls10->setStyleSheet("font: 15px Arial, sans-serif");
    ui->ls2->setStyleSheet("font: 15px Arial, sans-serif");
    ui->ls3->setStyleSheet("font: 15px Arial, sans-serif");
    ui->ls4->setStyleSheet("font: 15px Arial, sans-serif");
    ui->ls6->setStyleSheet("font: 15px Arial, sans-serif");
    ui->ls5->setStyleSheet("font: 15px Arial, sans-serif");
    ui->ls7->setStyleSheet("font: 15px Arial, sans-serif");
    ui->ls8->setStyleSheet("font: 15px Arial, sans-serif");
    ui->ls9->setStyleSheet("font: 15px Arial, sans-serif");
    ui->ls1->setStyleSheet("font: 15px Arial, sans-serif");
    ui->artistName->setStyleSheet("color: white");
    ui->songName->setStyleSheet("color: white");
    ui->albumName->setStyleSheet("color: white");
    ui->decriptionName->setStyleSheet("font: 15px Arial, sans-serif");
    ui->genreName->setStyleSheet("font: 15px Arial, sans-serif");
    ui->genreNameArt->setStyleSheet("font: 15px Arial, sans-serif");
    ui->genreNameAlb->setStyleSheet("font: 15px Arial, sans-serif");
    ui->aNameAlb->setStyleSheet("font: 15px Arial, sans-serif");
    ui->aName->setStyleSheet("font: 15px Arial, sans-serif");
    ui->aNameArt->setStyleSheet("font: 15px Arial, sans-serif");
    ui->dateName->setStyleSheet("font: 15px Arial, sans-serif");
    ui->alName->setStyleSheet("font: 15px Arial, sans-serif");
    ui->liveName->setStyleSheet("font: 15px Arial, sans-serif");
    ui->playedName->setStyleSheet("font: 15px Arial, sans-serif");
}

void MainWindow::font15Set(){

}

void MainWindow::font20Set(){

}

