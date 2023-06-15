using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace Product.Core.Module
{

    public static class CustomClaim
    {
        public const string PersonId = "PersonId";
    }
    public class CurrentUser
    {


        private IHttpContextAccessor _httpContextAccessor;
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            Initialize();
        }
        public void Initialize()
        {
            if (this._httpContextAccessor.HttpContext?.User != null)
            {
                var user = this._httpContextAccessor.HttpContext.User;
                IsAuthenticated = user.Identity.IsAuthenticated;
                if (IsAuthenticated)
                { 
                    _username = user.Identity.Name;
                    _claims = user.Claims.ToList();
                    _id = int.Parse(_claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
                    _personId = int.Parse(_claims.Single(x => x.Type == CustomClaim.PersonId)?.Value ?? "0");
                    _name = _claims.SingleOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
                }

            }

        }

        private List<Claim> _claims;
        private string _username;
        private string _name;
        private int _id;
        private int _personId;
        public bool IsAuthenticated { get; private set; }

        public string DisplayName
        {
            get
            {
                if (!IsAuthenticated)
                    Initialize();

                return _name;

            }
        }
      
        public int ID
        {
            get
            {
                if (!IsAuthenticated)
                    Initialize();

                return _id;
            }
        }

        public int PersonId
        {
            get
            {
                if (!IsAuthenticated)
                    Initialize();

                return _personId;
            }
        }


        public string Username
        {
            get
            {
                if (!IsAuthenticated)
                    Initialize();

                return _username;
            }
        }


        
        public string[] Roles
        {
            get
            {
                if (!IsAuthenticated)
                    Initialize();
                return _claims.Where(x => x.Type == ClaimTypes.Role).Select(s => s.Value).ToArray();
            }
        }
        public bool HasRole(string role)
        {
                if (!IsAuthenticated)
                    Initialize();

                return Roles.Contains(role);
        }

    }
}
