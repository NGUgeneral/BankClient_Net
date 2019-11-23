using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankClient.Logic
{
  class Worker
  {
    //create 50 clients
    //closure/print total ballance
    //create 10 threads
    //run 1000000 transfer operations for each thread with random amount
    //print and compare total ballance afterwards

    private readonly Dictionary<int, string> dict;

    public Worker()
    {
      dict = new Dictionary<int, string>();
    }

    internal void Run()
    {
      Console.WriteLine("Attempting add a pair to readonly dictionary ...");
      dict.Add(1, "One");
      Console.WriteLine("Success");
      Thread.Sleep(2000);
      Console.WriteLine("Attempting to remove a pair from readonly dictionary ...");
      dict.Remove(1);
      Console.WriteLine("Success");
      Thread.Sleep(2000);
      Console.WriteLine("Attempting to delete a readonly dictionary ...");
      Console.WriteLine("... will fail in compile time");
    }
  }
}
