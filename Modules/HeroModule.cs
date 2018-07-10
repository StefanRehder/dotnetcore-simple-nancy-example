using Nancy;
using Nancy.ModelBinding;
using Nancy.Swagger;
using Nancy.Swagger.Modules;
using Nancy.Swagger.Services;
using Nancy.Swagger.Services.RouteUtils;
using SimpleNancyExample.Data;
using System.Threading.Tasks;

namespace SimpleNancyExample
{
    public class HeroModule : NancyModule
    {
        private static HeroCollection heroes = new HeroCollection();

        public HeroModule() : base("/hero")
        {
            Get("/{name}", parameters => GetHero(parameters.name), null, "GetHero");

            Delete("/{name}", parameters => DeleteHero(parameters.name), null, "DeleteHero");

            Get("/list", _ => GetHeroList(), null, "GetHeroList");

            Put("/", parameters => PutHero(), null, "PutHero");

            // Async Get operation as an example
            // Can easily be modified to get data from a database instead
            Get("/async/{name}", GetHeroAsync);
        }

        private Response GetHero(string name)
        {
            Hero hero = heroes.GetHero(name);
            if (hero != null)
            {
                return Response.AsJson<Hero>(hero);
            }
            else
            {
                return new NotFoundResponse();
            }
        }

        private Response DeleteHero(string name)
        {
            heroes.DeleteHero(name);
            return HttpStatusCode.NoContent;
        }


        private Response GetHeroList()
        {
            Hero[] heroArr = heroes.GetHeroes();
            return Response.AsJson<Hero[]>(heroArr);
        }

        private Response PutHero()
        {
            BindingConfig conf = new BindingConfig();
            conf.BodyOnly = true;
            var hero = this.Bind<Hero>(conf);

            if (hero != null)
            {
                heroes.PutHero(hero);
                return HttpStatusCode.Created;
            }
            else
            {
                return HttpStatusCode.BadRequest;
            }
        }

        private async Task<Response> GetHeroAsync(dynamic parameters)
        {
            Hero hero = null;
            await Task.Run(() => hero = heroes.GetHero(parameters.name));

            if (hero != null)
            {
                return Response.AsJson<Hero>(hero);
            }
            else
            {
                return new NotFoundResponse();
            }
        }
    }

    public class HeroMetadataModule : SwaggerMetadataModule
    {
        public HeroMetadataModule(ISwaggerModelCatalog modelCatalog, ISwaggerTagCatalog tagCatalog) : base(modelCatalog, tagCatalog)
        {
            //modelCatalog.AddModels(typeof(Hero));
            //modelCatalog.AddModel<Hero>();

            RouteDescriber.AddBaseTag(new Swagger.ObjectModel.Tag()
                { Name = "Hero", Description = "Operations for handling hero objects" });

            RouteDescriber.DescribeRouteWithParams<Hero>("GetHero", "", "Get Hero", new[]
            {
                new HttpResponseMetadata {Code = (int)HttpStatusCode.OK, Message = "A hero object is returned."},
                new HttpResponseMetadata {Code = (int)HttpStatusCode.NotFound, Message = "No hero was found with the name specified in the request."}
            },
            new[]
            {
                new Swagger.ObjectModel.Parameter
                {
                    Name = "name",
                    In = Swagger.ObjectModel.ParameterIn.Path,
                    Description = "The name of the hero to return.",
                    Required = true,
                    Type = "string"
                }
            });

            RouteDescriber.DescribeRouteWithParams("DeleteHero", "", "Delete Hero", new[]
            {
                new HttpResponseMetadata {Code = (int)HttpStatusCode.NoContent, Message = "The object was successfully deleted or did not exist to begin with."}
            },
            new[]
            {
                new Swagger.ObjectModel.Parameter
                {
                    Name = "name",
                    In = Swagger.ObjectModel.ParameterIn.Path,
                    Description = "The name of the hero to delete.",
                    Required = true,
                    Type = "string"
                }
            });

            RouteDescriber.DescribeRoute<Hero[]>("GetHeroList", "", "Get Hero List", new[]
            {
                new HttpResponseMetadata {Code = (int)HttpStatusCode.OK, Message = "An array with hero objects is returned."}
            });

            RouteDescriber.DescribeRouteWithParams("PutHero", "", "Put Hero", new[]
            {
                new HttpResponseMetadata {Code = (int)HttpStatusCode.Created, Message = "The hero object was created/updated."},
                new HttpResponseMetadata {Code = (int)HttpStatusCode.BadRequest, Message = "The body of the request does not conform to the Hero model."}
            },
            new[]
            {
                new Swagger.ObjectModel.Parameter
                {
                    Name = "hero",
                    In = Swagger.ObjectModel.ParameterIn.Body,
                    Description = "The Hero object to put. If an object with the same name exists it will be replaced.",
                    Required = true,
                    Type = "Hero"
                }
            });
        }
    }
}
