using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SingleLayerNNetwork
{
    class NNetwork
    {
        public int iterations = 1;
        public int[][] Weights = new int[35][];
        public bool biasNeuron = false;
        public int[] Weights0 = new int[5];

        public NNetwork()
        {
            for (int i = 0; i < 35; i++)
            {
                Weights[i] = new int[5];
            }
            for (int i = 0; i < 5; i++)
            {
                Weights0[i] = 0;
            }
        }

        public int Output(int data)
        {
            int y = 0;
            if (data >= 0)
                y = 1;
            return y;
        }


        public int Learn(int[][] trainData, int[][] tOut) //обучение сети
        {
            while (true)
            {
                int errors = 0;
                for (int k = 0; k < 5; k++)
                {
                    int[] yOut = new int[5];
                    int[] s = new int[5];
                    for (int i = 0; i < 5; i++)
                    {
                        s[i] = 0;
                        yOut[i] = 0;
                    };
                    for (int j = 0; j < 5; j++)
                    {
                        for (int i = 0; i < 35; i++)
                        {
                            s[j] += Weights[i][j] * trainData[k][i];
                            if(biasNeuron)
                                yOut[j] = Output(s[j]+Weights0[j]);
                            else
                                yOut[j] = Output(s[j]);
                        };
                    };
                    for (int i = 0; i < 5; i++)
                    {
                        if (tOut[k][i] != yOut[i])
                            errors += 1;
                    };
                    for (int i = 0; i < 35; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            Weights[i][j] += trainData[k][i] * (tOut[k][j] - yOut[j]);
                        }
                    }
                    if(biasNeuron)
                        for (int i = 0; i < 5; i++)
                        {
                            Weights0[i] += tOut[k][i] - yOut[i];
                        };
                }
                if (errors == 0)
                    break;
                else
                    iterations += 1;
            }
            return iterations;
        }

        public int [][] Recognise(int[][] recogniseData) //распознавание
        {
            int[][] result = new int[5][];
            for (int c = 0; c < 5; c++)
            {
                result[c] = new int[5];   
            }

            for (int c = 0; c < 5; c++)
            {
                int[] yOut = new int[5];
                for (int i = 0; i < 5; i++)
                {
                    yOut[i] = 0;
                };
                for (int j = 0; j < 5; j++)
                {                  
                    for (int i = 0; i < 35; i++)
                    {
                        yOut[j] += Weights[i][j] * recogniseData[c][i]; 
                        if(biasNeuron)
                            yOut[j] = Output(yOut[j]+Weights0[j]);
                        else
                            yOut[j] = Output(yOut[j]);
                        result[c][j] = yOut[j];
                    }
                }
            }

            return result;
        }

        
        public double CalculateWeightUsage() //подсчёт не учавствующих в распознавании элементов
        {
            int zeroWeightsAmount = 0;
            for (int j = 0; j < 5; j++)
            {
                if (Weights0[j] == 0 && biasNeuron)
                    zeroWeightsAmount += 1;
                for (int i = 0; i < 35; i++)
                {
                    if (Weights[i][j] == 0)
                        zeroWeightsAmount += 1;                   
                }
            }
            double percentage;
            if (biasNeuron)
                percentage=zeroWeightsAmount * 100 / 180;
            else
                percentage = zeroWeightsAmount * 100 / 175;
            return percentage;
        }

        public List<int[][]> GetTrainSet() //наборы различающихся тренировочных образов
        {
            int[][] trainData = new int[5][];

            List<int[][]> trainDataList = new List<int[][]>(); // лист для хранения обучающих наборов данных

            string[][] images = new string [3][];
            for(int n=0;n<3;n++)
            {
                images[n] = new string[5];
            }
            images[0][0] = "10000011000001111111110000011000001"; // символ I0
            images[0][1] = "01000101010001100100110001010100010"; // символ S0
            images[0][2] = "11111110100000001000001000001111111"; // символ M0
            images[0][3] = "01111101000001100000110000010111110"; // символ 00
            images[0][4] = "10000011000010100010010010001110000"; // символ 70

            images[1][0] = "00000001000001111111110000010000000"; 
            images[1][1] = "01100101001001100100110010010100110"; 
            images[1][2] = "11111111100000011000011000001111111";
            images[1][3] = "11111111000001100000110000011111111";
            images[1][4] = "10000111000100100100010100001100000"; 

            images[2][0] = "00000000000000111111100000000000000";
            images[2][1] = "01100011001001100100110010011000110";
            images[2][2] = "11111111000000010000010000001111111";
            images[2][3] = "11111111100011110001111000111111111";
            images[2][4] = "10000001000111100100010100001100000"; 


            for (int n = 0; n < 3; n++)
            {
                for (int i = 0; i < 5; i++)
                {
                    trainData[i] = images[n][i].Select(digit => int.Parse(digit.ToString())).ToArray();
                }
                trainDataList.Add(trainData);
            }

            return trainDataList;
        }
    }
}
