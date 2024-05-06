using MancalaLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace MancalaLibrary
{
    public static class GameLogic
    {
        public static MancalaBoardModel CreateMancalaBoard(PlayerModel player1, PlayerModel player2)
        {
            MancalaBoardModel board = new MancalaBoardModel();
            board.player1Store = player1.StoreSeeds;
            board.player2Store = player2.StoreSeeds;
            int pitNumber = 0;

            while (pitNumber < 12)
            {
                board.Pits.Add(CreatePit(pitNumber));
                pitNumber++;
            }

            return board;
        }

        public static PlayerModel CreatePlayer(string UsersName)
        {
            PlayerModel newPlayer = new PlayerModel();
            newPlayer.UsersName = UsersName;
            newPlayer.StoreSeeds = 0;

            return newPlayer;
        }

        public static bool SowSeedsAndCheckIfActiveStatusSwaps(MancalaBoardModel currentBoard, int selectedPit, PlayerModel activePlayer, PlayerModel player1, PlayerModel player2)
        {
            PitModel currentPit = currentBoard.Pits[selectedPit - 1];
            int startingPitSeeds = currentPit.Seeds;
            currentPit.Seeds -= startingPitSeeds;
            bool playerOneIsActive = false;

            if (activePlayer.UsersName == "Player 1")
            {
                playerOneIsActive = true;
            } 

            int pitIndex = selectedPit;


            while (startingPitSeeds > 0)
            {
                if (pitIndex == 6)
                {
                    if (playerOneIsActive)
                    {
                        pitIndex++;
                        currentBoard.player1Store++;
                        player1.StoreSeeds++;
                        startingPitSeeds--;

                        if (startingPitSeeds == 0)
                        {
                            return false;
                        }
                        else
                        {
                            currentBoard.Pits[6].Seeds++;
                            startingPitSeeds--;
                        }
                    }
                    else
                    {
                        currentBoard.Pits[pitIndex].Seeds++;
                        pitIndex++;
                        startingPitSeeds--;
                    }
                }
                else if (pitIndex == 12)
                {
                    if (playerOneIsActive)
                    {
                        pitIndex = 0;
                        currentBoard.Pits[pitIndex].Seeds++;
                        startingPitSeeds--;
                    }
                    else
                    {
                        pitIndex = 0;
                        currentBoard.player2Store++;
                        player2.StoreSeeds++;
                        startingPitSeeds--;

                        if (startingPitSeeds == 0)
                        {
                            return false;
                        }
                        else
                        {
                            currentBoard.Pits[pitIndex].Seeds++;
                            startingPitSeeds--;
                        }
                    }
                }
                else if (pitIndex <= 12 && pitIndex >= 0)
                {
                    currentBoard.Pits[pitIndex].Seeds++;
                    pitIndex++;
                    startingPitSeeds--;
                }

                /*if (currentBoard.Pits[pitIndex].Seeds == 1)
                {
                    TakeSeedsFromOppositeSide(currentBoard, pitIndex, activePlayer, player1, player2);
                }*/
            }
            return true;
        }

        private static void TakeSeedsFromOppositeSide(MancalaBoardModel currentBoard, int pitIndex, PlayerModel activePlayer, PlayerModel player1, PlayerModel player2)
        {
            if (activePlayer.UsersName == "Player 1" && pitIndex <= 5) 
            {
                currentBoard.player1Store += currentBoard.Pits[11 - pitIndex].Seeds + 1;
                player1.StoreSeeds += currentBoard.Pits[11 - pitIndex].Seeds + 1; 
            }
            else if (activePlayer.UsersName == "Player 2" && pitIndex <= 11) {
               
                currentBoard.player2Store += currentBoard.Pits[11 - pitIndex].Seeds + 1;
                player2.StoreSeeds += currentBoard.Pits[11 - pitIndex].Seeds + 1;
            }
        }

        private static PitModel CreatePit(int pitNumber)
        {
            PitModel newPit = new PitModel();
            newPit.Seeds = 5;
            newPit.PitNumber = pitNumber;
            return newPit;
        }

        public static bool CheckIfWinner(MancalaBoardModel currentBoard)
        {
            int storeTotal = currentBoard.player1Store + currentBoard.player2Store;
            
            if (storeTotal == 48)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static PlayerModel IdentifyWinner(PlayerModel player1, PlayerModel player2)
        {
            if (player1.StoreSeeds > player2.StoreSeeds)
            {
                return player1;
            }
            else
            {
                return player2;
            }
        }
    }
}
