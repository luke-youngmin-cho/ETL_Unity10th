using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject
{
    internal class Account
    {
        public Account(string ownerName, int balance =0)
        {
            _ownerName = ownerName;
            _balance = balance;
        }

        private object _balanceLock = new object();

        public int Balance
        {
            get
            {
                lock (_balanceLock)
                {
                    return _balance;
                }
            }
            private set
            {
                lock ( _balanceLock)
                {
                    _balance = value;
                }
            }
        }
        private int _balance;
        private string _ownerName;

        public void Deposit(int amount)
        {
            lock (_balanceLock)
            {
                _balance += amount;
                Console.WriteLine($"{_ownerName} account : Deposited {amount}, Balance = {_balance}");
            }
        }

        public void Withdraw(int amount)
        {
            lock (_balanceLock)
            {
                _balance -= amount;
                Console.WriteLine($"{_ownerName} account : Withdrawed {amount}, Balance = {_balance}");
            }
        }

        public void Transfer(Account other, int amount)
        {
            Withdraw(amount);
            other.Deposit(amount);
            Console.WriteLine($"{_ownerName} account : Transferred {amount} to {other._ownerName}");
        }
    }
}
