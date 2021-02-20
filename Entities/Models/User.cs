using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class User
    {
        public User()
        {
            FavorieteBieren = new HashSet<Bier>();
        }
        public int UserId { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public DateTime GeboorteDatum { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Bier> FavorieteBieren { get; set; }
    }
}
