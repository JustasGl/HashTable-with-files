using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isorine
{
    class Program
    {
        static Random random;
        static void Main(string[] args)
        {
            int n = 150;
            int k = 4;

            List<string> list = new List<string>();
            var strTree = new FixedSizeGenericHashTable(n);
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
            random = new Random(seed);
           
            System.IO.DirectoryInfo di = new DirectoryInfo(System.IO.Directory.GetCurrentDirectory() + "\\list");
            var watch1 = new System.Diagnostics.Stopwatch();
            var watch2 = new System.Diagnostics.Stopwatch();
            var watch4 = new System.Diagnostics.Stopwatch();
            var watch3 = new System.Diagnostics.Stopwatch();
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            watch1.Start();
            for (int i = 0; i < n; i++)
            {
                string s = RandomName(k);
                strTree.Add(s, NextFloat());
                list.Add(s);
            }
            watch1.Stop();
            watch3.Start();
            Console.WriteLine(strTree.FileContains(list[n / 2]));
            watch3.Stop();
            watch4.Start();
            Console.WriteLine(strTree.FindValue(list[n / 2]));
            watch4.Stop();
            watch2.Start();
            strTree.Remove(list[n / 2]);
            watch2.Stop();
            Console.WriteLine("{0} elementu sudeta per {1}", n, watch1.Elapsed);
            Console.WriteLine("{0} elementu sarase 1 elementas FileContains per {1}", n, watch3.Elapsed);
            Console.WriteLine("{0} elementu sarase 1 elementas surastas per {1}", n, watch4.Elapsed);
            Console.WriteLine("{0} elementu sarase 1 elementas pasalintas per {1}", n, watch2.Elapsed);
        }
        static float NextFloat()
        {
            double mantissa = (random.NextDouble() * 8998) + 1000;
            mantissa = mantissa - (mantissa % 1);
            return (float)mantissa;
        }
        static string RandomName(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 *
           random.NextDouble() + 65)));
            builder.Append(ch);
            for (int i = 1; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 *
               random.NextDouble() + 97)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}
