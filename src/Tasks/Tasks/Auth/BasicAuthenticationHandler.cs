/***********************************************************************

Basic Authentication filter.

To add authentication to a controller, add this attribute to the class: [Authorize]

To skip authentication for a specific endpoint, add this attribute: [AllowAnonymous]
 
************************************************************************/

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Tasks.Domain.Models;
using Tasks.Repositories.Interfaces;
using Tasks.Security;

namespace Tasks.Auth
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private sealed class DecodedAuthCredentials
        { 
            public string? Username { get; set; }
            public string? Password { get; set; } 
        }


        private readonly IUserRepository _userRepository;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IUserRepository userRepository) : base(options, logger, encoder, clock)
        {
            _userRepository = userRepository;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // skip authentication if endpoint has [AllowAnonymous] attribute
            if (IsAllowAnonymous())
            {
                return AuthenticateResult.NoResult();
            }

            // make sure the request contains the Authorization header
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization Header");
            }

            // try to get the user object from the Authorization header credentials
            User? user = null;

            try
            {
                user = await GetUserFromAuthHeader();
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (user is null)
            {
                return AuthenticateResult.Fail("Invalid Username or Password");
            }

            
            // save the user id to the request memory for later access
            bool added = Request.HttpContext.Items.TryAdd(RequestStorageKeys.CLIENT_USER_ID, user.Id);

            // add the ticket to the return value
            var ticket = GetAuthenticationTicket(user);

            return AuthenticateResult.Success(ticket);
        }

        /// <summary>
        /// Checks if the requested endpoint has the [AllowAnonymous] attribute.
        /// This signals that we should not authenticate this request.
        /// </summary>
        /// <returns></returns>
        private bool IsAllowAnonymous()
        {
            // skip authentication if endpoint has [AllowAnonymous] attribute
            var endpoint = Context.GetEndpoint();

            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get the current user object from the request header
        /// </summary>
        /// <returns></returns>
        private async Task<User?> GetUserFromAuthHeader()
        {
            var credentials = GetDecodedAuthCredentials();

            var user = await _userRepository.GetUserAsync(credentials.Username, credentials.Password);

            return user;
        }

        /// <summary>
        /// Get the decoded auth credentials from the request
        /// </summary>
        /// <returns></returns>
        private DecodedAuthCredentials GetDecodedAuthCredentials()
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);

            DecodedAuthCredentials result = new()
            {
                Username = credentials[0],
                Password = credentials[1],
            };

            return result;

        }

        /// <summary>
        /// Create a new AuthenticationTicket from the given User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private AuthenticationTicket GetAuthenticationTicket(User user)
        {
            var claims = new[] { new Claim("userId", user.Id.ToString()) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return ticket;
        }



    }

}
