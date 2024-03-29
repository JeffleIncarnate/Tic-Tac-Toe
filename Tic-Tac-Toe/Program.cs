﻿using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
    internal class Program
    {
        static bool isPlayerRight = true;
        static char[][] boardPosition =
        {
            new char[] {'.', ',', '.' },
            new char[] {'.', ',', '.' },
            new char[] {'.', ',', '.' }
        };
        static string[] arrayOfMoves = { "a1", "a2", "a3", "b1", "b2", "b3", "c1", "c2", "c3" };
        static int counter = 0;
        static int currentPlayer = 1;

        static void Main(string[] args)
        {
            Console.WriteLine("Positions are:");
            Console.WriteLine("a1 | a2 | a3");
            Console.WriteLine("b1 | b2 | b3");
            Console.WriteLine("c1 | c2 | c3\n");
            Console.WriteLine("Player 1 is 'X'");
            Console.WriteLine("Player 2 is 'O'");

            while (true)
            {
                string move;
                if (counter % 2 == 0)
                {
                    while (true)
                    {
                        Console.WriteLine("\nPlayer 1 move: ");
                        move = Console.ReadLine();
                        if (arrayOfMoves.Contains(move))
                            break;
                        if (move == "print_board")
                            PrintBoard();
                    }
                    isPlayerRight = true;
                    UpdateBoard(move, currentPlayer);
                }
                else
                {
                    while (true)
                    {
                        Console.WriteLine("\nPlayer 2 move: ");
                        move = Console.ReadLine();
                        if (arrayOfMoves.Contains(move))
                            break;
                        if (move == "print_board")
                            PrintBoard();
                    }
                    UpdateBoard(move, currentPlayer);
                }
                if (isPlayerRight)
                    counter++;
            }

        }

        static void UpdateBoard(string move, int player) 
        {
            Dictionary<char, int> valuesToCharacters = new Dictionary<char, int>
            {
                { 'a', 0 },
                { 'b', 1 },
                { 'c', 2 },
            };
            int col;
            int.TryParse(move[1].ToString(), out col);
            col -= 1;
            int row = valuesToCharacters[move[0]];

            if (!valuesToCharacters.ContainsKey(move[0]))
            {
                isPlayerRight = false;
                return;
            }
            if (boardPosition[row][col] != ',' && boardPosition[row][col] != '.')
            {
                isPlayerRight = false;
                return;
            }

            if (player == 1)
            {
                currentPlayer = 2;
                switch (move[0])
                {
                    case 'a':
                        boardPosition[0][col] = 'X';
                        break;
                    case 'b':
                        boardPosition[1][col] = 'X';
                        break;
                    case 'c':
                        boardPosition[2][col] = 'X';
                        break;
                }
            }
            else
            {
                currentPlayer = 1;
                switch (move[0])
                {
                    case 'a':
                        boardPosition[0][col] = 'O';
                        break;
                    case 'b':
                        boardPosition[1][col] = 'O';
                        break;
                    case 'c':
                        boardPosition[2][col] = 'O';
                        break;
                }
            }
            PrintBoard();
            CheckBoard();
        }

        static void CheckBoard()
        { 
            foreach (char[] row in boardPosition) 
                if (row[0] == row[1] && row[0] == row[2])
                    Winner(row[0] == 'X' ? "Player 1" : "Player 2");

            if (boardPosition[0][0] == boardPosition[1][1] && boardPosition[0][0] == boardPosition[2][2])
                Winner(boardPosition[0][0] == 'X' ? "Player 1" : "Player 2");
            
            if (boardPosition[2][0] == boardPosition[1][1] && boardPosition[2][0] == boardPosition[0][2])
                Winner(boardPosition[0][0] == 'X' ? "Player 1" : "Player 2");
        }

        static void Winner(string winner)
        {
            Console.WriteLine($"{winner} is the winner!");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(0);
        }

        static void PrintBoard()
        {
            foreach (char[] arr in boardPosition)
            {
                for (int i = 0; i < arr.Length; i++)
                    Console.Write($"{arr[i]} ");
                Console.WriteLine("");
            }
        }
    }
}
