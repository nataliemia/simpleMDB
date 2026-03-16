namespace Smdb.Core.Movies.Actors;

using Shared.Http;

public interface IActorRepository
{
    public Task<PagedResult<Actor>?> ReadActors(int page, int size);
    public Task<Actor?> CreateActor(Actor newActor);
    public Task<Actor?> ReadActor(int id);
    public Task<Actor?> UpdateActor(int id, Actor newData);
    public Task<Actor?> DeleteActor(int id);
}