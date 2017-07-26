using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace MatrixStepTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var Teo = new Func<Matrix, Matrix, int>((a, b) => a.X * a.Y * b.Y);

            Console.Write("Digite o número de saídas para o aplicativo gerar: ");
            for (int T = 1; T < int.Parse(Console.ReadLine()); T++)
            {
                Console.Write($"Try {T}...");
                var FPath = Path.Combine(Environment.CurrentDirectory, $"Saida {T}.txt");
                if (File.Exists(FPath))
                    File.Delete(FPath);

                var Old = Console.Out;
                using (var Stream = File.Create(FPath))
                {
                    using (var Writer = new StreamWriter(Stream))
                    {
                        Console.SetOut(Writer);

                        for (int x = 1; x <= T; x++)
                        {
                            for (int y = 1; y <= T; y++)
                            {
                                for (int z = 1; z <= T; z++)
                                {
                                    for (int w = 1; w <= T; w++)
                                    {
                                        Console.Title = $"n = {(x + 1) * (y + 1) * (z + 1) * (w + 1)}";
                                        try
                                        {
                                            Console.Write($"Testing for A({x},{y})*B({z},{w})....");

                                            Matrix A = new Matrix(x, y), B = new Matrix(z, w);
                                            A.Randomize(1, 10);
                                            B.Randomize(1, 10);

                                            int Count;
                                            var Result = A.Multiply(B, out Count);

                                            Console.WriteLine($"Resultant A[{x},{y}]*B[{z},{w}] = C({Result.X},{Result.Y}), C = Teo | {Count} = {Teo(A, B)} == {Count == Teo(A, B)}");
                                        }
                                        catch (InvalidOperationException)
                                        {
                                            Console.WriteLine($"Non-valid matrix multiply dimensions (A[{x},{y}] * B[{z},{w}].");
                                        }
                                    }
                                }
                            }
                        }
                    }

                    Console.SetOut(Old);
                    Console.WriteLine($"End of {T} test!");
                }
            }

            Console.WriteLine("End of all tests");
            Console.ReadLine();
        }
    }
}