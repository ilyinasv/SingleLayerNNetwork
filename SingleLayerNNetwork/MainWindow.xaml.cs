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
            int iter = 0;
            double weightUsage = 0;

            int[][] tOut = new int[5][];
            for (int c = 0; c < 5; c++)
            {
                tOut[c] = new int[5];
                for (int i = 0; i < 5; i++)
                {
                    if (i == c)
                        tOut[c][i] = 1;
                    else
                        tOut[c][i] = 0; 
                }
            }

            if ((bool)singleImagesRadioButton.IsChecked) //если выбрано обучение на одиночных входных образах
            {
                int[][] trainSet = new int[5][];

                for (int c = 0; c < 5; c++)
                {
                    trainSet[c] = new int[35];
                    for (int i = 0; i < 35; i++)
                    {
                        trainSet[c][i] = 0;
                    }
                }

                string[] input = trainDataInput.Text.Split(' ');

                for (int n = 0; n < 5; n++)
                {
                    int[] intList = input[n].Select(digit => int.Parse(digit.ToString())).ToArray();
                    for (int i = 0; i < 35; i++)
                    {
                        trainSet[n][i] = intList[i];
                    }
                }

                iter = network.Learn(trainSet, tOut);
                weightUsage = network.CalculateWeightUsage();
            }
            else //если обучение на различающихся образах
            {
                List<int[][]> trainDataList = network.GetTrainSet();
                foreach(int[][] trainDataSet in trainDataList)
                {
                    int iterat = network.Learn(trainDataSet,tOut);
                    if (iterat > iter)
                        iter = iterat;
                }
                weightUsage = network.CalculateWeightUsage();
            };
            
            OutputTextBox.Text = "Сеть обучена. Количество итераций: " + iter.ToString() + "\n" + "Процент не участвующих в распознавании элементов: " + weightUsage.ToString() + "%" + "\n";// + iterations;
            trainDataInput.Text = " ";

            //вывод матрицы весовых коэффициентов
            for (int n = 0; n < 5; n++)
            {
                for (int i = 0; i < 35; i++)
                {
                    weightsTextBox.Text += network.Weights[i][n].ToString() + "\t";
                }
                weightsTextBox.Text += "\n";
            }  
        }

        private void recognizeData_Click(object sender, RoutedEventArgs e)
        {
            //разбиваем входную строку на подстроки
            int[][] recogniseSet = new int[5][];

            for (int c = 0; c < 5; c++)
            {
                recogniseSet[c] = new int[35];
                for (int i = 0; i < 35; i++)
                {
                    recogniseSet[c][i] = 0;
                }
            }

            string[] input = recognizeTextbox.Text.Split(' ');

            for (int n = 0; n < 5; n++)
            {
                int[] intList = input[n].Select(digit => int.Parse(digit.ToString())).ToArray();
                for (int i = 0; i < 35; i++)
                {
                    recogniseSet[n][i] = intList[i];
                }
            }

            int [][] res = network.Recognise(recogniseSet);

            for (int n = 0; n < 5; n++)
            {
                for (int i = 0; i < 5; i++)
                {
                    resultTextBox.Text += recogniseSet[n][i].ToString()+ "\t";
                }
                resultTextBox.Text += "\n";
            }

        }
    }
}
