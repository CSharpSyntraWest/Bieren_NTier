using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class BrouwersConfiguration : IEntityTypeConfiguration<Brouwer>
    {
        public void Configure(EntityTypeBuilder<Brouwer> builder)
        {
            builder.HasData(
                    new Brouwer
                    {
                         BrouwerNr = 1,
                         BrNaam="Artois",
                         Adres="ArtoisLaan 1",
                         PostCode=2500,
                         Gemeente="TestGemeente",
                         Omzet=10000
                    });
        }
    }
}
