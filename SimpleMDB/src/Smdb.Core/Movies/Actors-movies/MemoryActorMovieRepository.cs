namespace Smdb.Core.Movies.ActorMovies;

using Shared.Http;
using Smdb.Core.Db;

public class MemoryActorMovieRepository : IActorMovieRepository
{
  private MemoryDatabase db;

  public MemoryActorMovieRepository(MemoryDatabase db)
  {
    this.db = db;
  }

  public async Task<PagedResult<ActorMovie>?> ReadActorMovies(int page, int size)
  {
    int totalCount = db.ActorMovies.Count;
    int start = Math.Clamp((page - 1) * size, 0, totalCount);
    int length = Math.Clamp(size, 0, totalCount - start);
    var values = db.ActorMovies.Slice(start, length);
    var result = new PagedResult<ActorMovie>(totalCount, values);

    return await Task.FromResult(result);
  }

  public async Task<ActorMovie?> CreateActorMovie(ActorMovie newActorMovie)
  {
    newActorMovie.Id = db.NextActorMovieId();
    db.ActorMovies.Add(newActorMovie);

    return await Task.FromResult(newActorMovie);
  }

  public async Task<ActorMovie?> ReadActorMovie(int id)
  {
    ActorMovie? result = db.ActorMovies.FirstOrDefault(a => a.Id == id);

    return await Task.FromResult(result);
  }

  public async Task<ActorMovie?> UpdateActorMovie(int id, ActorMovie newData)
  {
    ActorMovie? result = db.ActorMovies.FirstOrDefault(a => a.Id == id);
    if (result != null)
    {
      result.ActorId = newData.ActorId;
      result.MovieId = newData.MovieId;
      result.Role = newData.Role;
      result.Description = newData.Description;
    }

    return await Task.FromResult(result);
  }

  public async Task<ActorMovie?> DeleteActorMovie(int id)
  {
    ActorMovie? result = db.ActorMovies.FirstOrDefault(a => a.Id == id);

    if (result != null) { db.ActorMovies.Remove(result); }

    return await Task.FromResult(result);
  }
}
