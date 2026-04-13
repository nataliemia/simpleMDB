namespace Smdb.Api;

using Shared.Http;
using Smdb.Api.Movies;
using Smdb.Api.Actors;
using Smdb.Api.ActorMovies;
using Smdb.Core.Movies;
using Smdb.Core.Movies.Actors;
using Smdb.Core.Movies.ActorMovies;
using Smdb.Core.Db;

public class App : HttpServer
{
    public override void Init()
    {
        var db = new MemoryDatabase();
        var movieRepo = new MemoryMovieRepository(db);
        var movieServ = new DefaultMovieService(movieRepo);
        var movieCtrl = new MoviesController(movieServ);
        var movieRouter = new MoviesRouter(movieCtrl);
        var actorRepo = new MemoryActorRepository(db);
        var actorServ = new DefaultActorService(actorRepo);
        var actorCtrl = new ActorsController(actorServ);
        var actorRouter = new ActorsRouter(actorCtrl);
        var actorMovieRepo = new MemoryActorMovieRepository(db);
        var actorMovieServ = new DefaultActorMovieService(actorMovieRepo);
        var actorMovieCtrl = new ActorMoviesController(actorMovieServ);
        var actorMovieRouter = new ActorMoviesRouter(actorMovieCtrl);
        var apiRouter = new HttpRouter();

        router.Use(HttpUtils.StructuredLogging);
        router.Use(HttpUtils.CentralizedErrorHandling);
        router.Use(HttpUtils.AddResponseCorsHeaders);
        router.Use(HttpUtils.DefaultResponse);
        router.Use(HttpUtils.ParseRequestUrl);
        router.Use(HttpUtils.ParseRequestQueryString);
        router.UseParametrizedRouteMatching();

        router.UseRouter("/api/v1", apiRouter);
        apiRouter.UseRouter("/movies", movieRouter);
        apiRouter.UseRouter("/actors", actorRouter);
        apiRouter.UseRouter("/actor-movies", actorMovieRouter);
    }
}