#ifndef CONTAINERS_H
#define CONTAINERS_H

#include <vector>
#include <string>

using namespace std;

struct DateTime{
    int year;
    int month;
    int day;
};

struct ContainerSong {
    int id;
    int artId;
    int albumId;
    string name;
    int live;
    int played;
    vector<string> genre;
    string path;
};

struct ContainerAlbum {
    int id;
    int artId;
    string name;
    DateTime date;
    vector<string> genre;
};

struct ContainerArtist {
    int id;
    std::string name;
    vector<string> genre;
    string description;
};

struct ContainerGenre {
    int id;
    string name;
};

struct ContainerPlaylist{
    int id;
    int artId;
    string name;
    vector<ContainerSong> songs;
};

class Containers
{
public:
    Containers();
    vector<ContainerArtist> artists;
    vector<ContainerAlbum> albums;
    vector<ContainerSong> songs;
    vector<ContainerGenre> genres;
    vector<ContainerPlaylist> playlists;
};

#endif // CONTAINERS_H
