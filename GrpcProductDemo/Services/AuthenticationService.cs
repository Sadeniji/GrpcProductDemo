using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcProductDemo.Protos;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace GrpcProductDemo.Services;

public class AuthenticationService : AuthProtoService.AuthProtoServiceBase
{
    public override async Task<CreateIdentityResponse> GenerateToken(Empty request, ServerCallContext context)
    {
       var expiration = DateTime.UtcNow.AddHours(1);

       Claim[] claims = [new(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString())];

       var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Secret_Key_Here_Replace_with_your_secret"));
       var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

       var token = new JwtSecurityToken(
           issuer: "https://localhost:7107",
           audience: "https://localhost:7107",
           claims: claims,
           expires: expiration,
           signingCredentials: creds);

       string _token = new JwtSecurityTokenHandler().WriteToken(token);
       await Task.Delay(100);
       return new CreateIdentityResponse { Token = _token };
    }
}