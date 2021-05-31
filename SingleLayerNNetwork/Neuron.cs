using System;
using System.Collections.Generic;
using System.Text;

namespace SingleLayerNNetwork
{
    class Neuron
    {
        public double[][] Weights = new double[5][];

        public Neuron()
        {
            for (int i = 0; i < 5; i++)
            {
                Weights[i] = new double[7];
            }
        }

        public double Output(int[][] matrix)
        {
            double output = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    //вычисляем уровень возбуждения нейрона
                    output += matrix[i][j] * Weights[i][j];
                }
            }
            return output;
        }
    }
}
