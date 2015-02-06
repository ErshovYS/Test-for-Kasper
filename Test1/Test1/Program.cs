using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Test1
{
    class Program
    {
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

            StreamReader SR = new StreamReader(FileName, Encoding.Default);
            string text = SR.ReadToEnd();
            List<string> WordS = new List<string>(text.Split(new char[] { ' ', ',', '\n', '.', '\r' }));
            SR.Close();

            // Сортировка слов по длине
            QuickS(WordS, 0, WordS.Count - 1);

            int i = 0;
            while (i < WordS.Count)
            {
                // Если найдены анаграммы слова, то они выводятся и удаляются вместе со словом
                if (!Anagramma(WordS, i))
                    i++;
            }
            Console.ReadKey();
        }

        // Быстрая сортировка
        static void QuickS(List<string> WordS, int first, int last)
        {
            int i = first;
            int j = last;
            int mid = (last + first) / 2;
            string bufS;

            while (i <= j)
            {
                while (WordS[i].Length < WordS[mid].Length)
                    i++;
                while (WordS[j].Length > WordS[mid].Length)
                    j--;

                if (i <= j)
                {
                    if (i < j)
                    {
                        bufS = WordS[i];
                        WordS[i] = WordS[j];
                        WordS[j] = bufS;
                    }
                    i++;
                    j--;
                }
            }

            if (first < j)
                QuickS(WordS, first, j);
            if (i < last)
                QuickS(WordS, i, last);
        }

        // Поиск анаграмм для слова под номером Num
        static bool Anagramma(List<string> WordS, int Num)
        {
            int Len = WordS[Num].Length, NumWord = Num+1, Kol;  
            bool[] ch = new bool[Len];                          // Совпал ли определенный символ
            bool Annagr = false;                                // Есть ли у слова анаграммы

            while (NumWord < WordS.Count && WordS[NumWord].Length == Len)
            {
                for (int i = 0; i < Len; i++)
                    ch[i] = false;

                Kol = 0;
                for (int i = 0; i < Len; i++ )
                {
                    for (int j = 0; j < Len; j++)
                    {
                        if (!ch[j] && WordS[Num][i] == WordS[NumWord][j])
                        {
                            Kol++;
                            ch[j] = true;
                            break;
                        }
                    }
                }
                if (Kol == Len)
                {
                    Console.Write(WordS[NumWord] + " ");
                    WordS.RemoveAt(NumWord);
                    NumWord--;
                    Annagr = true;
                }
                NumWord++;
            }

            if (Annagr)
            {
                Console.Write(WordS[Num] + "\n");
                WordS.RemoveAt(Num);
                return true;
            }
            else
            { 
                return false;
            }
        }
    }
}
