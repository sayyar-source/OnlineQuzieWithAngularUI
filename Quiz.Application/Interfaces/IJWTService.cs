using Quiz.Data.Models;
using Quiz.Data.Models.Dtos;
using Quiz.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz.Application.Interfaces
{
   public interface IJWTService
    {
        Tokens Authenticate(User user);
        RegistrationResponseDto AddUser(User user);
        User GetUser(LoginViewModel loginView);
    }
}
