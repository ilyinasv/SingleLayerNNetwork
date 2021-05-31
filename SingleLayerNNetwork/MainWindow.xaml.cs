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

namespace SingleLayerNNetwork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NNetwork network = new NNetwork();

        public MainWindow()
        {
            InitializeComponent();
        }
     

        private void buttonTrain_Click(object sender, RoutedEventArgs e)
        {
            int[][][] testMatrices = new int[5][][];

            for (int c = 0; c < 5; c++)
            {
                testMatrices[c] = new int[5][];
                for (int i = 0; i < 5; i++)
                {
                    testMatrices[c][i] = new int[7];
                    for (int j = 0; j < 7; j++)
                    {
                        testMatrices[c][i][j] = 0;
                    }
                }
            }

            //разбиваем входную строку на подстроки
            string[] input = trainDataInput.Text.Split(' ');

            int iterations = 0;

            int[] data = new int[5];

            int h = 0;

            for(int n=0; n<5; n++)
            {               
                int[] intList = input[n].Select(digit => int.Parse(digit.ToString())).ToArray();
                for (int i=0;i<5;i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        testMatrices[n][i][j] = intList[h];
                        h++;
                    }

                }
                h = 0;
            }

            for (int i = 0; i < 5; i++)
            {
                if (!network.Learn(testMatrices[i], i))
                {
                    i = -1;
                    iterations += 1;
                    OutputTextBox.Text = "Обучение в процессе. Итерация:" + iterations;
                }
            }

            OutputTextBox.Text = "Сеть обученна. Количество итераций:" + iterations;
        }
    }
}
