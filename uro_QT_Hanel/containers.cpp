#include "containers.h"

Containers::Containers()
{
    vector<string> genre;

    genre.push_back("Dubstep");
    genre.push_back("Metal");
    ContainerArtist art = {0, "Sullivan king", genre, "All i need is someone else"};

    genre.clear();
    genre.push_back("Metal");
    genre.push_back("Rock");
    ContainerArtist art1 = {1, "SiM", genre, "Oh blah blah blah"};

    genre.clear();
    genre.push_back("Rap");
    ContainerArtist art2 = {2, "DMX", genre, "Hrrrrmmmmmmmmmm"};


    this->artists.push_back(art);
    this->artists.push_back(art1);
    this->artists.push_back(art2);

    //Albums

    genre.clear();
    genre.push_back("Dubstep");
    DateTime date = {2017, 9, 19};
    ContainerAlbum alb = {0, 0, "Singles", date, genre};
    this->albums.push_back(alb);

    date = {2011, 10, 12};
    genre.clear();
    genre.push_back("Metal");
    genre.push_back("Rock");
    alb = {1, 1, "Seeds of Hope", date, genre};
    this->albums.push_back(alb);


    genre.clear();
    genre.push_back("Rap");
    date = {2003, 2, 18};
    alb = {2, 2, "Cradle 2 the Grave", date, genre};
    this->albums.push_back(alb);

    //songs

    genre.clear();
    genre.push_back("Dubstep");
    ContainerSong song = {0, 0, 0, "Don't Care", 200, 666666, genre, "sking.png"};
    this->songs.push_back(song);

    genre.clear();
    genre.push_back("Dubstep");
    song = {1, 0, 0, "I'll Fight Back", 350, 1666666, genre, "sking.png"};
    this->songs.push_back(song);

    genre.clear();
    genre.push_back("Metal");
    song = {2, 1, 1, "KiLLiNG mE", 2, 6666, genre, "sim.png"};
    this->songs.push_back(song);

    genre.clear();
    genre.push_back("Rap");
    song = {3, 2, 2, "X Gon Give It To Ya", 15000, 3000050, genre, "dmx.png"};
    this->songs.push_back(song);


    ContainerGenre g = {0, "Pop"};
    this->genres.push_back(g);
    g = {1, "Metal"};
    this->genres.push_back(g);
    g = {2, "Rock"};
    this->genres.push_back(g);
    g = {3, "Dubstep"};
    this->genres.push_back(g);
    g = {4, "DnB"};
    this->genres.push_back(g);
    g = {5, "Rap"};
    this->genres.push_back(g);
    g = {6, "Trap"};
    this->genres.push_back(g);
    g = {7, "Hip-hop"};
    this->genres.push_back(g);

    vector<ContainerSong> son;
    song = {2, 1, 1, "KiLLiNG mE", 2, 6666, genre, "path"};
    son.push_back(song);
    song = {1, 0, 0, "I'll Fight Back", 350, 1666666, genre, "path"};
    son.push_back(song);

    ContainerPlaylist pl = {0, 0, "DAAAMN",son };

    this->playlists.push_back(pl);

}
