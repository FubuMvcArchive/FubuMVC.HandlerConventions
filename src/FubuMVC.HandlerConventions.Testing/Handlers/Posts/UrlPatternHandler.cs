using FubuMVC.Core;

namespace FubuMVC.HandlerConventions.Testing.Handlers.Posts
{
    public class UrlPatternHandler
    {
        [UrlPattern("some-crazy-url/as-a-subfolder")]
        public JsonResponse Execute()
        {
            return new JsonResponse();
        }
    }
}