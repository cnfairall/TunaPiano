using TunaPiano.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace TunaPiano.Api
{
    public class GenreApi
    {
        public static void Map(WebApplication app)
        {
            //list genres
            app.MapGet("/api/genres", (TunaPianoDbContext db) =>
            {
                return db.Genres;
            });

            //create genre
            app.MapPost("/api/genres/new", (TunaPianoDbContext db, Genre newGenre) =>
            {
                db.Genres.Add(newGenre);
                db.SaveChanges();
                return Results.Created($"/api/genres/{newGenre.Id}", newGenre);
            });

            //delete genre
            app.MapDelete("/api/genres/{id}", (TunaPianoDbContext db, int id) =>
            {
                Genre genreToDelete = db.Genres.SingleOrDefault(g => g.Id == id);
                if (genreToDelete != null)
                {
                    db.Genres.Remove(genreToDelete);
                    db.SaveChanges();
                    return Results.NoContent();
                }
                return Results.BadRequest("Genre not found");
            });

            //update genre
            app.MapPut("/api/genres/{id}", (TunaPianoDbContext db, Genre updatedGenre) =>
            {
                Genre genreToUpdate = db.Genres.SingleOrDefault(g => g.Id ==  updatedGenre.Id);
                if (genreToUpdate != null)
                {
                    genreToUpdate.Description = updatedGenre.Description;
                    db.SaveChanges();
                    return Results.Ok(genreToUpdate);
                }
                return Results.BadRequest("Genre not found");
            });

            //get genre with songs
            app.MapGet("/api/genres/{id}", (TunaPianoDbContext db, int id) =>
            {
                Genre genre = db.Genres
                         .Include(g => g.Songs)
                         .SingleOrDefault(s => s.Id == id);
                if (genre == null)
                {
                    return Results.BadRequest("No genre found");
                }
                return Results.Ok(genre);
            });

            //get genres by number of songs
            app.MapGet("/api/genres/popular", (TunaPianoDbContext db) =>
            {
                IEnumerable<Genre> genresByCount = db.Genres
                                            .Include(g => (g as Derived).SongTotal)      
                                            .OrderByDescending(g => g.SongTotal)
                                            .ToList();
                return Results.Ok(genresByCount);
            });
        }

    }
}
