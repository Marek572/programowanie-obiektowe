using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Lab9
{
    class Program
    {
        static void Main(string[] args)
        {
            AppContext context = new AppContext();
            //context.Database.EnsureCreated();
            context.InsertData();

            IQueryable<Car> cars = from car in context.Cars
            where car.Power > 100
            select car;

            var carUsers = from car in context.Cars
            join user in context.Users
            on car.Id equals user.CarId
            where car.Power > 100
            select new { UserId = user.Id, CarId = car.Id, UserName = user.Name };
            Console.WriteLine(string.Join("\n", cars));
        }
    }


    public record Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public decimal Power { get; set; }
    }
    
    public record User
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string Name { get; set; }
    }

    class AppContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DATASOURCE = d:\\database\\sqltest.db");
        }

        public void InsertData()
        {
            this.Cars.Add(new Car() { Model = "Audi A4", Power = 200 });
            this.Cars.Add(new Car() { Model = "Audi A5", Power = 300 });
            this.Cars.Add(new Car() { Model = "Audi A6", Power = 400 });
            this.Users.Add(new User() { Id = 1, CarId = 3, Name = "Darek" });
            this.Users.Add(new User() { Id = 2, CarId = 2, Name = "Arek" });
            this.Users.Add(new User() { Id = 3, CarId = 1, Name = "Eryk" });
            this.SaveChanges();
        }
    }

}
