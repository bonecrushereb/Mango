using Mango.Web.Service.IService;

namespace Mango.Web.Service
{
    public class TokenProvider : ITokenProvider
    {
        public IHttpContextAccessor _contextAccessor { get; }
        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            
        }
        public void ClearToken()
        {
            _contextAccessor.HttpContext.Response.Cookies.Delete(SD.TokenCookie);
        }

        public string GetToken()
        {
            string token = null;
            bool hasToken = _contextAccessor.HttpContext.Request.Cookies.TryGetValue(SD.TokenCookie, out token);
            return hasToken is true ? token : null;
        }

        public void SetToken(string token)
        {
            _contextAccessor.HttpContext.Response.Cookies.Append(SD.TokenCookie, token);
        }
    }
}