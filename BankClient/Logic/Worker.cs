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
    private readonly Bank _bank;
    private readonly Random _random;
    private long _incomeAmount;

    public Worker()
    {
      _bank = new Bank();
      _random = new Random();
      _incomeAmount = 0;
    }

    internal void Run()
    {
      CreateClients(50);
      PutMoneyOnAccounts(3);
      MakeTransfers(1000000);

      _bank.Status(_incomeAmount);
    }

    private void CreateClients(int n)
    {
      var tasks = new List<Task>();
      for (int i = 0; i < n; i++)
      {
        tasks.Add(new Task(() => new Client(_bank, _random)));
      }

      foreach (var task in tasks)
      {
        task.Start();
      }
    }

    private void PutMoneyOnAccounts(int n)
    {
      var tasks = new List<Task>();

      var clients = _bank.GetClients();
      

      foreach (var client in clients)
      {
        for (int i = 0; i < n; i++)
        {
          var amount = _random.Next(1, 1000000);
          _incomeAmount += amount;
          tasks.Add(new Task(() => client.PutOnBalance(amount)));
        }
      }

      Console.WriteLine($"Income amount: {_incomeAmount}");

      foreach (var task in tasks)
      {
        task.Start();
      }
    }

    private void MakeTransfers(int n)
    {
      var tasks = new List<Task>();
      var clientsCount = _bank.GetClientsCount();

      for (int i = 0; i < n; i++)
      {
        long amount = _random.Next(0, 1000000);
        int fromId = _random.Next(0, clientsCount);
        int toId = _random.Next(0, clientsCount);
        while (fromId == toId)
        {
          toId = _random.Next(0, clientsCount);
        }

        tasks.Add(new Task(() => _bank.Transfer(fromId, toId, amount)));
      }

      foreach (var task in tasks)
      {
        task.Start();
      }
    }
  }
}
