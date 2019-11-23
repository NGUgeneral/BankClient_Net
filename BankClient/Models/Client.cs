using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankClient.Models
{
  class Client
  {
    public int Id;
    private Bank ClientBank;
    public ClientInfo Info;

    public Client(Bank bank, Random random)
    {
      Info = new ClientInfo(random, true);
      ClientBank = bank;
      Id = ClientBank.RegisterClient(this);
      CreateAccount();
    }

    public bool CreateAccount()
    {
      return ClientBank.RegisterAccount(this.Id);
    }

    public bool TransferBalance(int toClientId, long amount)
    {
      return ClientBank.Transfer(this.Id, toClientId, amount);
    }

    public bool PutOnBalance(long amount)
    {
      return ClientBank.PutOnBalance(this.Id, amount);
    }

    public bool WithdrawFromBalance(long amount)
    {
      return ClientBank.WithdrawFromBalance(this.Id, amount);
    }
  }
}
