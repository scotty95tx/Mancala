using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MancalaLibrary;
using MancalaLibrary.Models;

namespace Mancala
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WelcomeMessage();

            AskUserToStart();

            Console.WriteLine();

            PlayerModel player1 = GameLogic.CreatePlayer("Player 1");
            PlayerModel player2 = GameLogic.CreatePlayer("Player 2");
            MancalaBoardModel currentBoard = GameLogic.CreateMancalaBoard(player1, player1);

            PlayerModel activePlayer = player1;
            PlayerModel opponent = player2;
            PlayerModel winner = null;

            do
            {
                DisplayBoard(currentBoard, player1, player2);

                Console.WriteLine();
                Console.WriteLine();

                if (GameLogic.SowSeedsAndCheckIfActiveStatusSwaps(currentBoard, GetStartingPitFromUser(activePlayer), activePlayer, player1, player2))
                {
                    (activePlayer, opponent) = (opponent, activePlayer);
                }
                if (GameLogic.CheckIfWinner(currentBoard))
                {
                    winner = GameLogic.IdentifyWinner(player1, player2);
                }

                Console.Clear();

            } while (winner == null);

            Console.WriteLine($"Congratulations {winner.UsersName}, you have won the game!");
            Console.ReadLine();
        }

        private static int GetStartingPitFromUser(PlayerModel activePlayer)
        {
            bool isValidPit = false;
            int enteredPit = 0;

            do
            {
                Console.WriteLine($"Hi {activePlayer.UsersName}! Please enter the Pit # you would like to start from: ");
                string userInput = Console.ReadLine();
                isValidPit = int.TryParse(userInput, out int selectedPit);

                if (isValidPit)
                {
                    if (selectedPit <= 12 && selectedPit >= 1)
                    {
                        return selectedPit;
                    }
                } else
                {
                    Console.WriteLine("Sorry, that was not a valid pit number.");
                }

            } while (isValidPit == false);

            // Application is not building unless all paths return a value. It is not possible with the do/while loop for this enteredPit to ever actually return, but is necessary to avoid a build error.
            return 0;
        }

        private static void DisplayBoard(MancalaBoardModel currentBoard, PlayerModel player1, PlayerModel player2)
        {
            Console.Write("PlayerTwo\t");

            int pitNumber = 12;

            while (pitNumber > 6)
            {
                Console.Write($"Pit#{pitNumber}({currentBoard.Pits[pitNumber - 1].Seeds}Seeds) ");
                pitNumber--;
            }

            Console.Write("\t\tPlayerOne\t");
            Console.WriteLine();
            Console.Write($"Store({player2.StoreSeeds} Seeds)\t");

            pitNumber = 1;

            while (pitNumber < 7)
            {
                Console.Write($"Pit#{pitNumber}({currentBoard.Pits[pitNumber - 1].Seeds} Seeds) ");
                pitNumber++;
            }

            Console.Write($"\tStore({player1.StoreSeeds} Seeds)");
        }

        private static bool AskUserToStart()
        {
            Console.Write("Would you like to start playing? Type Yes to start playing: ");
            string userInput = Console.ReadLine();

            do
            {
                if (userInput.ToLower() == "yes")
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Yes was not entered, so the game will not begin. Type Yes if you would like to start playing: ");
                    userInput = Console.ReadLine();
                }

            } while (userInput.ToLower() != "yes");

            return false;

        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Mancala in the Console, by Scott Lowe");
        }
    }
}