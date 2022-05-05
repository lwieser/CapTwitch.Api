using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapTwitch.Api.Controllers;
using CapTwitch.Model.Model;
using CapTwitch.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapTwitch.Api.Tests.Integration
{
    public class ControllerGenericTester<T>
    {
        protected T _controller;
        protected IServiceProvider _serviceProvider;
        public ControllerGenericTester()
        {

            WebApplicationBuilder builder = TwitchApiBuilder.Builder(Array.Empty<string>());
            var webapp = builder.Build();
            _serviceProvider = webapp.Services.CreateScope().ServiceProvider;
        }


        [TestMethod]
        public void CanBuild()
        {
            Assert.IsNotNull(_controller);
        }
    }

    [TestClass]
    public class UserControllerIntegrationTester : ControllerGenericTester<UserController>
    {
        public UserControllerIntegrationTester()
        {
            _controller = new UserController(
                _serviceProvider.GetRequiredService<CapTwitchDbContext>(),
                _serviceProvider.GetRequiredService<IService<User>>()
            );
        }
    }

    [TestClass]
    public class StreamEventControllerIntegrationTester : ControllerGenericTester<StreamEventController>
    {

        public StreamEventControllerIntegrationTester()
        {
            _controller = new StreamEventController(
                _serviceProvider.GetRequiredService<CapTwitchDbContext>(),
                _serviceProvider.GetRequiredService<IService<StreamEvent>>()
            );
        }
    }


    [TestClass]
    public class StreamRequestControllerIntegrationTester : ControllerGenericTester<StreamRequestController>
    {

        public StreamRequestControllerIntegrationTester()
        {
            _controller = new StreamRequestController(
                _serviceProvider.GetRequiredService<CapTwitchDbContext>(),
                _serviceProvider.GetRequiredService<IService<StreamRequest>>()
            );
        }
    }
}
