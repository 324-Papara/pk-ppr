using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Para.Base.Token;
using Para.Data.Domain;
using Para.Data.UnitOfWork;

namespace Para.Bussiness.Token;

public class TokenService : ITokenService
{
    private readonly JwtConfig jwtConfig;
    
    public TokenService(JwtConfig jwtConfig)
    {
        this.jwtConfig = jwtConfig;
    }
 
    public async Task<string> GetToken(User user)
    {
        return await GenerateToken(user);
    }

    public async Task<string> GenerateToken(User user)
    {
        Claim[] claims = GetClaims(user);
        var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);
        
        JwtSecurityToken jwtToken = new JwtSecurityToken(
            jwtConfig.Issuer,
            jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
        );

        string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return token;
    }
    
    private Claim[] GetClaims(User user)
    {
        var claims = new[]
        {
            new Claim("UserName",user.UserName),
            new Claim("UserId",user.Id.ToString()),          
            new Claim("Role",user.Role),
            new Claim("Status",user.Status.ToString()),
            new Claim(ClaimTypes.Role,user.Role),
            new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName}")
        };

        return claims;
    }
    
}