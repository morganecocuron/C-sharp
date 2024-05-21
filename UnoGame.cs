using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brouillon_projet
{
    public class UnoGame
    {
        private Stack stack;
        private List<Player> players;
        private int currentPlayerIndex;
        private Card topCard;
        private bool reverseOrder;
        private CardColor currentColor;

        public UnoGame(List<Card> initialCards, int numPlayers)
        {
            stack = new Stack(initialCards);
            stack.Shuffle();

            players = new List<Player>();
            for (int i = 0; i < numPlayers; i++)
            {
                players.Add(new Player());
            }

            currentPlayerIndex = 0;
            topCard = stack.Draw(); // Prendre une carte aléatoire au début du jeu
            reverseOrder = false;
            currentColor = topCard.Color;
        }

        public void DistributeCards(int numCards)
        {
            foreach (Player player in players)
            {
                for (int i = 0; i < numCards; i++)
                {
                    Card drawnCard = stack.Draw();
                    if (drawnCard != null)
                    {
                        player.DrawCard(drawnCard);
                    }
                    else
                    {
                        Console.WriteLine("No cards left in the stack.");
                        break;
                    }
                }
            }
        }

        public void PlayGame()
        {
            // Distribuer 7 cartes à chaque joueur au début
            DistributeCards(7);

            // Boucle de jeu principale
            while (true)
            {
                Player currentPlayer = players[currentPlayerIndex];
                Console.WriteLine($"Turn of Player {currentPlayerIndex + 1}");

                // Afficher la carte sur le dessus de la pile
                Console.WriteLine("Top card on the stack:");
                topCard.DisplayDetails();

                // Afficher les cartes disponibles pour le joueur actuel
                currentPlayer.DisplayHand();

                bool validChoice = false;
                while (!validChoice)
                {
                    // Demander au joueur de choisir une carte
                    Console.WriteLine("Choose a card to play (enter card ID):");
                    int chosenCardId = int.Parse(Console.ReadLine());

                    // Vérifier si la carte choisie est jouable
                    Card chosenCard = currentPlayer.Hand.Find(card => card.Id == chosenCardId);
                    if (chosenCard != null && (chosenCard.Color == topCard.Color || chosenCard.Type == topCard.Type || (chosenCard.Number.HasValue && chosenCard.Number == topCard.Number)))
                    {
                        // Jouer la carte
                        Console.WriteLine($"Player {currentPlayerIndex + 1} plays:");
                        chosenCard.DisplayDetails();
                        topCard = chosenCard; // La carte jouée devient la nouvelle top card
                        currentPlayer.RemoveCard(chosenCard);

                        // Vérifier si le joueur a gagné
                        if (currentPlayer.IsEmpty())
                        {
                            Console.WriteLine($"Player {currentPlayerIndex + 1} wins!");
                            return;
                        }

                        validChoice = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid card choice. You must play a card with the same color, type, or number.");
                        currentPlayer.DisplayHand();
                    }
                }

                Console.WriteLine(); // Espacement entre les tours

                // Passer au joueur suivant
                currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
            }
        }
    }
}
