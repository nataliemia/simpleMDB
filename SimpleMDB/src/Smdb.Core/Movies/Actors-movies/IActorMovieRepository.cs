namespace Smdb.Core.Movies.ActorMovies;

using Shared.Http;

public interface IActorMovieRepository
{
  public Task<PagedResult<ActorMovie>?> ReadActorMovies(int page, int size);
  public Task<ActorMovie?> CreateActorMovie(ActorMovie newActorMovie);
  public Task<ActorMovie?> ReadActorMovie(int id);
  public Task<ActorMovie?> UpdateActorMovie(int id, ActorMovie newData);
  public Task<ActorMovie?> DeleteActorMovie(int id);
}
