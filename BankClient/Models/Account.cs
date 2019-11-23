using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankClient.Models
{
  class Account
  {
    public int Id;
    public int OwnerId;
    internal long Balance;
    private object Baton = new object();

    public Account(int id, int ownerId, long balance)
    {
      Id = id;
      OwnerId = ownerId;
      Balance = balance;
    }

    public bool Add(long amount)
    {
      lock (Baton)
      {
        Balance += amount;
        return true;
      }
    }

    public bool Subtract(long amount)
    {
      lock (Baton)
      {
        if (Balance < amount)
        {
          return false;
        } 

        Balance -= amount;
        return true;
      }
    }
  }
}
