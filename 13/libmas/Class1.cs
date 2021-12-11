using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace LibMas
{
    public class Class1
    {
        
       
        
        //Заполнение матрицы
        public static void Заполнить( int row, int column, out int[,] matr)
        {
            //задаем массиву размерность
            
            matr = new int[row, column];

            //Заполняем матрицу случайными числами
            Random rnd = new Random();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    matr[i, j] = rnd.Next(25);
                }
            }
        }


        //Очищение матрицы
        public static void Сброс(int[,] matr)
        {
            //Заполняем матрицу нулями
            for (int i = 0; i < matr.GetLength(0); i++)
            {
                for (int j = 0; j < matr.GetLength(1); j++)
                {
                    matr[i, j] = 0;
                }
            }

        }


        //Сохранение матрицы
        public static void Savematr(int[,] matr)
        {
            //Создаем и настраиваем элемент SaveFileDialog
           SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            save.Filter = "Все файлы (*.*) | *.* | Текстовые файлы | *.txt";
            save.FilterIndex = 2;
            save.Title = "Сохранение таблицы";

            //Открываем диалоговое окно и при успехе работаем с файлом
            if (save.ShowDialog() == true)
            {
                //Создаем поток для работы с файлом и указываем ему имя файла
                StreamWriter file = new StreamWriter(save.FileName);

                //Записываем размер матрицы в файл
                file.WriteLine(Convert.ToString(matr.GetLongLength(0)));
                file.WriteLine(Convert.ToString(matr.GetLongLength(1)));
              

                //Записываем элементы матрицы в файл
                for (int i = 0; i < matr.GetLength(0); i++)
                {
                    for (int j = 0; j < matr.GetLength(1); j++)
                    {
                        file.WriteLine(matr[i, j]);
                    }
                }
                file.Close();
            }
        }


        //Открытие матрицы
        public static void Openmatr(out int[,] matr)
        {
            matr = new int[1,1];

            //Создаем и настраиваем элемент OpenFileDialog
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = ".txt";
            open.Filter = "Все файлы (*.*) | *.* | Текстовые файлы | *.txt";
            open.FilterIndex = 2;
            open.Title = "Открыть таблицы";

            //Открываем диалоговое окно и при успехе работаем с файлом
            if (open.ShowDialog() == true)
            {
                //Создаем поток для работы с файлом и указываем ему имя файла
                StreamReader file = new StreamReader(open.FileName);

                //Читаем размер матрицы
                int x = Convert.ToInt32(file.ReadLine());
                int y = Convert.ToInt32(file.ReadLine());

                //Создаем матрицу
                matr = new Int32[x, y];

                //Считываем матрицу из файла
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        matr[i, j] = Convert.ToInt32(file.ReadLine());
                    }
                }
                file.Close();
            }

        }
    }
}

