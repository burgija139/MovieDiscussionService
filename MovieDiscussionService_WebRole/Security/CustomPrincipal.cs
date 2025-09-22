using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace MovieDiscussionService_WebRole.Security
{
    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(string email, bool isAuthenticated, bool isAdmin, bool isVerified, string authenticationType = "Forms")
        {
            Name = email; // email iz User.RowKey
            IsAuthenticated = isAuthenticated;
            AuthenticationType = authenticationType;
            IsAdmin = isAdmin;
            IsVerified = isVerified;
        }

        public string Name { get; }                 // email
        public string AuthenticationType { get; }
        public bool IsAuthenticated { get; }
        public bool IsAdmin { get; }
        public bool IsVerified { get; }
    }

    public class CustomPrincipal : IPrincipal
    {
        public CustomPrincipal(CustomIdentity identity) { Identity = identity; }
        public IIdentity Identity { get; }

        public bool IsInRole(string role)
        {
            var r = (role ?? "").Trim();
            if (r.Equals("Admin", System.StringComparison.OrdinalIgnoreCase))
                return ((CustomIdentity)Identity).IsAdmin;
            if (r.Equals("Verified", System.StringComparison.OrdinalIgnoreCase))
                return ((CustomIdentity)Identity).IsVerified;
            return false;
        }

        public bool IsAdmin => ((CustomIdentity)Identity).IsAdmin;
        public bool IsVerified => ((CustomIdentity)Identity).IsVerified;
        public string Email => Identity.Name;
    }
}