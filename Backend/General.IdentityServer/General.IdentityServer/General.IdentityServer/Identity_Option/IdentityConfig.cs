using System.Collections.Generic;

using IdentityServer4.Models;
using IdentityServer4.Test;

namespace General.IdentityServer.Identity_Option
{
    public class IdentityConfig
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            var apiResources = new List<ApiResource>();

            apiResources.Add(new ApiResource("api1", "My API"));

            return apiResources;
        }

        public static IEnumerable<Client> GetClient()
        {
            var clients = new List<Client>();

            var baseClient = new Client
            {
                ClientId = "client",
                ClientSecrets = new[] { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes = new[] { "api1" }
            };

            clients.Add(baseClient);

            return clients;
        }

        public static List<TestUser> GetUsers()
        {
            var users = new List<TestUser>();

            var admin = new TestUser
                              {
                                  SubjectId = "1",
                                  Username = "admin",
                                  Password = "admin"
                              };

            var user = new TestUser
                              {
                                  SubjectId = "2",
                                  Username = "user",
                                  Password = "user"
                              };

            users.Add(admin);
            users.Add(user);

            return users;
        }
    }
}