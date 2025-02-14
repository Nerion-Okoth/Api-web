namespace Api_web1.Dtos;

public record CreateGameDto(string Name, int GenreId, decimal Price, DateOnly ReleaseDate);
