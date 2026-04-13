namespace Smdb.Core.Movies.ActorMovies;

using Shared.Http;

public interface IActorMovieService
{
  public Task<Result<PagedResult<ActorMovie>>> ReadActorMovies(int page, int size);
  public Task<Result<ActorMovie>> CreateActorMovie(ActorMovie actorMovie);
  public Task<Result<ActorMovie>> ReadActorMovie(int id);
  public Task<Result<ActorMovie>> UpdateActorMovie(int id, ActorMovie newData);
  public Task<Result<ActorMovie>> DeleteActorMovie(int id);
}
