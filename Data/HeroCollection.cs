using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleNancyExample.Data
{
    // In memory data used for this example
    // It could be replaced by a database or a file
    class HeroCollection
    {
        private Hashtable heroes = new Hashtable();

        public HeroCollection()
        {
            // Always start with one Hero
            AddHero(new Hero()
            {
                Name = "Super Woman",
                Strength = 5
            });
        }

        public void AddHero(Hero hero)
        {
            try
            {
                heroes.Add(hero.Name, hero);
            }
            catch (ArgumentException e)
            {
                // Hero already exists
                // For now just write exception message to console and ignore
                Console.WriteLine(e.Message);
            }
        }
 
        public Hero GetHero(string name)
        {
            return heroes[name] as Hero;
        }

        public void PutHero(Hero hero)
        {
            if (heroes.ContainsKey(hero.Name))
            {
                DeleteHero(hero.Name);
            }
            AddHero(hero);
        }

        public void DeleteHero(string name)
        {
            heroes.Remove(name);
        }

        public Hero[] GetHeroes()
        {
            Hero[] heroArr = new Hero[heroes.Count];
            heroes.Values.CopyTo(heroArr, 0);
            return heroArr;
        }
    }
}
