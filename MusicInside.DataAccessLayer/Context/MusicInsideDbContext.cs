using Microsoft.EntityFrameworkCore;
using MusicInside.DataAccessLayer.Models;

namespace MusicInside.DataAccessLayer.Context
{
    public class MusicInsideDbContext : DbContext
    {
        public MusicInsideDbContext(DbContextOptions<MusicInsideDbContext> options) : base(options) { }

        #region Declaration Of DbSet
        public DbSet<Song> Songs { get; set; }
        public DbSet<Moment> Moments { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<CoverFile> Covers { get; set; }
        public DbSet<MediaFile> Medias { get; set; }
        public DbSet<Slide> Slides { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SongConfiguration());
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
            modelBuilder.ApplyConfiguration(new MediaFileConfiguration());
            modelBuilder.ApplyConfiguration(new CoverFileConfiguration());
            modelBuilder.ApplyConfiguration(new ArtistConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new MomentConfiguration());
            modelBuilder.ApplyConfiguration(new StatisticConfiguration());
            modelBuilder.ApplyConfiguration(new SlideConfiguration());

            modelBuilder.ApplyConfiguration(new SongGenreConfiguration());
            modelBuilder.ApplyConfiguration(new SongArtistConfiguration());
            modelBuilder.ApplyConfiguration(new SongMomentConfiguration());
        }
    }
}
