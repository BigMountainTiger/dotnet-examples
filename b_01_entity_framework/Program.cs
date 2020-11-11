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
        private static void Example_1() {
          var students = Example1DB.GetStudents().Result;

          foreach(var s in students) {
            Console.WriteLine($"ID = {s.ID}, Name = {s.Name}");
          }
        }

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
        private static async Task Example_2_Load_UnusedDataOrder() {
          var orderId = 87746;
          List<UnusedDataOrder> data = null;

          using (var context = new Example2Context())
          {
            data = await context.UnusedDataOrder
            .Where( x => x.OrderId == orderId )
            .ToListAsync();
          }

          foreach (var d in data) {
            Console.WriteLine($"ID = {d.CorelogicDataId}, Name = {d.ProgramTypeId}");
          }

          Console.WriteLine($"Total count = {data.Count()}");
        }

        static void Main(string[] args)
        {
          DotEnv.Config();

          //Example_1();
          //Test_List_Take_More_Than_Available();
          
          Example_2_Load_UnusedDataOrder().Wait();
        }
    }
}
