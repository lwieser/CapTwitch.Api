using System;
using CapTwitch.Api.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapTwitch.Api.Tests;

[TestClass]
public class PasswordValdatorTester
{
    [TestMethod]
    public void IsValid_WithEmpty_ThenFalse()
    {
        Assert.IsFalse(PasswordValdator.IsValid(String.Empty));
    }

    [TestMethod]
    public void IsValid_WithNull_ThenFalse()
    {
        Assert.IsFalse(PasswordValdator.IsValid(null));
    }

    [TestMethod]
    public void IsValid_WithLessThan8Characters_ThenFalse()
    {
        Assert.IsFalse(PasswordValdator.IsValid("123456"));
    }

    [TestMethod]
    public void IsValid_WithoutSpecial_ThenFalse()
    {
        Assert.IsFalse(PasswordValdator.IsValid("azertyuyiop"));
    }
    [TestMethod]
    public void IsValid_WithoutPasfou_ThenFalse()
    {
        Assert.IsFalse(PasswordValdator.IsValid("        !67"));
    }

    [TestMethod]
    public void IsValid_WithoutCaps_ThenFalse()
    {
        Assert.IsFalse(PasswordValdator.IsValid("azertyuyiop"));
    }
}