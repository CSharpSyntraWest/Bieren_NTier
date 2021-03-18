using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public static implicit operator Bier(EntityEntry<Bier> v)
        {
            throw new NotImplementedException();
        }

        public static explicit operator Bier(ValueTask<Bier> v)
        {
            throw new NotImplementedException();
        }
    }
}
