using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Quiz.Application.Interfaces;
using Quiz.Data;
using Quiz.Data.Models;
using Quiz.Data.Models.Dtos;
using Quiz.Models.Dtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Quiz.Application.Implementations
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _iconfiguration;
        private readonly QuizDbContext _db;
        public JWTService(IConfiguration iconfiguration, QuizDbContext db)
        {
            _iconfiguration = iconfiguration;
            _db = db;
        }

        public RegistrationResponseDto AddUser(User user)
        {
            var result = _db.Add(user);
            RegistrationResponseDto rs = new RegistrationResponseDto();
            if (result!=null)
            {
                rs.IsSuccessfulRegistration = true;
            }
            else
            {
                rs.IsSuccessfulRegistration = true;
            }
            return rs;
        }

        public Tokens Authenticate(User user)
        {
            var resultUser = _db.Users.Any(a => a.UserName == user.UserName && a.Password == user.Password);
            if (!resultUser)
            {
                return null;
            }
            // Else we generate JSON Web Token
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfiguration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
             
                new Claim("DateOflogin", DateTime.Now.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_iconfiguration["Jwt:Issuer"],
                _iconfiguration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new Tokens {Token= new JwtSecurityTokenHandler().WriteToken(token) };
        }

        public User GetUser(LoginViewModel loginView)
        {
            try
            {
                var user = _db.Users.Where(a => a.UserName == loginView.UserName && a.Password == loginView.Password).FirstOrDefault();
                return user;

            }
            catch (Exception)
            {

                throw;
            }    
           
        }
    }
}
