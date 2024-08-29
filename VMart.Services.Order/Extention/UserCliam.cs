using System.Security.Claims;

namespace VMart.Services.Order.Extention
{
    public static class UserCliam
    {
        public static string GetLoginName(this ClaimsPrincipal claimsPrincipal)
        {
            var Userid = claimsPrincipal.FindFirst(ClaimTypes.Name);
            return Userid.Value;
        }
        public static T GetLogingId<T>(this ClaimsPrincipal claimsPrincipal)
        {
            var Userid = claimsPrincipal.FindFirst("UserId");
            return (T)Convert.ChangeType(Userid.Value, typeof(T));
        }
        public static string GetLoginEmail(this ClaimsPrincipal claimsPrincipal)
        {
            var Userid = claimsPrincipal.FindFirst(ClaimTypes.Email);
            return Userid.Value;
        }
    }
}
