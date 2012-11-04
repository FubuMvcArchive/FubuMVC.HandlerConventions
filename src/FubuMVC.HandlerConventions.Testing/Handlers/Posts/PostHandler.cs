using FubuMVC.Core;

namespace FubuMVC.HandlerConventions.Testing.Handlers.Posts
{
    public class PostHandler
    {
        public PostHandlerViewModel Execute(ViewPostHandlerRequestModel request)
        {
            return new PostHandlerViewModel();
        }
    }

    public class PostHandlerViewModel
    {
    }

    public class ViewPostHandlerRequestModel
    {
        [QueryString]
        public string Optional { get; set; }
    }
}