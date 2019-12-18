using System;
using System.Collections.Generic;

namespace BullsAndCows
{
    using Language;

    public class Game
    {
        private string m_ComputerLuckyPattern;
        private Guess[] m_Board;

        public Game(int i_LengthPattern)
        {
            randomWordToComputer(i_LengthPattern);
        }

        public void Start()
        {
            initializeGameProperties();
            getUserGuess();
            initBoard();
            gameLoop();
        }

        private void initializeGameProperties()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Printer.InfoMode();
            Guess.m_NumberOfGuesses = 0;
        }

        private void getUserGuess()
        {
            Printer.PrintMessage(2);
            int numberOfGuesses = 0;
            bool successParse;

            successParse = int.TryParse(Console.ReadLine(), out numberOfGuesses);
            while (numberOfGuesses < 4 || numberOfGuesses > 10 || !successParse)
            {
                if (!successParse)
                {
                    Printer.PrintErrorMessage(8);
                }
                else
                {
                    Printer.PrintErrorMessage(3);
                }

                successParse = int.TryParse(Console.ReadLine(), out numberOfGuesses);
            }

            m_Board = new Guess[numberOfGuesses];
        }

        private void initBoard()
        {
            for (int i = 0; i < m_Board.Length; i++)
            {
                m_Board[i] = new Guess();
            }
        }

        private void gameLoop()
        {
            string currentGuessStr = string.Empty;
            bool shouldRunLoop = true;
            bool winning = false;

            while (Guess.m_NumberOfGuesses < m_Board.Length)
            {
                Printer.PrintBoard(m_Board, m_ComputerLuckyPattern.Length);
                Printer.PrintMessage(0);
                while (shouldRunLoop)
                {
                    try
                    {
                        currentGuessStr = Console.ReadLine().Replace(" ", string.Empty).ToUpper();
                        shouldRunLoop = !Guess.GuessValidation(currentGuessStr);
                    }
                    catch (Exception e)
                    {
                        Printer.PrintErrorMessage(e.Message + Messages.GetMessage(1));
                    }
                }

                if (currentGuessStr.ToUpper().Equals("Q"))
                {
                    Printer.EndOfGame();
                    Console.ReadLine();
                    break;
                }
                else
                {
                    shouldRunLoop = true;
                    m_Board[Guess.m_NumberOfGuesses] = new Guess(currentGuessStr);
                    Guess.m_NumberOfGuesses++;
                    
                    if (currentGuessStr == m_ComputerLuckyPattern)
                    {
                        winning = true;
                        Printer.SuccessMode();                        
                        Printer.PrintBoard(m_Board, m_ComputerLuckyPattern.Length);
                        if(Guess.m_NumberOfGuesses > 1)
                        {
                            Printer.PrintFormatMessage(4, new List<string>() { Guess.m_NumberOfGuesses.ToString() });
                        }
                        else
                        {
                            Printer.PrintBoard(m_Board, m_ComputerLuckyPattern.Length);
                            Printer.PrintMessage(10);
                        }

                        restart();
                        break;
                    }
                }
            }

            if (Guess.m_NumberOfGuesses == m_Board.Length && !winning)
            {
                Printer.FailureMode();
                Printer.PrintBoard(m_Board, m_ComputerLuckyPattern.Length);
                Printer.PrintErrorMessage(5);
                restart();
            }
        }

        private void restart()
        {
            Printer.PrintMessage(6);
            string answer = Console.ReadLine().ToLower();
            while (answer != "y" && answer != "n")
            {
                Printer.PrintErrorMessage(7);
                answer = Console.ReadLine().ToLower();
            }

            if (answer == "y")
            {
                randomWordToComputer(m_ComputerLuckyPattern.Length);
                Start();
            }
            else
            {
                Printer.EndOfGame();
                Console.ReadLine();
            }
        }

        private void randomWordToComputer(int i_LengthOfPattern)
        {
            m_ComputerLuckyPattern = Language.RandomWordWithoutRepeatLetters(i_LengthOfPattern);
            Guess.m_ComputerPattern = m_ComputerLuckyPattern;
        }
    }
}
