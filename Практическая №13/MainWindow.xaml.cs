using System;
using System.Collections.Generic;
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
using LibraryMatrix;
using Microsoft.Win32;

namespace Практическая__13
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

        private MyMatrix _myMatrix;

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            int resultMultiply = int.MaxValue;
            int resultindex = 0;

            for (int i = 0; i < _myMatrix.ColumnLength; i++)
            {
                int multiplication = 1;
                for (int j = 0; j < _myMatrix.RowLength; j++)
                {
                    multiplication *= _myMatrix[j, i];
                }
                if (resultMultiply > multiplication)
                {
                    resultMultiply = multiplication;
                    resultindex = i;
                }
            }
            Columnindex.Text = resultindex.ToString();
            minMultiplication.Text = resultMultiply.ToString();
        }

        private void FillMatrix_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(row.Text) || String.IsNullOrEmpty(column.Text))
            {
                MessageBox.Show("Введите размер матрицы");
            }
            else
            {
                _myMatrix = new MyMatrix(int.Parse(row.Text), int.Parse(column.Text));
                sizeRow.Text = $"Строк {row.Text}";
                sizeColumn.Text = $"Столбцов {column.Text}";
                _myMatrix.FillMatrix();
                dataGrid.ItemsSource = _myMatrix.ToDataTable().DefaultView;
            }
        }

        private void SaveArray_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.ItemsSource == null)
            {
                MessageBox.Show("Заполните матрицу", "Предупреждение!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*";
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.Title = "Экспорт массива";

            if (saveFileDialog.ShowDialog() == true)
            {
                _myMatrix.Export(saveFileDialog.FileName);
            }
            dataGrid.ItemsSource = null;
        }

        private void OpenArray_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*";
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Title = "Импорт массива";

            if (openFileDialog.ShowDialog() == true)
            {
                _myMatrix.Import(openFileDialog.FileName);
            }
            dataGrid.ItemsSource = _myMatrix.ToDataTable().DefaultView;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            row.Clear();
            column.Clear();
            minMultiplication.Clear();
            Columnindex.Clear();
            sizeColumn.Clear();
            sizeRow.Clear();
            selectedCell.Clear();
        }

        private void CellIndex_Click(object sender, MouseEventArgs e)
        {
            selectedCell.Text = $"[{dataGrid.Items.IndexOf(dataGrid.CurrentItem)};" +
                $"{dataGrid.CurrentColumn.DisplayIndex}]";
        }

        private void ChangeInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            minMultiplication.Clear();
            Columnindex.Clear();
        }

        private void TaskInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("10. Дана матрица размера M × N. Найти номер ее столбца с наименьшим " +
               "произведением элементов и вывести данный номер, а также значение наименьшего произведения", "Задание");
        }

        private void DeveloperInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Харенко Кирилл  ИСП-34", "Разработчик", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
