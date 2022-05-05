using CapTwitch.Api.Controllers;
using CapTwitch.Api.Tests.Builders;
using CapTwitch.Model.Model;
using CapTwitch.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CapTwitch.Api.Tests;

[TestClass]
public class UserServiceTester
{
    private readonly IService<User> _service;
    private readonly Mock<IRepository<User>> _repoMock;
    private readonly Mock<IBadWordChecker> _badWordChecker;

    public UserServiceTester()
    {
        _repoMock = new Mock<IRepository<User>>();
        _badWordChecker = new Mock<IBadWordChecker>();
        _service = new UserService(_repoMock.Object, _badWordChecker.Object);
    }

    [TestMethod]
    public void Add_WithEmptyPseudoUser_ThenRepoIsNotCalled()
    {
        TestPseudoShouldFail("");
    }

    [TestMethod]
    public void Add_WithWhiteSpaceUser_ThenRepoIsNotCalled()
    {
        TestPseudoShouldFail("       ");
    }

    [TestMethod]
    public void Add_WithWhiteSpaaaceUser_ThenRepoIsNotCalled()
    {
        TestPseudoShouldFail("      ");
    }

    [TestMethod]
    public void Add_WithUserWithPseudo_ThenSameUserIsReturned()
    {
        var user = UserBuilder.Build("azeazeazeaze");
        _repoMock.Setup(x => x.Add(user)).Returns(user);
        var res = _service.Add(user);
        AddIsCalled();
    }

    [TestMethod]
    public void Add_WithPseudoIsBadWord_ThenRepoIsNotCalled()
    {
        _badWordChecker.Setup(x => x.IsBad(It.IsAny<string>())).Returns(true);
        _service.Add(new User());
        AddIsNotCalled();
    }

    [TestMethod]
    public void Add_WithNullUser_ThenRepoIsNotCalled()
    {
        _service.Add(null);
        AddIsNotCalled();
    }

    [TestMethod]
    public void Add_WithPseudoIsNotBadWord_ThenRepoIsCalled()
    {
        _badWordChecker.Setup(x => x.IsBad(It.IsAny<string>())).Returns(false);
        _service.Add(UserFactory.Laurent(true));
        AddIsCalled();
    }
    //
    // [TestMethod]
    // public void Add_WithPseudoIsHugo_ThenNull()
    // {
    //     var user = UserBuilder.Build("Hugo");
    //     _service.Add(user);
    //     AddIsNotCalled();
    // }
    //
    // [TestMethod]
    // public void Add_WithPseudoIsPleutre_ThenNull()
    // {
    //     _service.Add(UserBuilder.Build("Pleutre"));
    //     AddIsNotCalled();
    // }

    private void AddIsNotCalled()
    {
        _repoMock.Verify(x => x.Add(It.IsAny<User>()), Times.Never);
    }

    private void AddIsCalled()
    {
        _repoMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
    }

    public void TestPseudoShouldFail(string pseudo)
    {
        var user = UserBuilder.Build(pseudo);
        var res = _service.Add(user);
        Assert.IsNull(res);
        AddIsNotCalled();
    }
}