using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Test2
{
    class Program
    {
        static int count;                       // Количество строк в файле
        static List<int> NumStr = new List<int>();                // Номера строк, в которых уже есть анаграммы

        static void Main(string[] args)
        {
            string FileName;
            if (args.Length > 0)
            {
                FileName = args[0];
            }
            else
            {
                Console.WriteLine("Введите адрес файла:");
                FileName = Console.ReadLine();
            }


            count = File.ReadAllLines(FileName).Length;

            int Num1 = 0;
            string Word1, Word2, LineS;
            while (Num1 < count)
            {
                if (NumStr.IndexOf(Num1) < 0)
                {
                    Word1 = File.ReadLines(FileName, Encoding.Default).Skip(Num1).First();
                    LineS = "";
                    int Num2 = Num1 + 1;

                    while (Num2 < count)
                    {
                        if (NumStr.IndexOf(Num2) < 0)
                        {
                            Word2 = File.ReadLines(FileName, Encoding.Default).Skip(Num2).First();

                            if (Word1.Length == Word2.Length)
                            {
                                if (Anagramma(Word1, Word2, ref LineS))
                                {
                                    if (NumStr.IndexOf(Num1) < 0)
                                        NumStr.Add(Num1);
                                    NumStr.Add(Num2);
                                }
                            }
                        }
                        Num2++;
                    }
                    if (NumStr.IndexOf(Num1) >= 0)
                    {
                        Console.WriteLine(LineS);
                    }
                }
                Num1++;
            }
            Console.ReadKey();
        }

        // Сравнение слов и запись анаграмм в строку
        static bool Anagramma(string Word1, string Word2, ref string LineS)
        {
            int Len = Word1.Length, Kol;
            bool[] ch = new bool[Len];                          // Совпал ли определенный символ

            for (int i = 0; i < Len; i++)
                ch[i] = false;

            Kol = 0;
            for (int i = 0; i < Len; i++)
            {
                for (int j = 0; j < Len; j++)
                {
                    if (!ch[j] && Word1[i] == Word2[j])
                    {
                        Kol++;
                        ch[j] = true;
                        break;
                    }
                }
            }
            if (Kol == Len)
            {
                if (LineS.Length > 0)
                    LineS += Word2 + " ";
                else
                    LineS += Word1 + " " + Word2 + " ";
                return true;
            }
            return false;
        }
    }
}
