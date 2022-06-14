using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace k2_s22825.Models
{
    public class MusicDbContext : DbContext
    {
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<MusicLabel> MusicLabels { get; set; }
        public DbSet<MusicianTrack> MusicianTracks { get; set; }
        public MusicDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Musician>(e =>
            {
                e.HasKey(e => e.IdMusician);
                e.Property(e => e.FirstName).IsRequired().HasMaxLength(30);
                e.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                e.Property(e => e.Nickname).HasMaxLength(20);

                e.HasData(new Musician
                {
                    IdMusician = 1,
                    FirstName = "Adam",
                    LastName = "Kopysz"
                });
                

                e.ToTable("Musician");
            });
            
            modelBuilder.Entity<Track>(e =>
            {
                e.HasKey(e => e.IdTrack);
                e.Property(e => e.TrackName).IsRequired().HasMaxLength(20);
                e.Property(e => e.Duration).IsRequired();
                e.Property(e => e.IdMusicAlbum);

                e.HasOne(e => e.Album)
                .WithMany(e => e.Tracks)
                .HasForeignKey(e => e.IdMusicAlbum)
                .OnDelete(DeleteBehavior.ClientCascade);

                e.HasData(new Track
                {
                    IdTrack = 1,
                    TrackName = "Moja zona",
                    Duration = 2.3F,
                    IdMusicAlbum = 1
                });

                e.ToTable("Track");
            });
            
            modelBuilder.Entity<MusicLabel>(e =>
            {
                e.HasKey(e => e.IdMusicLabel);
                e.Property(e => e.Name).IsRequired().HasMaxLength(50);

                e.HasData(new MusicLabel
                {
                    IdMusicLabel=1,
                    Name="Godtyru"
                });

                e.ToTable("MusicLabel");
            });

            modelBuilder.Entity<Album>(e =>
            {
                e.HasKey(e => e.IdAlbum);
                e.Property(e => e.AlbumName).IsRequired().HasMaxLength(30);
                e.Property(e => e.PublishDate).IsRequired();

                e.HasOne(e => e.MusicLabel)
                .WithMany(e => e.Albums)
                .HasForeignKey(e => e.IdMusicLabel)
                .OnDelete(DeleteBehavior.ClientCascade);

                e.HasData(new Album
                {
                    IdAlbum=1,
                    AlbumName = "Potrusz",
                    PublishDate = DateTime.Now,
                    IdMusicLabel=1
                });

                e.ToTable("Album");
            });

            modelBuilder.Entity<MusicianTrack>(e =>
            {
                e.HasKey(e => new { e.IdMusician, e.IdTrack });

                e.HasOne(e => e.Musician)
                .WithMany(e => e.MusicianTracks)
                .HasForeignKey(e => e.IdMusician)
                .OnDelete(DeleteBehavior.ClientSetNull);

                e.HasOne(e => e.Track)
                .WithMany(e => e.MusicianTracks)
                .HasForeignKey(e => e.IdTrack)
                .OnDelete(DeleteBehavior.ClientSetNull);

                e.HasData(new MusicianTrack
                {
                    IdMusician = 1,
                    IdTrack = 1
                });

                e.ToTable("Musician_Track");
            });
        }
    }
}
