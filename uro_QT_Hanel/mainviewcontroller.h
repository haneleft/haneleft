#ifndef MAINVIEWCONTROLLER_H
#define MAINVIEWCONTROLLER_H

#include "containers.h"
#include <QMainWindow>
#include "QStandardItemModel"
#include "qtableview.h"
#include <QHeaderView>
#include <vector>
#include <string>
#include <QDialog>
#include "songsinplaylist.h"

using namespace std;

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class mainViewController
{
private:

    Ui::MainWindow *mw;
    vector<ContainerArtist> artists;
    vector<ContainerAlbum> albums;
    vector<ContainerSong> songs;
    vector<ContainerPlaylist> *playlists;
    vector<ContainerArtist> currentArtists;
    vector<ContainerAlbum> currentAlbums;
    vector<ContainerSong> currentSongs;
    vector<ContainerPlaylist> currentPlaylists;
    vector<ContainerSong> currentPlaylistSongs;
    vector<ContainerSong> currentWindowSongs;
    vector<ContainerAlbum> currentAlbumsDetail;
    vector<ContainerSong> currentSongsDetail;
    vector<ContainerGenre> genres;
    QStandardItemModel *artistModel;
    QStandardItemModel *albumModel;
    QStandardItemModel *songsModel;
    QDialog *win;
    int cSongId;
    int cSongRow;
    int cPlaylistId;
    int cPlaylistRow;
    int cWindowId;
    int cWindowRow;
    QTableView *view;
    QPushButton *but3;
    QPushButton *but2;
public:
    mainViewController(Ui::MainWindow *mwindow, vector<ContainerArtist> &artists,vector<ContainerAlbum> &albums,vector<ContainerSong> &songs, vector<ContainerGenre> &genres, vector<ContainerPlaylist> *playlists);
    void loadData();
    string findArtist(ContainerSong cs);
    string findArtist(ContainerAlbum ca);
    string findAlbum(ContainerSong cs);
    string getGenreString(vector<string> genres);
    void onTableClickedSong(const QModelIndex &index, bool x);
    void onTableClickedAlbum(const QModelIndex &index, bool x);
    void onTableClickedArtist(const QModelIndex &index);
    void onTableClickedPlaylist(const QModelIndex &index);
    void onTableClickedPlaylistSong(const QModelIndex &index);
    void onTableClickedAddSong(const QModelIndex &index);
    void addPlButtonClick();
    void savePlButtonClick();
    void deletePlButtonClick();
    void loadPlaylistSongs(int id);
    void addToPl();
    void updateData( vector<ContainerArtist> &artists,vector<ContainerAlbum> &albums,vector<ContainerSong> &songs, vector<ContainerGenre> &genres, vector<ContainerPlaylist> *playlists);
    void search();
    SongsInPlaylist *spl;
public slots:
    void update();
};

#endif // MAINVIEWCONTROLLER_H
