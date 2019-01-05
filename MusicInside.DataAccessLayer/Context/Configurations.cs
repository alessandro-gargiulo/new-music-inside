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
            builder.Property(p => p.Title).IsRequired();

            builder.Property(p => p.TrackNo)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(p => p.Year).IsRequired(false);

            #endregion

            #region One-To-One Navigation Configurations
            builder.HasOne(p => p.Statistic)
                .WithOne(p => p.Song)
                .HasForeignKey<Song>(p => p.StatisticId);

            builder.HasOne(p => p.Media)
                .WithOne(p => p.Song)
                .HasForeignKey<Song>(p => p.MediaId);
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
            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.CoverId).IsRequired(false);
            #endregion

            #region One-To-One Navigation Configurations
            builder.HasOne(p => p.Cover)
                .WithOne(p => p.Album)
                .HasForeignKey<Album>(p => p.CoverId);
            #endregion
        }
    }

    public class CoverFileConfiguration : IEntityTypeConfiguration<CoverFile>
    {
        public void Configure(EntityTypeBuilder<CoverFile> builder)
        {
            builder.ToTable("CoverFile");

            #region Property Configurations
            builder.Property(p => p.Path).IsRequired(false);
            builder.Property(p => p.FileName).IsRequired(false);
            builder.Property(p => p.Extension).IsRequired(false);
            #endregion
        }
    }

    public class MediaFileConfiguration : IEntityTypeConfiguration<MediaFile>
    {
        public void Configure(EntityTypeBuilder<MediaFile> builder)
        {
            builder.ToTable("MediaFile");

            #region Property Configurations
            builder.Property(p => p.Path).IsRequired(false);
            builder.Property(p => p.FileName).IsRequired(false);
            builder.Property(p => p.Extension).IsRequired(false);
            #endregion
        }
    }

    public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.ToTable("Artist");

            #region Property Configurations
            builder.Property(p => p.Name).IsRequired(false);
            builder.Property(p => p.Surname).IsRequired(false);
            builder.Property(p => p.BirthYear).IsRequired(false);
            builder.Property(p => p.IsBand).IsRequired(false);
            #endregion
        }
    }

    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genre");

            #region Property Configurations
            builder.Property(p => p.Description).IsRequired();
            #endregion
        }
    }

    public class MomentConfiguration : IEntityTypeConfiguration<Moment>
    {
        public void Configure(EntityTypeBuilder<Moment> builder)
        {
            builder.ToTable("Moment");

            #region Property Configurations
            builder.Property(p => p.Description).IsRequired();
            #endregion
        }
    }

    public class StatisticConfiguration : IEntityTypeConfiguration<Statistic>
    {
        public void Configure(EntityTypeBuilder<Statistic> builder)
        {
            builder.ToTable("Statistic");

            #region Property Configurations
            builder.Property(p => p.NumPlay).IsRequired().HasDefaultValue(0);
            builder.Property(p => p.LastPlay).IsRequired(false);
            #endregion
        }
    }

    public class SlideConfiguration : IEntityTypeConfiguration<Slide>
    {
        public void Configure(EntityTypeBuilder<Slide> builder)
        {
            builder.ToTable("Slide");

            #region Property Configurations
            builder.Property(p => p.Source).IsRequired(false);
            builder.Property(p => p.ValidityFrom).IsRequired(false);
            builder.Property(p => p.ValidityTo).IsRequired(false);
            builder.Property(p => p.Section).IsRequired(false);
            builder.Property(p => p.Header).IsRequired(false);
            builder.Property(p => p.Text).IsRequired(false);
            builder.Property(p => p.AltText).IsRequired(false);
            builder.Property(p => p.Order).IsRequired(false);
            #endregion
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
            builder.HasKey(k => k.Id);
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
            builder.HasKey(k => k.Id);
            builder.Property(p => p.IsPrincipalArtist).IsRequired(false);
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
            builder.HasKey(k => k.Id);
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
