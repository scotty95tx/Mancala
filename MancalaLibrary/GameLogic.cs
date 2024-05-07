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
                        currentBoard.player1Store++;
                        player1.StoreSeeds++;
                        startingPitSeeds--;

                        if (startingPitSeeds == 0)
                        {
                            return false;
                        }
                        else
                        {
                            currentBoard.Pits[pitIndex].Seeds++;
                            startingPitSeeds--;
                            CheckAndTakeSeedsFromOppositeSide(currentBoard, pitIndex, startingPitSeeds, activePlayer, player1, player2);
                            pitIndex++;
                        }
                    }
                    else
                    {
                        currentBoard.Pits[pitIndex].Seeds++;
                        startingPitSeeds--;
                        CheckAndTakeSeedsFromOppositeSide(currentBoard, pitIndex, startingPitSeeds, activePlayer, player1, player2);
                        pitIndex++;
                    }
                }
                else if (pitIndex == 12)
                {
                    if (playerOneIsActive)
                    {
                        pitIndex = 0;
                        currentBoard.Pits[pitIndex].Seeds++;
                        startingPitSeeds--;
                        CheckAndTakeSeedsFromOppositeSide(currentBoard, pitIndex, startingPitSeeds, activePlayer, player1, player2);
                        pitIndex++;
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
                            CheckAndTakeSeedsFromOppositeSide(currentBoard, pitIndex, startingPitSeeds, activePlayer, player1, player2);
                            pitIndex++;
                        }
                    }
                }
                else if (pitIndex <= 11 && pitIndex >= 0)
                {
                    currentBoard.Pits[pitIndex].Seeds++;
                    startingPitSeeds--;
                    CheckAndTakeSeedsFromOppositeSide(currentBoard, pitIndex, startingPitSeeds, activePlayer, player1, player2);
                    pitIndex++;
                }
            }
            return true;
        }

        private static void CheckAndTakeSeedsFromOppositeSide(MancalaBoardModel currentBoard, int pitIndex, int startingPitSeeds, PlayerModel activePlayer, PlayerModel player1, PlayerModel player2)
        {
            if (currentBoard.Pits[pitIndex].Seeds == 1 && startingPitSeeds == 0)
            {
                if (activePlayer.UsersName == "Player 1" && pitIndex <= 5)
                {
                    int oppositePitIndex = 11 - pitIndex;

                    currentBoard.player1Store += currentBoard.Pits[oppositePitIndex].Seeds + 1;
                    player1.StoreSeeds += currentBoard.Pits[oppositePitIndex].Seeds + 1;

                    currentBoard.Pits[oppositePitIndex].Seeds = 0;
                    currentBoard.Pits[pitIndex].Seeds = 0;
                }
                else if (activePlayer.UsersName == "Player 2" && pitIndex <= 11 && pitIndex >= 6)
                {
                    int oppositePitIndex = 11 - pitIndex;

                    currentBoard.player2Store += currentBoard.Pits[oppositePitIndex].Seeds + 1;
                    player2.StoreSeeds += currentBoard.Pits[oppositePitIndex].Seeds + 1;

                    currentBoard.Pits[oppositePitIndex].Seeds = 0;
                    currentBoard.Pits[pitIndex].Seeds = 0;
                }
            }
        }

        private static PitModel CreatePit(int pitNumber)
        {
            PitModel newPit = new PitModel();
            newPit.Seeds = 4;
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
