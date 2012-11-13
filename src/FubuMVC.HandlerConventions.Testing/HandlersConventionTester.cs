using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Conventions;
using FubuMVC.HandlerConventions.Testing.Handlers;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.HandlerConventions.Testing
{
    [TestFixture]
    public class HandlersConventionTester
    {
        [Test]
        public void should_include_handlers()
        {
            var graph = BehaviorGraph.BuildFrom(r =>
            {
                r.Import<HandlerConvention>(x => x.MarkerType<HandlersMarker>());
            });

            verifyRoutes(graph);
        }

        [Test]
        public void generic_marker_should_include_handlers()
        {
            var graph = BehaviorGraph.BuildFrom(r =>
            {
                r.Import<HandlerConvention>(x => x.MarkerType<HandlersMarker>());
            });

            verifyRoutes(graph);
        }

        [Test]
        public void should_run_in_isolation_from_other_action_matching()
        {
            var graph = BehaviorGraph.BuildFrom(registry =>
            {
                registry.Import<HandlerConvention>(x => x.MarkerType<HandlersMarker>());
                registry
                    .Actions
                    .IncludeType<TestController>();

            });

            verifyRoutes(graph);
            graph
                .Actions()
                .Where(call => call.HandlerType == typeof (TestController))
                .ShouldHaveCount(6);
        }

        [Test]
        public void should_avoid_duplicates()
        {
            var singleHandlerGraph = BehaviorGraph.BuildFrom(registry =>
            {
                registry.Import<HandlerConvention>(x => x.MarkerType<HandlersMarker>());
            });


            var duplicatedHandlerGraph = BehaviorGraph.BuildFrom(registry =>
            {
                registry.Import<HandlerConvention>(x => x.MarkerType<HandlersMarker>());
                registry.Import<HandlerConvention>(x => x.MarkerType<HandlersMarker>());
            });
                

            duplicatedHandlerGraph.Routes.Count().ShouldEqual(singleHandlerGraph.Routes.Count());
        }

        private void verifyRoutes(BehaviorGraph graph)
        {
            var routes = new List<string>
                             {
                                 "posts/create",
                                 "posts/complex-route",
                                 "posts/sub/route",
                                 "some-crazy-url/as-a-subfolder",
                                 "posts/{Year}/{Month}/{Title}"
                             };

            routes
                .Each(route =>
                    {
                        graph.Routes.ShouldContain(r =>
                                {
                                   return r.Pattern.Equals(route);
                                });
                    });
        }
    }






    public class TestViewModel
    {
        public bool BoolProperty { get; set; }
        public string StringProperty { get; set; }
        public int IntProperty { get; set; }
    }

    public class TestController
    {
        public TestOutputModel Index()
        {
            return new TestOutputModel();
        }

        public TestOutputModel SomeAction(TestInputModel value)
        {
            return new TestOutputModel
            {
                Prop1 = value.Prop1
            };
        }

        public TestOutputModel SomeAction(int not_used)
        {
            return new TestOutputModel();
        }

        public TestOutputModel2 AnotherAction(TestInputModel value)
        {
            return new TestOutputModel2
            {
                Prop1 = value.Prop1,
                Name = value.Name,
                Age = value.Age
            };
        }

        public TestOutputModel3 ThirdAction(TestInputModel value)
        {
            return new TestOutputModel3
            {
                Prop1 = value.Prop1
            };
        }

        public void RedirectAction(TestInputModel value)
        {
        }
    }

    public class TestInputModel
    {
        public int PropInt { get; set; }
        public string Prop1 { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class TestOutputModel
    {
        public string Prop1 { get; set; }
    }

    public class TestOutputModel2 : TestOutputModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class TestOutputModel3 : TestOutputModel
    {
    }

    public class TestPartialModel
    {
        public string PartialModelProp1 { get; set; }
    }

    public class TestBehavior2 : IActionBehavior
    {
        public IActionBehavior InsideBehavior { get; set; }

        #region IActionBehavior Members

        public void Invoke()
        {
            throw new NotImplementedException();
        }

        public void InvokePartial()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class TestBehavior : IActionBehavior
    {
        public IActionBehavior InsideBehavior { get; set; }

        #region IActionBehavior Members

        public void Invoke()
        {
            throw new NotImplementedException();
        }

        public void InvokePartial()
        {
            throw new NotImplementedException();
        }

        #endregion
    }


}