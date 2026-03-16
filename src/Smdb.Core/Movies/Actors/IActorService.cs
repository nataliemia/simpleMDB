namespace Smdb.Core.Movies.Actors;

using Shared.Http;

public interface IActorService
{
  public Task<Result<PagedResult<Actor>>> ReadActors(int page, int size);
  public Task<Result<Actor>> CreateActor(Actor actor);
  public Task<Result<Actor>> ReadActor(int id);
  public Task<Result<Actor>> UpdateActor(int id, Actor newData);
  public Task<Result<Actor>> DeleteActor(int id);
}