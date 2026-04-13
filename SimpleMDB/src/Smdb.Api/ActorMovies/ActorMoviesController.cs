namespace Smdb.Api.ActorMovies;

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Shared.Http;
using Smdb.Core.Movies.ActorMovies;

public class ActorMoviesController
{
  private IActorMovieService actorMovieService;

  public ActorMoviesController(IActorMovieService actorMovieService)
  {
    this.actorMovieService = actorMovieService;
  }

  // curl -X GET "http://localhost:8080/api/v1/actor-movies?page=1&size=10"
  public async Task ReadActorMovies(HttpListenerRequest req, HttpListenerResponse res,
    Hashtable props, Func<Task> next)
  {
    int page = int.TryParse(req.QueryString["page"], out int p) ? p : 1;
    int size = int.TryParse(req.QueryString["size"], out int s) ? s : 9;
    var result = await actorMovieService.ReadActorMovies(page, size);
    await JsonUtils.SendPagedResultResponse(req, res, props, result, page, size);
    await next();
  }

  // curl -X POST "http://localhost:8080/api/v1/actor-movies" -H "Content-Type: application/json" -d "{ \"actorId\": 1, \"movieId\": 1, \"role\": \"Vito Corleone\", \"description\": \"Don of the Corleone family.\" }"
  public async Task CreateActorMovie(HttpListenerRequest req,
    HttpListenerResponse res, Hashtable props, Func<Task> next)
  {
    var text = (string) props["req.text"]!;
    var actorMovie = JsonSerializer.Deserialize<ActorMovie>(text,
      JsonSerializerOptions.Web);
    var result = await actorMovieService.CreateActorMovie(actorMovie!);
    await JsonUtils.SendResultResponse(req, res, props, result);
    await next();
  }

  // curl -X GET "http://localhost:8080/api/v1/actor-movies/1"
  public async Task ReadActorMovie(HttpListenerRequest req, HttpListenerResponse res,
    Hashtable props, Func<Task> next)
  {
    var uParams = (NameValueCollection) props["req.params"]!;
    int id = int.TryParse(uParams["id"]!, out int i) ? i : -1;
    var result = await actorMovieService.ReadActorMovie(id);
    await JsonUtils.SendResultResponse(req, res, props, result);
    await next();
  }

  // curl -X PUT "http://localhost:8080/api/v1/actor-movies/1" -H "Content-Type:application/json" -d "{ \"actorId\": 1, \"movieId\": 1, \"role\": \"Vito Corleone\", \"description\": \"Updated role description.\" }"
  public async Task UpdateActorMovie(HttpListenerRequest req,
    HttpListenerResponse res, Hashtable props, Func<Task> next)
  {
    var uParams = (NameValueCollection) props["req.params"]!;
    int id = int.TryParse(uParams["id"]!, out int i) ? i : -1;
    var text = (string) props["req.text"]!;
    var actorMovie = JsonSerializer.Deserialize<ActorMovie>(text,
      JsonSerializerOptions.Web);
    var result = await actorMovieService.UpdateActorMovie(id, actorMovie!);
    await JsonUtils.SendResultResponse(req, res, props, result);
    await next();
  }

  // curl -X DELETE http://localhost:8080/api/v1/actor-movies/1
  public async Task DeleteActorMovie(HttpListenerRequest req,
    HttpListenerResponse res, Hashtable props, Func<Task> next)
  {
    var uParams = (NameValueCollection) props["req.params"]!;
    int id = int.TryParse(uParams["id"]!, out int i) ? i : -1;
    var result = await actorMovieService.DeleteActorMovie(id);
    await JsonUtils.SendResultResponse(req, res, props, result);
    await next();
  }
}
