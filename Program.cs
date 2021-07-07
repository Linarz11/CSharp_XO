using System;
class Program
{
    static string[] pos = new string[10] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }; // ноль не используется
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

    static void Main(string[] args) 
    {
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        string player1 = "", player2 = "";
        int choice = 0, turn = 1, score1 = 0, score2 = 0;
        bool winFlag = false, playing = true, correctInput = false;

        Console.WriteLine("Введите имя первого игрока?");
        player1 = Console.ReadLine();
        Console.WriteLine("Введите имя второго игрока?");
        player2 = Console.ReadLine();
        Console.WriteLine("{0} Играет O, а {1} играет X.", player1, player2);
        Console.WriteLine("{0} Ходит первым. Нажмите любую клавишу для начала", player1);
        Console.ReadLine();
        Console.Clear();

        while (playing == true)
        {
            while (winFlag == false) 
            {
                DrawBoard();
                Console.WriteLine("");

                Console.WriteLine("Score: {0} - {1}     {2} - {3}", player1, score1, player2, score2);
                if (turn == 1)
                {
                    Console.WriteLine("Ходит {0}, поставьте (O) в незанятую клетку", player1);
                }
                if (turn == 2)
                {
                    Console.WriteLine("Ходит {0}, поставьте (X) в незанятую клетку", player2);
                }

                while (correctInput == false)
                {
                    Console.WriteLine("Куда будете ходить?");
                    choice = int.Parse(Console.ReadLine());
                    if (choice > 0 && choice < 10)
                    {
                        correctInput = true;
                    }
                    else
                    {
                        continue;
                    }
                }

                correctInput = false; // Повтор 

                if (turn == 1)
                {
                    if (pos[choice] == "X") // проверка на занятость клетки
                    {
                        Console.WriteLine("Вы не можете сходить в эту клетку ");
                        Console.Write("Повторите попытку.");
                        Console.ReadLine();
                        Console.Clear();
                        continue;
                    }
                    else
                    {
                        pos[choice] = "O";
                    }
                }
                if (turn == 2)
                {
                    if (pos[choice] == "O") // Проверка на занятость клетки
                    {
                        Console.WriteLine("Вы не можете сходить в эту клетку");
                        Console.Write("Повторите попытку.");
                        Console.ReadLine();
                        Console.Clear();
                        continue;
                    }
                    else
                    {
                        pos[choice] = "X";
                    }
                }

                winFlag = CheckWin();

                if (winFlag == false)
                {
                    if (turn == 1)
                    {
                        turn = 2;
                    }
                    else if (turn == 2)
                    {
                        turn = 1;
                    }
                    Console.Clear();
                }
            }

            Console.Clear();

            DrawBoard();

            for (int i = 1; i < 10; i++) // Resets board ------------------------
            {
                pos[i] = i.ToString();
            }

            if (winFlag == false) // Проверка на ничью
            {
                Console.WriteLine("Ничья!");
                Console.WriteLine("Score: {0} - {1}     {2} - {3}", player1, score1, player2, score2);
                Console.WriteLine("");
                Console.WriteLine("Повторить игру или выйти?");
                Console.WriteLine("1. Повторить");
                Console.WriteLine("2. Выйти");
                Console.WriteLine("");

                while (correctInput == false)
                {
                    Console.WriteLine("Сделайте выбор: ");
                    choice = int.Parse(Console.ReadLine());

                    if (choice > 0 && choice < 3)
                    {
                        correctInput = true;
                    }
                }

                correctInput = false; 

                switch (choice)
                {
                    case 1:
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Спасибо за игру!");
                        Console.ReadLine();
                        playing = false;
                        break;
                }
            }

            if (winFlag == true) // Один из игроков победил
            {
                if (turn == 1)
                {
                    score1++;
                    Console.WriteLine("{0} Победил!", player1);
                    Console.WriteLine("Повторить игру или выйти?");
                    Console.WriteLine("1. Повторить");
                    Console.WriteLine("2. Выйти");

                    while (correctInput == false)
                    {
                        Console.WriteLine("Сделайте выбор: ");
                        choice = int.Parse(Console.ReadLine());

                        if (choice > 0 && choice < 3)
                        {
                            correctInput = true;
                        }
                    }

                    correctInput = false;

                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            winFlag = false;
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine("Спасибо за игру!");
                            Console.ReadLine();
                            playing = false;
                            break;
                    }
                }

                if (turn == 2)
                {
                    score2++;
                    Console.WriteLine("{0} Победил!", player2);
                    Console.WriteLine("Повторить игру или выйти?");
                    Console.WriteLine("1. Повторить");
                    Console.WriteLine("2. Выйти");

                    while (correctInput == false)
                    {
                        Console.WriteLine("Сделайте выбор: ");
                        choice = int.Parse(Console.ReadLine());

                        if (choice > 0 && choice < 3)
                        {
                            correctInput = true;
                        }
                    }

                    correctInput = false; // Reset -----------------

                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            winFlag = false;
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine("Спасибо за игру!");
                            Console.ReadLine();
                            playing = false;
                            break;
                    }
                }
            }
        }
    }

    static bool CheckWin() // метод определния победителя
    {
        if (pos[1] == "O" && pos[2] == "O" && pos[3] == "O") // победа по строке
        {
            return true;
        }
        else if (pos[4] == "O" && pos[5] == "O" && pos[6] == "O")
        {
            return true;
        }
        else if (pos[7] == "O" && pos[8] == "O" && pos[9] == "O")
        {
            return true;
        }

        else if (pos[1] == "O" && pos[5] == "O" && pos[9] == "O") // победа по диагонали
        {
            return true;
        }
        else if (pos[7] == "O" && pos[5] == "O" && pos[3] == "O")
        {
            return true;
        }

        else if (pos[1] == "O" && pos[4] == "O" && pos[7] == "O")// победа по столбцу
        {
            return true;
        }
        else if (pos[2] == "O" && pos[5] == "O" && pos[8] == "O")
        {
            return true;
        }
        else if (pos[3] == "O" && pos[6] == "O" && pos[9] == "O")
        {
            return true;
        }

        if (pos[1] == "X" && pos[2] == "X" && pos[3] == "X") // Победа по строке
        {
            return true;
        }
        else if (pos[4] == "X" && pos[5] == "X" && pos[6] == "X")
        {
            return true;
        }
        else if (pos[7] == "X" && pos[8] == "X" && pos[9] == "X")
        {
            return true;
        }

        else if (pos[1] == "X" && pos[5] == "X" && pos[9] == "X") // Победа по диагонали
        {
            return true;
        }
        else if (pos[7] == "X" && pos[5] == "X" && pos[3] == "X")
        {
            return true;
        }

        else if (pos[1] == "X" && pos[4] == "X" && pos[7] == "X") // Победа по столбцу
        {
            return true;
        }
        else if (pos[2] == "X" && pos[5] == "X" && pos[8] == "X")
        {
            return true;
        }
        else if (pos[3] == "X" && pos[6] == "X" && pos[9] == "X")
        {
            return true;
        }
        //else if (pos[1] == "O" && pos[2] == "O" && pos[3] == "X") return false;
        else // ничья
        {
            return false;
        }
    }
}