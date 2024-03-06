namespace TunaPiano.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<Song> Songs { get; set; }
        public virtual int? SongTotal
        {
            get
            {
                if (Songs != null)
                {
                    return Songs.Count;
                }
                return null;
            }
        }

    }
}
