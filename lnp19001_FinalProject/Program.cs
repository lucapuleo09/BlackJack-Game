using System;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new game and start playing
            var game = new BlackjackGame();
            game.Play();
        }
    }
    class BlackjackGame
    {
        // Method to start the game
        public void Play()
        {
            // Keep playing until the player wants to stop
            bool playing = true;
            while (playing)
            {
                // Create a new deck and shuffle it
                var deck = new Deck();
                deck.Shuffle();

                // Deal two cards to the player and the dealer
                var playerHand = new Hand();
                playerHand.AddCard(deck.DealCard());
                playerHand.AddCard(deck.DealCard());

                var dealerHand = new Hand();
                dealerHand.AddCard(deck.DealCard());
                dealerHand.AddCard(deck.DealCard());


                // Show the player's hand and the value of the hand
                Console.WriteLine("Your hand:");
                playerHand.Show();
                Console.WriteLine($"Hand value: {playerHand.GetValue()}");

                // Show the dealer's hand and the value of the hand
                Console.WriteLine("Dealer hand:");
                dealerHand.Show();
                Console.WriteLine($"Hand value: {dealerHand.GetValue()}");

                // Check if the player has blackjack
                if (playerHand.IsBlackjack())
                {
                    Console.WriteLine("You have blackjack!");
                }
                else
                {
                    // Allow the player to hit or stand
                    bool playerDone = false;
                    bool dealerDone = false;
                    while (!playerDone)
                    {
                        Console.WriteLine("Do you want to hit or stand (h/s)?");
                        var input = Console.ReadLine();
                        if (input == "h")
                        {
                            // Deal a card to the player
                            playerHand.AddCard(deck.DealCard());

                            // Show the player's hand and the value of the hand
                            Console.WriteLine("Your hand:");
                            playerHand.Show();
                            Console.WriteLine($"Hand value: {playerHand.GetValue()}");

                            if (playerHand.IsBlackjack2())
                            {
                                Console.WriteLine("You have blackjack!");
                                dealerDone = true;
                                playerDone = true;
                            }

                            // Check if the player has busted
                            if (playerHand.IsBusted())
                            {
                                playerDone = true;
                                dealerDone = true;
                            }
                        }

                        if (input == "s")
                        {
                            Console.WriteLine("Your hand:");
                            playerHand.Show();
                            Console.WriteLine($"Hand value: {playerHand.GetValue()}");
                            playerDone = true;

                        }

                        else if (input != "h" && input != "s")
                            try
                            {
                                throw new Exception("Invalid input. Please try again.");
                            }

                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);

                            }

                    }
                            // Allow the dealer to hit or stand
                             
                            while (!dealerDone)
                            {
                                // Check if the dealer has blackjack or busted
                                if (dealerHand.IsBlackjack() || dealerHand.IsBusted())
                                {
                                    dealerDone = true;
                                }
                                else
                                {
                                    // Check if the dealer should hit or stand
                                    if (dealerHand.GetValue() < playerHand.GetValue())
                                    {
                                        // Deal a card to the dealer
                                        dealerHand.AddCard(deck.DealCard());

                                        // Show the dealer's hand and the value of the hand
                                        Console.WriteLine("Dealer hand:");
                                        dealerHand.Show();
                                        Console.WriteLine($"Hand value: {dealerHand.GetValue()}");
                                    }
                                    else
                                    {
                                        dealerDone = true;
                                    }
                                }
                            }

                            // Check who won the game
                            if (playerHand.IsBusted())
                            {
                                Console.WriteLine("You busted! Dealer wins!");
                            }
                            else if (dealerHand.IsBusted())
                            {
                                Console.WriteLine("Dealer busted! You win!");
                            }
                            else if (playerHand.GetValue() > dealerHand.GetValue() && playerHand.GetValue() < 21)
                            {
                                Console.WriteLine("You win!");
                            }
                            else
                            {
                                Console.WriteLine("Dealer wins!");
                            }
                        



                            Console.WriteLine("Do you want to play again (y/n)?");
                            var playAgain = Console.ReadLine();
                            if (playAgain == "y")
                            {
                                Play();
                            }
                            else if (playAgain == "n")
                            {
                                playing = false;
                            }
                            else
                            {
                                try
                                {
                                    throw new Exception("Invalid input. Please try again.");
                                }

                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);

                                }

                            }

                        }
                    }
                }
            }
        }
        class Card
        {
            // Suit and rank of the card
            private int suit;
            private int rank;

            // Constructor to create a new card
            public Card(int suit, int rank)
            {
                this.suit = suit;
                this.rank = rank;
            }

            // Method to get the value of the card
            public int GetValue()
            {
                // Check if the rank is a face card
                if (rank >= 11)
                {
                    return 10;
                }
                else
                {
                    return rank;
                }
            }

            // Override the ToString() method to display the card
            public override string ToString()
            {
                // Get the suit and rank of the card
                var suitString = "";
                switch (suit)
                {
                    case 0:
                        suitString = "♠ ";
                        break;
                    case 1:
                        suitString = "♥";
                        break;
                    case 2:
                        suitString = "♦";
                        break;
                    case 3:
                        suitString = "♣";
                        break;
                }

                var rankString = "";
                switch (rank)
                {
                    case 1:
                        rankString = "A";
                        break;
                    case 11:
                        rankString = "J";
                        break;
                    case 12:
                        rankString = "Q";
                        break;
                    case 13:
                        rankString = "K";
                        break;
                    default:
                        rankString = rank.ToString();
                        break;
                }

                // Return the string representation of the card
                return $"{suitString}{rankString}";
            }
        }

        class Deck
        {
            // List of cards in the deck
            private List<Card> cards;

            // Constructor to create a new deck
            public Deck()
            {
                // Create a new list of cards
                cards = new List<Card>();

                // Add all 52 cards to the deck
                for (int suit = 0; suit < 4; suit++)
                {
                    for (int rank = 1; rank <= 13; rank++)
                    {
                        cards.Add(new Card(suit, rank));
                    }
                }
            }

            // Method to shuffle the deck
            public void Shuffle()
            {
                // Randomly shuffle the list of cards
                var random = new Random();
                cards = cards.OrderBy(c => random.Next()).ToList();
            }

            // Method to deal a card from the deck
            public Card DealCard()
            {
                // Return the first card in the deck
                var card = cards[0];
                cards.RemoveAt(0);
                return card;
            }
        }

        class Hand
        {
            // List of cards in the hand
            private List<Card> cards;

            // Constructor to create a new hand
            public Hand()
            {
                // Create a new list of cards
                cards = new List<Card>();
            }

            // Method to add a card to the hand
            public void AddCard(Card card)
            {
                // Add the card to the list of cards in the hand
                cards.Add(card);
            }

            // Method to show the cards in the hand
            public void Show()
            {
                // Loop through the list of cards and display each one
                foreach (var card in cards)
                {
                    Console.WriteLine(card);
                }
            }

            // Method to get the value of the hand
            public int GetValue()
            {
                // Initialize the value of the hand to 0
                int value = 0;

                // Loop through the list of cards and add the value of each one
                foreach (var card in cards)
                {
                    value += card.GetValue();
                }

                // Return the value of the hand
                return value;
            }

            // Method to check if the hand has blackjack
            public bool IsBlackjack()
            {
                // Check if the hand has two cards and the value is 21
                if (cards.Count == 2 && GetValue() == 21)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
    public bool IsBlackjack2()
    {
        // Check if the hand has two cards and the value is 21
        if (cards.Count >= 2 && GetValue() == 21)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Method to check if the hand is busted
    public bool IsBusted()
            {
                // Check if the value of the hand is greater than 21
                if (GetValue() > 21)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    



