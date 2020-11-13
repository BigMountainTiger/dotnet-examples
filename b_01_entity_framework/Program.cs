using dotenv.net;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using b_01_entity_framework.Example_1;
using b_01_entity_framework.Example_2;

namespace b_01_entity_framework
{
    class Program
    {
        private static void Test_List_Take_More_Than_Available() {
          var l = new List<int>();
          l.Add(1);
          l.Add(2);
          l.Add(3);
          l.Add(4);
          l.Add(5);

          var ll = l.Take(100);

          foreach(var i in ll) {
            Console.WriteLine(i);
          }
        }

        static void Main(string[] args)
        {
          DotEnv.Config();

          //Test_List_Take_More_Than_Available();

          //A_Example1_Test
          //A_Example1_Test.Test();

          //A_Example2_Test
          //A_Example2_Test.Load_UnusedDataOrder().Wait();
          //A_Example2_Test.Load_UnusedData().Wait();
          //A_Example2_Test.Load_DataProgramtype().Wait();
        }
    }
}
