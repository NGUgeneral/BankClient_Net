using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankClient.Models
{
  class Bank
  {
    private readonly Dictionary<int, Account> Accounts;
    private readonly Dictionary<int, Client> Clients;
    private readonly Dictionary<int, int> AccountIdClientIdMap;
    private readonly Dictionary<int, int> ClientIdAccountIdMap;
    private int _accountIdCounter = 0;
    private int _clientIdCounter = 0;

    public Bank()
    {
      Accounts = new Dictionary<int, Account>();
      Clients = new Dictionary<int, Client>();
      AccountIdClientIdMap = new Dictionary<int, int>();
      ClientIdAccountIdMap = new Dictionary<int, int>();
    }

    public int RegisterClient(Client client)
    {
      lock (Clients)
      {
        Clients.Add(_clientIdCounter, client);
        var clientId = _clientIdCounter;
        _clientIdCounter++;
        return clientId;
      }
    }

    internal bool RegisterAccount(int clientId)
    {
      lock (Accounts)
      {
        var newAccount = new Account(_accountIdCounter, clientId, 0);
        _accountIdCounter++;
        Accounts.Add(newAccount.Id, newAccount);
        AccountIdClientIdMap.Add(newAccount.Id, clientId);
        ClientIdAccountIdMap.Add(clientId, newAccount.Id);
        return true;
      }
    }

    public bool Transfer(int fromClientId, int toClientId, long amount)
    {
      if (!WithdrawFromBalance(fromClientId, amount))
      {
        return false;
      }

      if(!PutOnBalance(toClientId, amount))
      {
        PutOnBalance(fromClientId, amount);
        return false;
      }
      
      return true;
    }

    internal bool PutOnBalance(int clientId, long amount)
    {
      var toAccountId = ClientIdAccountIdMap[clientId];
      lock (Accounts[toAccountId])
      {
        if (!Accounts[toAccountId].Add(amount))
        {
          return false;
        }
      }
      return true;
    }

    internal bool WithdrawFromBalance(int clientId, long amount)
    {
      var fromAccountId = ClientIdAccountIdMap[clientId];

      lock (Accounts[fromAccountId])
      {
        if (!Accounts[fromAccountId].Subtract(amount))
        {
          return false;
        }
      }

      return true;
    }

    internal void Status()
    {
      Console.WriteLine($@"Accounts: {Accounts.Count}");
      Console.WriteLine($@"Money: {Accounts.Values.Sum(x => x.Balance)}");
    }
  }
}
