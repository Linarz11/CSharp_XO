using System;
class Program
{
    static string[] pos = new string[10] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }; 
    static void DrawBoard() // Доска с полями
    {

        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        var width = Console.WindowWidth;
        var padding = "                                    ";
        Console.WriteLine(padding + "   {0}  |  {1}  |  {2}   ", pos[1], pos[2], pos[3]);
        Console.WriteLine(padding + "-------------------");
        Console.WriteLine(padding + "   {0}  |  {1}  |  {2}   ", pos[4], pos[5], pos[6]);
        Console.WriteLine(padding + "-------------------");
        Console.WriteLine(padding + "   {0}  |  {1}  |  {2}   ", pos[7], pos[8], pos[9]);
    }

    private static string [] EnterPlayers()
    {
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Введите имя первого игрока?");
        var player1 = Console.ReadLine();
        Console.WriteLine("Введите имя второго игрока?");
        var player2 = Console.ReadLine();
        Console.WriteLine("{0} Играет O, а {1} играет X.", player1, player2);
        Console.WriteLine("{0} Ходит первым. Нажмите любую клавишу для начала", player1);
        Console.ReadLine();
        Console.Clear();
        return new [] { player1, player2 };
    }

    static void Main(string[] args)
    {
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.DarkBlue;

        string[] players = EnterPlayers();
        string[] pieces = { "O", "X" };
        int[] scores = { 0, 0 };

        bool playing = true;
        while (playing)
        {
            PlayGame(players, scores, pieces);
            playing = AskToPlayAgain();
        }
    }

    private static void PlayGame (string[] players, int[] scores, string[] pieces)
    {
        bool isGameWon = false;
        bool isGameOver = false;
        int playerIndex = 0;

        while (isGameOver == false)
        {
            var nextPlayerIndex = 1 - playerIndex;
            var player = players[playerIndex];
            var piece = pieces[playerIndex];
            var opponentPiece = pieces[nextPlayerIndex];

            PlayerMakesAMove(players, scores, player, piece, opponentPiece);

            isGameWon = CheckWin();
            isGameOver = isGameWon || CheckDraw();

            if (isGameOver == false)
            {
                playerIndex = nextPlayerIndex;
            }
        }

        Console.Clear();
        DrawBoard();
        ResetBoard();

        if (isGameWon) // Подбедитель
        {
            IncrementPlayerScore(scores, playerIndex);
            Console.WriteLine("{0} Выиграл!", players[playerIndex]);
        }
        else // No one won ---------------------------
        {
            Console.WriteLine("Ничья!");
        }

        ReportScores(players, scores);
    }

    private static void PlayerMakesAMove(string[] players, int[] scores, string player, string piece, string opponentPiece)
    {
        do
        {
            Console.Clear();
            DrawBoard();
            Console.WriteLine("");
            ReportScores(players, scores);
        } while (!TryToPlaceAPiece(player, piece, opponentPiece));
    }

    private static bool CheckDraw()
    {
         return false;
    }

    private static void IncrementPlayerScore(int[] scores, int playerIndex)
    {
        scores[playerIndex] = scores[playerIndex] + 1;
    }

    private static void ReportScores(string[] players, int[] scores)
    {
        Console.WriteLine("Score: {0} - {1}     {2} - {3}", players[0], scores[0], players[1], scores[1]);
    }

    private static bool TryToPlaceAPiece(string player, string playerPiece, string opponentsPiece)
    {


        Console.WriteLine("Ходит {0} поставьте ({1}) ", player, playerPiece);
        var move = AskTheUser("Куда вы хотите сходить?", 1, 9);
        if (!IsMoveTaken(playerPiece, opponentsPiece, move))
        {
            pos[move] = playerPiece;
            return true;
        }

        Console.WriteLine("Эта клетка занята! ");
        Console.Write("Повторите попытку.");
        Console.ReadLine();
        Console.Clear();
        return false;
    }

    private static bool IsMoveTaken(string playerPiece, string opponentsPiece, int move)
    {
        return pos[move] == opponentsPiece || pos[move] == playerPiece;
    }

    private static bool AskToPlayAgain()
    {
        Console.WriteLine("");
        Console.WriteLine("Что дальше?");
        Console.WriteLine("1. Повторить");
        Console.WriteLine("2. Выйти");

        var choice = AskTheUser("Сделайте выбор: ", 1, 2);

        Console.Clear();
        if (choice == 1) return true;

        Console.WriteLine("Спасибо за игру!");
        Console.ReadLine();
        return false;
    }

    private static int AskTheUser(string prompt, int min, int max)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            int choice = int.Parse(Console.ReadLine());

            if (choice >= min && choice <= max)
            {
                return choice;
            }
        }
    }

    private static void ResetBoard()
    {
        for (int i = 1; i < 10; i++)
        {
            pos[i] = i.ToString();
        }
    }

    static bool CheckWin() // 
    {
        return IsAnyHorizontalLine(1) ||
               IsAnyHorizontalLine(4) ||
               IsAnyHorizontalLine(7) ||
               IsAnyLine(1, 4) ||  
               IsAnyLine(3, 2) ||  
               IsAnyVerticalLine(1) ||
               IsAnyVerticalLine(2) ||
               IsAnyVerticalLine(3);
    }

    private static bool IsLine(int index0, int index1, int index2, string piece)
    {
        return pos[index0] == piece && pos[index1] == piece && pos[index2] == piece;
    }

    private static bool IsAnyLine(int start, int step)
    {
        return IsLine(start, start + step, start + step + step, pos[start]);
    }

    private static bool IsAnyHorizontalLine(int startindex)
    {
        return IsAnyLine(startindex, 1);
    }

    private static bool IsAnyVerticalLine(int startindex)
    {
        return IsAnyLine(startindex, 3);
    }
}