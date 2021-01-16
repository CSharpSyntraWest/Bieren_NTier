using System;
using System.Collections.Generic;
using System.Text;

namespace Bieren.BusinessLayer.Models
{
    public class BO_User
    {
        public BO_User()
        {
            FavorieteBieren = new List<BO_Bier>();
        }
        public int UserNr { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public DateTime GeboorteDatum { get; set; }

        public string Email { get; set; }
        public IList<BO_Bier> FavorieteBieren { get; set; }

    }
}
