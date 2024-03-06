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

            
        }

    }
}
