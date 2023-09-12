﻿using Microsoft.AspNetCore.Mvc;
using Application_test_repo.Repos;
using Application_test_repo.Modal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Application_test_repo.Services;

namespace Application_test_repo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly Test_DBContext _dbContext;
        private readonly JwtSettings _jwtSettings;
        private readonly IRefreshHandlercs _refreshHandler;
        public AuthorizeController(Test_DBContext context, IOptions<JwtSettings> options,IRefreshHandlercs refreshHandlercs)
        {
            this._dbContext = context;
            this._jwtSettings = options.Value;
            this._refreshHandler = refreshHandlercs;
            
        }

        [HttpPost("GenerateToken")]
        public async Task<IActionResult> GenerateToken([FromBody] UserCred userCred)
        {
            var user = await this._dbContext.TblUsers.FirstOrDefaultAsync(item => item.Code == userCred.username && item.Password == userCred.password);
            if (user != null)
            {
                //generate token
                var tokenhandler = new JwtSecurityTokenHandler();
                var tokenkey = Encoding.UTF8.GetBytes(this._jwtSettings.securitykey);
                var tokendesc = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name,user.Code),
                        new Claim(ClaimTypes.Role,user.Role)
                    }),
                    Expires = DateTime.UtcNow.AddSeconds(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
                };
                var token = tokenhandler.CreateToken(tokendesc);
                var finaltoken = tokenhandler.WriteToken(token);
                return Ok(new TokenResponse() { Token = finaltoken, RefreshToken = await this._refreshHandler.GenerateToken(userCred.username) });

            }
            else
            {
                return Unauthorized();
            }

        }

        [HttpPost("GenerateRefreshToken")]
        public async Task<IActionResult> GenerateRefreshToken([FromBody] TokenResponse token)
        {
            var _refreshtoken = await this._dbContext.TblRefreshtokens.FirstOrDefaultAsync(item => item.RefreshToken == token.RefreshToken);
            if (_refreshtoken != null)
            {
                var tokenhandler = new JwtSecurityTokenHandler();
                var tokenkey = Encoding.UTF8.GetBytes(this._jwtSettings.securitykey);
                SecurityToken securityToken;

                var principal = tokenhandler.ValidateToken(token.Token, new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(tokenkey),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                }, out securityToken);

                var _token = securityToken as JwtSecurityToken;
                if (_token != null && _token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
                {
                    string username = principal.Identity?.Name;

                    var _existdata = this._dbContext.TblRefreshtokens.FirstOrDefaultAsync(item => item.UserId == username
                    && item.RefreshToken == token.RefreshToken);

                    if (_existdata != null)
                    {
                        var _newtoken = new JwtSecurityToken(

                            claims: principal.Claims.ToArray(),
                            expires: DateTime.Now.AddSeconds(30),
                            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._jwtSettings.securitykey)),
                            SecurityAlgorithms.HmacSha256)
                        );

                        var _finaltoken = tokenhandler.WriteToken(_newtoken);
                        return Ok(new TokenResponse()
                        {
                            Token = _finaltoken,
                            RefreshToken = await this._refreshHandler.GenerateToken(username)
                        });
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return Unauthorized();
                }

            }
            else
            {
                return Unauthorized();
            }
            
        }
    }
}
