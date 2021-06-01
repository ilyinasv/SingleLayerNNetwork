using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SingleLayerNNetwork
{
    class NNetwork
    {
        public int iterations = 1;
        public Neuron[] neurons = new Neuron[5];
        public int[][] Weights = new int[35][];
        public int[][] yOut = new int[5][]; //Выходной вектор Y

        public int[] w0 = new int[5];
        public int[] s = new int[5];

        public NNetwork()
        {
            for (int c = 0; c < 5; c++)
            {
                yOut[c] = new int [5];
                w0[c] = 0;
                s[c] = 0;
                neurons[c] = new Neuron();
                for (int i = 0; i < 5; i++)
                {
                    if (i == c)
                        yOut[c][i] = 1;
                    else
                        yOut[c][i] = 0;                   
                }
            }
            for (int i = 0; i < 35; i++)
            {
                Weights[i] = new int[5];
            }
        }

        public int Output(int data)
        {
            int y = 0;
            if (y >= 0)
                y = 1;
            return y;
        }


        public int Learn(int[][] trainData, int[][] tOut, bool bias = false)
        {
            int errors = 0;
            while (true)
            {
                errors = 0;
                for (int k = 0; k < trainData.GetLength(0); k++)
                {
                    //ll;ll;
                    for (int j = 0; j < 5; j++)
                    {
                        for (int i = 0; i < 35; i++)
                        {
                            s[j] += Weights[i][j] * trainData[k][i];
                            if (bias)
                                yOut[k][j] = Output(s[j] + w0[j]);
                            else
                                yOut[k][j] = Output(s[j]);
                        };
                    };
                    for (int i = 0; i < 5; i++)
                    {
                        if (tOut[k][i] != yOut[k][i])
                            errors += 1;
                    };

                    for (int i = 0; i < 35; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            Weights[i][j] += trainData[k][i] * (tOut[k][j] - yOut[k][j]);
                        }
                    }
                    if (bias)
                    {
                        for (int i = 0; i < 5; i++)
                            w0[i] += (tOut[k][i] - yOut[k][i]);
                    }
                }
                if (errors == 0)
                    break;
                else
                    iterations += 1;
            }
            return iterations;
        }

        public int [][] Recognise(int[][] recogniseData)
        {
            int[][] result = new int[5][];
            for (int c = 0; c < 5; c++)
            {
                result[c] = new int[5];   
            }

            for (int c = 0; c < 5; c++)
            {
                for (int j = 0; j < 5; j++)
                {                  
                    for (int i = 0; i < 35; i++)
                    {
                        yOut[c][j] += Weights[i][j] * recogniseData[c][i]; //
                        //yOut[c][j] = Output(yOut[c][j]-w0[j]);
                        yOut[c][j] = Output(yOut[c][j]);
                        result[c][j] = yOut[c][j];
                    }
                }
            }

            return result;
        }
    }
}
