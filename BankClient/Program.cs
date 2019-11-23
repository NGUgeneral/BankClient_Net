using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankClient.Logic;

namespace BankClient
{
  class Program
  {
    static void Main(string[] args)
    {
      var worker = new Worker();
      worker.Run();
      Console.WriteLine("\nPress any key to exit ...");
      Console.ReadKey();
    }
  }
}
