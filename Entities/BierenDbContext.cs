using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Configuration;
using Entities.Configuration;
#nullable disable

namespace Entities.Models
{
    public partial class BierenDbContext : DbContext
    {
        private readonly string _connString;
        public BierenDbContext()
        {
        }

        public BierenDbContext(DbContextOptions<BierenDbContext> options)
            : base(options)
        {
           // _connString = ConfigurationManager.ConnectionStrings["BierenDbCon"].ConnectionString;
        }

        public virtual DbSet<Bier> Bieren { get; set; }
        public virtual DbSet<Brouwer> Brouwers { get; set; }
        public virtual DbSet<Soort> Soorten { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//                //_connString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=BierenEnUsersDb;Integrated Security=True;Pooling=False"
////#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer(_connString);
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Bier>(entity =>
            {
                entity.HasKey(e => e.BierNr);

                entity.ToTable("Bier");

                entity.Property(e => e.Naam)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Brouwers)
                    .WithMany(p => p.Bieren)
                    .HasForeignKey(d => d.BrouwerNr);
                // .HasConstraintName("FK_Bieren_Brouwers");

                entity.HasOne(d => d.Soorten)
                    .WithMany(p => p.Bieren)
                    .HasForeignKey(d => d.SoortNr);
                    //.HasConstraintName("FK_Bieren_Soorten");
                entity.HasMany(e => e.Users);

            });

            modelBuilder.Entity<Brouwer>(entity =>
            {
                entity.HasKey(e => e.BrouwerNr);
                    //.HasName("PK_Brouwers");

                entity.ToTable("DbBrouwer");

                entity.Property(e => e.Adres)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BrNaam)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gemeente)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Soort>(entity =>
            {
                entity.HasKey(e => e.SoortNr);
                    //.HasName("PK_Soorten");

                entity.ToTable("Soort");

                entity.Property(e => e.SoortNaam)
                    .HasColumnName("Soort")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
              //  .HasName("PK_Users");
                entity.Property(e => e.UserId).ValueGeneratedOnAdd();
                entity.ToTable("User");
                entity.Property(e => e.Voornaam)
                    .HasMaxLength(50)
                    .IsUnicode(false); //indien false: varchar(50) indien true: nvarchar(50)
                entity.Property(e => e.Familienaam)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Email)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                //entity.Property(e => e.GeboorteDatum).HasColumnType("datetime");
                entity.HasMany(e => e.FavorieteBieren);
            });

            modelBuilder.ApplyConfiguration(new BrouwersConfiguration());
            modelBuilder.ApplyConfiguration(new BierSoortenConfiguration());
            modelBuilder.ApplyConfiguration(new BierenConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());

        
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
