﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MeuLivroDeReceitas.Application.Servicos.Token;

public class TokenController
{
    private const string EmailAlias = "eml";
    private readonly double _tempoDeVidaDoTokenEmMinutos;
    private readonly string _chaveDeSeguranca;

    public TokenController(double tempoDeVidaDoTokenEmMinutos, string chaveDeSeguranca)
    {
        _tempoDeVidaDoTokenEmMinutos = tempoDeVidaDoTokenEmMinutos;
        _chaveDeSeguranca = chaveDeSeguranca;
    }

    public string GerarToken(string emailDoUsuario)
    {
        var claims = new List<Claim>
        {
            new Claim(EmailAlias, emailDoUsuario),
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_tempoDeVidaDoTokenEmMinutos),
            SigningCredentials = new SigningCredentials(SimetriCkey(), SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    public string RecuperarEmail(string token)
    {
        var claims = ValidarToken(token);
        return claims.FindFirst(EmailAlias).Value;

    }

    public ClaimsPrincipal ValidarToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var parametrosValidacao = new TokenValidationParameters
        {
            RequireExpirationTime = true,
            IssuerSigningKey = SimetriCkey(),
            ClockSkew = new TimeSpan(0),
            ValidateIssuer = false,
            ValidateAudience = false,

        };

       var claims =  tokenHandler.ValidateToken(token, parametrosValidacao,out _);

        return claims;
    }



    private SymmetricSecurityKey SimetriCkey()
    {
        var symmetrichey = Convert.FromBase64String(_chaveDeSeguranca);
        return new SymmetricSecurityKey(symmetrichey);
    }
}


