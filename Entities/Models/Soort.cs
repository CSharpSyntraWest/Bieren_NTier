using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Soort
    {
        public Soort()
        {
            Bieren = new HashSet<Bier>();
        }

        public int SoortNr { get; set; }
        public string SoortNaam { get; set; }

        public virtual ICollection<Bier> Bieren { get; set; }
    }
}
