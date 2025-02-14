using Api_web1.Data;
using Api_web1.Dtos;
using Api_web1.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api_web1.Endpoints;

public static class GamesEndpoints
{
    private const string GetGameEndpointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
            .WithParameterValidation();

        // GET all games
        group.MapGet("/", async (GamestoreContext dbContext) =>
        {
            var games = await dbContext.Games
                .Include(game => game.Genre)
                .Select(game => new GameDto(
                    game.Id,
                    game.Name,
                    game.Genre != null ? game.Genre.Name : "Unknown", // Fixed
                    game.Price,
                    game.ReleaseDate
                ))
                .ToListAsync();

            return Results.Ok(games);
        });

        // GET a single game by ID
        group.MapGet("/{id:int}", async (int id, GamestoreContext dbContext) =>
        {
            var game = await dbContext.Games
                .Include(g => g.Genre)
                .Where(g => g.Id == id)
                .Select(g => new GameDto(
                    g.Id,
                    g.Name,
                    g.Genre != null ? g.Genre.Name : "Unknown", // Fixed
                    g.Price,
                    g.ReleaseDate
                ))
                .FirstOrDefaultAsync();

            return game is null
                ? Results.NotFound(new { Message = $"Game with ID {id} not found." })
                : Results.Ok(game);
        })
        .WithName(GetGameEndpointName);

        // POST a new game
        group.MapPost("/", async (CreateGameDto newGame, GamestoreContext dbContext) =>
        {
            var genre = await dbContext.Genres.FindAsync(newGame.GenreId);
            if (genre == null)
            {
                return Results.BadRequest(new { Message = $"Genre with ID {newGame.GenreId} does not exist." });
            }

            var game = new Game
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            var createdGame = new GameDto(
                game.Id,
                game.Name,
                genre.Name,
                game.Price,
                game.ReleaseDate
            );

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, createdGame);
        });

        // PUT update an existing game
        group.MapPut("/{id:int}", async (int id, UpdateGameDto updateGame, GamestoreContext dbContext) =>
        {
            var game = await dbContext.Games.Include(g => g.Genre).FirstOrDefaultAsync(g => g.Id == id);
            if (game == null)
            {
                return Results.NotFound(new { Message = $"Game with ID {id} not found." });
            }

            var genre = await dbContext.Genres.FindAsync(updateGame.GenreId);
            if (genre == null)
            {
                return Results.BadRequest(new { Message = $"Genre with ID {updateGame.GenreId} does not exist." });
            }

            // Update game properties
            game.Name = updateGame.Name;
            game.GenreId = updateGame.GenreId;
            game.Price = updateGame.Price;
            game.ReleaseDate = updateGame.ReleaseDate;

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE a game by ID
        group.MapDelete("/{id:int}", async (int id, GamestoreContext dbContext) =>
        {
            var game = await dbContext.Games.FindAsync(id);
            if (game == null)
            {
                return Results.NotFound(new { Message = $"Game with ID {id} not found." });
            }

            dbContext.Games.Remove(game);
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        return group;
    }
}
