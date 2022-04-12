using CapTwitch.Api.Model;

namespace CapTwitch.Api.Tests.Builders
{
    internal static class UserBuilder
    {
        public static User Build(string pseudo)
        {
            var user = new User()
            {
                Pseudo = pseudo
            };
            return user;
        }
    }

    static class UserFactory
    {
        public static User Hugo()
        {
            return UserBuilder.Build("Hugo");
        }

        public static User Laurent(bool withId)
        {
            var user = UserBuilder.Build("Laurent");
            if (withId)
            {
                user.Id = 100;
            }
            return user;
        }
    }
}
