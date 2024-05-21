

using brouillon_projet;
using System;
using System.Collections.Generic;
using System.Linq;
using MySqlConnector; 


// Définition des couleurs des cartes
public enum CardColor { Red, Green, Blue, Yellow, Black }

// Définition des types de cartes
public enum CardType { Number, Skip, Reverse, DrawTwo, Wild, WildDrawFour }

class Program
{
    static void Main(string[] args)
    {
        MariaDB newConnection = new MariaDB();
        newConnection.testConnection();
        bool access_ok = false;
        string username = "";


        do
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1 - Se connecter / S'inscrire");
            Console.WriteLine("2 - Liste des joueurs");
            Console.WriteLine("3 - Quitter");
            Console.WriteLine("Enter your choice:");
            int choice = Convert.ToInt32(Console.ReadLine());

            access_ok = MenuChoice(choice, access_ok, newConnection, ref username);

            
        } while (!access_ok);


    
        Console.WriteLine("Enter the number of players:");
        int numPlayers = int.Parse(Console.ReadLine());

        for (int i = 0; i < numPlayers; i++)
        {
            Console.WriteLine($"Enter username for player {i + 1}:");
            string playerName = Console.ReadLine();
            Console.WriteLine($"Enter password for {playerName}:");
            string playerPassword = Console.ReadLine();

            newConnection.CreateOrRetrieveUser(playerName, playerPassword);
        }

        // Création des cartes Uno
        List<Card> unoCards = new List<Card>();
        int idCounter = 1;
        foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
        {
            if (color == CardColor.Black) continue; 

            for (int i = 0; i <= 9; i++)
            {
                if (i == 0)
                {
                    unoCards.Add(new NumberCard(idCounter++, color, i));
                }
                else
                {
                    unoCards.Add(new NumberCard(idCounter++, color, i));
                    unoCards.Add(new NumberCard(idCounter++, color, i));
                }
            }

            for (int i = 0; i < 2; i++)
            {
                unoCards.Add(new SpecialCard(idCounter++, CardType.Skip, color));
                unoCards.Add(new SpecialCard(idCounter++, CardType.Reverse, color));
                unoCards.Add(new SpecialCard(idCounter++, CardType.DrawTwo, color));
            }
        }

        for (int i = 0; i < 4; i++)
        {
            unoCards.Add(new SpecialCard(idCounter++, CardType.Wild, CardColor.Black));
            unoCards.Add(new SpecialCard(idCounter++, CardType.WildDrawFour, CardColor.Black));
        }

        // Initialisation du jeu Uno avec le nombre de joueurs spécifié
        UnoGame unoGame = new UnoGame(unoCards, numPlayers);
        Console.WriteLine("Starting Uno game...");
        unoGame.PlayGame();

        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.L)
                {
                    newConnection.ListTable();
                }
                else if (key == ConsoleKey.M)
                {
                    // Afficher le score principal du joueur actuel
                    Console.WriteLine("Enter your score:");
                    int score = int.Parse(Console.ReadLine());
                    newConnection.AddScore(username, score);

                    Console.WriteLine("Main scores for " + username + ":");
                    newConnection.ShowUserScores(username);
                }
            }
        }
    }

 

    public static bool MenuChoice(int choice, bool access_ok, MariaDB newConnection, ref string username)
    {
        switch (choice)
        {
            case 1:
                do
                {
                    Console.WriteLine("Enter your username:");
                    username = Console.ReadLine();
                    Console.WriteLine("Enter your password:");
                    string password = Console.ReadLine();
                    access_ok = newConnection.Login(username, password);
                    if (!access_ok)
                    {
                        Console.WriteLine("Invalid username or password. Please try again.");
                        if (Console.ReadLine().ToLower() == "y")
                        {
                            newConnection.createUser(username, password);
                            access_ok = true;
                        }
                    }
                } while (!access_ok);

                break;
            case 2:
                Console.WriteLine("List of players:");
                newConnection.ListTable();
                break;
            case 3:
                Console.WriteLine("Goodbye!");
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
        return access_ok;
    }

}





