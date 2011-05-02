using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Rhino.Mocks;
using System.Web;
using System.Web.Routing;

namespace NHibernateTests.MVCTests
{
    public static class TestHelper
    {
        public static ControllerContext MockControllerContext(Controller controller)
        {
            var mockHttpContext = MockRepository.GenerateMock<HttpContextBase>();
            var mockRequest = MockRepository.GenerateMock<HttpRequestBase>();
            mockHttpContext.Stub(x => x.Request).Return(mockRequest);
            mockRequest.Stub(x => x.HttpMethod).Return("POST");
            return new ControllerContext(mockHttpContext, new RouteData(),controller);
        }
    }
}
