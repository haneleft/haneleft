using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Projekt_HAN0334.ORM
{
    public class Comments
    {
        public int IdComment { get; set; }
        public string Text { get; set; }
        public int Stars { get; set; }
        public DateTime? Date { get; set; }
        public int? IdSong { get; set; }
        public int? IdUser { get; set; }
        public int? IdAlbum { get; set; }
    }
}
