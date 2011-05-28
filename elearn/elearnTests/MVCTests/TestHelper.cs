using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhino.Mocks;
using MvcContrib.TestHelper.Fakes;
using System.Reflection;
using NUnit.Framework;
using NHiberanteDal.DTO;

namespace elearnTests.MVCTests
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

        public static FormCollection ConvertEntityToFormCollection(object entity)
        {
            var form = new FormCollection();
            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                if (!prop.PropertyType.IsGenericType)
                {
                    var name = prop.Name;
                    var value = prop.GetValue(entity, null) ?? String.Empty;

                    form.Add(name, value.ToString());
                }
            }

            return form;
        }
    }

    [TestFixture]
    public class TestHelperTests
    {

        [Test]
        public void Can_convert_entity_to_form_collection()
        {
            #region Arrange
            var profile = new ProfileModelDto { Email = "test@test.com", ID = 1, IsActive = true, Name = "test", Role = "test" };
            #endregion

            #region Act

            var form = TestHelper.ConvertEntityToFormCollection(profile);

            #endregion

            #region Assert
            Assert.That(form.Count,Is.EqualTo(5));
            Assert.That(form["Email"], Is.EqualTo("test@test.com"));
            Assert.That(form["IsActive"], Is.EqualTo("True"));
            Assert.That(form["Name"], Is.EqualTo("test"));
            Assert.That(form["ID"], Is.EqualTo("1"));
            Assert.That(form["Role"], Is.EqualTo("test"));
            #endregion
        }
				
    }
}
