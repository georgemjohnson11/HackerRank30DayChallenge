using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SamuraiApp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            InsertMultipleSamurais();
        }

        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "former Ninja" };
            var samuraiSammy = new Samurai { Name = "former pirate" };
            using (var context = new SamuraiContext())
            {
                context.Samurais.Add(samurai);
                context.Samurais.Add(samuraiSammy);
                context.SaveChanges();
            }
        }

        private static void InsertMultipleSamuraisViaBatch()
        {
            //EF Core 2.1 added a minimum batch size which defaults to 4
            //this 4 object insert WILL be batched
            var samurai1 = new Samurai { Name = "Samurai1" };
            var samurai2 = new Samurai { Name = "Samurai2" };
            var samurai3 = new Samurai { Name = "Samurai3" };
            var samurai4 = new Samurai { Name = "Samurai4" };
            using (var context = new SamuraiContext())
            {
                context.Samurais.AddRange(samurai1, samurai2, samurai3, samurai4);
                context.SaveChanges();
            }
        }

        private static void SimpleSamuraiQuery()
        {
            using (var context = new SamuraiContext())
            {
                var samurais = context.Samurais.ToList();
                //var query = context.Samurais;
                //var samuraisAgain = query.ToList();
                foreach (var samurai in context.Samurais)
                {
                    Console.WriteLine(samurai.Name);
                }
            }
        }


    }
}
