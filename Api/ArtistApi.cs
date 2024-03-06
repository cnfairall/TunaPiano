using TunaPiano.Models;
using Microsoft.EntityFrameworkCore;

namespace TunaPiano.Api
{
    public class ArtistApi
    {
        public static void Map(WebApplication app)
        {
            //list artists
            app.MapGet("/api/artists", (TunaPianoDbContext db) =>
            {
                return db.Artists;
            });

            //create artist
            app.MapPost("/api/artists/new", (TunaPianoDbContext db, Artist artist) =>
            {
                db.Artists.Add(artist);
                db.SaveChanges();
                return Results.Created($"/api/artists/{artist.Id}", artist);
            });

            //delete artist
            app.MapDelete("/api/artists/{id}", (TunaPianoDbContext db, int id) =>
            {
                Artist artistToDelete = db.Artists.SingleOrDefault(a => a.Id == id);
                if (artistToDelete != null)
                {
                    db.Artists.Remove(artistToDelete);
                    db.SaveChanges();
                    return Results.NoContent();
                }
                return Results.BadRequest("Artist not found");
            });

            //update song
            app.MapPut("/api/artists/{id}", (TunaPianoDbContext db, Artist updatedArtist) =>
            {
                Artist artistToUpdate = db.Artists.SingleOrDefault(a => a.Id == updatedArtist.Id);
                if (artistToUpdate != null)
                {
                    artistToUpdate.Name = updatedArtist.Name;
                    artistToUpdate.Age = updatedArtist.Age;
                    artistToUpdate.Bio = updatedArtist.Bio;
                    db.SaveChanges();
                    return Results.Ok(artistToUpdate);
                }
                return Results.BadRequest("No artist found");
            });

            //get artist details
            app.MapGet("/api/artists/{id}", (TunaPianoDbContext db, int id) =>
            {
                Artist artist = db.Artists
                         .Include(a => a.Songs)
                         .SingleOrDefault(s => s.Id == id);
                if (artist == null)
                {
                    return Results.BadRequest("No artist found");
                }
                return Results.Ok(artist);
            });

            //search artists by genre
            app.MapGet("/api/artists/genre/{genreId}", (TunaPianoDbContext db, int genreId) =>
            {
                var genre = db.Genres.SingleOrDefault(g => g.Id == genreId);
                if (genre == null)
                {
                    return Results.BadRequest("No genre found");
                }

                var genreSongs = db.Songs.Where(s => s.Genres.Contains(genre)).ToList();
                if (genreSongs == null)
                {
                    return Results.NotFound("No songs found for genre");
                }
                var genreArtists = new List<Artist>();
                foreach (Song song in genreSongs)
                {
                    var artist = db.Artists.SingleOrDefault(a => a.Songs.Contains(song));
                    genreArtists.Add(artist);
                }
                return Results.Ok(genreArtists);
            });

            //get related artists
            app.MapGet("/api/{artistId}/related", (TunaPianoDbContext db, int artistId) =>
            {
                var selectedArtist = db.Artists
                .Include(a => a.Songs)
                .ThenInclude(s => s.Genres)
                .SingleOrDefault(a => a.Id == artistId);

                if (selectedArtist == null)
                {
                    return Results.BadRequest("No artist found");
                }

                var artistGenres = new List<Genre>();

                foreach (Song song in selectedArtist.Songs)
                {
                    foreach (var genre in song.Genres)
                    {
                       var artistGenre = genre;
                       artistGenres.Add(artistGenre);
                    }
                }

                var genreArtists = new List<Artist>();
         
                foreach (Genre genre in artistGenres)
                {
                    var artists = db.Artists.Where(a => a.Songs.Any(s => s.Genres.Contains(genre))).ToList();
                    foreach (var a in artists)
                    {
                        genreArtists.Add(a);
                    }
                }
                genreArtists.RemoveAll(a => a.Id == selectedArtist.Id);
                return Results.Ok(genreArtists.Distinct());
            });
        }

    }
}
