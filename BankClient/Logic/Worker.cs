using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BankClient.Models;

namespace BankClient.Logic
{
  class Worker
  {
    //create 50 clients
    //closure/print total ballance
    //create 10 threads
    //run 1000000 transfer operations for each thread with random amount
    //print and compare total ballance afterwards

    private readonly Bank Bank;

    public Worker()
    {
      Bank = new Bank();
    }

    internal void Run()
    {
      Console.WriteLine("Creating client instance ...");
      var client1 = new Client(Bank);
      Console.WriteLine("Client instance created successfully!");
      Console.WriteLine("Attempting to put money on balance ...");
      if (client1.PutOnBalance(1000000))
      {
        Console.WriteLine("Money putted on balance successfully!");
      }
      else
      {
        Console.WriteLine("Failed to put money on balance.");
      }

      Console.WriteLine("Attempting to withdraw money from balance ...");
      if (client1.WithdrawFromBalance(5000))
      {
        Console.WriteLine("Money withdrawn from balance successfully!");
      }
      else
      {
        Console.WriteLine("Failed to withdraw money on balance.");
      }

      Bank.Status();
    }
  }
}
