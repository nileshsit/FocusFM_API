
using FocusFM.Model.Token;

namespace FocusFM.Service.JWTAuthentication
{
    public interface IJWTAuthenticationService
    {
        AccessTokenModel GenerateToken(TokenModel userToken, string JWT_Secret, int JWT_Validity_Mins);
        TokenModel GetUserTokenData(string jwtToken=null);
    }
}