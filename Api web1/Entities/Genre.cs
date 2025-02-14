using System.ComponentModel.DataAnnotations;

namespace Api_web1.Entities;

public class Genre
{
    [Key] // Specifies the primary key
    public int Id { get; set; }

    [Required] // Ensures the `Name` field is mandatory
    [MaxLength(50)] // Sets a maximum length for the genre name
    public string Name { get; set; } = string.Empty; // Default value for required property
}
