#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include "containers.h"
#include "mainviewcontroller.h"
#include "profile.h"
#include "options.h"


QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();
    void loadMainData();
private:
    Ui::MainWindow *ui;
    profile *p;
    mainViewController *mvc;
    Containers *c;
    bool pl = false;
    options *op;
private slots:
    void onTableClickedSong(const QModelIndex &index);
    void onTableClickedArtist(const QModelIndex &index);
    void onTableClickedAlbum(const QModelIndex &index);
    void onTableClickedSong2(const QModelIndex &index);
    void onTableClickedAlbum2(const QModelIndex &index);
    void onTableClickedPlaylist(const QModelIndex &index);
    void onTableClickedPlaylistSong(const QModelIndex &index);
    void buttonClickSearch();
    void buttonClickProfile();
    void buttonClickPlaylists();
    void buttonClickAddPlaylist();
    void buttonClickSavePlaylist();
    void buttonClickDeletePlaylist();
    void buttonClickAddToPl();
    void showThis();
    void lightMode();
    void darkMode();
    void font10Set();
    void font15Set();
    void font20Set();
};
#endif // MAINWINDOW_H
