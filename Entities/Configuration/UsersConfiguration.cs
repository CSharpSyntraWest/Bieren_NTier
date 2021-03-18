using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                     UserId = 1,
                     Voornaam = "Jos",
                     Familienaam="De Klos",
                     Email="jos.deKlos@gmail.com",
                     GeboorteDatum = new DateTime(1974,5,1)
                });
        }
    }
}
