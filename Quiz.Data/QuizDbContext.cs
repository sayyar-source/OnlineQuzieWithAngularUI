using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quiz.Data.Models;
using System;

namespace Quiz.Data
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        
        

        }
        public DbSet<Quizes> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }  
        public DbSet<User> Users { get; set; }
    }
}
