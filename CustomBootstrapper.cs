namespace SimpleNancyExample
{
    using Nancy;
    using Nancy.Bootstrapper;
    using Nancy.Configuration;
    using Nancy.Swagger.Services;
    using Nancy.TinyIoc;


    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            SwaggerMetadataProvider.SetInfo("Nancy Swagger Example", "v1.0.0", "Example REST web service");

            base.ApplicationStartup(container, pipelines);
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            // Enable CORS to allow swagger-ui to access the web service
            pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            {
                ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                            .WithHeader("Access-Control-Allow-Methods", "GET, POST, DELETE, PUT")
                            .WithHeader("Access-Control-Allow-Headers", "Accept, Content-Type");

            });
        }

        public override void Configure(INancyEnvironment environment)
        {
            environment.Tracing(enabled: false, displayErrorTraces: true);
            base.Configure(environment);
        }
    }
}
