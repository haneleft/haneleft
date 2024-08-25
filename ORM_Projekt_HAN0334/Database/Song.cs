using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Projekt_HAN0334.ORM
{
    public class Song
    {
        public int IdSong { get; set; }
        public string Name { get; set; }
        public TimeSpan Length { get; set; }
        public int IdOrder { get; set; }
        public int Listened { get; set; }
        public int Live { get; set; }
        public int IdAlbum { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public int  Stars { get; set; }
        public Collection<Genre> Genres { get; set; }
    }
}
