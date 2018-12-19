using Data;
using Web.Api.Main.Models;

namespace Web.Api.Main.Servicies
{
    public interface IAuth
    {
        bool IsAuthenticated(string token);
        LoginResponseModel Authenticate(string username, string password);
    }
}
