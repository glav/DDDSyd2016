using DDDSyd2016.IdentityServer.Extensions;
using DDDSyd2016.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDDSyd2016.IdentityServer.CustomServices
{
    public class MyMembershipService
    {
        private DapperRepo _repo;
        public MyMembershipService(DapperRepo repo)
        {
            _repo = repo;
        }

        public User AuthenticateUser(string loginId, string password)
        {
            var user = _repo.GetUserByLoginId(loginId);
            if (user == null)
            {
                return null;
            }
            if (user.PasswordHash == password.Sha256())
            {
                return user;
            }
            return null;
        }
    }
}