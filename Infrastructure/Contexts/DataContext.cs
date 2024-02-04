

using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public partial class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{   
    //Ange alla tabeller som vi vill ska skapas i databasen och vad tabellen ska heta
    public virtual DbSet<BreedsEntity> Breeds { get; set; }
    public virtual DbSet<AddressesEntity> Addresses { get; set; }
    public virtual DbSet<BreedersEntity> Breeders { get; set; }
    public virtual DbSet<OwnersEntity> Owners { get; set; }
    public virtual DbSet<HorsesEntity> Horses { get; set; }


    //om vi vill ändra något/specificera upp något i en entitet så kan vi göra en override. Tex att något ska vara unikt eller vid sammansatt nyckel
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BreedsEntity>()
            .HasIndex(x => x.NameOfBreed)
            .IsUnique();

        //modelBuilder.Entity<HorsesEntity>()
        //    .HasOne(e => e.Owner)
        //    .WithMany(u => u.Horses)
        //    .OnDelete(DeleteBehavior.Restrict);
    }

    //i vissa fall lägger vi också connectionstring till databasen här i en override (on configuring. optionBuilder.UseSqlServer(@"")
    // Alternativt dependency injection i annat project
}
