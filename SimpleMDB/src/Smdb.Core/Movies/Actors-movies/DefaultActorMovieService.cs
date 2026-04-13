namespace Smdb.Core.Movies.ActorMovies;

using Shared.Http;
using System.Net;
using Smdb.Core.Db;

public class DefaultActorMovieService : IActorMovieService
{
  private IActorMovieRepository actorMovieRepository;

  public DefaultActorMovieService(IActorMovieRepository actorMovieRepository)
  {
    this.actorMovieRepository = actorMovieRepository;
  }

  public async Task<Result<PagedResult<ActorMovie>>> ReadActorMovies(int page, int size)
  {
    if (page < 1)
    {
      return new Result<PagedResult<ActorMovie>>(
        new Exception("Page must be >= 1."),
        (int)HttpStatusCode.BadRequest);
    }

    if (size < 1)
    {
      return new Result<PagedResult<ActorMovie>>(
        new Exception("Page size must be >= 1."),
        (int)HttpStatusCode.BadRequest);
    }

    var pagedResult = await actorMovieRepository.ReadActorMovies(page, size);
    var result = pagedResult == null
      ? new Result<PagedResult<ActorMovie>>(new Exception(
          $"Could not read actor movies from page {page} and size {size}."),
          (int)HttpStatusCode.NotFound)
      : new Result<PagedResult<ActorMovie>>(pagedResult, (int)HttpStatusCode.OK);

    return result;
  }

  public async Task<Result<ActorMovie>> CreateActorMovie(ActorMovie newActorMovie)
  {
    var validationResult = ValidateActorMovie(newActorMovie);

    if (validationResult != null) { return validationResult; }

    var actorMovie = await actorMovieRepository.CreateActorMovie(newActorMovie);
    var result = actorMovie == null
      ? new Result<ActorMovie>(
          new Exception($"Could not create actor movie {newActorMovie}."),
          (int)HttpStatusCode.NotFound)
      : new Result<ActorMovie>(actorMovie, (int)HttpStatusCode.Created);

    return result;
  }

  public async Task<Result<ActorMovie>> ReadActorMovie(int id)
  {
    var actorMovie = await actorMovieRepository.ReadActorMovie(id);
    var result = actorMovie == null
      ? new Result<ActorMovie>(
          new Exception($"Could not read actor movie with id {id}."),
          (int)HttpStatusCode.NotFound)
      : new Result<ActorMovie>(actorMovie, (int)HttpStatusCode.OK);

    return result;
  }

  public async Task<Result<ActorMovie>> UpdateActorMovie(int id, ActorMovie newData)
  {
    var validationResult = ValidateActorMovie(newData);

    if (validationResult != null) { return validationResult; }

    var actorMovie = await actorMovieRepository.UpdateActorMovie(id, newData);
    var result = actorMovie == null
      ? new Result<ActorMovie>(
          new Exception($"Could not update actor movie {newData} with id {id}."),
          (int)HttpStatusCode.NotFound)
      : new Result<ActorMovie>(actorMovie, (int)HttpStatusCode.OK);

    return result;
  }

  public async Task<Result<ActorMovie>> DeleteActorMovie(int id)
  {
    var actorMovie = await actorMovieRepository.DeleteActorMovie(id);
    var result = actorMovie == null
      ? new Result<ActorMovie>(
          new Exception($"Could not delete actor movie with id {id}."),
          (int)HttpStatusCode.NotFound)
      : new Result<ActorMovie>(actorMovie, (int)HttpStatusCode.OK);

    return result;
  }

  private static Result<ActorMovie>? ValidateActorMovie(ActorMovie? actorMovieData)
  {
    if (actorMovieData is null)
    {
      return new Result<ActorMovie>(
        new Exception("Actor movie payload is required."),
        (int)HttpStatusCode.BadRequest);
    }

    if (actorMovieData.ActorId < 1)
    {
      return new Result<ActorMovie>(
        new Exception("ActorId must be a positive integer."),
        (int)HttpStatusCode.BadRequest);
    }

    if (actorMovieData.MovieId < 1)
    {
      return new Result<ActorMovie>(
        new Exception("MovieId must be a positive integer."),
        (int)HttpStatusCode.BadRequest);
    }

    if (string.IsNullOrWhiteSpace(actorMovieData.Role))
    {
      return new Result<ActorMovie>(
        new Exception("Role is required and cannot be empty."),
        (int)HttpStatusCode.BadRequest);
    }

    if (actorMovieData.Role.Length > 256)
    {
      return new Result<ActorMovie>(
        new Exception("Role cannot be longer than 256 characters."),
        (int)HttpStatusCode.BadRequest);
    }

    if (actorMovieData.Description.Length > 1024)
    {
      return new Result<ActorMovie>(
        new Exception("Description cannot be longer than 1024 characters."),
        (int)HttpStatusCode.BadRequest);
    }

    return null;
  }
}
