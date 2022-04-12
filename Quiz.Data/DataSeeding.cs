﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quiz.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quiz.Data
{
   public static class DataSeeding
    {
        public static void Seed(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<QuizDbContext>();
            if (context.Users.Count() == 0)
            {
                context.Users.AddRange(
                    new List<User>() {
                         new User() {Id=Guid.NewGuid(), FirstName="Ahmet", LastName="Demirel", UserName="admin",Password="123",Email="admin@gmail.com",PhoneNumber="46465"},
                         new User() {Id=Guid.NewGuid(), FirstName="Mehmet", LastName="Çetin", UserName="manager",Password="123",Email="manager@gmail.com",PhoneNumber="678768"},
                    }); 
            }
            context.SaveChanges();
        }
    }
}
