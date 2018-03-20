using System;
using System.Net;
using System.Security.Claims;
using Nancy.Authentication.Basic;

namespace POI.Service
{
    public class BasicUserValidator : IUserValidator
    {
        public ClaimsPrincipal Validate(string username, string password)
        {
            if (password.ToLowerInvariant() != $"MediaProject-{DateTime.Now:yyyyMMdd}")
            {
                return null;
            }
            return new ClaimsPrincipal(new HttpListenerBasicIdentity(username, password));
        }
    }
}
