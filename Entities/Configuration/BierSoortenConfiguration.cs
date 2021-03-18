using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class BierSoortenConfiguration : IEntityTypeConfiguration<Soort>
    {
        public void Configure(EntityTypeBuilder<Soort> builder)
        {
            builder.HasData(
                new Soort
                {
                    SoortNr = 1,
                    SoortNaam = "Pils"
                });
        }
    }
}
