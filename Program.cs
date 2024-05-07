using System;
using System.Threading;
using Figgle;

class Program
{
    private static void Main(string[] args)
    {
        Console.Title = "C# Chess";

        string[,] board = new string[8, 8];

        InitializeBoard();

        while (true)
        {
            Console.Clear();
            Console.WriteLine();
            PrintAsciiArt();
            Console.WriteLine("Welcome to the console-based C# version of the chess game. What would you like to do?");
            Console.WriteLine("1) Start the game :)");
            Console.WriteLine("2) Show me the rules...");
            Console.WriteLine("3) Quit the game!");
            Console.WriteLine();
            Console.Write("->  ");
            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                while (true)
                {
                    Console.Clear();

                    DisplayBoard();
                    Console.WriteLine();

                    Console.Write("Do you want to move pieces on the board? (Y/N): ");
                    string? ans = Console.ReadLine();

                    Console.WriteLine();

                    if (ans == "Y")
                    {
                        Console.Write("Enter the position of the piece you want to move (e.g., A2): ");
                        string sourcePos = Console.ReadLine();

                        Console.Write("Enter the target position (e.g., A4): ");
                        string targetPos = Console.ReadLine();

                        ValidateAndMovePiece(sourcePos, targetPos);
                    }
                    else if (ans == "N")
                    {
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Respond with only Y/N!");

                        ClearConsole();
                    }
                }
            }

            else if (choice == "2")
            {
                Console.Clear();
                DisplayRules();
            }

            else if (choice == "3")
            {
                Console.Clear();
                Console.WriteLine("The game has been terminated!");
                Console.WriteLine();
                Environment.Exit(0);
            }

            else
            {
                Console.Clear();
                Console.WriteLine("Please select one of the options!");
                ClearConsole();
            }
        }

        void InitializeBoard()
        {
            board[0, 0] = "R";
            board[0, 1] = "N";
            board[0, 2] = "B";
            board[0, 3] = "Q";
            board[0, 4] = "K";
            board[0, 5] = "B";
            board[0, 6] = "N";
            board[0, 7] = "R";

            for (int i = 0; i < 8; i++)
            {
                board[1, i] = "P";
            }

            for (int i = 2; i < 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = "-";
                }
            }

            for (int i = 0; i < 8; i++)
            {
                board[6, i] = "P";
            }

            board[7, 0] = "R";
            board[7, 1] = "N";
            board[7, 2] = "B";
            board[7, 3] = "Q";
            board[7, 4] = "K";
            board[7, 5] = "B";
            board[7, 6] = "N";
            board[7, 7] = "R";
        }

        void DisplayBoard()
        {
            Console.WriteLine("  Legend:");
            Console.WriteLine("  R - Rook");
            Console.WriteLine("  N - Knight");
            Console.WriteLine("  B - Bishop");
            Console.WriteLine("  Q - Queen");
            Console.WriteLine("  K - King");
            Console.WriteLine("  P - Pawn");
            Console.WriteLine();
            Console.WriteLine("  ---------------------------------");

            for (int i = 7; i >= 0; i--)
            {
                Console.Write(i + 1 + " ");

                for (int j = 0; j < 8; j++)
                {
                    Console.Write("| " + board[i, j] + " ");
                }
                Console.Write("|");
                Console.WriteLine();
                Console.WriteLine("  ---------------------------------");
            }

            Console.WriteLine("    A   B   C   D   E   F   G   H");
        }

        void ValidateAndMovePiece(string sourcePos, string targetPos)
        {
            int sourceRow = int.Parse(sourcePos[1].ToString()) - 1;
            int sourceCol = sourcePos[0] - 'A';
            int targetRow = int.Parse(targetPos[1].ToString()) - 1;
            int targetCol = targetPos[0] - 'A';

            if (
                sourceRow < 0
                || sourceRow > 7
                || sourceCol < 0
                || sourceCol > 7
                || targetRow < 0
                || targetRow > 7
                || targetCol < 0
                || targetCol > 7
            )
            {
                Console.WriteLine("Invalid position!");
                return;
            }

            string piece = board[sourceRow, sourceCol];

            switch (piece)
            {
                case "R": // Rook
                    if (sourceRow == targetRow || sourceCol == targetCol)
                    {
                        if (board[targetRow, targetCol] == "-")
                        {
                            board[targetRow, targetCol] = board[sourceRow, sourceCol];
                            board[sourceRow, sourceCol] = "-";
                            Console.WriteLine(
                                "Piece moved from position {0} to position {1}",
                                sourcePos,
                                targetPos
                            );
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("The target position is already occupied!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid move for rook!");
                    }
                    break;
                case "N": // Knight
                    if (

                            Math.Abs(targetRow - sourceRow) == 2
                            && Math.Abs(targetCol - sourceCol) == 1

                        ||
                            Math.Abs(targetRow - sourceRow) == 1
                            && Math.Abs(targetCol - sourceCol) == 2

                    )
                    {
                        if (board[targetRow, targetCol] == "-")
                        {
                            board[targetRow, targetCol] = board[sourceRow, sourceCol];
                            board[sourceRow, sourceCol] = "-";
                            Console.WriteLine(
                                "Piece moved from position {0} to position {1}",
                                sourcePos,
                                targetPos
                            );
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("The target position is already occupied!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid move for knight!");
                    }
                    break;
                // Similar cases for other pieces...
                default:
                    Console.WriteLine("Invalid piece!");
                    return;
            }
        }

        void ClearConsole()
        {
            Thread.Sleep(3000);
            Console.Clear();
            return;
        }

        void PrintAsciiArt()
        {
            string font = FiggleFonts.Doom.Render("Console Chess");
            Console.WriteLine(font);
        }

        void DisplayRules()
        {
            Console.WriteLine("Rules of movement in chess:");
            Console.WriteLine("1. Pawn: The pawn can move only forward by one square. On its first move, it has the option to move forward by two squares.");
            Console.WriteLine("2. Rook: The rook can move any number of squares horizontally or vertically.");
            Console.WriteLine("3. Knight: The knight can move to any square that is not on the same rank, file, or diagonal. It moves in an L-shape pattern.");
            Console.WriteLine("4. Bishop: The bishop can move any number of squares diagonally.");
            Console.WriteLine("5. Queen: The queen can move any number of squares horizontally, vertically, or diagonally.");
            Console.WriteLine("6. King: The king can move one square in any direction.");
            Console.WriteLine();
            Console.Write("Press ENTER to return to the main menu...");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            return;
        }
    }
}
