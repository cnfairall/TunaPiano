using TunaPiano.Models;
using Microsoft.EntityFrameworkCore;

namespace TunaPiano.Api
{
    public class SongApi
    {
        public static void Map(WebApplication app)
        {
            //list songs
            app.MapGet("/api/songs", (TunaPianoDbContext db) =>
            {
                return db.Songs;
            });

            //create song
            app.MapPost("/api/songs/new", (TunaPianoDbContext db, Song newSong, int genreId) =>
            {
                newSong.Genres = new List<Genre>();
                Genre genreToAdd = db.Genres.SingleOrDefault(g => g.Id == genreId);
                newSong.Genres.Add(genreToAdd);
                db.Songs.Add(newSong);
                db.SaveChanges();
                return Results.Created($"/api/songs/{newSong.Id}", newSong);
            });

            //delete song
            app.MapDelete("/api/songs/{id}", (TunaPianoDbContext db, int id) =>
            {
                Song songToDelete = db.Songs.SingleOrDefault(s => s.Id == id);
                if (songToDelete != null)
                {
                    db.Songs.Remove(songToDelete);
                    db.SaveChanges();
                    return Results.NoContent();
                }
                return Results.BadRequest("Song not found");
            });

            //update song
            app.MapPut("/api/songs/{id}", (TunaPianoDbContext db, Song updatedSong) =>
            {
                Song songToUpdate = db.Songs.SingleOrDefault(s => s.Id ==  updatedSong.Id);
                if (songToUpdate != null)
                {
                    songToUpdate.Title = updatedSong.Title;
                    songToUpdate.Album = updatedSong.Album;
                    songToUpdate.ArtistId = updatedSong.ArtistId;
                    songToUpdate.Length = updatedSong.Length;
                    songToUpdate.Genres = updatedSong.Genres;
                    db.SaveChanges();
                    return Results.Ok(songToUpdate);
                }
                return Results.BadRequest("No song found");
            });

            //get song details
            app.MapGet("/api/songs/{id}", (TunaPianoDbContext db, int id) =>
            {
                Song song = db.Songs
                         .Include(s => s.Artist)
                         .Include(s => s.Genres)
                         .SingleOrDefault(s => s.Id == id);
                if (song == null)
                {
                    return Results.BadRequest("No song found");
                }
                return Results.Ok(song);
            });

            //search songs by genre
            app.MapGet("/api/songs/genre/{genreId}", (TunaPianoDbContext db, int genreId) =>
            {
                List<Song> genreSongs = db.Songs
                                          .Where(s => s.Genres.Any(g => g.Id == genreId))
                                          .ToList();
                if (genreSongs == null)
                {
                    return null;
                }
                return Results.Ok(genreSongs);
            });
        }
    }
}

