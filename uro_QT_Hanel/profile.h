#ifndef PROFILE_H
#define PROFILE_H

#include "containers.h"
#include <QMainWindow>
#include <string>
#include <vector>
#include <QMessageBox>

using namespace std;

namespace Ui {
class profile;
}

class profile : public QMainWindow
{
    Q_OBJECT

private:
    vector<ContainerArtist> *artists;
    vector<ContainerAlbum> *albums;
    vector<ContainerSong> *songs;
    vector<ContainerGenre> *genres;
    vector<ContainerPlaylist> *playlists;
    vector<ContainerAlbum> currentAlbums;
    vector<ContainerSong> currentSongs;
    vector<ContainerAlbum> userAlbums;
    ContainerArtist currentArtist;
    int cSongRow;
    int cSongId;
    int cAlbumId;
    int cAlbumRow;
    Ui::profile *ui;
public:
    profile(QWidget *parent = nullptr);
    ~profile();
    void loadData();
    string findAlbum(ContainerSong ca);
    string getGenreString(vector<string> genres);
    void deleteInPlaylists(int id);
    void setData(vector<ContainerArtist> *artists,vector<ContainerAlbum> *albums,vector<ContainerSong> *songs, vector<ContainerGenre> *genres, int ar, vector<ContainerPlaylist> *playlists);
public slots:
    void returnButtonClick();
    void songsTableClicked(const QModelIndex &index);
    void albumsTableClicked(const QModelIndex &index);
    void addButtonClickedSong();
    void deleteButtonClickedSong();
    void saveButtonClickedSong();
    void addButtonClickedAlbum();
    void deleteButtonClickedAlbum();
    void saveButtonClickedAlbum();
    void showButtonClicked();
    void saveButtonClicked();
signals:
    void comeBack();
};

#endif // PROFILE_H
