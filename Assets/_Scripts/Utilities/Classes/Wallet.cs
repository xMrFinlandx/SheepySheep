using System;
using UnityEngine;

namespace _Scripts.Utilities.Classes
{
    public class Wallet
    {
        private int _balance = 0;
        private int _buffer;

        public int Buffer => _buffer;
        
        public int Balance
        {
            get => _balance;
            private set
            {
                _balance = value;
                BalanceChangedAction?.Invoke(_balance);
            }
        }

        public Action<int> BalanceChangedAction;

        public Wallet(int initialValue)
        {
            _balance = initialValue;
        }

        public void Add(int value)
        {
            if (value < 1)
                Debug.LogError("value cannot be less than 1");

            Balance += value;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void AddToBuffer(int value)
        {
            if (value < 1)
                Debug.LogError("value cannot be less than 1");
            
            _buffer += value;
        }

        public void ApplyBuffer()
        {
            _balance += _buffer;
        }

        public void ResetBuffer()
        {
            _buffer = 0;
        }
    }
}