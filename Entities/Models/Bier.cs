using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Bier
    {
        public Bier()
        {
            Users = new HashSet<User>();
        }
        public int BierNr { get; set; }
        public string Naam { get; set; }
        public int? BrouwerNr { get; set; }
        public int? SoortNr { get; set; }
        public double? Alcohol { get; set; }

        public virtual Brouwer Brouwers { get; set; }
        public virtual Soort Soorten { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}
