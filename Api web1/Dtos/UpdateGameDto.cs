namespace Api_web1.Dtos;

public record UpdateGameDto(string Name, int GenreId, decimal Price, DateOnly ReleaseDate);
