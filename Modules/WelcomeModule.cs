using Nancy;

namespace SimpleNancyExample
{
    public class WelcomeModule : NancyModule
    {
        public WelcomeModule()
        {
            // Serve the welcome page
            Get("/", _ => View["index.html"]);
        }
    }
}
