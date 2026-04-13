namespace Smdb.Core.Movies.ActorMovies;

public class ActorMovie
{
  public int Id { get; set; }
  public int ActorId { get; set; }
  public int MovieId { get; set; }
  public string Role { get; set; }
  public string Description { get; set; }

  public ActorMovie(int id, int actorId, int movieId, string role, string description)
  {
    Id = id;
    ActorId = actorId;
    MovieId = movieId;
    Role = role;
    Description = description;
  }

  public override string ToString()
  {
    return $"ActorMovie[Id={Id}, ActorId={ActorId}, MovieId={MovieId}, Role={Role}, Description={Description}]";
  }
}
