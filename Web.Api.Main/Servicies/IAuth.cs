using Data;

namespace Web.Api.Main.Servicies
{
    public interface IAuth
    {
        bool IsAuthenticated(string token);
        string Authenticate(string username, string password);
    }
}
