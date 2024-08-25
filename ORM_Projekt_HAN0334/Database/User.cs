using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Projekt_HAN0334.ORM
{
    public class User
    {
        public int IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }
        public string ArtistName { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public string Status { get; set; }
        public Collection<Genre> Genres { get; set; }
    }
}
