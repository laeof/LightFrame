using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Photographer> Photographers { get; set; }
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = Guid.Parse("262b4688-36b3-406e-9717-15ddb09b87d4"), RoleName = "Admin" },
            new Role { Id = Guid.Parse("1a2feeb0-31e1-47cf-b29a-e18a1d134364"), RoleName = "User" }
        );

        modelBuilder.Entity<Photographer>().HasData(
            new Photographer
            {
                Id = Guid.Parse("c8d33e49-2b69-469c-86ca-9ec5dbae3101"),
                Email = "photographer1@email.com",
                Name = "John Doe",
                PhotoUrl = "https://www.copyright.gov/engage/photographers/images/side-pic-1.jpg",
                WorkExperience = "2 years"
            },
            new Photographer
            {
                Id = Guid.Parse("ae9af145-4624-464c-96cd-66b6556921dc"),
                Email = "photographer2@email.com",
                Name = "Michael Johnson",
                PhotoUrl = "https://www.fujixpassion.com/wp-content/uploads/2023/08/Destaque-1.jpeg",
                WorkExperience = "3 years"
            }
        );

        modelBuilder.Entity<Room>().HasData(
            new Room { 
                Id = Guid.Parse("f2231291-e0c9-4823-a471-0e18c86a1e9c"),
                Address = "123 Main St, Springfield, IL 62701",
                Name = "Білий зал",
                PhotoUrl = ["https://artmix.kh.ua/wp-content/uploads/2021/02/bv5c0002sajt-white-glavnaya-1-1.jpg"],
                Price = 500,
            },
            new Room { 
                Id = Guid.Parse("160b2313-7bb9-4227-a6b6-5b0e70c201a0"),
                Address = "456 Oak Ave, Los Angeles, CA 90001",
                Name = "Чорний зал",
                PhotoUrl = ["https://artmix.kh.ua/wp-content/uploads/2020/02/bv5c0505-1.jpg"],
                Price = 550,
            },
            new Room { 
                Id = Guid.Parse("8aa73e16-8e81-499f-8dce-977ba605396a"),
                Address = "789 Pine Rd, Dallas, TX 75201",
                Name = "Затишний зал",
                PhotoUrl = ["https://artmix.kh.ua/wp-content/uploads/2020/02/655724130_3_1000x700_interernaya-fotostudiya-v-samom-tsentre-harkova-razvlecheniya-iskusstvo-foto-video.jpg"],
                Price = 450
            },
            new Room { 
                Id = Guid.Parse("53b11b95-56a4-4e72-9d1f-9b33f4ed3f68"),
                Address = "321 Maple Dr, New York, NY 10001",
                Name = "Світлий зал",
                PhotoUrl = ["https://artmix.kh.ua/wp-content/uploads/2020/02/ciklorama1.jpg"],
                Price = 650
            }
        );
    }
}