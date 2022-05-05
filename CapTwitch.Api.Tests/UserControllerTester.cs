using CapTwitch.Api.Controllers;
using CapTwitch.Model.Model;
using CapTwitch.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CapTwitch.Api.Tests
{
    [TestClass]
    public class UserControllerTester
    {
        [TestMethod]
        public void Post_WithUser_ThenServiceMethodIsCalledWithSameUser()
        {
            var user = new User();
            var serviceMock = new Mock<IService<User>>();
            UserController controller = new UserController(null, serviceMock.Object);
            controller.Post(user);

            serviceMock.Verify(x => x.Add(user));
        }
    }
}