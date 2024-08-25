using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Projekt_HAN0334.ORM
{
    public class Album
    {
        public int IdAlbum { get; set; }
        public string Name { get; set; }
        public DateTime? AlbumDate { get; set; }
        public int IdUser { get; set; }
        public string User { get; set; }
        public int Stars { get; set; }
    }
}
