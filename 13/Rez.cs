using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13
{
    class Rez
    {
        public static int Рассчитать(int row, int column, int[,] matr)
        {
            int kol = 0;
            bool p;
            for (int j = 0; j < column; j++)
            {
                p = true;
                for (int i = 0; i < row - 1; i++)
                {
                    if (matr[i, j] <= matr[i + 1, j])
                    {
                        p = false;
                        break;
                    }
                }
                if (p) kol++;//p = true тоже самое
            }
            return kol;
        }
    }
}
