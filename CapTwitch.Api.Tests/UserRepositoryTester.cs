using CapTwitch.Api.Controllers;
using CapTwitch.Api.Tests.Builders;
using CapTwitch.Model.Model;
using CapTwitch.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapTwitch.Api.Tests;

[TestClass]
public class UserRepositoryTester
{
    private readonly Repository<User> _repo;

    public UserRepositoryTester()
    {
        var builder = new DbContextOptionsBuilder<CapTwitchDbContext>()
            .UseInMemoryDatabase("LABase");
            
        var db = new CapTwitchDbContext(builder.Options);
        _repo = new Repository<User>(db);
    }

    [TestMethod]
    public void PostWithUserAndPseudo_AddObject()
    {
        var baseData = _repo.All();
        var baseCount = baseData.Count;
        _repo.Add(UserFactory.Laurent(false));
        var nextData = _repo.All();
        var nextCount = nextData.Count;
        Assert.AreEqual(nextCount, baseCount + 1);

    }
}