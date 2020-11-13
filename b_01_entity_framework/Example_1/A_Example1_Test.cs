using System;

namespace b_01_entity_framework.Example_1 {
  public class A_Example1_Test
  {
    public static void Test() {
      var students = Example1DB.GetStudents().Result;

      foreach(var s in students) {
        Console.WriteLine($"ID = {s.ID}, Name = {s.Name}");
      }
    }
  }
}