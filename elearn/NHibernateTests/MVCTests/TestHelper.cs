using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Rhino.Mocks;
using System.Web;
using System.Web.Routing;
using System.Security.Principal;
using MvcContrib.TestHelper;
using MvcContrib.TestHelper.Fakes;

namespace NHibernateTests.MVCTests
{
    public static class TestHelper
    {
        public static ControllerContext MockControllerContext(Controller controller)
        {
            var httpContext = MockRepository.GenerateMock<HttpContextBase>();
            var httpRequest = MockRepository.GenerateMock<HttpRequestBase>();
            httpContext.Stub(x => x.Request).Return(httpRequest);
            return new ControllerContext(httpContext,new RouteData(),controller);
        }

        public static ControllerContext WithAuthenticatedUser(this ControllerContext context, string userName)
        {
            var user = new FakePrincipal(new FakeIdentity(userName),null);
            context.HttpContext.Stub(x => x.User).Return(user);
            return new ControllerContext(context.HttpContext,new RouteData(),context.Controller);
        }

        public static ControllerContext WithNotAuthenticatedUser(this ControllerContext context)
        {
            var user = new FakePrincipal(new FakeIdentity(String.Empty), null);
            context.HttpContext.Stub(x => x.User).Return(user); 
            return new ControllerContext(context.HttpContext, new RouteData(), context.Controller);
        }

    }
}
