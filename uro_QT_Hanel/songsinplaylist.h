#ifndef SONGSINPLAYLIST_H
#define SONGSINPLAYLIST_H

#include <QDialog>
#include "containers.h"
#include <QHeaderView>
#include <vector>
#include <string>
#include <QDialog>
#include "QStandardItemModel.h"


namespace Ui {
class SongsInPlaylist;
}

class SongsInPlaylist : public QDialog
{
    Q_OBJECT

public:
    explicit SongsInPlaylist(QWidget *parent = nullptr);
    ~SongsInPlaylist();
    void setData(vector<ContainerSong> songs, vector<ContainerSong> playlistSongs, vector<ContainerPlaylist> *playlist, int id);
    void loadData();
public slots:
    void onTableClickedSong(const QModelIndex &index);
    void exitButtonClick();
    void addSongButtonClick();
    void search();
private:
    vector<ContainerSong> songs;
    vector<ContainerSong> currentSongs;
    vector<ContainerSong> playlistSongs;
    vector<ContainerPlaylist> *playlist;
    int cSongId;
    int cSongRow;
    int playlistId;
    Ui::SongsInPlaylist *ui;
};

#endif // SONGSINPLAYLIST_H
