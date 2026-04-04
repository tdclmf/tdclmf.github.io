using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_8
{
    public class PuzzleLogic
    {
        public int[,] Grid { get; private set; } // Двумерный массив 4х4
        public int EmptyRow { get; private set; } // Строка пустой ячейки
        public int EmptyCol { get; private set; } // Колонка пустой ячейки

        public Color TileColor { get; set; } = Color.SkyBlue; // Цвет фишек по умолчанию

        public PuzzleLogic()
        {
            Grid = new int[4, 4];
            Reset();
        }

        // Сброс поля в выигрышное состояние (цифры по порядку)
        public void Reset()
        {
            int counter = 1;
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    Grid[r, c] = counter++;
                }
            }
            Grid[3, 3] = 0; // 0 будет означать пустую ячейку
            EmptyRow = 3;
            EmptyCol = 3;
        }

        // Перемешивание фишек (делаем 300 случайных легальных ходов, 
        // чтобы головоломка 100% имела решение)
        public void Shuffle()
        {
            Random rnd = new Random();
            int[] dRow = { -1, 1, 0, 0 }; // Вверх, вниз, влево, вправо
            int[] dCol = { 0, 0, -1, 1 };

            for (int i = 0; i < 300; i++)
            {
                int dir = rnd.Next(4);
                int newR = EmptyRow + dRow[dir];
                int newC = EmptyCol + dCol[dir];

                if (newR >= 0 && newR < 4 && newC >= 0 && newC < 4)
                {
                    Move(newR, newC);
                }
            }
        }

        // Ход фишкой
        public bool Move(int r, int c)
        {
            // Проверяем, что ячейка (r, c) находится рядом с пустой (разница координат = 1)
            if (Math.Abs(EmptyRow - r) + Math.Abs(EmptyCol - c) == 1)
            {
                // Меняем местами с пустой
                Grid[EmptyRow, EmptyCol] = Grid[r, c];
                Grid[r, c] = 0;

                // Запоминаем новые координаты пустой ячейки
                EmptyRow = r;
                EmptyCol = c;
                return true;
            }
            return false;
        }

        // Проверка победы
        public bool CheckWin()
        {
            int counter = 1;
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    // Если дошли до последней ячейки, она должна быть пустой (0)
                    if (r == 3 && c == 3) return Grid[r, c] == 0;

                    // Иначе проверяем порядок
                    if (Grid[r, c] != counter++) return false;
                }
            }
            return true;
        }
    }
}
