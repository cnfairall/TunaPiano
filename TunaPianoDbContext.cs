using Microsoft.EntityFrameworkCore;
using TunaPiano.Models;
    public class TunaPianoDbContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public TunaPianoDbContext(DbContextOptions<TunaPianoDbContext> context) : base(context)
        {

        }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Song>().HasData(new Song[]
        {
            new Song {Id = 1, Title = "Sunday Morning", ArtistId = 1, Album = "Tragic Kingdom", Length = 4 },
            new Song {Id = 2, Title = "Just a Girl", ArtistId = 1, Album = "Tragic Kingdom", Length = 3 },
            new Song {Id = 3, Title = "My Name is Jonas", ArtistId = 2, Album = "Blue Album", Length = 5 },
            new Song {Id = 4, Title = "That Thing", ArtistId = 3, Album = "The Miseducation of Lauryn Hill", Length = 4 },
            new Song {Id = 5, Title = "They Won't Go When I Go", ArtistId = 4, Album = "Fulfillingness' First Finale", Length = 5 },
            new Song {Id = 6, Title = "Higher Ground", ArtistId = 4, Album = "Innervisions", Length = 6},
            new Song {Id = 7, Title = "Cornflake Girl", ArtistId = 5, Album = "Under The Pink", Length = 4},
            new Song {Id = 8, Title = "Lovin' You Baby", ArtistId = 6, Album = "No Time For Dreaming", Length = 6 },
            new Song {Id = 9, Title = "HUMBLE", ArtistId = 7, Album = "DAMN", Length = 4},
        });

        modelBuilder.Entity<Artist>().HasData(new Artist[] {
            new Artist {Id = 1, Name = "No Doubt", Age = 40, Bio = "first fave"},
            new Artist {Id = 2, Name = "Weezer", Age = 40, Bio = "stole my sister's tape" },
            new Artist {Id = 3, Name = "Lauryn Hill", Age = 40, Bio = "one album wonder"},
            new Artist {Id = 4, Name = "Stevie Wonder", Age = 80, Bio = "actually a diety"},
            new Artist {Id = 5, Name = "Tori Amos", Age = 50, Bio = "witch store vibes"},
            new Artist {Id = 6, Name = "Charles Bradley", Age = 70, Bio = "former James Brown impersonator"},
            new Artist {Id = 7, Name = "Kendrick Lamar", Age = 35, Bio = "cool dude"}
        });

        modelBuilder.Entity<Genre>().HasData(new Genre[]
        {
            new Genre {Id = 1, Description = "Rock"},
            new Genre {Id = 2, Description = "Hip-Hop"},
            new Genre {Id = 3, Description = "R&B"}
        });
    }
}

