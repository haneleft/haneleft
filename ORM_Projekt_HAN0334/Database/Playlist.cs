using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Projekt_HAN0334.ORM
{
    public class Playlist
    {
        public int IdPlaylist { get; set; }
        public int IdUser { get; set; }
        public TimeSpan Length { get; set; }
        public string Name { get; set; }
        public string FakeTime { get; set; }
    }
}
