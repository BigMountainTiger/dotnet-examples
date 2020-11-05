using dotenv.net;

using System;
using b_01_entity_framework.Example_1;

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
        static void Main(string[] args)
        {
          DotEnv.Config();
          Example_1();
        }
    }
}
