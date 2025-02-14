using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_web1.Entities;

public class Game
{
    [Key] // Specifies the primary key
    public int Id { get; set; }

    [Required] // Ensures the `Name` field is mandatory
    [MaxLength(100)] // Sets a maximum length for the game name
    public string Name { get; set; } = string.Empty; // Default value for required property

    [Required] // Ensures the `GenreId` field is mandatory
    [ForeignKey(nameof(Genre))] // Specifies the foreign key relationship
    public int GenreId { get; set; }

    public Genre Genre { get; set; } = null!; // Non-nullable navigation property with a default initializer

    [Required] // Ensures the `Price` field is mandatory
    [Range(0, double.MaxValue)] // Sets a valid range for the price
    public decimal Price { get; set; }

    [Required] // Ensures the `ReleaseDate` field is mandatory
    public DateOnly ReleaseDate { get; set; }
}
