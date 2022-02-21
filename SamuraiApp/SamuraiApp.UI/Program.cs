using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Linq;

namespace SamuraiApp.UI
{
    internal class Program
    {
        private static SamuraiContext _context = new SamuraiContext();
        static void Main(string[] args)
        {
            //_context.Database.EnsureCreated();
            //AddSamuraiByName("Chio25");
            //GetSamurais();
            //GetSamuraisByName("Chio2");
            //UpdateNameByName("Chio2", "Chio22");
            //AddSamuraiAndQuote();
            //AddQuoteToExistingSamurai();

            //GetQuotesBySamuraiName("Chio");


            //GetQuotesFilter();
            //GetQuotesFilterWithAnonymous();
            //ExplicitLoad();
            //UpdateQuoteBySamuraiName("Chio");

            //AddBattlesToDb(
            //    "Dan-no-Ura",
            //    "Third Battle of Uji",
            //    "Siege of Chihaya",
            //    "Anegawa",
            //    "Nagashino"
            //    );

            //AddSamuraiInBattle();
            //AddAllSamuraisToAllBattles();
            //GettAllSamuraisWithBattles();
            //RemoveSamurai();
            AddHorse();
            Console.ReadKey();
        }

        private static void AddSamuraiByName(params string[] names)
        {
            foreach (var name in names)
                _context.Samurais.Add(new Samurai() { Name = name });
            _context.SaveChanges();
        }
        private static void GetSamurais()
        {
            var samurais = _context.Samurais.ToList();
            Console.WriteLine($"Number of samurais is {samurais.Count()}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }

        // բոլոր էն ֆիլդերը, որոնց անունը տրված անունն է 
        private static void GetSamuraisByName(string name)
        {
            var samurais = _context.Samurais.Where(s => s.Name == name).ToList();

            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Id + " " + samurai.Name);
            }
        }

        private static void UpdateNameByName(string oldName, string newName)
        {
            var samurai = _context.Samurais.First(s => s.Name == oldName);
            samurai.Name = newName;
            _context.SaveChanges();
        }

        private static void AddSamuraiAndQuote()
        {
            var samurai = new Samurai() { Name = "Anna" };
            samurai.Quotes.Add(new Quote() { Text = "I am a samurai" });
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void AddQuoteToExistingSamurai()
        {
            //1e6b58e2 - 21cb - 449f - 251b - 08d9eb314220
            var quote = new Quote()
            {
                Text = "kill you",
                SamuraiId = Guid.Parse("1e6b58e2-21cb-449f-251b-08d9eb314220")
            };
            _context.Quotes.Add(quote);
            _context.SaveChanges();
        }
        private static void GetQuotesBySamuraiName(string name)
        {
            //var Quotes = _context.Samurais.First(s => s.Name == name).Include(s => s.Quotes);
            var Quotes = _context.Samurais.Include(s => s.Quotes).First(s => s.Name == name);
        }

        private static void GetQuotesFilter()
        {
            //var Quotes = _context.Samurais.First(s => s.Name == name).Include(s => s.Quotes);
            var samuraiAndQuotes = _context.Samurais
                .Include(s => s.Quotes).ToList();

            var quotes = _context.Samurais
                .Where(s => s.Quotes.Any(q => q.Text.Contains("kill")))
                .ToList();
        }

        // does not work properly
        private static void GetQuotesFilterWithAnonymous()
        {
            /*
            var Quotes = _context.Samurais
                .Include(s => s.Quotes.Select(q => new { text = q.Text }))
                .ToList();
            */
            //var Quotes = _context.Samurais.Select(s => new { name = s.Name });
        }

        private static void ExplicitLoad()
        {
            var samurai = _context.Samurais.First();
            // in case of one to many
            _context.Entry(samurai).Collection(s => s.Quotes).Load();
            // in case of one to one
            _context.Entry(samurai).Reference(s => s.Horse).Load();
        }

        private static void UpdateQuoteBySamuraiName(string name)
        {
            var samurais = _context.Samurais.Include(s => s.Quotes).First(s => s.Name == name);
            samurais.Quotes[0].Text += "added";
            _context.SaveChanges();
        }

        private static void AddBattlesToDb(params string[] names)
        {
            foreach (var name in names)
            {
                _context.Battles.Add(new Battle() { Name = name });
            }
            _context.SaveChanges();
        }


        private static void AddSamuraiInBattle()
        {
            var samurai = _context.Samurais.ToList()[0];
            var battle = _context.Battles.Find(Guid.Parse("9d19f52f-3bdc-49b4-2695-08d9f1e93fae"));
            battle.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void AddAllSamuraisToAllBattles()
        {
            var samurais = _context.Samurais.ToList();
            var battles = _context.Battles.Where(b => b.Id != Guid.Parse("9d19f52f-3bdc-49b4-2695-08d9f1e93fae")).ToList();

            foreach (var battle in battles)
            {
                battle.Samurais.AddRange(samurais);
            }
            _context.SaveChanges();
        }

        private static void GettAllSamuraisWithBattles()
        {
            var samuraisAndBattles = _context.Samurais.Include(s => s.Battles).Include(s => s.Horse).ToList();
            var battlesAndSamurais = _context.Battles.Include(b => b.Samurais).ToList();
        }

        private static void RemoveSamurai()
        {
            //var samuraisAndBattles = _context.Samurais.Include(s => s.Battles).ToList();
            var samurai = _context.Samurais.Find(Guid.Parse("9f482a0b-6628-4ce7-6bab-08d9f1ed560f"));
            _context.Samurais.Remove(samurai);
            _context.SaveChanges();
            GettAllSamuraisWithBattles();
        }

        private static void AddHorse()
        {
            var horse = new Horse() { Name = "Nobu", SamuraiId= Guid.Parse("df8e4ec6-9a63-484f-54e2-08d9f16e3346")};
            _context.Add(horse);
            _context.SaveChanges();
            GettAllSamuraisWithBattles();
        }

        //private static void RemoveSamuraiFromABattleExplicit()
        //{
        //    var b_s = _context.Set<BattleSamurai>()
        //        .SingleOrDefault(bs => bs.BattleId == 1 && bs.SamuraiId == 10);
        //    if (b_s != null)
        //    {
        //        _context.Remove(b_s); //_context.Set<BattleSamurai>().Remove works, too
        //        _context.SaveChanges();
        //    }
        //}
    }
}
