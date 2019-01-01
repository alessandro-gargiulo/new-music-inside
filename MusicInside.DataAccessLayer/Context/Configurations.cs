using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicInside.DataAccessLayer.AssociationClasses;
using MusicInside.DataAccessLayer.Models;

namespace MusicInside.DataAccessLayer.Context
{
    #region Single Class Configurations
    public class SongConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.ToTable("Song");

            #region Property Configurations
            builder.Property(p => p.Title)
                .IsRequired();

            builder.Property(p => p.TrackNo)
                .IsRequired()
                .HasDefaultValue(1);
            #endregion

            #region One-To-One Navigation Configurations
            builder.HasOne(p => p.Statistic)
                .WithOne(p => p.Song)
                .HasForeignKey<Song>(p => p.StatisticId)
                .IsRequired();

            builder.HasOne(p => p.Media)
                .WithOne(p => p.Song)
                .HasForeignKey<Song>(p => p.MediaId)
                .IsRequired();

            builder.HasOne(p => p.Media)
                .WithOne(p => p.Song)
                .HasForeignKey<Song>(p => p.MediaId)
                .IsRequired();
            #endregion

            #region One-To-Many Navigation Configurations
            builder.HasOne(p => p.Album)
                .WithMany(p => p.Songs)
                .HasForeignKey(p => p.AlbumId)
                .IsRequired();
            #endregion
        }
    }

    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable("Album");

            #region Property Configurations
            builder.Property(p => p.Title)
                .IsRequired();
            #endregion

            #region One-To-One Navigation Configurations
            builder.HasOne(p => p.Cover)
                .WithOne(p => p.Album)
                .HasForeignKey<Album>(p => p.CoverId)
                .IsRequired();
            #endregion

            #region One-To-Many Navigation Configurations
            builder.HasOne(p => p.Cover)
                .WithOne(p => p.Album)
                .HasForeignKey<Album>(p => p.CoverId)
                .IsRequired();
            #endregion
        }
    }

    public class CoverFileConfiguration : IEntityTypeConfiguration<CoverFile>
    {
        public void Configure(EntityTypeBuilder<CoverFile> builder)
        {
            builder.ToTable("CoverFile");

            #region Property Configurations
            builder.Property(p => p.Path).IsRequired();
            builder.Property(p => p.FileName).IsRequired();
            builder.Property(p => p.Extension).IsRequired();
            #endregion
        }
    }

    public class MediaFileConfiguration : IEntityTypeConfiguration<MediaFile>
    {
        public void Configure(EntityTypeBuilder<MediaFile> builder)
        {
            builder.ToTable("MediaFile");

            #region Property Configurations
            builder.Property(p => p.Path).IsRequired();
            builder.Property(p => p.FileName).IsRequired();
            builder.Property(p => p.Extension).IsRequired();
            #endregion
        }
    }

    public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.ToTable("Artist");
        }
    }

    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genre");
        }
    }

    public class MomentConfiguration : IEntityTypeConfiguration<Moment>
    {
        public void Configure(EntityTypeBuilder<Moment> builder)
        {
            builder.ToTable("Moment");
        }
    }

    public class StatisticConfiguration : IEntityTypeConfiguration<Statistic>
    {
        public void Configure(EntityTypeBuilder<Statistic> builder)
        {
            builder.ToTable("Statistic");
        }
    }
    #endregion

    #region Association Class Configurations
    public class SongGenreConfiguration : IEntityTypeConfiguration<SongGenre>
    {
        public void Configure(EntityTypeBuilder<SongGenre> builder)
        {
            builder.ToTable("SongGenre");

            #region Property Configurations
            builder.HasKey(k => new { k.SongId, k.GenreId });
            #endregion

            #region One-To-Many Navigation Configurations
            builder.HasOne(p => p.Genre)
                .WithMany(p => p.Songs)
                .HasForeignKey(p => p.GenreId)
                .IsRequired();

            builder.HasOne(p => p.Song)
                .WithMany(p => p.Genres)
                .HasForeignKey(p => p.SongId)
                .IsRequired();
            #endregion
        }
    }

    public class SongArtistConfiguration : IEntityTypeConfiguration<SongArtist>
    {
        public void Configure(EntityTypeBuilder<SongArtist> builder)
        {
            builder.ToTable("SongArtist");

            #region Property Configurations
            builder.HasKey(k => new { k.SongId, k.ArtistId });
            builder.Property(p => p.IsPrincipalArtist).IsRequired();
            #endregion

            #region One-To-Many Navigation Configurations
            builder.HasOne(p => p.Artist)
                .WithMany(p => p.Songs)
                .HasForeignKey(p => p.ArtistId)
                .IsRequired();

            builder.HasOne(p => p.Song)
                .WithMany(p => p.Artists)
                .HasForeignKey(p => p.SongId)
                .IsRequired();
            #endregion
        }
    }

    public class SongMomentConfiguration : IEntityTypeConfiguration<SongMoment>
    {
        public void Configure(EntityTypeBuilder<SongMoment> builder)
        {
            builder.ToTable("SongMoment");

            #region Property Configurations
            builder.HasKey(k => new { k.SongId, k.MomentId });
            #endregion

            #region One-To-Many Navigation Configurations
            builder.HasOne(p => p.Moment)
                .WithMany(p => p.Songs)
                .HasForeignKey(p => p.MomentId)
                .IsRequired();

            builder.HasOne(p => p.Song)
                .WithMany(p => p.Moments)
                .HasForeignKey(p => p.SongId)
                .IsRequired();
            #endregion
        }
    }
    #endregion
}
