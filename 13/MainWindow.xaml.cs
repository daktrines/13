using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using LibMas;
using Масивы;

namespace _13
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        int[,] matr;
        DispatcherTimer _timer;// Описываем таймер

        private void Инфо_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Калион Екатерина " +
                "\n13 пр" +
                "\nДана матрица размера M * N. " +
                "\nНайти количество ее столбцов, элементы которых " +
                "\nупорядочены по убыванию", "Информация", MessageBoxButton.OK, MessageBoxImage.Question);
        }
        private void Выход_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            //Добавляем таймер
            _timer = new DispatcherTimer();
            _timer.Tick += Timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            _timer.IsEnabled = true;
        }

        //Создаем вручную событие таймера
        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now;//Создание обьекта
            time.Text = d.ToString("HH:mm");//Время
            date.Text = d.ToString("dd.MM.yyyy");//Дата
        }

        //Редактирование ячеек
        private void МатрицаDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //Очищаем textbox с результатом 
            Rez1.Clear();

            //Определяем номер столбца
            int columnIndex = e.Column.DisplayIndex;
            //Определяем номер строки
            int rowIndex = e.Row.GetIndex();

            //Заносим  отредоктированое значение в соответствующую ячейку матрицы

            if (Int32.TryParse(((TextBox)e.EditingElement).Text, out matr[rowIndex, columnIndex]))
            { }
            else MessageBox.Show("Неверные данные!", "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
        }

        //Заполнение матрицы
        private void Заполнить_Click(object sender, RoutedEventArgs e)
        {
            //Проверка поля на корректность введенных данных
            if (Int32.TryParse(kolStrok.Text, out int row) && Int32.TryParse(kolStolbcov.Text, out int column) && row > 0 && column > 0)
            {
                //Определяем размер матрицы
                v.Text = $"Matrix: {kolStrok.Text}" + "*" + $"{kolStolbcov.Text}";
               
                Class1.Заполнить(row, column, out matr);

                //Выводим матрицу на форму
                matrData.ItemsSource = VisualArray.ToDataTable(matr).DefaultView;

                //очищаем результат
              Rez1.Clear();
            }
            else MessageBox.Show("Вы не создали матрицу, укажите размеры матрицы и нажмите кнопку Заполнить", "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
        }

        //Расчет задания для матрицы
        private void Вычислить_Click(object sender, RoutedEventArgs e)
        {
             Rez1.Clear();

            if (matr == null || matr.Length == 0)
            {
                MessageBox.Show("Вы не создали матрицу, укажите размеры матрицы и нажмите кнопку Заполнить", "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                int row = Convert.ToInt32(kolStrok.Text);
                int column = Convert.ToInt32(kolStolbcov.Text);
                int kol = Rez.Рассчитать(row, column, matr);
                Rez1.Text = Convert.ToString(kol);
            }
        }

        //Очищение матрицы
        private void Сброс_Click(object sender, RoutedEventArgs e)
        {
            //Очищаем остальные текстбоксы
            kolStrok.Focus();
            kolStolbcov.Clear();
            kolStrok.Clear();
            Rez1.Clear();

            if (matr != null && matr.Length != 0)
            {
                Class1.Сброс(matr);
                //Выводим матрицу на форму
                matrData.ItemsSource = VisualArray.ToDataTable(matr).DefaultView;
            }
            else MessageBox.Show("Вы не создали матрицу, укажите размеры матрицы и нажмите кнопку \"Заполнить" , "Ошибка",  MessageBoxButton.OK,
                    MessageBoxImage.Error);
        }

        //Сохранение матрицы
        private void Savematr_Click(object sender, RoutedEventArgs e)
        {
            Class1.Savematr(matr);
        }

        //Открытие матрицы
        private void Openmatr_Click(object sender, RoutedEventArgs e)
        {
            Class1.Openmatr(out matr);
            for (int i = 0; i < matr.GetLength(0); i++)
            {
                for (int j = 0; j < matr.GetLength(1); j++)
                {
                    //Выводим матрицу на форму
                    matrData.ItemsSource = VisualArray.ToDataTable(matr).DefaultView;
                }
            }
        }

        //Когда изменяем текстбокс, очищает остальные текстбоксы
        private void kolStrok_TextChanged(object sender, TextChangedEventArgs e)
        {
            Rez1.Clear();
        }

        //Когда изменяем текстбокс, очищает остальные текстбоксы
        private void kolStolbcov_TextChanged(object sender, TextChangedEventArgs e)
        {
            Rez1.Clear();
        }
       
        //Определяем номер ячейки в матрице
        private void matrData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Определяем номер столбца
            int indexColumn = matrData.CurrentCell.Column.DisplayIndex;
            //Определяем номер строки
            int indexRow = matrData.SelectedIndex;
            s.Text = $"Строка {indexRow + 1}" + " / " + $"Столбец {indexColumn + 1}";
        }
    }
}
