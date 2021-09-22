using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace TransactionsApi.Middleware
{
    public class SampleMiddleware
    {
        private readonly RequestDelegate _next;
        public SampleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var userClaims = context.User.Claims;
            if (userClaims.Any())
            {
                var permissions = PermissionManagement.GetPermission("Customer");

                //add the permission for the user based on role
                //addd the permission to the claim
                context.User.AddUpdateClaim("GetAllBanks", "GetAllBanks");
                context.User.AddUpdateClaim("Delete", "Delete");
                context.User.AddUpdateClaim("Edit", "Edit");
            }

            await _next(context);
        }
    }

    public static class PermissionManagement
    {
        public static List<string> GetPermission(string role)
        {
            List<string> permission = new List<string>();
            if (role == "Customer")
            {
                permission.AddRange(new List<string>
                {
                    "Add",
                    "Edit",
                    "Delete",
                    "ViewAll"
                });
            }

            return permission;
        }
    }

    public static class Extensions
    {
        public static void AddUpdateClaim(this IPrincipal currentPrincipal, string key, string value)
        {
            var identity = currentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
                return;

            // check for existing claim and remove it
            var existingClaim = identity.FindFirst(key);
            if (existingClaim != null)
                identity.RemoveClaim(existingClaim);

            // add new claim
            identity.AddClaim(new Claim(key, value));
        }

        public static string GetClaimValue(this IPrincipal currentPrincipal, string key)
        {
            var identity = currentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
                return null;

            var claim = identity.Claims.FirstOrDefault(c => c.Type == key);
            return claim.Value;
        }
    }
}
