using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectArchive.Projects.AstroneerAnniversaryCalc
{
    [Project("Astroneer Anniversary Item Calculator", 1711753200, timeEnd: 1711753200, "Calculator for required items for astroneer recipes from the birthday event")]
    internal class Program : IProject
    {
        private static readonly (string, int)[] _ingredients =
        {
            ( "Leek", 0 ),
            ( "Resipound", 0 ),
            ( "Cosmic Bauble", 0 ),
            ( "Automaton 009", 0 ),
            ( "Noxothane", 0 ),
            ( "Unknown Biofuel", 0 ),
            ( "Squashothane", 0 ),
            ( "Cosmic Automaton", 0 ),
            ( "Cosmic Squash", 0 ),
            ( "Noxomation 002", 0 ),
            ( "Omnugget", 0 ),
            ( "Burrito?", 0 ),
        };

        private static readonly Dictionary<string, string[]> _recipes = new()
        {
            { "Burrito?", ["Resipound", "Leek", "Squashothane", "Omnugget"] },
            { "Omnugget", ["Cosmic Automaton", "Cosmic Squash", "Noxomaton 002"] },
            { "Noxomaton 002", ["Noxothane", "Automaton 009", "Leek"] },
            { "Cosmic Squash", ["Cosmic Bauble", "Unknown Biofuel", "Resipound"] },
            { "Squashothane", ["Noxothane", "Unknown Biofuel"] },
            { "Cosmic Automaton", ["Cosmic Bauble", "Automaton 009"] }
        };

        public void Run()
        {
            var position = 0;
            while (true)
            {
                DisplayAll(_ingredients[position].Item1);

                var key = Console.ReadKey().Key;
                Console.Clear();
                if (key == ConsoleKey.Enter)
                    break;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        position = Math.Max(position - 1, 0);
                        break;
                    case ConsoleKey.DownArrow:
                        position = Math.Min(position + 1, _ingredients.Length - 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        if (_ingredients[position].Item2 > 0)
                            _ingredients[position].Item2--;
                        break;
                    case ConsoleKey.RightArrow:
                        _ingredients[position].Item2++;
                        break;
                    default: break;
                }
            }

            while (Craft("Burrito?"))
            {
                var index = GetIngredientIndex("Burrito?");
                if (index.HasValue)
                    Console.WriteLine(_ingredients[index.Value].Item2);
            }
        }

        private static int? GetIngredientIndex(string ingredient)
        {
            for (int i = 0; i < _ingredients.Length; i++)
            {
                if (_ingredients[i].Item1 == ingredient)
                    return i;
            }
            return null;
        }

        private static void DisplayAll(string position)
        {
            var total = new List<string>();
            foreach (var (name, amount) in _ingredients)
            {
                var prefix = name == position ? '>' : ' ';
                total.Add($"{prefix} {name} ({amount})");
            }
            Console.WriteLine(string.Join("\n", total));
        }

        private static bool Craft(string item)
        {
            Console.WriteLine($"Crafting {item}...");

            if (!_recipes.TryGetValue(item, out var recipe))
            {
                Console.WriteLine($"Unable to find recipe for {item}");
                return false;
            }

            foreach (var recipeItem in recipe)
            {
                if (!HasItem(recipeItem, false))
                {
                    Console.WriteLine($"Does not own item {recipeItem}");
                    if (!Craft(recipeItem))
                    {
                        Console.WriteLine($"Could not craft item {recipeItem}");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine($"Owns item {recipeItem}");
                }

                if (!HasItem(recipeItem, true))
                {
                    Console.WriteLine($"Could not consume item {recipeItem}");
                    return false;
                }
            }

            var index = GetIngredientIndex(item);
            if (!index.HasValue)
            {
                Console.WriteLine($"Couldnt find result {item} in ingredients");
                return false;
            }
            _ingredients[index.Value].Item2++;
            Console.WriteLine($"Crafted {item}");
            return true;
        }

        private static bool HasItem(string item, bool consume)
        {
            var index = GetIngredientIndex(item);
            if (!index.HasValue)
                return false;
            if (_ingredients[index.Value].Item2 < 1)
                return false;

            if (consume)
                _ingredients[index.Value].Item2--;

            return true;
        }
    }
}
