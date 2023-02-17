using System.Security.Claims;

namespace ToDo.WebAPI.Models
{
    public class AuthUser
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public AuthUser(ClaimsPrincipal user)
        {
            if (user.Identity == null || !user.Identity.IsAuthenticated)
                throw new InvalidOperationException("User is not authorised");

            var claims = user.Claims;

            Id = Convert.ToInt32(claims.First(c => c.Type == ClaimTypes.Sid).Value);
            Name = claims.First(c => c.Type == ClaimTypes.Name).Value;
        }
    }
}
