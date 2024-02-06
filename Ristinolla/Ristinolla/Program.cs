using System;

class Program
{
    static char[,] board = {
        { '1', '2', '3' },
        { '4', '5', '6' },
        { '7', '8', '9' }
    };

    static char currentPlayer = 'X';

    static void Main()
    {
        Console.WriteLine("Welcome to Tic-Tac-Toe!");

        do
        {
            // Pelilauta
            DrawBoard();

            // Pelaaja syöttää komennon
            GetPlayerMove();

            if (IsWinner() || IsBoardFull())
            {
                DrawBoard();
                break;
            }

            // Pelaajien vuorot
            SwitchPlayer();
        } while (true);

        // Tulos
        DisplayResult();

        Console.ReadLine(); // Konsoli on auki
    }

    static void DrawBoard()
    {
        Console.Clear();
        Console.WriteLine($" {board[0, 0]} | {board[0, 1]} | {board[0, 2]} ");
        Console.WriteLine("---|---|---");
        Console.WriteLine($" {board[1, 0]} | {board[1, 1]} | {board[1, 2]} ");
        Console.WriteLine("---|---|---");
        Console.WriteLine($" {board[2, 0]} | {board[2, 1]} | {board[2, 2]} ");
    }

    static void GetPlayerMove()
    {
        int move;
        bool isValidMove;

        do
        {
            Console.Write($"Player {currentPlayer}, enter your move (1-9): ");
            string input = Console.ReadLine();

            isValidMove = int.TryParse(input, out move) && move >= 1 && move <= 9 && IsCellEmpty(move);

            if (!isValidMove)
            {
                Console.WriteLine("Invalid move. Please try again.");
            }
        } while (!isValidMove);

        UpdateBoard(move);
    }

    static bool IsCellEmpty(int move)
    {
        int row = (move - 1) / 3;
        int col = (move - 1) % 3;

        return board[row, col] != 'X' && board[row, col] != 'O';
    }

    static void UpdateBoard(int move)
    {
        int row = (move - 1) / 3;
        int col = (move - 1) % 3;

        board[row, col] = currentPlayer;
    }

    static void SwitchPlayer()
    {
        currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
    }

    static bool IsWinner()
    {
        // Tarkistaa onko voittaja selvillä
        for (int i = 0; i < 3; i++)
        {
            if ((board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer) ||
                (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer))
            {
                Console.WriteLine($"Player {currentPlayer} wins!");
                return true;
            }
        }

        if ((board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer) ||
            (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer))
        {
            Console.WriteLine($"Player {currentPlayer} wins!");
            return true;
        }

        return false;
    }

    static bool IsBoardFull()
    {
        // Onko pelilauta täynnä (tasapeli)
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] != 'X' && board[i, j] != 'O')
                {
                    return false;
                }
            }
        }

        Console.WriteLine("It's a tie! The board is full.");
        return true;
    }

    static void DisplayResult()
    {
        Console.WriteLine("Thanks for playing!");
    }
}