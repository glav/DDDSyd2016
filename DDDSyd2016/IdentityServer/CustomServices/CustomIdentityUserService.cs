using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core;
using IdentityServer3.Core.Extensions;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;

namespace DDDSyd2016.IdentityServer.CustomServices
{
    public class CustomIdentityUserService : UserServiceBase
    {
        //public class CustomUser
        //{
        //    public string Subject { get; set; }
        //    public string Username { get; set; }
        //    public string Password { get; set; }
        //    public List<Claim> Claims { get; set; }
        //}

        private MyMembershipService _membershipService;
        private DapperRepo _repo;

        public CustomIdentityUserService(MyMembershipService membershipService, DapperRepo repo)
        {
            _membershipService = membershipService;
            _repo = repo;
        }

        public override Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            var validUser = _membershipService.AuthenticateUser(context.UserName, context.Password);

            if (validUser != null && validUser.IsActive)
            {
                List<System.Security.Claims.Claim> claims = GetClaimsForUser(validUser.Id, validUser.LoginId);
                // Note: Subject is set to the user Id
                context.AuthenticateResult = new AuthenticateResult(validUser.Id.ToString(), validUser.LoginId, claims);
            }

            return Task.FromResult(0);
        }

        private List<System.Security.Claims.Claim> GetClaimsForUser(long userId, string loginId)
        {
            // Query your subsystem for claims relevant to the user - example only below
            return new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim("HasHair","true"),
                new System.Security.Claims.Claim("IsAuthome","true"),
            };
        }

        public override Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            long userId;
            if (long.TryParse(context.Subject.GetSubjectId(), out userId))
            {
                var user = _repo.GetUserById(userId);
                if (user != null)
                {
                    context.IssuedClaims = GetClaimsForUser(user.Id, user.LoginId);
                    return Task.FromResult(0);
                }
            }

            return base.GetProfileDataAsync(context);
        }

        public override Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = false;
            long userId;
            if (long.TryParse(context.Subject.GetSubjectId(), out userId))
            {
                var user = _repo.GetUserById(userId);
                context.IsActive = user != null && user.IsActive;
            }
            return Task.FromResult(0);
        }
    }

}
