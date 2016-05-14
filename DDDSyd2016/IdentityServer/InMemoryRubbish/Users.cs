using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;

namespace DDDSyd2016.IdentityServer.InMemoryRubbish
{
    static class Users
    {
        public static List<InMemoryUser> Get()
        {
            var users = new List<InMemoryUser>
            {
                new InMemoryUser{Subject = "123", Username = "mary", Password = "password",
                    Claims = new Claim[]
                    {
                        new Claim(Constants.ClaimTypes.Name, "Mary Unicorn"),
                        new Claim(Constants.ClaimTypes.GivenName, "Mary"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Unicorn"),
                        new Claim(Constants.ClaimTypes.Email, "mary@rainbows.com"),
                        new Claim(Constants.ClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(Constants.ClaimTypes.Role, "Dreamer"),
                        new Claim(Constants.ClaimTypes.Role, "JoyBringer"),
                    }
                },
                new InMemoryUser{Subject = "456", Username = "mort", Password = "password",
                    Claims = new Claim[]
                    {
                        new Claim(Constants.ClaimTypes.Name, "Mort DeMortMort"),
                        new Claim(Constants.ClaimTypes.GivenName, "Mort"),
                        new Claim(Constants.ClaimTypes.FamilyName, "DeMortMort"),
                        new Claim(Constants.ClaimTypes.Email, "mort@demort.com"),
                        new Claim(Constants.ClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(Constants.ClaimTypes.Role, "Developer"),
                        new Claim(Constants.ClaimTypes.Role, "CoffeeDrinker"),
                    }
                }
            };

            return users;
        }
    }
}