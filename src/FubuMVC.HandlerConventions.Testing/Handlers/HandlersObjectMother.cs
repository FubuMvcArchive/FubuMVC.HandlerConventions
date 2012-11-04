using FubuMVC.Core.Registration.Nodes;
using FubuMVC.HandlerConventions.Testing.Handlers.Posts;
using FubuMVC.HandlerConventions.Testing.Handlers.Posts.Create;

namespace FubuMVC.HandlerConventions.Testing.Handlers
{
    public class HandlersObjectMother
    {
        public static ActionCall HandlerCall()
        {
            return ActionCall.For<get_handler>(h => h.Execute(new CreatePostRequestModel()));
        }
        public static ActionCall ComplexHandlerCall()
        {
            return ActionCall.For<Handlers.Posts.ComplexRoute.GetHandler>(h => h.Execute());
        }

        public static ActionCall NonHandlerCall()
        {
            return ActionCall.For<SampleController>(c => c.Hello());
        }

        public static ActionCall HandlerWithAttributeCall()
        {
            return ActionCall.For<UrlPatternHandler>(h => h.Execute());
        }

        public static ActionCall HandlerWithRouteInput()
        {
            return ActionCall.For<get_Year_Month_Title_handler>(h => h.Execute(new ViewPostRequestModel()));
        }

        public static ActionCall VerbHandler()
        {
            return ActionCall.For<PostHandler>(h => h.Execute(new ViewPostHandlerRequestModel()));
        }
    }
}