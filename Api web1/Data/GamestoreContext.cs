using System;
using Api_web1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api_web1.Data
{
    public class GamestoreContext : DbContext
    {
        public GamestoreContext(DbContextOptions<GamestoreContext> options)
            : base(options)
        {
        }

        // DbSets for your entities
        public DbSet<Game> Games => Set<Game>();
        public DbSet<Genre> Genres => Set<Genre>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ensure base class initialization

            // Configure the Game entity
            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(g => g.Name)
                      .IsRequired()
                      .HasMaxLength(100); // Enforces maximum length of 100 for Name

                entity.Property(g => g.Price)
                      .HasPrecision(18, 2); // Configures precision for the Price property

                // Value converter for DateOnly to DateTime
                var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
                    dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue), // Convert DateOnly to DateTime
                    dateTime => DateOnly.FromDateTime(dateTime) // Convert DateTime back to DateOnly
                );

                entity.Property(g => g.ReleaseDate)
                      .HasConversion(dateOnlyConverter); // Apply the value converter

                // Configure the relationship between Game and Genre
                entity.HasOne(g => g.Genre)
                      .WithMany() // Indicates Genre can relate to many Game entities
                      .HasForeignKey(g => g.GenreId) // Configures GenreId as the foreign key
                      .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes
            });

            // Configure the Genre entity
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(g => g.Name)
                      .IsRequired()
                      .HasMaxLength(50); // Enforces maximum length of 50 for Name
            });

            // Seed data for Genres
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Adventure" },
                new Genre { Id = 3, Name = "RPG" },
                new Genre { Id = 4, Name = "Strategy" },
                new Genre { Id = 5, Name = "Simulation" }
            );

            // Seed data for Games
            modelBuilder.Entity<Game>().HasData(
                new Game { Id = 1, Name = "Game A", GenreId = 1, Price = 49.99m, ReleaseDate = DateOnly.Parse("2022-10-01") },
                new Game { Id = 2, Name = "Game B", GenreId = 2, Price = 59.99m, ReleaseDate = DateOnly.Parse("2021-08-15") },
                new Game { Id = 3, Name = "Game C", GenreId = 3, Price = 39.99m, ReleaseDate = DateOnly.Parse("2023-05-20") },
                new Game { Id = 4, Name = "Game D", GenreId = 4, Price = 29.99m, ReleaseDate = DateOnly.Parse("2020-03-10") },
                new Game { Id = 5, Name = "Game E", GenreId = 5, Price = 19.99m, ReleaseDate = DateOnly.Parse("2024-01-05") }
            );
        }
    }
}
