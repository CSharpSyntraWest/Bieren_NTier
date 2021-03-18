using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class BierenConfiguration : IEntityTypeConfiguration<Bier>
    {
        public void Configure(EntityTypeBuilder<Bier> builder)
        {
            builder.HasData(
                    new Bier
                    {
                        BierNr = 1,
                        Naam = "Heineken",
                        BrouwerNr = 1,
                        SoortNr = 1,
                        Alcohol = 5.2  
                    });
        }
    }
}
