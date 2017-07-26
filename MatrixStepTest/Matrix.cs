using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixStepTest
{
    public class Matrix
    {
        public static Matrix One => GetIdentity(1);
        public static Matrix Identity2 => GetIdentity(2);
        public static Matrix Identity3 => GetIdentity(3);

        public static Matrix GetIdentity(int Dimension)
        {
            var M = new Matrix(Dimension, Dimension);
            for (int x = 0; x < Dimension; x++)
                M[x, x] = 1;
            return M;
        }

        private double[][] Values;
        public double this[int x, int y]
        {
            get => Values[x][y];
            set => Values[x][y] = value;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public Matrix(int X) : this(X, X) { }
        public Matrix(int X, int Y)
        {
            this.X = X;
            this.Y = Y;

            Values = new double[X][];
            for (int x = 0; x < X; x++)
            {
                Values[x] = new double[Y];
                for (int y = 0; y < Y; y++)
                {
                    Values[x][y] = 0;
                }
            }
        }

        public void SetAll(double Value)
        {
            for (int x = 0; x < X; x++)
                for (int y = 0; y < Y; y++)
                    Values[x][y] = Value;
        }

        public void Randomize(int Min = 0, int Max = 100)
        {
            var R = new Random((int)DateTime.Now.Ticks);
            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    Values[x][y] = R.Next(Min, Max);
                }
            }
        }

        public Matrix Multiply(Matrix B, out int Count)
        {
            if (Y != B.X)
                throw new InvalidOperationException();
            else
            {
                var M = new Matrix(X, B.Y);
                Count = 0;

                for (int x = 0; x < X; x++)
                {
                    for (int y = 0; y < B.Y; y++)
                    {
                        for (int z = 0; z < Y; z++)
                        {
                            M[x, y] += this[x, z] * B[z, y];
                            Count++;
                        }
                    }
                }

                return M;
            }
        }

        public override string ToString()
        {
            var Result = string.Empty;
            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    Result += $"{Values[x][y]}";
                    if (y != Y - 1)
                        Result += " ";
                }

                if (x != x - 1)
                    Result += Environment.NewLine;
            }
            return Result;
        }

        public static Matrix operator *(Matrix A, Matrix B)
        {
            int Count = 0;
            return A.Multiply(B, out Count);
        }

        public static Matrix operator *(Matrix A, double Scalar)
        {
            var M = new Matrix(A.X, A.Y);

            for (int x = 0; x < A.X; x++)
            {
                for (int y = 0; y < A.Y; y++)
                {
                    M[x, y] = A[x, y] * Scalar;
                }
            }

            return M;
        }
    }
}