using System;
using System.Net.Security;
using System.Runtime.CompilerServices;

public class Character
{
    public string name { get; set; }
    public int level { get; set; }
    public int maxhp => level * 10;
    public int currenthp { get; set; }

    public Character(int level, string name)
    {
        this.level = level;
        this.name = name;
        currenthp = maxhp;
    }
    public void attack(Character enemy)
    {
        int random = new Random().Next(1, 11);
        int damage = level + random;
        if (random > 7)
        {
            Console.WriteLine("Colpo Critico!");
        }
        enemy.currenthp -= damage;
        Console.WriteLine($"L'attacco infligge {damage} danni! HP rimasti al nemico: {enemy.currenthp}.");
        if (enemy.currenthp <= 0)
        {
            Console.WriteLine($"{this.name} sconfigge {enemy.name}! Grazie per aver giocato a Simple RPG, by RickyAll.");
            Environment.Exit(0);
        }
    }
}

class Hero : Character
{
    public Hero(int level, string name) : base(level, name)
    {
    }
}

class Villain : Character
{
    public Villain(int level, string name) : base(level, name)
    {
    }
}

public class Encounter
{
    private static Character Chargen()
    {
        string name = Console.ReadLine();
        while (String.IsNullOrEmpty(name))
        {
            Console.WriteLine("Digita un nome valido.");
            name = Console.ReadLine();
        }
        Console.WriteLine($"Digitare il livello personaggio di {name}");
        int level;
        bool isParsable = Int32.TryParse(Console.ReadLine(), out level);
        while (!isParsable)
        {
            Console.WriteLine("Digitare un numero valido.");
            isParsable = Int32.TryParse(Console.ReadLine(), out level);
        }
        return new Character(level, name);
    }

    static void Main()
    {
        Console.WriteLine("Benvenuto in Simple RPG, digitare il nome dell'Eroe:");
        Character player = Chargen();
        Console.WriteLine($"Salve {player.name}, livello {player.level}");
        Console.WriteLine("Digitare il nome del Nemico");
        Character enemy = Chargen();
        Console.WriteLine($"L'Eroe {player.name} si prepara a scontrarsi contro {enemy.name}!");
        while (player.currenthp > 0 && enemy.currenthp > 0)
        {
            int randomizer = new Random().Next(0, 2);
            if (randomizer == 0)
            {
                Console.WriteLine("L'Eroe vince l'Iniziativa per questo turno!");
                Console.WriteLine($"{player.name} attacca!");
                player.attack(enemy);
                Thread.Sleep(2000);
                Console.WriteLine($"{enemy.name} attacca!");
                enemy.attack(player);
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("Il nemico vince l'Iniziativa per questo turno!");
                Console.WriteLine($"{enemy.name} attacca!");
                Thread.Sleep(2000);
                enemy.attack(player);
                Console.WriteLine($"{player.name} attacca!");
                Thread.Sleep(2000);
                player.attack(enemy);
            }
        }

    }
}